//
//  UnityManager.swift
//  UnityFramework
//
//  Created by link on 2022/6/30.
//

import Foundation
import RxSwift
import SwiftyJSON
import UnityFramework

import ParticleAuthService
import ParticleNetworkBase
import ParticleWalletAPI
import ParticleWalletConnect
import ParticleWalletGUI

import ConnectCommon
import ParticleConnect

#if canImport(ConnectEVMAdapter)
import ConnectEVMAdapter
#endif

#if canImport(ConnectSolanaAdapter)
import ConnectSolanaAdapter
#endif

#if canImport(ConnectPhantomAdapter)
import ConnectPhantomAdapter
#endif

#if canImport(ConnectWalletConnectAdapter)
import ConnectWalletConnectAdapter
#endif

import UIKit

@objcMembers
class UnityManager: NSObject, UnityFrameworkListener, NativeCallsProtocol {
    let bag = DisposeBag()
    static var shared = UnityManager()
    
    var ufw: UnityFramework?
    
    static let apiSystemName = "ParticleWalletAPI"
    static let authSystemName = "ParticleAuthService"
    static let connectSystemName = "ParticleConnect"
    static let guiSystemName = "ParticleWalletGUI"
    
    override init() {
        super.init()
    }
    
    func startGame() {
        let ufw = unityFrameworkLoad()
        self.ufw = ufw
        ufw?.register(self)
        FrameworkLibAPI.registerAPIforNativeCalls(self)
        ufw?.runEmbedded(withArgc: CommandLine.argc, argv: CommandLine.unsafeArgv, appLaunchOpts: [:])
    }
    
    func unityFrameworkLoad() -> UnityFramework? {
        let bundlePath = Bundle.main.bundlePath.appending("/Frameworks/UnityFramework.framework")
        if let bundle = Bundle(path: bundlePath) {
            if !bundle.isLoaded {
                bundle.load()
            }
            if let ufw = (bundle.principalClass as? UnityFramework.Type)?.getInstance() {
                if ufw.appController() == nil {
                    ufw.setExecuteHeader(mhExecHeaderPtr)
                }
                return ufw
            }
        }

        return nil
    }
}

// MARK: - Particle Network Base

extension UnityManager {
    func initialize(_ json: String) {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainName = matchChain(name: name, chainId: chainId) else {
            return print("initialize error, can't find right chain for \(name), chainId \(chainId)")
        }
        let env = data["env"].stringValue.lowercased()
        var devEnv: ParticleNetwork.DevEnvironment = .production
        if env == "dev" {
            devEnv = .debug
        } else if env == "staging" {
            devEnv = .staging
        } else if env == "production" {
            devEnv = .production
        }
        
        let config = ParticleNetworkConfiguration(chainInfo: chainName, devEnv: devEnv)
        ParticleNetwork.initialize(config: config)
    }
    
    func getDevEnv() -> Int {
        return ParticleNetwork.getDevEnv().rawValue
    }
    
    func setChainInfo(_ json: String) -> Bool {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainInfo = matchChain(name: name, chainId: chainId) else { return false }
        ParticleNetwork.setChainInfo(chainInfo)
        return true
    }
    
    func getChainInfo() -> String {
        let chainInfo = ParticleNetwork.getChainInfo()
        return ["chain_name": chainInfo.name, "chain_id": chainInfo.chainId, "chain_id_name": chainInfo.network].jsonString() ?? ""
    }
    
    func setLanguage(_ json: String) {
        /**
         SYSTEM,
         EN,
         ZH_HANS,
         */
        if json.lowercased() == "en" {
            ParticleNetwork.setLanguage(Language.en)
        } else if json.lowercased() == "zh_hans" {
            ParticleNetwork.setLanguage(Language.zh_Hans)
        } else if json.lowercased() == "zh_hant" {
            ParticleNetwork.setLanguage(Language.zh_Hant)
        } else if json.lowercased() == "ko" {
            ParticleNetwork.setLanguage(Language.ko)
        } else if json.lowercased() == "ja" {
            ParticleNetwork.setLanguage(Language.ja)
        }
    }
    
    func setInterfaceStyle(_ json: String) {
        /**
         SYSTEM,
         LIGHT,
         DARK,
         */
        if json.lowercased() == "system" {
            ParticleNetwork.setInterfaceStyle(UIUserInterfaceStyle.unspecified)
        } else if json.lowercased() == "light" {
            ParticleNetwork.setInterfaceStyle(UIUserInterfaceStyle.light)
        } else if json.lowercased() == "dark" {
            ParticleNetwork.setInterfaceStyle(UIUserInterfaceStyle.dark)
        }
    }
}

// MARK: - Particle Auth Service

extension UnityManager {
    func login(_ json: String) {
        let data = JSON(parseJSON: json)
        let loginType = LoginType(rawValue: data["loginType"].stringValue.lowercased()) ?? .email
        var supportAuthTypeArray: [SupportAuthType] = []
        
        let array = data["supportAuthTypeValues"].arrayValue.map {
            $0.stringValue.lowercased()
        }
        if array.contains("all") {
            supportAuthTypeArray = [.all]
        } else {
            array.forEach {
                if $0 == "apple" {
                    supportAuthTypeArray.append(.apple)
                } else if $0 == "google" {
                    supportAuthTypeArray.append(.google)
                } else if $0 == "facebook" {
                    supportAuthTypeArray.append(.facebook)
                } else if $0 == "github" {
                    supportAuthTypeArray.append(.github)
                } else if $0 == "twitch" {
                    supportAuthTypeArray.append(.twitch)
                } else if $0 == "microsoft" {
                    supportAuthTypeArray.append(.microsoft)
                } else if $0 == "linkedin" {
                    supportAuthTypeArray.append(.linkedin)
                } else if $0 == "discord" {
                    supportAuthTypeArray.append(.discord)
                }
            }
        }
        
        var account = data["account"].string
        if account != nil, account!.isEmpty {
            account = nil
        }
        let loginFormMode = data["loginFormMode"].bool
        
        ParticleAuthService.login(type: loginType, account: account, supportAuthType: supportAuthTypeArray, loginFormMode: loginFormMode).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let userInfo):
                guard let userInfo = userInfo else { return }
                let statusModel = UnityStatusModel(status: true, data: userInfo)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func logout() {
        ParticleAuthService.logout().subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let success):
                let statusModel = UnityStatusModel(status: true, data: success)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func isLogin() -> Bool {
        return ParticleAuthService.isLogin()
    }
    
    func signMessage(_ message: String) {
        var serializedMessage = ""
        switch ParticleNetwork.getChainInfo().chain {
        case .solana:
            serializedMessage = Base58.encode(message.data(using: .utf8)!)
        default:
            serializedMessage = "0x" + message.data(using: .utf8)!.map { String(format: "%02x", $0) }.joined()
        }
        
        ParticleAuthService.signMessage(serializedMessage).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = UnityStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func signTransaction(_ transaction: String) {
        ParticleAuthService.signTransaction(transaction).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signed):
                let statusModel = UnityStatusModel(status: true, data: signed)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func signAllTransactions(_ transactions: String) {
        let transactions = JSON(parseJSON: transactions).arrayValue.map { $0.stringValue }
        ParticleAuthService.signAllTransactions(transactions).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = UnityStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func signAndSendTransaction(_ message: String) {
        ParticleAuthService.signAndSendTransaction(message).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signature):
                let statusModel = UnityStatusModel(status: true, data: signature)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func signTypedData(_ json: String) {
        let data = JSON(parseJSON: json)
        let message = data["message"].stringValue
        let version = data["version"].stringValue.lowercased()
        
        let hexString = "0x" + message.data(using: .utf8)!.map { String(format: "%02x", $0) }.joined()
       
        ParticleAuthService.signTypedData(hexString, version: EVMSignTypedDataVersion(rawValue: version) ?? .v1).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = UnityStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func getAddress() -> String {
        return ParticleAuthService.getAddress()
    }
    
    func getUserInfo() -> String {
        guard let userInfo = ParticleAuthService.getUserInfo() else {
            return ""
        }
        let data = try! JSONEncoder().encode(userInfo)
        let json = String(data: data, encoding: .utf8)
        return json ?? ""
    }
    
    func setChainInfoAsync(_ json: String) {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainInfo = matchChain(name: name, chainId: chainId) else { return }
        ParticleAuthService.setChainInfo(chainInfo).subscribe { [weak self] result in
            guard let self = self else { return }

            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.ufw?.sendMessageToGO(withName: UnityManager.authSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
            case .success(let userInfo):
                guard let userInfo = userInfo else { return }
                let statusModel = UnityStatusModel(status: true, data: userInfo)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.ufw?.sendMessageToGO(withName: UnityManager.authSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
            }
        }.disposed(by: bag)
    }
    
    func setModalPresentStyle(_ style: String) {
        if style.lowercased() == "fullscreen" {
            ParticleAuthService.setModalPresentStyle(.fullScreen)
        } else {
            ParticleAuthService.setModalPresentStyle(.formSheet)
        }
    }
    
    func setMediumScreen(_ isMedium: Bool) {
        if #available(iOS 15.0, *) {
            ParticleAuthService.setMediumScreen(isMedium)
        } else {
            // Fallback on earlier versions
        }
    }
}

// MARK: - Particle Wallet API

extension UnityManager {
    func solanaGetTokenList() {
        ParticleWalletAPI.getSolanaService().getTokenList().subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenList):
                let rawData = tokenList.map {
                    self.map2UnityTokenInfo(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
            
        }.disposed(by: bag)
    }
    
    func solanaGetTokensAndNFTs(_ address: String) {
        ParticleWalletAPI.getSolanaService().getTokensAndNFTs(by: address, tokenAddresses: []).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokenResult(from: tokenResult)
                
                let statusModel = UnityStatusModel(status: false, data: unityTokenResult)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func solanaGetTokensAndNFTs(fromDB address: String) {
        ParticleWalletAPI.getSolanaService().getTokensAndNFTsFromDB(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "solanaGetTokensAndNFTsFromDB")
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokenResult(from: tokenResult)
                
                let statusModel = UnityStatusModel(status: true, data: unityTokenResult)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "solanaGetTokensAndNFTsFromDB")
            }
        }.disposed(by: bag)
    }
    
    func solanaAddCustomTokens(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        
        let tokenAddresses = JSON(parseJSON: data["token_addresses"].stringValue).arrayValue.map {
            $0.stringValue
        }
        
        ParticleWalletAPI.getSolanaService().addCustomTokens(address: address, tokenAddresses: tokenAddresses).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityTokenModel(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func solanaGetTransactions(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        let beforeSignature = data["before"].string
        let untilSignature = data["until"].string
        let limit = data["limit"].intValue
        
        ParticleWalletAPI.getSolanaService().getTransactions(by: address, beforeSignature: beforeSignature, untilSignature: untilSignature, limit: limit).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func solanaGetTransactions(fromDB json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        let beforeSignature = data["before"].string
        let untilSignature = data["until"].string
        let limit = data["limit"].intValue
        
        ParticleWalletAPI.getSolanaService().getTransactionsFromDB(by: address, beforeSignature: beforeSignature, untilSignature: untilSignature, limit: limit).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "solanaGetTransactionsFromDB")
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "solanaGetTransactionsFromDB")
            }
        }.disposed(by: bag)
    }
    
    func solanaGetTokenTransactions(_ json: String) {
        let data = JSON(parseJSON: json)
        
        let address = data["address"].stringValue
        let mintAddress = data["mint_address"].stringValue
        let beforeSignature = data["before"].string
        let untilSignature = data["until"].string
        let limit = data["limit"].intValue
        
        ParticleWalletAPI.getSolanaService().getTokenTransactions(by: address, mintAddress: mintAddress, beforeSignature: beforeSignature, untilSignature: untilSignature, limit: limit).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func solanaGetTokenTransactions(fromDB json: String) {
        let data = JSON(parseJSON: json)
        
        let address = data["address"].stringValue
        let mintAddress = data["mint_address"].stringValue
        let beforeSignature = data["before"].string
        let untilSignature = data["until"].string
        let limit = data["limit"].intValue
        
        ParticleWalletAPI.getSolanaService().getTokenTransactionsFromDB(address: address, mintAddress: mintAddress, beforeSignature: beforeSignature, untilSignature: untilSignature, limit: limit).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "solanaGetTokenTransactionsFromDB")
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "solanaGetTokenTransactionsFromDB")
            }
        }.disposed(by: bag)
    }
    
    func evmGetTokenList() {
        ParticleWalletAPI.getEvmService().getTokenList().subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenList):
                let rawData = tokenList.map {
                    self.map2UnityTokenInfo(from: $0)
                }
                
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
            
        }.disposed(by: bag)
    }
    
    func evmGetTokensAndNFTs(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        
        let tokenAddresses = JSON(parseJSON: data["token_addresses"].stringValue).arrayValue.map {
            $0.stringValue
        }
        ParticleWalletAPI.getEvmService().getTokensAndNFTs(by: address, tokenAddresses: tokenAddresses).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokenResult(from: tokenResult)
                let statusModel = UnityStatusModel(status: true, data: unityTokenResult)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func evmGetTokensAndNFTs(fromDB address: String) {
        ParticleWalletAPI.getEvmService().getTokensAndNFTsFromDB(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "evmGetTokensAndNFTsFromDB")
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokenResult(from: tokenResult)
                let statusModel = UnityStatusModel(status: true, data: unityTokenResult)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "evmGetTokensAndNFTsFromDB")
            }
        }.disposed(by: bag)
    }
    
    func evmGetTransactions(_ address: String) {
        ParticleWalletAPI.getEvmService().getTransactions(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityEvmTransaction(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func evmGetTransactions(fromDB address: String) {
        ParticleWalletAPI.getEvmService().getTransactionsFromDB(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "evmGetTransactionsFromDB")
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityEvmTransaction(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName, methodName: "evmGetTransactionsFromDB")
            }
        }.disposed(by: bag)
    }
    
    func evmAddCustomTokens(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        
        let tokenAddresses = JSON(parseJSON: data["token_addresses"].stringValue).arrayValue.map {
            $0.stringValue
        }
        
        ParticleWalletAPI.getEvmService().addCustomTokens(address: address, tokenAddresses: tokenAddresses).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityTokenModel(from: $0)
                }
                let statusModel = UnityStatusModel(status: true, data: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
}

// MARK: - Particle Wallet GUI

extension UnityManager {
    func enablePay(_ enable: Bool) {
        ParticleWalletGUI.enablePay(enable)
    }
    
    func getEnablePay() -> Bool {
        ParticleWalletGUI.getEnablePay()
    }
    
    func navigatorWallet(_ display: Int) {
        if display != 0 {
            PNRouter.navigatorWallet(display: .nft)
        } else {
            PNRouter.navigatorWallet(display: .token)
        }
    }
    
    func navigatorTokenReceive(_ json: String?) {
        PNRouter.navigatorTokenReceive(tokenReceiveConfig: TokenReceiveConfig(tokenAddress: json))
    }
    
    func navigatorTokenSend(_ json: String?) {
        if let json = json {
            let data = JSON(parseJSON: json)
            let tokenAddress = data["token_address"].string
            let toAddress = data["to_address"].string
            let amount = data["amount"].string
            let config = TokenSendConfig(tokenAddress: tokenAddress, toAddress: toAddress, amountString: amount)
            
            PNRouter.navigatorTokenSend(tokenSendConfig: config)
        } else {
            PNRouter.navigatorTokenSend()
        }
    }
    
    func navigatorTokenTransactionRecords(_ json: String?) {
        if let json = json {
            let config = TokenTransactionRecordsConfig(tokenAddress: json)
            PNRouter.navigatorTokenTransactionRecords(tokenTransactionRecordsConfig: config)
        } else {
            PNRouter.navigatorTokenTransactionRecords()
        }
    }
    
    func navigatorNFTSend(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["mint"].stringValue
        let toAddress = data["receiver_address"].stringValue
        let tokenId = data["token_id"].stringValue
        let config = NFTSendConfig(address: address, toAddress: toAddress.isEmpty ? nil : toAddress, tokenId: tokenId)
        PNRouter.navigatroNFTSend(nftSendConfig: config)
    }
    
    func navigatorNFTDetails(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["mint"].stringValue
        let tokenId = data["token_id"].stringValue
        let config = NFTDetailsConfig(address: address, tokenId: tokenId)
        PNRouter.navigatorNFTDetails(nftDetailsConfig: config)
    }
    
    func navigatorPay() {
        PNRouter.navigatorPay()
    }
    
    func navigatorBuyCrypto(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletAddress = data["wallet_address"].string
        let networkString = data["network"].stringValue.lowercased()
        var network: OpenBuyNetwork?
        /*
         Solana,
         Ethereum,
         BinanceSmartChain,
         Avalanche,
         Polygon,
         */
        if networkString == "solana" {
            network = .solana
        } else if networkString == "ethereum" {
            network = .ethereum
        } else if networkString == "binancesmartchain" {
            network = .binanceSmartChain
        } else if networkString == "avalanche" {
            network = .avalanche
        } else if networkString == "polygon" {
            network = .polygon
        } else {
            network = nil
        }
        let fiatCoin = data["fiat_coin"].string
        let fiatAmt = data["fiat_amt"].int
        let cryptoCoin = data["crypto_coin"].string
        
        PNRouter.navigatorBuy(walletAddress: walletAddress, network: network, cryptoCoin: cryptoCoin, fiatCoin: fiatCoin, fiatAmt: fiatAmt)
    }
    
    func navigatorSwap(_ json: String?) {
        if let json = json {
            let data = JSON(parseJSON: json)
            let fromTokenAddress = data["from_token_address"].string
            let toTokenAddress = data["to_token_address"].string
            let amount = data["amount"].string
            let config = SwapConfig(fromTokenAddress: fromTokenAddress, toTokenAddress: toTokenAddress, fromTokenAmountString: amount)
            
            PNRouter.navigatorSwap(swapConfig: config)
        } else {
            PNRouter.navigatorSwap()
        }
    }
    
    func showTestNetwork(_ show: Bool) {
        ParticleWalletGUI.showTestNetwork(show)
    }
    
    func showManageWallet(_ show: Bool) {
        ParticleWalletGUI.showManageWallet(show)
    }
    
    func supportChain(_ json: String) {
        let chains = JSON(parseJSON: json).arrayValue.map {
            $0.stringValue.lowercased()
        }.compactMap {
            self.matchChain(name: $0)
        }
        ParticleWalletGUI.supportChain(chains)
    }
    
    func enableSwap(_ enable: Bool) {
        ParticleWalletGUI.enableSwap(enable)
    }
    
    func getEnableSwap() -> Bool {
        ParticleWalletGUI.getEnableSwap()
    }
    
    func navigatorLoginList() {
        PNRouter.navigatorLoginList().subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.guiSystemName, methodName: "loginList")
            case .success(let (walletType, account)):
                guard let account = account else { return }
                
                let unityLoginListModel = UnityLoginListModel(walletType: walletType.stringValue, account: account)
                let statusModel = UnityStatusModel(status: true, data: unityLoginListModel)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.guiSystemName, methodName: "loginList")
            }
        }.disposed(by: bag)
    }
    
    func switchWallet(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        
        if let walletType = map2WalletType(from: walletTypeString) {
            let result = ParticleWalletGUI.switchWallet(walletType: walletType, publicAddress: publicAddress)
            
            let statusModel = UnityStatusModel(status: true, data: result == true ? "success" : "failed")
            
            let data = try! JSONEncoder().encode(statusModel)
            guard let json = String(data: data, encoding: .utf8) else { return }
            callBackMessage(json, unityName: UnityManager.guiSystemName, methodName: "switchWallet")
        } else {
            print("walletType \(walletTypeString) is not existed")
            let response = UnityResponseError(code: nil, message: "walletType \(walletTypeString) is not existed", data: nil)
            let statusModel = UnityStatusModel(status: false, data: response)
            let data = try! JSONEncoder().encode(statusModel)
            guard let json = String(data: data, encoding: .utf8) else { return }
            callBackMessage(json, unityName: UnityManager.guiSystemName, methodName: "switchWallet")
        }
    }
    
    func guiSetLanguage(_ json: String) {
        /**
         EN,
         ZH_HANS,
         ZH_HANT,
         JA,
         KO
         */
        if json.lowercased() == "en" {
            ParticleWalletGUI.setLanguage(Language.en)
        } else if json.lowercased() == "zh_hans" {
            ParticleWalletGUI.setLanguage(Language.zh_Hans)
        } else if json.lowercased() == "zh_hant" {
            ParticleWalletGUI.setLanguage(Language.zh_Hant)
        } else if json.lowercased() == "ko" {
            ParticleWalletGUI.setLanguage(Language.ko)
        } else if json.lowercased() == "ja" {
            ParticleWalletGUI.setLanguage(Language.ja)
        }
    }
    
    func showLanguageSetting(_ show: Bool) {
        ParticleWalletGUI.showLanguageSetting(show)
    }
    
    func showAppearanceSetting(_ show: Bool) {
        ParticleWalletGUI.showAppearanceSetting(show)
    }
    
    func supportWalletConnect(_ enable: Bool) {
        ParticleWalletGUI.supportWalletConnect(enable)
    }
    
    func particleWalletConnectInitialize(_ json: String) {
        let data = JSON(parseJSON: json)
        let name = data["name"].stringValue
        let iconString = data["icon"].stringValue
        let urlString = data["url"].stringValue
        let description = data["description"].string
        
        let icon = URL(string: iconString) != nil ? URL(string: iconString)! : URL(string: "https://static.particle.network/wallet-icons/Particle.png")!
        
        let url = URL(string: urlString) != nil ? URL(string: urlString)! : URL(string: "https://static.particle.network")!
        
        let walletMetaData = WalletMetaData(name: name, icon: icon, url: url, description: description)
        
        ParticleWalletConnect.initialize(walletMetaData)
    }
    
    func setSupportAddToken(_ enable: Bool) {
        ParticleWalletGUI.setSupportAddToken(enable)
    }
    
    func setDisplayTokenAddresses(_ json: String) {
        let data = JSON(parseJSON: json)
        let tokenAddresses = data.arrayValue.map {
            $0.stringValue
        }
        ParticleWalletGUI.setDisplayTokenAddresses(tokenAddresses)
    }

    func setDisplayNFTContractAddresses(_ json: String) {
        let data = JSON(parseJSON: json)
        let nftContractAddresses = data.arrayValue.map {
            $0.stringValue
        }
        ParticleWalletGUI.setDisplayNFTContractAddresses(nftContractAddresses)
    }

    func setFiatCoin(_ json: String) {
        let fiatCoin = json
        ParticleWalletGUI.setFiatCoin(fiatCoin)
    }

    func loadCustomUIJsonString(_ json: String) {
        let jsonString = json
        do {
            try ParticleWalletGUI.loadCustomUIJsonString(jsonString)
        } catch {
            print("loadCustomUIJsonString error = \(error)")
        }
    }
}

// MARK: - Particle Connect

extension UnityManager {
    func particleConnectInitialize(_ json: String) {
        let data = JSON(parseJSON: json)
        let chainName = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainInfo = matchChain(name: chainName, chainId: chainId) else {
            return print("initialize error, can't find right chain for \(chainName), chainId \(chainId)")
        }
        let env = data["env"].stringValue.lowercased()
        var devEnv: ParticleNetwork.DevEnvironment = .production
        if env == "dev" {
            devEnv = .debug
        } else if env == "staging" {
            devEnv = .staging
        } else if env == "production" {
            devEnv = .production
        }
        
        let dAppName = data["metadata"]["name"].stringValue
        let dAppIconString = data["metadata"]["icon"].stringValue
        let dAppUrlString = data["metadata"]["url"].stringValue
        
        let dAppIconUrl = URL(string: dAppIconString) != nil ? URL(string: dAppIconString)! : URL(string: "https://static.particle.network/wallet-icons/Particle.png")!
        
        let dAppUrl = URL(string: dAppUrlString) != nil ? URL(string: dAppUrlString)! : URL(string: "https://static.particle.network")!
        
        let dAppData = DAppMetaData(name: dAppName, icon: dAppIconUrl, url: dAppUrl)
        
        var adapters: [ConnectAdapter] = [ParticleConnectAdapter()]
#if canImport(ConnectEVMAdapter)
        let evmRpcUrl = data["rpc_url"]["evm_url"].stringValue
        if evmRpcUrl.isEmpty {
            adapters.append(EVMConnectAdapter())
        } else {
            adapters.append(EVMConnectAdapter(rpcUrl: evmRpcUrl))
        }
#endif
        
#if canImport(ConnectSolanaAdapter)
        let solanaRpcUrl = data["rpc_url"]["sol_url"].stringValue
        if solanaRpcUrl.isEmpty {
            adapters.append(SolanaConnectAdapter())
        } else {
            adapters.append(SolanaConnectAdapter(rpcUrl: solanaRpcUrl))
        }
#endif
        
#if canImport(ConnectPhantomAdapter)
        adapters.append(PhantomConnectAdapter())
#endif
        
#if canImport(ConnectWalletConnectAdapter)
        adapters.append(MetaMaskConnectAdapter())
        adapters.append(RainbowConnectAdapter())
        adapters.append(BitkeepConnectAdapter())
        adapters.append(ImtokenConnectAdapter())
        adapters.append(WalletConnectAdapter())
#endif
        
        ParticleConnect.initialize(env: devEnv, chainInfo: chainInfo, dAppData: dAppData) {
            adapters
        }
    }
    
    func particleConnectSetChainInfo(_ json: String) -> Bool {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainInfo = matchChain(name: name, chainId: chainId) else { return false }
        ParticleConnect.setChain(chainInfo: chainInfo)
        return true
    }
    
    func particleConnectSetChainInfoAsync(_ json: String) {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainInfo = matchChain(name: name, chainId: chainId) else { return }
        
        if ParticleAuthService.isLogin() {
            ParticleAuthService.setChainInfo(chainInfo).subscribe { [weak self] result in
                guard let self = self else { return }

                switch result {
                case .failure(let error):
                    let response = self.ResponseFromError(error)
                    let statusModel = UnityStatusModel(status: false, data: response)
                    let data = try! JSONEncoder().encode(statusModel)
                    guard let json = String(data: data, encoding: .utf8) else { return }
                    
                    self.ufw?.sendMessageToGO(withName: UnityManager.connectSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
                case .success(let userInfo):
                    guard let userInfo = userInfo else { return }
                    let statusModel = UnityStatusModel(status: true, data: true)
                    let data = try! JSONEncoder().encode(statusModel)
                    guard let json = String(data: data, encoding: .utf8) else { return }
                    
                    self.ufw?.sendMessageToGO(withName: UnityManager.connectSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
                }
            }.disposed(by: bag)
        } else {
            ParticleNetwork.setChainInfo(chainInfo)
            let statusModel = UnityStatusModel(status: true, data: true)
            let data = try! JSONEncoder().encode(statusModel)
            guard let json = String(data: data, encoding: .utf8) else { return }
            ufw?.sendMessageToGO(withName: UnityManager.connectSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
        }
    }
    
    func adapterGetAccounts(_ json: String) -> String {
        let walletTypeString = json
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return ""
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return ""
        }
        
        let accounts = adapter.getAccounts()
        let statusModel = UnityStatusModel(status: true, data: accounts)
        let data = try! JSONEncoder().encode(statusModel)
        let json = String(data: data, encoding: .utf8) ?? ""
        
        return json
    }
    
    func adapterConnect(_ json: String, configJson: String) {
        let walletTypeString = json
        
        var connectConfig: ParticleConnectConfig?
        if !configJson.isEmpty {
            let data = JSON(parseJSON: configJson)
            let loginType = LoginType(rawValue: data["loginType"].stringValue.lowercased()) ?? .email
            var supportAuthTypeArray: [SupportAuthType] = []
            
            let array = data["supportAuthTypeValues"].arrayValue.map {
                $0.stringValue.lowercased()
            }
            if array.contains("all") {
                supportAuthTypeArray = [.all]
            } else {
                array.forEach {
                    if $0 == "apple" {
                        supportAuthTypeArray.append(.apple)
                    } else if $0 == "google" {
                        supportAuthTypeArray.append(.google)
                    } else if $0 == "facebook" {
                        supportAuthTypeArray.append(.facebook)
                    } else if $0 == "github" {
                        supportAuthTypeArray.append(.github)
                    } else if $0 == "twitch" {
                        supportAuthTypeArray.append(.twitch)
                    } else if $0 == "microsoft" {
                        supportAuthTypeArray.append(.microsoft)
                    } else if $0 == "linkedin" {
                        supportAuthTypeArray.append(.linkedin)
                    } else if $0 == "discord" {
                        supportAuthTypeArray.append(.discord)
                    }
                }
            }
            
            var account = data["account"].string
            
            if account != nil, account!.isEmpty {
                account = nil
            }
            
            let loginFormMode = data["loginFormMode"].boolValue
            connectConfig = ParticleConnectConfig(loginType: loginType, supportAuthType: supportAuthTypeArray, loginFormMode: loginFormMode, phoneOrEmailAccount: account)
        }
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        guard let vc = ufw?.appController().rootViewController else {
            print("unity root view controller is nil")
            return
        }
        
        var observable: Single<Account?>
        if walletType == .walletConnect {
            observable = (adapter as! WalletConnectAdapter).connectWithQrCode(from: vc)
        } else if walletType == .particle {
            observable = adapter.connect(connectConfig)
        } else {
            observable = adapter.connect(ConnectConfig.none)
        }
        
        observable.subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "connect")
            case .success(let account):
                guard let account = account else { return }
                let statusModel = UnityStatusModel(status: true, data: account)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "connect")
            }
        }.disposed(by: bag)
    }
    
    func adapterDisconnect(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.disconnect(publicAddress: publicAddress).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "disconnect")
            case .success(let success):
                let statusModel = UnityStatusModel(status: true, data: success)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "disconnect")
            }
        }.disposed(by: bag)
    }
    
    func adapterIsConnected(_ json: String) -> Bool {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return false
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return false
        }
        
        return adapter.isConnected(publicAddress: publicAddress)
    }
    
    func adapterSignAndSendTransaction(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let transaction = data["transaction"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.signAndSendTransaction(publicAddress: publicAddress, transaction: transaction).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signAndSendTransaction")
            case .success(let signature):
                let statusModel = UnityStatusModel(status: true, data: signature)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signAndSendTransaction")
            }
        }.disposed(by: bag)
    }
    
    func adapterSignTransaction(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let transaction = data["transaction"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.signTransaction(publicAddress: publicAddress, transaction: transaction).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signTransaction")
            case .success(let signed):
                let statusModel = UnityStatusModel(status: true, data: signed)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signTransaction")
            }
        }.disposed(by: bag)
    }
    
    func adapterSignAllTransactions(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        
        let transactions = data["transactions"].arrayValue.map {
            $0.stringValue
        }
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.signAllTransactions(publicAddress: publicAddress, transactions: transactions).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signAllTransactions")
            case .success(let signed):
                let statusModel = UnityStatusModel(status: true, data: signed)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signAllTransactions")
            }
        }.disposed(by: bag)
    }
    
    func adapterSignMessage(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let message = data["message"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.signMessage(publicAddress: publicAddress, message: message).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signMessage")
            case .success(let signed):
                let statusModel = UnityStatusModel(status: true, data: signed)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signMessage")
            }
        }.disposed(by: bag)
    }
    
    func adapterSignTypedData(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let message = data["message"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.signTypeData(publicAddress: publicAddress, data: message).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signTypedData")
            case .success(let signed):
                let statusModel = UnityStatusModel(status: true, data: signed)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "signTypedData")
            }
        }.disposed(by: bag)
    }
    
    func adapterImportWallet(fromPrivateKey json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let privateKey = data["private_key"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard walletType == WalletType.evmPrivateKey || walletType == WalletType.solanaPrivateKey else {
            print("walletType \(walletTypeString) is not support import from private key ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        (adapter as! LocalAdapter).importWalletFromPrivateKey(privateKey).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "importWalletFromPrivateKey")
            case .success(let account):
                guard let account = account else { return }
                let statusModel = UnityStatusModel(status: true, data: account)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "importWalletFromPrivateKey")
            }
        }.disposed(by: bag)
    }
    
    func adapterImportWallet(fromMnemonic json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let mnemonic = data["mnemonic"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard walletType == WalletType.evmPrivateKey || walletType == WalletType.solanaPrivateKey else {
            print("walletType \(walletTypeString) is not support import from mnemonic ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        (adapter as! LocalAdapter).importWalletFromMnemonic(mnemonic).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "importWalletFromMnemonic")
            case .success(let account):
                guard let account = account else { return }
                let statusModel = UnityStatusModel(status: true, data: account)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "importWalletFromMnemonic")
            }
        }.disposed(by: bag)
    }
    
    func adapterExportWalletPrivateKey(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard walletType == WalletType.evmPrivateKey || walletType == WalletType.solanaPrivateKey else {
            print("walletType \(walletTypeString) is not support import from mnemonic ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        (adapter as! LocalAdapter).exportWalletPrivateKey(publicAddress: publicAddress).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "exportWalletPrivateKey")
            case .success(let privateKey):
                let statusModel = UnityStatusModel(status: true, data: privateKey)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "exportWalletPrivateKey")
            }
        }.disposed(by: bag)
    }
    
    func adapterLogin(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let domain = data["domain"].stringValue
        let address = publicAddress
        guard let uri = URL(string: data["uri"].stringValue) else { return }
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        let siwe = try! SiweMessage(domain: domain, address: address, uri: uri)
        
        adapter.login(config: siwe, publicAddress: publicAddress).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "login")
            case .success(let (sourceMessage, signedMessage)):
                let result = UnityConnectLoginResult(message: sourceMessage, signature: signedMessage)
                let statusModel = UnityStatusModel(status: true, data: result)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "login")
            }
        }.disposed(by: bag)
    }
    
    func adapterVerify(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let message = data["message"].stringValue

        var signature = data["signature"].stringValue
                
        if ConnectManager.getChainType() == .solana {
            signature = Base58.encode(Data(base64Encoded: signature)!)
        }
                
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        let siwe = try! SiweMessage(message)
        
        adapter.verify(message: siwe, against: signature).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = UnityStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "verify")
            case .success(let flag):
                let statusModel = UnityStatusModel(status: true, data: flag)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "verify")
            }
        }.disposed(by: bag)
    }
    
    func adapterAddEthereumChain(_ json: String) {
        let data = JSON(parseJSON: json)
        
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let chainId = data["chain_id"].intValue
        let chainName = data["chain_name"].string
        let rpcUrl = data["rpc_url"].string
        let blockExplorerUrl = data["block_explorer_url"].string
        let nativeCurrencyName = data["native_currency"]["name"].string
        let nativeCurrencySymbol = data["native_currency"]["symbol"].string
        let nativeCurrencyDecimals = data["native_currency"]["decimals"].uInt8
        
        var nativeCurrency: NativeCurrency?
        if nativeCurrencyName != nil, nativeCurrencySymbol != nil, nativeCurrencyDecimals != nil {
            nativeCurrency = NativeCurrency(name: nativeCurrencyName!, symbol: nativeCurrencySymbol!, decimals: nativeCurrencyDecimals!)
        }
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.addEthereumChain(publicAddress: publicAddress, chainId: chainId, chainName: chainName, nativeCurrency: nativeCurrency, rpcUrl: rpcUrl, blockExplorerUrl: blockExplorerUrl).subscribe {
            [weak self] result in
                guard let self = self else { return }
                switch result {
                case .failure(let error):
                    let response = self.ResponseFromError(error)
                    let statusModel = UnityStatusModel(status: false, data: response)
                    let data = try! JSONEncoder().encode(statusModel)
                    guard let json = String(data: data, encoding: .utf8) else { return }
                    self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "addEthereumChain")
                case .success(let flag):
                    let statusModel = UnityStatusModel(status: true, data: flag)
                    let data = try! JSONEncoder().encode(statusModel)
                    guard let json = String(data: data, encoding: .utf8) else { return }
                    self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "addEthereumChain")
                }
        }.disposed(by: bag)
    }
    
    func adapterSwitchEthereumChain(_ json: String) {
        let data = JSON(parseJSON: json)
            
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let chainId = data["chain_id"].intValue
        
        guard let walletType = map2WalletType(from: walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            return
        }
        
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init ")
            return
        }
        
        adapter.switchEthereumChain(publicAddress: publicAddress, chainId: chainId).subscribe {
            [weak self] result in
                guard let self = self else { return }
                switch result {
                case .failure(let error):
                    let response = self.ResponseFromError(error)
                    let statusModel = UnityStatusModel(status: false, data: response)
                    let data = try! JSONEncoder().encode(statusModel)
                    guard let json = String(data: data, encoding: .utf8) else { return }
                    self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "switchEthereumChain")
                case .success(let flag):
                    let statusModel = UnityStatusModel(status: true, data: flag)
                    let data = try! JSONEncoder().encode(statusModel)
                    guard let json = String(data: data, encoding: .utf8) else { return }
                    self.callBackMessage(json, unityName: UnityManager.connectSystemName, methodName: "switchEthereumChain")
                }
            
        }.disposed(by: bag)
    }
}

// MARK: - Help methods

extension UnityManager {
    func callBackMessage(_ message: String, unityName: String, methodName: String = #function) {
        var methodName = methodName.replacingOccurrences(of: "\\([\\w\\s:]*\\)", with: "", options: .regularExpression)
        methodName = methodName.prefix(1).uppercased() + methodName.dropFirst() + "CallBack"
        ufw?.sendMessageToGO(withName: unityName, functionName: methodName, message: message)
    }
}

extension Dictionary {
    /// - Parameter prettify: set true to prettify string (default is false).
    /// - Returns: optional JSON String (if applicable).
    func jsonString(prettify: Bool = false) -> String? {
        guard JSONSerialization.isValidJSONObject(self) else { return nil }
        let options = (prettify == true) ? JSONSerialization.WritingOptions.prettyPrinted : JSONSerialization.WritingOptions()
        guard let jsonData = try? JSONSerialization.data(withJSONObject: self, options: options) else { return nil }
        return String(data: jsonData, encoding: .utf8)
    }
}

extension UnityManager {
    private func map2UnityTokenResult(from tokenResult: TokenResult) -> UnityTokenResult {
        let tokens = tokenResult.tokens.map {
            map2UnityTokenModel(from: $0)
        }
        
        let nfts = tokenResult.nfts.map {
            map2UnityNFTModel(from: $0)
        }
        return UnityTokenResult(tokens: tokens, nfts: nfts)
    }
    
    private func map2UnityTokenModel(from tokenModel: TokenModel) -> UnityTokenModel {
        UnityTokenModel(mintAddress: tokenModel.mintAddress,
                        amount: tokenModel.amount.asString(radix: 10),
                        decimals: Int(tokenModel.decimals),
                        updateAt: Int(tokenModel.updateAt),
                        symbol: tokenModel.symbol,
                        logoURI: tokenModel.imageUrl)
    }
    
    private func map2UnityNFTModel(from nftModel: NFTModel) -> UnityNFTModel {
        UnityNFTModel(mintAddress: nftModel.mintAddress,
                      image: nftModel.image,
                      symbol: nftModel.symbol,
                      name: nftModel.name,
                      sellerFeeBasisPoints: nftModel.sellerFeeBasisPoints,
                      description: nftModel.descriptionString,
                      externalUrl: nftModel.externalUrl,
                      animationUrl: nftModel.animationUrl,
                      data: nftModel.data,
                      isSemiFungible: nftModel.isSemiFungible,
                      tokenId: nftModel.tokenId,
                      tokenBalance: nftModel.tokenBalance.asString(radix: 10))
    }
    
    private func map2UnityTokenInfo(from tokenInfo: TokenInfo) -> UnityTokenInfo {
        return UnityTokenInfo(chainId: tokenInfo.chainId,
                              address: tokenInfo.mintAddress,
                              symbol: tokenInfo.symbol,
                              name: tokenInfo.name,
                              decimals: Int(tokenInfo.decimals),
                              logoURI: tokenInfo.logoURI)
    }
    
    private func map2UnitySolanaTransaction(from transaction: SolanaTransactionModel) -> UnitySolanaTransactionModel {
        var data = ""
        if transaction.data != nil {
            data = String(data: try! JSONEncoder().encode(transaction.data!), encoding: .utf8) ?? ""
        }
        return UnitySolanaTransactionModel(from: transaction.from,
                                           to: transaction.to,
                                           type: transaction.type.rawValue,
                                           lamportsChange: transaction.lamportsChange.asString(radix: 10),
                                           lamportsFee: transaction.lamportsFee.asString(radix: 10),
                                           signature: transaction.signature,
                                           blockTime: transaction.blockTime,
                                           status: transaction.status.rawValue,
                                           data: data,
                                           mint: transaction.mintAddress ?? "")
    }
    
    private func map2UnityEvmTransaction(from transaction: EVMTransactionModel) -> UnityEvmTransactionModel {
        UnityEvmTransactionModel(from: transaction.from,
                                 to: transaction.to,
                                 hash: transaction.hashString,
                                 value: transaction.value.asString(radix: 10),
                                 data: transaction.data,
                                 gasLimit: transaction.gasLimit.asString(radix: 10),
                                 gasSpent: transaction.gasSpent.asString(radix: 10),
                                 gasPrice: transaction.gasPrice.asString(radix: 10),
                                 fees: transaction.fees.asString(radix: 10),
                                 type: transaction.type,
                                 nonce: "0x" + transaction.nonce.asString(radix: 16),
                                 maxPriorityFeePerGas: transaction.maxPriorityFeePerGas.asString(radix: 10),
                                 maxFeePerGas: transaction.maxFeePerGas.asString(radix: 10),
                                 timestamp: transaction.timestamp,
                                 status: transaction.status.rawValue)
    }
    
    private func getMethodName(_ methodName: String = #function) -> String {
        let methodName = methodName.replacingOccurrences(of: "\\([\\w\\s:]*\\)", with: "", options: .regularExpression)
        return methodName
    }
    
    private func ResponseFromError(_ error: Error) -> UnityResponseError {
        if let responseError = error as? ParticleNetwork.ResponseError {
            return UnityResponseError(code: responseError.code, message: responseError.message ?? "", data: responseError.data)
        } else if let error = error as? ConnectError {
            return UnityResponseError(code: error.code, message: error.message!, data: nil)
        } else {
            return UnityResponseError(code: nil, message: String(describing: error), data: nil)
        }
    }
    
    private func map2WalletType(from string: String) -> WalletType? {
        /* Define in unity
         Particle,
         EvmPrivateKey,
         SolanaPrivateKey,
         MetaMask,
         Rainbow,
         Trust,
         ImToken,
         BitKeep,
         WalletConnect,
         Phantom,
         */
        let str = string.lowercased()
        var walletType: WalletType?
        if str == "particle" {
            walletType = .particle
        } else if str == "evmprivatekey" {
            walletType = .evmPrivateKey
        } else if str == "solanaprivatekey" {
            walletType = .solanaPrivateKey
        } else if str == "metamask" {
            walletType = .metaMask
        } else if str == "rainbow" {
            walletType = .rainbow
        } else if str == "trust" {
            walletType = .trust
        } else if str == "imtoken" {
            walletType = .imtoken
        } else if str == "bitkeep" {
            walletType = .bitkeep
        } else if str == "walletconnect" {
            walletType = .walletConnect
        } else if str == "phantom" {
            walletType = .phantom
        } else {
            walletType = nil
        }
        
        return walletType
    }
    
    private func map2ConnectAdapter(from walletType: WalletType) -> ConnectAdapter? {
        let adapters = ParticleConnect.getAllAdapters().filter {
            $0.walletType == walletType
        }
        let adapter = adapters.first
        return adapter
    }
}

extension UnityManager {
    func matchChain(name: String, chainId: Int) -> ParticleNetwork.ChainInfo? {
        var chainInfo: ParticleNetwork.ChainInfo?
        
        if name == "solana" {
            if chainId == 101 {
                chainInfo = .solana(.mainnet)
            } else if chainId == 102 {
                chainInfo = .solana(.testnet)
            } else if chainId == 103 {
                chainInfo = .solana(.devnet)
            }
        } else if name == "ethereum" {
            if chainId == 1 {
                chainInfo = .ethereum(.mainnet)
            } else if chainId == 5 {
                chainInfo = .ethereum(.goerli)
            }
        } else if name == "bsc" {
            if chainId == 56 {
                chainInfo = .bsc(.mainnet)
            } else if chainId == 97 {
                chainInfo = .bsc(.testnet)
            }
        } else if name == "polygon" {
            if chainId == 137 {
                chainInfo = .polygon(.mainnet)
            } else if chainId == 80001 {
                chainInfo = .polygon(.mumbai)
            }
        } else if name == "avalanche" {
            if chainId == 43114 {
                chainInfo = .avalanche(.mainnet)
            } else if chainId == 43113 {
                chainInfo = .avalanche(.testnet)
            }
        } else if name == "fantom" {
            if chainId == 250 {
                chainInfo = .fantom(.mainnet)
            } else if chainId == 4002 {
                chainInfo = .fantom(.testnet)
            }
        } else if name == "arbitrum" {
            if chainId == 42161 {
                chainInfo = .arbitrum(.mainnet)
            } else if chainId == 421613 {
                chainInfo = .arbitrum(.goerli)
            }
        } else if name == "moonbeam" {
            if chainId == 1284 {
                chainInfo = .moonbeam(.mainnet)
            } else if chainId == 1287 {
                chainInfo = .moonbeam(.testnet)
            }
        } else if name == "moonriver" {
            if chainId == 1285 {
                chainInfo = .moonriver(.mainnet)
            } else if chainId == 1287 {
                chainInfo = .moonriver(.testnet)
            }
        } else if name == "heco" {
            if chainId == 128 {
                chainInfo = .heco(.mainnet)
            } else if chainId == 256 {
                chainInfo = .heco(.testnet)
            }
        } else if name == "aurora" {
            if chainId == 1313161554 {
                chainInfo = .aurora(.mainnet)
            } else if chainId == 1313161555 {
                chainInfo = .aurora(.testnet)
            }
        } else if name == "harmony" {
            if chainId == 1666600000 {
                chainInfo = .harmony(.mainnet)
            } else if chainId == 1666700000 {
                chainInfo = .harmony(.testnet)
            }
        } else if name == "kcc" {
            if chainId == 321 {
                chainInfo = .kcc(.mainnet)
            } else if chainId == 322 {
                chainInfo = .kcc(.testnet)
            }
        } else if name == "optimism" {
            if chainId == 10 {
                chainInfo = .optimism(.mainnet)
            } else if chainId == 420 {
                chainInfo = .optimism(.goerli)
            }
        } else if name == "platon" {
            if chainId == 210425 {
                chainInfo = .platON(.mainnet)
            } else if chainId == 2203181 {
                chainInfo = .platON(.testnet)
            }
        }
        return chainInfo
    }
    
    func matchChain(name: String) -> ParticleNetwork.Chain? {
        var chain: ParticleNetwork.Chain?
        
        if name == "solana" {
            chain = .solana
        } else if name == "ethereum" {
            chain = .ethereum
        } else if name == "bsc" {
            chain = .bsc
        } else if name == "polygon" {
            chain = .polygon
        } else if name == "avalanche" {
            chain = .avalanche
        } else if name == "fantom" {
            chain = .fantom
        } else if name == "arbitrum" {
            chain = .arbitrum
        } else if name == "moonbeam" {
            chain = .moonbeam
        } else if name == "moonriver" {
            chain = .moonriver
        } else if name == "heco" {
            chain = .heco
        } else if name == "aurora" {
            chain = .aurora
        } else if name == "harmony" {
            chain = .harmony
        } else if name == "kcc" {
            chain = .kcc
        } else if name == "optimism" {
            chain = .optimism
        }
        return chain
    }
}

struct UnityTokenInfo: Codable {
    let chainId: Int
    let address: String
    let symbol: String
    let name: String
    let decimals: Int
    let logoURI: String
}

struct UnityTokenResult: Codable {
    let tokens: [UnityTokenModel]
    let nfts: [UnityNFTModel]
}

struct UnityTokenModel: Codable {
    let mintAddress: String
    let amount: String
    let decimals: Int
    let updateAt: Int
    let symbol: String
    let logoURI: String
}

struct UnityNFTModel: Codable {
    let mintAddress: String
    let image: String
    let symbol: String
    let name: String
    let sellerFeeBasisPoints: Int
    let description: String
    let externalUrl: String
    let animationUrl: String
    let data: String
    let isSemiFungible: Bool
    let tokenId: String
    let tokenBalance: String
}

struct UnitySolanaTransactionModel: Codable {
    let from: String
    let to: String
    let type: String
    let lamportsChange: String
    let lamportsFee: String
    let signature: String
    let blockTime: Int
    let status: Int
    let data: String
    let mint: String
}

struct UnityEvmTransactionModel: Codable {
    let from: String
    let to: String
    let hash: String
    let value: String
    let data: String
    let gasLimit: String
    let gasSpent: String
    let gasPrice: String
    let fees: String
    let type: Int
    let nonce: String
    let maxPriorityFeePerGas: String
    let maxFeePerGas: String
    let timestamp: Int
    let status: Int
}

struct UnityResponseError: Codable {
    let code: Int?
    let message: String?
    let data: String?
}

struct UnityStatusModel<T: Codable>: Codable {
    let status: Bool
    let data: T
}

struct UnityLoginListModel: Codable {
    let walletType: String
    let account: Account
}

struct UnityConnectLoginResult: Codable {
    let message: String
    let signature: String
}

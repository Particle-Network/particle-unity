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
import ParticleWalletGUI

@objcMembers
class UnityManager: NSObject, UnityFrameworkListener, NativeCallsProtocol {
    let bag = DisposeBag()
    static var shared = UnityManager()
    
    var ufw: UnityFramework?
    
    static let apiSystemName = "ParticleWalletAPI"
    static let authSystemName = "ParticleAuthService"
    
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
        
        let config = ParticleNetworkConfiguration(chainName: chainName, devEnv: devEnv)
        ParticleNetwork.initialize(config: config)
    }
    
    func getDevEnv() -> Int {
        return ParticleNetwork.getDevEnv().rawValue
    }
    
    func setChainName(_ json: String) {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainName = matchChain(name: name, chainId: chainId) else { return }
        ParticleNetwork.setChainName(chainName)
    }
    
    func getChainName() -> String {
        let chainName = ParticleNetwork.getChainName()
        return ["chain_name": chainName.name, "chain_id": chainName.chainId, "chain_id_name": chainName.network].jsonString() ?? ""
    }
}

// MARK: - Particle Auth Service

extension UnityManager {
    func login(_ type: String, account: String?, supportAuthType: String) {
        let loginType = LoginType(rawValue: type) ?? .email
        var supportAuthTypeArray: [SupportAuthType] = []
        JSON(supportAuthType).arrayValue.forEach {
            if $0.string == "apple" {
                supportAuthTypeArray.append(.apple)
            } else if $0.string == "google" {
                supportAuthTypeArray.append(.google)
            } else if $0.string == "facebook" {
                supportAuthTypeArray.append(.facebook)
            }
        }
        
        ParticleAuthService.login(type: loginType, account: account, supportAuthType: supportAuthTypeArray).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let userInfo):
                guard let userInfo = userInfo else { return }
                let statusModel = StatusModel(status: true, json: userInfo)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let success):
                let statusModel = StatusModel(status: true, json: success)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func isUserLoggedIn() -> Bool {
        return ParticleAuthService.isUserLoggedIn()
    }
    
    func signMessage(_ message: String) {
        ParticleAuthService.signMessage(message).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = StatusModel(status: true, json: signedMessage)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = StatusModel(status: true, json: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            }
        }.disposed(by: bag)
    }
    
    func signAllTransactions(_ transactions: String) {
        let transactions = JSON(transactions).arrayValue.map { $0.stringValue }
        ParticleAuthService.signAllTransactions(transactions).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = StatusModel(status: true, json: signedMessage)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signature):
                let statusModel = StatusModel(status: true, json: signature)
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
        
        ParticleAuthService.signTypedData(message, version: EVMSignTypedDataVersion(rawValue: version) ?? .v1).subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.authSystemName)
            case .success(let signedMessage):
                let statusModel = StatusModel(status: true, json: signedMessage)
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
        guard let userInfo = ParticleAuthService.getUserInfo() else { return "" }
        let statusModel = StatusModel(status: true, json: userInfo)
        let data = try! JSONEncoder().encode(statusModel)
        let json = String(data: data, encoding: .utf8)
        return json ?? ""
    }
    
    func setChainNameAsync(_ json: String) {
        let data = JSON(parseJSON: json)
        let name = data["chain_name"].stringValue.lowercased()
        let chainId = data["chain_id"].intValue
        guard let chainName = matchChain(name: name, chainId: chainId) else { return }
        ParticleAuthService.setChainName(chainName).subscribe { [weak self] result in
            guard let self = self else { return }

            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.ufw?.sendMessageToGO(withName: UnityManager.authSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
            case .success:
                let statusModel = StatusModel(status: true, json: "")
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.ufw?.sendMessageToGO(withName: UnityManager.authSystemName, functionName: "SetChainInfoAsyncCallBack", message: json)
            }
        }.disposed(by: bag)
    }
    
    func setModalPresentStyle(_ style: String) {
        if style == "fullScreen" {
            ParticleAuthService.setModalPresentStyle(.fullScreen)
        } else {
            ParticleAuthService.setModalPresentStyle(.formSheet)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenList):
                let rawData = tokenList.map {
                    self.map2UnityTokenInfo(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
            
        }.disposed(by: bag)
    }
    
    func solanaGetTokensAndNFTs(_ address: String) {
        ParticleWalletAPI.getSolanaService().getTokensAndNFTs(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokentResult(from: tokenResult)
                
                let statusModel = StatusModel(status: true, json: unityTokenResult)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokentResult(from: tokenResult)
                
                let statusModel = StatusModel(status: true, json: unityTokenResult)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func solanaAddCustomTokens(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        let tokenAddresses = data["token_addresses"].arrayValue.map {
            $0.stringValue
        }
        
        ParticleWalletAPI.getSolanaService().addCustomTokens(address: address, tokenAddresses: tokenAddresses).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityTokenModel(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnitySolanaTransaction(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func evmGetTokenList() {
        ParticleWalletAPI.getEvmService().getTokenList().subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenList):
                let rawData = tokenList.map {
                    self.map2UnityTokenInfo(from: $0)
                }
                
                let statusModel = StatusModel(status: true, json: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
            
        }.disposed(by: bag)
    }
    
    func evmGetTokensAndNFTs(_ address: String) {
        ParticleWalletAPI.getEvmService().getTokensAndNFTs(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokentResult(from: tokenResult)
                let statusModel = StatusModel(status: true, json: unityTokenResult)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let tokenResult):
                let unityTokenResult = self.map2UnityTokentResult(from: tokenResult)
                let statusModel = StatusModel(status: true, json: unityTokenResult)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func evmGetTransactions(_ address: String) {
        ParticleWalletAPI.getEvmService().getTransactions(by: address).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityEvmTransaction(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
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
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityEvmTransaction(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            }
        }.disposed(by: bag)
    }
    
    func evmAddCustomTokens(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["address"].stringValue
        let tokenAddresses = data["token_addresses"].arrayValue.map {
            $0.stringValue
        }
        
        ParticleWalletAPI.getEvmService().addCustomTokens(address: address, tokenAddresses: tokenAddresses).subscribe { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                let response = self.ResponseFromError(error)
                let statusModel = StatusModel(status: true, json: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                
                self.callBackMessage(json, unityName: UnityManager.apiSystemName)
            case .success(let array):
                let rawData = array.map {
                    self.map2UnityTokenModel(from: $0)
                }
                let statusModel = StatusModel(status: true, json: rawData)
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
        let toAddress = data["receiver_address"].string
        let tokenId = data["token_id"].stringValue
        let config = NFTSendConfig(address: address, toAddress: toAddress, tokenId: tokenId)
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
}


// MARK: - Help methods

extension UnityManager {
    func callBackMessage(_ message: String, methodName: String = #function, unityName: String) {
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
    private func map2UnityTokentResult(from tokenResult: TokenResult) -> UnityTokenResult {
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
        if let error = error as? ParticleNetwork.Error {
            switch error {
            case .invalidResponse(let response):
                return UnityResponseError(code: response.code, message: response.message ?? "")
            case .invalidData(reason: let reason):
                return UnityResponseError(code: nil, message: reason ?? "")
            case .interrupt:
                return UnityResponseError(code: nil, message: "interrupt")
            }
        } else {
            return UnityResponseError(code: nil, message: String(describing: error))
        }
    }
}

extension UnityManager {
    func matchChain(name: String, chainId: Int) -> ParticleNetwork.ChainName? {
        var chainName: ParticleNetwork.ChainName?
        if name == ParticleNetwork.Name.solana.rawValue.lowercased() {
            if chainId == 101 {
                chainName = .solana(.mainnet)
            } else if chainId == 102 {
                chainName = .solana(.testnet)
            } else if chainId == 103 {
                chainName = .solana(.devnet)
            }
        } else if name == ParticleNetwork.Name.ethereum.rawValue.lowercased() {
            if chainId == 1 {
                chainName = .ethereum(.mainnet)
            } else if chainId == 42 {
                chainName = .ethereum(.kovan)
            }
        } else if name == ParticleNetwork.Name.bsc.rawValue.lowercased() {
            if chainId == 56 {
                chainName = .bsc(.mainnet)
            } else if chainId == 97 {
                chainName = .bsc(.testnet)
            }
        } else if name == ParticleNetwork.Name.polygon.rawValue.lowercased() {
            if chainId == 137 {
                chainName = .polygon(.mainnet)
            } else if chainId == 80001 {
                chainName = .polygon(.testnet)
            }
        } else if name == ParticleNetwork.Name.avalanche.rawValue.lowercased() {
            if chainId == 43114 {
                chainName = .avalanche(.mainnet)
            } else if chainId == 43113 {
                chainName = .avalanche(.testnet)
            }
        } else if name == ParticleNetwork.Name.fantom.rawValue.lowercased() {
            if chainId == 250 {
                chainName = .fantom(.mainnet)
            } else if chainId == 4002 {
                chainName = .fantom(.testnet)
            }
        } else if name == ParticleNetwork.Name.arbitrum.rawValue.lowercased() {
            if chainId == 42161 {
                chainName = .arbitrum(.mainnet)
            } else if chainId == 421611 {
                chainName = .arbitrum(.testnet)
            }
        } else if name == ParticleNetwork.Name.moonBeam.rawValue.lowercased() {
            if chainId == 1284 {
                chainName = .moonBeam(.mainnet)
            } else if chainId == 1287 {
                chainName = .moonBeam(.testnet)
            }
        } else if name == ParticleNetwork.Name.moonRiver.rawValue.lowercased() {
            if chainId == 1285 {
                chainName = .moonRiver(.mainnet)
            } else if chainId == 1287 {
                chainName = .moonRiver(.testnet)
            }
        } else if name == ParticleNetwork.Name.heco.rawValue.lowercased() {
            if chainId == 128 {
                chainName = .heco(.mainnet)
            } else if chainId == 256 {
                chainName = .heco(.testnet)
            }
        } else if name == ParticleNetwork.Name.aurora.rawValue.lowercased() {
            if chainId == 1313161554 {
                chainName = .aurora(.mainnet)
            } else if chainId == 1313161555 {
                chainName = .aurora(.testnet)
            }
        } else if name == ParticleNetwork.Name.harmony.rawValue.lowercased() {
            if chainId == 1666600000 {
                chainName = .harmony(.mainnet)
            } else if chainId == 1666700000 {
                chainName = .harmony(.testnet)
            }
        }
        return chainName
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
    let message: String
}

struct StatusModel<T: Codable>: Codable {
    let status: Bool
    let json: T
}

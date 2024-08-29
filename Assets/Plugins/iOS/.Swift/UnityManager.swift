//
//  UnityManager.swift
//  UnityFramework
//
//  Created by link on 2022/6/30.
//

import AuthCoreAdapter
import Base58_swift
import ConnectCommon
import ConnectEVMAdapter
import ConnectPhantomAdapter
import ConnectSolanaAdapter
import ConnectWalletConnectAdapter
import Foundation
import ParticleAA
import ParticleAuthCore
import ParticleConnect
import ParticleConnectKit
import ParticleNetworkBase
import ParticleNetworkChains
import ParticleWalletConnect
import ParticleWalletGUI
import RxSwift
import SwiftyJSON
import UIKit
import UnityFramework

@objcMembers
class UnityManager: NSObject, UnityFrameworkListener, NativeCallsProtocol {
    static var shared = UnityManager()

    var ufw: UnityFramework?

    static let connectSystemName = "ParticleConnect"
    static let guiSystemName = "ParticleWalletGUI"
    static let aaSystemName = "ParticleAA"

    // work with connect sdk
    var latestPublicAddress: String?
    static let authCoreSystemName = "ParticleAuthCore"

    override init() {
        super.init()
    }

    func startGame() {
        let ufw = self.unityFrameworkLoad()
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
        ShareBase.shared.initialize(json)
    }

    func setFiatCoin(_ json: String) {
        ShareBase.shared.setFiatCoin(json)
    }

    func setCustomUIConfigJsonString(_ json: String) {
        ShareBase.shared.setCustomUIConfigJsonString(json)
    }

    func setChainInfo(_ json: String) -> Bool {
        return ShareBase.shared.setChainInfo(json)
    }

    func getChainInfo() -> String {
        let value = ShareBase.shared.getChainInfo()
        return value
    }

    func setLanguage(_ json: String) {
        ShareBase.shared.setLanguage(json)
    }

    func getLanguage() -> String {
        var value = ShareBase.shared.getLanguage()
        if value == "zh_hans" {
            value = "zh_cn"
        } else if value == "zh_hant" {
            value = "zh_tw"
        }
        return value
    }

    func setAppearance(_ json: String) {
        ShareBase.shared.setAppearance(json)
    }

    func setSecurityAccountConfig(_ json: String) {
        ShareBase.shared.setSecurityAccountConfig(json)
    }

    func setThemeColor(_ json: String) {
        ShareBase.shared.setThemeColor(json)
    }

    func setUnsupportCountries(_ json: String) {
        ShareBase.shared.setUnsupportCountries(json)
    }
}

// MARK: - Particle Wallet GUI

extension UnityManager {
    func navigatorWallet(_ display: Int) {
        ShareWallet.shared.navigatorWallet(display)
    }

    func navigatorTokenReceive(_ json: String?) {
        ShareWallet.shared.navigatorTokenReceive(json ?? "")
    }

    func navigatorTokenSend(_ json: String?) {
        ShareWallet.shared.navigatorTokenSend(json ?? "")
    }

    func navigatorTokenTransactionRecords(_ json: String?) {
        ShareWallet.shared.navigatorTokenTransactionRecords(json ?? "")
    }

    func navigatorNFTSend(_ json: String) {
        ShareWallet.shared.navigatorNFTSend(json)
    }

    func navigatorNFTDetails(_ json: String) {
        ShareWallet.shared.navigatorNFTDetails(json)
    }

    func navigatorBuyCrypto(_ json: String) {
        ShareWallet.shared.navigatorBuyCrypto(json)
    }

    func navigatorSwap(_ json: String?) {
        ShareWallet.shared.navigatorSwap(json ?? "")
    }

    func setShowTestNetwork(_ show: Bool) {
        ShareWallet.shared.setShowTestNetwork(show)
    }

    func setShowManageWallet(_ show: Bool) {
        ShareWallet.shared.setShowManageWallet(show)
    }

    func setSupportChain(_ json: String) {
        ShareWallet.shared.setSupportChain(json)
    }

    func setSwapDisabled(_ disabled: Bool) {
        ShareWallet.shared.setSwapDisabled(disabled)
    }

    func getSwapDisabled() -> Bool {
        let value = ShareWallet.shared.getSwapDisabled()
        return value
    }

    func setPayDisabled(_ disabled: Bool) {
        ShareWallet.shared.setPayDisabled(disabled)
    }

    func getPayDisabled() -> Bool {
        let value = ShareWallet.shared.getPayDisabled()
        return value
    }

    func setBridgeDisabled(_ disabled: Bool) {
        ShareWallet.shared.setBridgeDisabled(disabled)
    }

    func getBridgeDisabled() -> Bool {
        let value = ShareWallet.shared.getBridgeDisabled()
        return value
    }

    func navigatorDappBrowser(_ json: String) {
        ShareWallet.shared.navigatorDappBrowser(json)
    }

    func switchWallet(_ json: String) {
        let value = ShareWallet.shared.switchWallet(json)
        let statusModel = PNStatusModel(status: true, data: value == true ? "success" : "failed")

        let data = try! JSONEncoder().encode(statusModel)
        guard let json = String(data: data, encoding: .utf8) else { return }
        UnityInteraction.callBackMessage(json, unityName: UnityManager.guiSystemName, methodName: "switchWallet")
    }

    func setShowLanguageSetting(_ show: Bool) {
        ShareWallet.shared.setShowLanguageSetting(show)
    }

    func setShowAppearanceSetting(_ show: Bool) {
        ShareWallet.shared.setShowAppearanceSetting(show)
    }

    func setSupportWalletConnect(_ enable: Bool) {
        ShareWallet.shared.setSupportWalletConnect(enable)
    }

    func setSupportDappBrowser(_ enable: Bool) {
        ShareWallet.shared.setSupportDappBrowser(enable)
    }

    func setShowSmartAccountSetting(_ show: Bool) {
        ShareWallet.shared.setShowSmartAccountSetting(show)
    }

    func particleWalletConnectInitialize(_ json: String) {
        ShareWallet.shared.initializeWalletMetaData(json)
    }

    func setSupportAddToken(_ enable: Bool) {
        ShareWallet.shared.setSupportAddToken(enable)
    }

    func setDisplayTokenAddresses(_ json: String) {
        ShareWallet.shared.setDisplayTokenAddresses(json)
    }

    func setDisplayNFTContractAddresses(_ json: String) {
        ShareWallet.shared.setDisplayNFTContractAddresses(json)
    }

    func setPriorityTokenAddresses(_ json: String) {
        ShareWallet.shared.setPriorityTokenAddresses(json)
    }

    func setPriorityNFTContractAddresses(_ json: String) {
        ShareWallet.shared.setPriorityNFTContractAddresses(json)
    }

    func setCustomWalletName(_ json: String) {
        ShareWallet.shared.setCustomWalletName(json)
    }

    func setCustomLocalizable(_ json: String) {
        ShareWallet.shared.setCustomLocalizable(json)
    }
}

// MARK: - Particle Connect

extension UnityManager {
    func particleConnectInitialize(_ json: String) {
        ShareConnect.shared.initialize(json)
    }

    func adapterGetAccounts(_ json: String) -> String {
        let value = ShareConnect.shared.getAccounts(json)
        return value
    }

    func adapterConnect(_ json: String) {
        ShareConnect.shared.connect(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "connect")
        }
    }

    func connect(withConnectKitConfig json: String) {
        ShareConnect.shared.connectWithConnectKitConfig(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "connectWithConnectKitConfig")
        }
    }

    func adapterDisconnect(_ json: String) {
        ShareConnect.shared.disconnect(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "disconnect")
        }
    }

    func adapterIsConnected(_ json: String) -> Bool {
        let value = ShareConnect.shared.isConnected(json)
        return value
    }

    func adapterSignAndSendTransaction(_ json: String) {
        ShareConnect.shared.signAndSendTransaction(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "signAndSendTransaction")
        }
    }

    func adapterBatchSendTransactions(_ json: String) {
        ShareConnect.shared.batchSendTransactions(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "batchSendTransactions")
        }
    }

    func adapterSignTransaction(_ json: String) {
        ShareConnect.shared.signTransaction(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "signTransaction")
        }
    }

    func adapterSignAllTransactions(_ json: String) {
        ShareConnect.shared.signAllTransactions(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "signAllTransactions")
        }
    }

    func adapterSignMessage(_ json: String) {
        ShareConnect.shared.signMessage(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "signMessage")
        }
    }

    func adapterSignTypedData(_ json: String) {
        ShareConnect.shared.signTypedData(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "signTypedData")
        }
    }

    func adapterImportWallet(fromPrivateKey json: String) {
        ShareConnect.shared.importPrivateKey(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "importWalletFromPrivateKey")
        }
    }

    func adapterImportWallet(fromMnemonic json: String) {
        ShareConnect.shared.importMnemonic(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "importWalletFromMnemonic")
        }
    }

    func adapterExportWalletPrivateKey(_ json: String) {
        ShareConnect.shared.exportPrivateKey(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "exportWalletPrivateKey")
        }
    }

    func adapterSignIn(withEthereum json: String) {
        ShareConnect.shared.signInWithEthereum(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "signInWithEthereum")
        }
    }

    func adapterVerify(_ json: String) {
        ShareConnect.shared.verify(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.connectSystemName, methodName: "verify")
        }
    }

    func adapterWalletReadyState(_ json: String) -> String {
        let value = ShareConnect.shared.walletReadyState(json)
        return value
    }

    func setWalletConnectV2SupportChainInfos(_ json: String) {
        ShareConnect.shared.setWalletConnectV2SupportChainInfos(json)
    }
}

// MARK: - Particle AA

extension UnityManager {
    func particleAAInitialize(_ json: String) {
        ShareAA.shared.initialize(json)
    }

    func enableAAMode() {
        ShareAA.shared.enableAAMode()
    }

    func disableAAMode() {
        ShareAA.shared.disableAAMode()
    }

    func isAAModeEnable() -> Bool {
        let value = ShareAA.shared.isAAModeEnable()
        return value
    }

    func isDeploy(_ json: String) {
        ShareAA.shared.isDeploy(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.aaSystemName)
        }
    }

    func rpcGetFeeQuotes(_ json: String) {
        ShareAA.shared.rpcGetFeeQuotes(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.aaSystemName)
        }
    }
}

// MARK: - Particle Auth Core

extension UnityManager {
    func authCoreInitialize() {
        ShareAuthCore.shared.initialize()
    }

    func authCoreSetBlindEnable(_ enable: Bool) {
        ShareAuthCore.shared.setBlindEnable(enable)
    }

    func authCoreGetBlindEnable() -> Bool {
        let value = ShareAuthCore.shared.getBlindEnable()
        return value
    }

    func authCoreConnect(_ json: String) {
        ShareAuthCore.shared.connect(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "connect")
        }
    }

    func authCoreConnect(withCode json: String) {
        ShareAuthCore.shared.connectWithCode(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "connectWithCode")
        }
    }

    func authCoreSendPhoneCode(_ json: String) {
        ShareAuthCore.shared.sendPhoneCode(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "sendPhoneCode")
        }
    }

    func authCoreSendEmailCode(_ json: String) {
        ShareAuthCore.shared.sendEmailCode(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "sendEmailCode")
        }
    }

    func authCoreDisconnect() {
        ShareAuthCore.shared.disconnect { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "disconnect")
        }
    }

    func authCoreIsConnected() {
        ShareAuthCore.shared.isConnected { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "isConnected")
        }
    }

    func authCoreGetUserInfo() -> String {
        let value = ShareAuthCore.shared.getUserInfo()
        return value
    }

    func authCoreSwitchChain(_ json: String) {
        ShareAuthCore.shared.switchChain(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "switchChain")
        }
    }

    func authCoreChangeMasterPassword() {
        ShareAuthCore.shared.changeMasterPassword { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "changeMasterPassword")
        }
    }

    func authCoreHasMasterPassword() {
        ShareAuthCore.shared.hasMasterPassword { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "hasMasterPassword")
        }
    }

    func authCoreHasPaymentPassword() {
        ShareAuthCore.shared.hasPaymentPassword { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "hasPaymentPassword")
        }
    }

    func authCoreOpenAccountAndSecurity() {
        ShareAuthCore.shared.openAccountAndSecurity { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "openAccountAndSecurity")
        }
    }

    func authCoreEvmGetAddress() -> String {
        let value = ShareAuthCore.shared.evmGetAddress()
        return value
    }

    func authCoreEvmPersonalSign(_ json: String) {
        ShareAuthCore.shared.evmPersonalSign(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "evmPersonalSign")
        }
    }

    func authCoreEvmPersonalSignUnique(_ json: String) {
        ShareAuthCore.shared.evmPersonalSignUnique(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "evmPersonalSignUnique")
        }
    }

    func authCoreEvmSignTypedData(_ json: String) {
        ShareAuthCore.shared.evmSignTypedData(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "evmSignTypedData")
        }
    }

    func authCoreEvmSignTypedDataUnique(_ json: String) {
        ShareAuthCore.shared.evmSignTypedDataUnique(json) { value in
            UnityInteraction.callBackMessage(value as! String, unityName: UnityManager.authCoreSystemName, methodName: "evmSignTypedDataUnique")
        }
    }

    func authCoreEvmSendTransaction(_ json: String) {
        ShareAuthCore.shared.evmSendTransaction(json) { _ in
            UnityInteraction.callBackMessage(json, unityName: UnityManager.authCoreSystemName, methodName: "evmSendTransaction")
        }
    }

    func authCoreEvmBatchSendTransactions(_ json: String) {
        ShareAuthCore.shared.evmBatchSendTransactions(json) { _ in
            UnityInteraction.callBackMessage(json, unityName: UnityManager.authCoreSystemName, methodName: "evmBatchSendTransactions")
        }
    }

    func authCoreSolanaGetAddress() -> String {
        let value = ShareAuthCore.shared.solanaGetAddress()
        return value
    }

    func authCoreSolanaSignMessage(_ json: String) {
        ShareAuthCore.shared.solanaSignMessage(json) { _ in
            UnityInteraction.callBackMessage(json, unityName: UnityManager.authCoreSystemName, methodName: "solanaSignMessage")
        }
    }

    func authCoreSolanaSignTransaction(_ json: String) {
        ShareAuthCore.shared.solanaSignTransaction(json) { _ in
            UnityInteraction.callBackMessage(json, unityName: UnityManager.authCoreSystemName, methodName: "solanaSignTransaction")
        }
    }

    func authCoreSolanaSignAllTransactions(_ json: String) {
        ShareAuthCore.shared.solanaSignAllTransactions(json) { _ in
            UnityInteraction.callBackMessage(json, unityName: UnityManager.authCoreSystemName, methodName: "solanaSignAllTransactions")
        }
    }

    func authCoreSolanaSignAndSendTransaction(_ json: String) {
        ShareAuthCore.shared.solanaSignAndSendTransaction(json) { _ in
            UnityInteraction.callBackMessage(json, unityName: UnityManager.authCoreSystemName, methodName: "solanaSignAndSendTransaction")
        }
    }
}

// MARK: - Help methods

class UnityInteraction {
    static var ufw: UnityFramework? = {
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
    }()

    class func callBackMessage(_ message: String, unityName: String, methodName: String = #function) {
        var methodName = methodName.replacingOccurrences(of: "\\([\\w\\s:]*\\)", with: "", options: .regularExpression)
        methodName = methodName.prefix(1).uppercased() + methodName.dropFirst() + "CallBack"
        self.ufw?.sendMessageToGO(withName: unityName, functionName: methodName, message: message)
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

struct PNResponseError: Codable {
    let code: Int?
    let message: String?
    let data: String?
}

struct PNStatusModel<T: Codable>: Codable {
    let status: Bool
    let data: T
}

struct PNConnectLoginResult: Codable {
    let message: String
    let signature: String
}

func map2ConnectAdapter(from walletType: WalletType) -> ConnectAdapter? {
    let adapters = ParticleConnect.getAllAdapters().filter {
        $0.walletType == walletType
    }
    let adapter = adapters.first
    return adapter
}

func responseFromError(_ error: Error) -> PNResponseError {
    if let responseError = error as? ParticleNetwork.ResponseError {
        return PNResponseError(code: responseError.code, message: responseError.message ?? "", data: responseError.data)
    } else {
        return PNResponseError(code: nil, message: String(describing: error), data: nil)
    }
}

typealias ShareCallback = (Any) -> Void

class ShareBase {
    static let shared: ShareBase = .init()

    func initialize(_ json: String) {
        let data = JSON(parseJSON: json)

        let chainId = data["chain_id"].intValue
        let chainName = data["chain_name"].stringValue.lowercased()
        let chainType: ChainType = chainName == "solana" ? .solana : .evm
        guard let chainInfo = ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType) else {
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

        let config = ParticleNetworkConfiguration(chainInfo: chainInfo, devEnv: devEnv)
        ParticleNetwork.initialize(config: config)
    }

    func setChainInfo(_ json: String) -> Bool {
        let data = JSON(parseJSON: json)

        let chainId = data["chain_id"].intValue
        let chainName = data["chain_name"].stringValue.lowercased()
        let chainType: ChainType = chainName == "solana" ? .solana : .evm

        guard let chainInfo = ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType) else {
            return false
        }
        ParticleNetwork.setChainInfo(chainInfo)
        return true
    }

    func setAppearance(_ json: String) {
        let appearance = json.lowercased()
        /**
         SYSTEM,
         LIGHT,
         DARK,
         */
        if appearance == "system" {
            ParticleNetwork.setAppearance(UIUserInterfaceStyle.unspecified)
        } else if appearance == "light" {
            ParticleNetwork.setAppearance(UIUserInterfaceStyle.light)
        } else if appearance == "dark" {
            ParticleNetwork.setAppearance(UIUserInterfaceStyle.dark)
        }
    }

    func setSecurityAccountConfig(_ json: String) {
        let data = JSON(parseJSON: json)
        let promptSettingWhenSign = data["prompt_setting_when_sign"].intValue
        let promptMasterPasswordSettingWhenLogin = data["prompt_master_password_setting_when_login"].intValue

        ParticleNetwork.setSecurityAccountConfig(config: .init(promptSettingWhenSign: promptSettingWhenSign, promptMasterPasswordSettingWhenLogin: promptMasterPasswordSettingWhenLogin))
    }

    func setFiatCoin(_ json: String) {
        /*
             USD,
             CNY,
             JPY,
             HKD,
             INR,
             KRW,
         */
        if json.lowercased() == "usd" {
            ParticleNetwork.setFiatCoin(.usd)
        } else if json.lowercased() == "cny" {
            ParticleNetwork.setFiatCoin(.cny)
        } else if json.lowercased() == "jpy" {
            ParticleNetwork.setFiatCoin(.jpy)
        } else if json.lowercased() == "hkd" {
            ParticleNetwork.setFiatCoin(.hkd)
        } else if json.lowercased() == "inr" {
            ParticleNetwork.setFiatCoin(.inr)
        } else if json.lowercased() == "krw" {
            ParticleNetwork.setFiatCoin(.krw)
        }
    }

    func setThemeColor(_ json: String) {
        if let color = UIColor(hex: json) {
            ParticleNetwork.setThemeColor(color)
        }
    }

    func setCustomUIConfigJsonString(_ json: String) {
        do {
            try ParticleNetwork.setCustomUIConfigJsonString(json)
        } catch {
            print("setCustomUIConfigJsonString error \(error)")
        }
    }

    func setUnsupportCountries(_ json: String) {
        let countries = JSON(parseJSON: json).arrayValue.map {
            $0.stringValue.lowercased()
        }
        ParticleNetwork.setCountryFilter {
            if countries.contains($0.iso.lowercased()) {
                return false
            } else {
                return true
            }
        }
    }

    func setLanguage(_ json: String) {
        let languageString = json.lowercased()
        /*
         en,
         zh_hans, zh_cn
         zh_hant, zh_tw
         ja,
         ko
         */

        var language: Language?
        switch languageString {
        case "en":
            language = .en
        case "ja":
            language = .ja
        case "ko":
            language = .ko
        case "zh_hans", "zh_cn":
            language = .zh_Hans
        case "zh_hant", "zh_tw":
            language = .zh_Hant
        default:
            language = nil
        }

        if language != nil {
            ParticleNetwork.setLanguage(language!)
        }
    }

    func getChainInfo() -> String {
        let chainInfo = ParticleNetwork.getChainInfo()

        let jsonString = ["chain_name": chainInfo.name, "chain_id": chainInfo.chainId].jsonString() ?? ""
        return jsonString
    }

    func getLanguage() -> String {
        var language = ""

        switch ParticleNetwork.getLanguage() {
        case .en:
            language = "en"
        case .zh_Hans:
            language = "zh_hans"
        case .zh_Hant:
            language = "zh_hant"
        case .ja:
            language = "ja"
        case .ko:
            language = "ko"
        default:
            language = "en"
        }

        return language
    }
}

class ShareConnect {
    static let shared: ShareConnect = .init()
    var latestPublicAddress: String?
    var latestWalletType: WalletType?

    var walletConnectAdapter: WalletConnectAdapter?

    let bag = DisposeBag()

    func initialize(_ json: String) {
        let data = JSON(parseJSON: json)
        let chainId = data["chain_id"].intValue
        let chainName = data["chain_name"].stringValue.lowercased()
        let chainType: ChainType = chainName == "solana" ? .solana : .evm
        guard let chainInfo = ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType) else {
            return print("initialize error, can't find right chain for \(chainName), chainId \(chainId)")
        }
        let env = data["env"].stringValue.lowercased()
        var devEnv: ParticleNetwork.DevEnvironment = .production
        // DEV, STAGING, PRODUCTION
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
        let dappDescription = data["metadata"]["description"].stringValue
        let redirectUniversalLink = data["metadata"]["redirect"].string

        let dAppIconUrl = URL(string: dAppIconString) != nil ? URL(string: dAppIconString)! : URL(string: "https://connect.particle.network/icons/512.png")!

        let dAppUrl = URL(string: dAppUrlString) != nil ? URL(string: dAppUrlString)! : URL(string: "https://connect.particle.network")!

        let dAppData = DAppMetaData(name: dAppName, icon: dAppIconUrl, url: dAppUrl, description: dappDescription, redirectUniversalLink: redirectUniversalLink)

        let adapters: [ConnectAdapter] = [
            AuthCoreAdapter(),
            MetaMaskConnectAdapter(),
            RainbowConnectAdapter(),
            BitgetConnectAdapter(),
            ImtokenConnectAdapter(),
            TrustConnectAdapter(),
            WalletConnectAdapter(),
            ZerionConnectAdapter(),
            MathConnectAdapter(),
            Inch1ConnectAdapter(),
            ZengoConnectAdapter(),
            AlphaConnectAdapter(),
            OKXConnectAdapter(),
            PhantomConnectAdapter(),
            SolanaConnectAdapter(),
            EVMConnectAdapter()
        ]

        ParticleConnect.initialize(env: devEnv, chainInfo: chainInfo, dAppData: dAppData, adapters: adapters)
    }

    func setWalletConnectV2SupportChainInfos(_ json: String) {
        let chainInfos = JSON(parseJSON: json).arrayValue.compactMap {
            let chainId = $0["chain_id"].intValue
            let chainName = $0["chain_name"].stringValue.lowercased()
            let chainType: ChainType = chainName == "solana" ? .solana : .evm
            return ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType)
        }

        ParticleConnect.setWalletConnectV2SupportChainInfos(chainInfos)
    }

    func setWalletConnectProjectId(_ json: String) {
        let walletConnectProjectId = json
        ParticleConnect.setWalletConnectV2ProjectId(walletConnectProjectId)
    }

    func getAccounts(_ json: String) -> String {
        let walletTypeString = json
        var accounts: [Account] = []
        if let walletType = WalletType.fromString(walletTypeString), let adapter = map2ConnectAdapter(from: walletType) {
            accounts = adapter.getAccounts()
        } else {
            accounts = []
        }

        let statusModel = PNStatusModel(status: true, data: accounts)
        let data = try! JSONEncoder().encode(statusModel)
        let jsonString = String(data: data, encoding: .utf8) ?? ""
        return jsonString
    }

    func connectWithConnectKitConfig(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let connectOptions: [ConnectOption] = data["connectOptions"].arrayValue.compactMap {
            ConnectOption(rawValue: $0.stringValue.lowercased())
        }

        let socialProviders: [EnableSocialProvider] = data["socialProviders"].arrayValue.compactMap {
            EnableSocialProvider(rawValue: $0.stringValue.lowercased())
        }

        let walletProviders: [EnableWalletProvider] = data["walletProviders"].arrayValue.compactMap {
            let label = $0["label"].stringValue.lowercased()
            let name = $0["enableWallet"].stringValue.lowercased()
            return EnableWalletProvider(name: name, state: .init(rawValue: label) ?? .none)
        }

        let layoutJson = data["additionalLayoutOptions"]

        let additionalLayoutOptions: AdditionalLayoutOptions = .init(isCollapseWalletList: layoutJson["isCollapseWalletList"].boolValue, isSplitEmailAndSocial: layoutJson["isSplitEmailAndSocial"].boolValue, isSplitEmailAndPhone: layoutJson["isSplitEmailAndPhone"].boolValue, isHideContinueButton: layoutJson["isHideContinueButton"].boolValue)
        let path = data["logo"].stringValue
        var imagePath: ImagePath?
        if let data = Data(base64Encoded: path), let image = UIImage(data: data) {
            imagePath = ImagePath.local(image)
        } else if !path.isEmpty {
            imagePath = ImagePath.url(path)
        } else {
            imagePath = nil
        }

        let designOptions = DesignOptions(icon: imagePath)
        let config: ConnectKitConfig = .init(connectOptions: connectOptions, socialProviders: socialProviders, walletProviders: walletProviders, additionalLayoutOptions: additionalLayoutOptions, designOptions: designOptions)

        let observable: Single<Account> = ParticleConnectUI.connect(config: config)

        subscribeAndCallback(observable: observable, callback: callback)
    }

    func connect(_ json: String, callback: @escaping ShareCallback) {
        let walletTypeString = JSON(parseJSON: json)["walletType"].stringValue
        let configJson = JSON(parseJSON: json)["particleConnectConfig"]

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        var particleAuthCoreConfig: ParticleAuthCoreConfig?

        if configJson != JSON.null {
            var supportAuthTypeArray: [SupportAuthType] = []
            var account: String?
            var code: String?
            var socialLoginPrompt: SocialLoginPrompt?

            let data = configJson

            let type = ((data["login_type"].string ?? data["loginType"].string) ?? "").lowercased()
            let loginType = LoginType(rawValue: type) ?? .email

            let array = ((data["support_auth_type_values"].array ?? data["supportAuthTypeValues"].array) ?? data["supportAuthType"].array ?? []).map {
                $0.stringValue.lowercased()
            }

            array.forEach { if $0 == "email" {
                supportAuthTypeArray.append(.email)
            } else if $0 == "phone" {
                supportAuthTypeArray.append(.phone)
            } else if $0 == "apple" {
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
            } else if $0 == "twitter" {
                supportAuthTypeArray.append(.twitter)
            }
            }

            account = data["account"].string

            if account != nil, account!.isEmpty {
                account = nil
            }

            code = data["code"].string
            if code != nil, code!.isEmpty {
                code = nil
            }

            let socialLoginPromptString = (data["social_login_prompt"].string ?? data["socialLoginPrompt"].string ?? "").lowercased()

            if socialLoginPromptString == "none" {
                socialLoginPrompt = SocialLoginPrompt.none
            } else if socialLoginPromptString == "consent" {
                socialLoginPrompt = SocialLoginPrompt.consent
            } else if socialLoginPromptString == "selectaccount" || socialLoginPromptString == "select_account" {
                socialLoginPrompt = SocialLoginPrompt.selectAccount
            }

            let config = data["login_page_config"] != JSON.null ? data["login_page_config"] : data["loginPageConfig"]

            var loginPageConfig: LoginPageConfig?
            if config != JSON.null {
                let projectName = config["projectName"].stringValue
                let description = config["description"].stringValue
                let path = config["imagePath"].stringValue
                var imagePath: ImagePath

                if let data = Data(base64Encoded: path), let image = UIImage(data: data) {
                    imagePath = ImagePath.local(image)
                } else {
                    imagePath = ImagePath.url(path)
                }

                loginPageConfig = LoginPageConfig(imagePath: imagePath, projectName: projectName, description: description)
            }

            particleAuthCoreConfig = ParticleAuthCoreConfig(loginType: loginType, supportAuthType: supportAuthTypeArray, account: account, code: code, socialLoginPrompt: socialLoginPrompt, loginPageConfig: loginPageConfig)
        }

        var observable: Single<Account>
        if walletType == .authCore {
            observable = adapter.connect(particleAuthCoreConfig)
        } else {
            observable = adapter.connect(ConnectConfig.none)
        }
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func disconnect(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        subscribeAndCallback(observable: adapter.disconnect(publicAddress: publicAddress), callback: callback)
    }

    func isConnected(_ json: String) -> Bool {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            return false
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            return false
        }
        return adapter.isConnected(publicAddress: publicAddress)
    }

    func signAndSendTransaction(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let transaction = data["transaction"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let mode = data["fee_mode"]["option"].stringValue
        var feeMode: AA.FeeMode = .native
        if mode == "native" {
            feeMode = .native
        } else if mode == "gasless" {
            feeMode = .gasless
        } else if mode == "token" {
            let feeQuoteJson = JSON(data["fee_mode"]["fee_quote"].dictionaryValue)
            let tokenPaymasterAddress = data["fee_mode"]["token_paymaster_address"].stringValue
            let feeQuote = AA.FeeQuote(json: feeQuoteJson, tokenPaymasterAddress: tokenPaymasterAddress)

            feeMode = .token(feeQuote)
        }

        let wholeFeeQuoteData = (try? data["fee_mode"]["whole_fee_quote"].rawData()) ?? Data()
        let wholeFeeQuote = try? JSONDecoder().decode(AA.WholeFeeQuote.self, from: wholeFeeQuoteData)

        let chainInfo = ParticleNetwork.getChainInfo()
        let aaService = ParticleNetwork.getAAService()
        var sendObservable: Single<String>
        if aaService != nil, aaService!.isAAModeEnable() {
            self.latestPublicAddress = publicAddress
            self.latestWalletType = walletType
            sendObservable = aaService!.quickSendTransactions([transaction], feeMode: feeMode, messageSigner: self, wholeFeeQuote: wholeFeeQuote, chainInfo: chainInfo)
        } else {
            sendObservable = adapter.signAndSendTransaction(publicAddress: publicAddress, transaction: transaction, feeMode: feeMode, chainInfo: chainInfo)
        }

        subscribeAndCallback(observable: sendObservable, callback: callback)
    }

    func batchSendTransactions(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let transactions = data["transactions"].arrayValue.map {
            $0.stringValue
        }
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }

        let mode = data["fee_mode"]["option"].stringValue
        var feeMode: AA.FeeMode = .native
        if mode == "native" {
            feeMode = .native
        } else if mode == "gasless" {
            feeMode = .gasless
        } else if mode == "token" {
            let feeQuoteJson = JSON(data["fee_mode"]["fee_quote"].dictionaryValue)
            let tokenPaymasterAddress = data["fee_mode"]["token_paymaster_address"].stringValue
            let feeQuote = AA.FeeQuote(json: feeQuoteJson, tokenPaymasterAddress: tokenPaymasterAddress)

            feeMode = .token(feeQuote)
        }

        let wholeFeeQuoteData = (try? data["fee_mode"]["whole_fee_quote"].rawData()) ?? Data()
        let wholeFeeQuote = try? JSONDecoder().decode(AA.WholeFeeQuote.self, from: wholeFeeQuoteData)
        guard let aaService = ParticleNetwork.getAAService() else {
            print("aa service is not init")
            callback(getErrorJson("aa service is not init"))
            return
        }

        guard aaService.isAAModeEnable() else {
            print("aa service is not enable")
            callback(getErrorJson("aa service is not enable"))
            return
        }

        self.latestPublicAddress = publicAddress
        self.latestWalletType = walletType
        let chainInfo = ParticleNetwork.getChainInfo()
        let sendObservable = aaService.quickSendTransactions(transactions, feeMode: feeMode, messageSigner: self, wholeFeeQuote: wholeFeeQuote, chainInfo: chainInfo)
        subscribeAndCallback(observable: sendObservable, callback: callback)
    }

    func signTransaction(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let transaction = data["transaction"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let observable = adapter.signTransaction(publicAddress: publicAddress, transaction: transaction)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func signAllTransactions(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let transactions = data["transactions"].arrayValue.map {
            $0.stringValue
        }

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let observable = adapter.signAllTransactions(publicAddress: publicAddress, transactions: transactions)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func signMessage(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        var message = data["message"].stringValue

        // solana message should encoded in base58
        if ParticleNetwork.getChainInfo().chainType == .solana {
            message = Base58.encode(message.data(using: .utf8)!)
        }

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed ")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }

        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let observable = adapter.signMessage(publicAddress: publicAddress, message: message)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func signTypedData(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let message = data["message"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let observable = adapter.signTypedData(publicAddress: publicAddress, data: message)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func importPrivateKey(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let privateKey = data["private_key"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        guard walletType == WalletType.evmPrivateKey || walletType == WalletType.solanaPrivateKey else {
            print("walletType \(walletTypeString) is not support import from private key")
            callback(getErrorJson("walletType \(walletTypeString) is not support import from private key"))
            return
        }

        let observable = (adapter as! LocalAdapter).importWalletFromPrivateKey(privateKey)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func importMnemonic(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let mnemonic = data["mnemonic"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        guard walletType == WalletType.evmPrivateKey || walletType == WalletType.solanaPrivateKey else {
            print("walletType \(walletTypeString) is not support import from private key")
            callback(getErrorJson("walletType \(walletTypeString) is not support import from private key"))
            return
        }

        let observable = (adapter as! LocalAdapter).importWalletFromMnemonic(mnemonic)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func exportPrivateKey(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        guard walletType == WalletType.evmPrivateKey || walletType == WalletType.solanaPrivateKey else {
            print("walletType \(walletTypeString) is not support import from private key")
            callback(getErrorJson("walletType \(walletTypeString) is not support import from private key"))
            return
        }

        let observable = (adapter as! LocalAdapter).exportWalletPrivateKey(publicAddress: publicAddress)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func signInWithEthereum(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        let domain = data["domain"].stringValue
        let address = publicAddress
        guard let uri = URL(string: data["uri"].stringValue) else { return }

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let siwe = try! SiweMessage(domain: domain, address: address, uri: uri)
        let observable = adapter.signInWithEthereum(config: siwe, publicAddress: publicAddress).map { sourceMessage, signedMessage in
            PNConnectLoginResult(message: sourceMessage, signature: signedMessage)
        }
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func verify(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let message = data["message"].stringValue
        var signature = data["signature"].stringValue

        if ParticleNetwork.getChainInfo().chainType == .solana {
            signature = Base58.encode(Data(base64Encoded: signature)!)
        }

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            callback(getErrorJson("walletType \(walletTypeString) is not existed"))
            return
        }
        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            callback(getErrorJson("adapter for \(walletTypeString) is not init"))
            return
        }

        let siwe = try! SiweMessage(message)

        let observable = adapter.verify(message: siwe, against: signature)
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func walletReadyState(_ json: String) -> String {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue

        guard let walletType = WalletType.fromString(walletTypeString) else {
            print("walletType \(walletTypeString) is not existed")
            return "unsupported"
        }

        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletTypeString) is not init")
            return "unsupported"
        }

        var str: String
        switch adapter.readyState {
        case .installed:
            str = "installed"
        case .notDetected:
            str = "notDetected"
        case .loadable:
            str = "loadable"
        case .unsupported:
            str = "unsupported"
        case .undetectable:
            str = "undetectable"
        @unknown default:
            str = "undetectable"
        }

        return str
    }

    func connectWalletConnect(callback: @escaping ShareCallback, eventCallback: @escaping ShareCallback) {
        guard let adapter = map2ConnectAdapter(from: .walletConnect) else {
            print("adapter for walletConnect is not init")
            return
        }
        self.walletConnectAdapter = adapter as? WalletConnectAdapter
        Task {
            do {
                let (uri, observable) = try await self.walletConnectAdapter!.getConnectionUrl()

                subscribeAndCallback(observable: observable, callback: callback)
                eventCallback(uri)
            } catch {
                print("error \(error)")
                let response = responseFromError(error)
                let statusModel = PNStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            }
        }
    }
}

extension ShareConnect {
    private func subscribeAndCallback<T: Codable>(observable: Single<T>, callback: @escaping ShareCallback) {
        observable.subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = responseFromError(error)
                let statusModel = PNStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            case .success(let signedMessage):
                let statusModel = PNStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            }
        }.disposed(by: self.bag)
    }
}

extension ShareConnect: MessageSigner {
    public func signMessage(_ message: String, chainInfo: ChainInfo?) -> RxSwift.Single<String> {
        guard let walletType = self.latestWalletType else {
            print("walletType is nil")
            return .error(ParticleNetwork.ResponseError(code: nil, message: "walletType is nil"))
        }

        guard let adapter = map2ConnectAdapter(from: walletType) else {
            print("adapter for \(walletType) is not init")
            return .error(ParticleNetwork.ResponseError(code: nil, message: "adapter for \(walletType) is not init"))
        }
        return adapter.signMessage(publicAddress: self.getEoaAddress(), message: message, chainInfo: chainInfo)
    }

    public func getEoaAddress() -> String {
        return self.latestPublicAddress ?? ""
    }
}

extension ShareConnect {
    private func getErrorJson(_ message: String) -> String {
        let response = PNResponseError(code: nil, message: message, data: nil)
        let statusModel = PNStatusModel(status: false, data: response)
        let data = try! JSONEncoder().encode(statusModel)
        guard let json = String(data: data, encoding: .utf8) else { return "" }
        return json
    }
}

class ShareAuthCore {
    static let shared: ShareAuthCore = .init()

    let auth = Auth()

    let bag = DisposeBag()

    func initialize() {
        ConnectManager.setMoreAdapters([AuthCoreAdapter()])
    }

    func switchChain(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)

        let chainId = data["chain_id"].intValue
        let chainName = data["chain_name"].stringValue.lowercased()
        let chainType: ChainType = chainName == "solana" ? .solana : .evm
        guard let chainInfo = ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType) else {
            callback(false)
            return
        }

        Task {
            do {
                let flag = try await auth.switchChain(chainInfo: chainInfo)
                callback(flag)
            } catch {
                callback(false)
            }
        }
    }

    func connect(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)

        let type = ((data["login_type"].string ?? data["loginType"].string) ?? "").lowercased()

        let account = data["account"].string
        let supportAuthType = (data["support_auth_type_values"].array ?? data["supportAuthTypeValues"].array) ?? []

        let socialLoginPromptString = (data["social_login_prompt"].string ?? data["socialLoginPrompt"].string ?? "").lowercased()

        var socialLoginPrompt: SocialLoginPrompt?
        if socialLoginPromptString == "none" {
            socialLoginPrompt = SocialLoginPrompt.none
        } else if socialLoginPromptString == "consent" {
            socialLoginPrompt = SocialLoginPrompt.consent
        } else if socialLoginPromptString == "select_account" || socialLoginPromptString == "selectaccount" {
            socialLoginPrompt = SocialLoginPrompt.selectAccount
        }

        let loginType = LoginType(rawValue: type) ?? .email

        let array = supportAuthType.map {
            $0.stringValue.lowercased()
        }

        var supportAuthTypeArray: [SupportAuthType] = []

        array.forEach {
            if $0 == "email" {
                supportAuthTypeArray.append(.email)
            } else if $0 == "phone" {
                supportAuthTypeArray.append(.phone)
            } else if $0 == "apple" {
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
            } else if $0 == "twitter" {
                supportAuthTypeArray.append(.twitter)
            }
        }

        var acc = account
        if acc != nil, acc!.isEmpty {
            acc = nil
        }

        let config = data["login_page_config"] != JSON.null ? data["login_page_config"] : data["loginPageConfig"]

        var loginPageConfig: LoginPageConfig?
        if config != JSON.null {
            let projectName = config["projectName"].stringValue
            let description = config["description"].stringValue
            let path = config["imagePath"].stringValue
            var imagePath: ImagePath

            if let data = Data(base64Encoded: path), let image = UIImage(data: data) {
                imagePath = ImagePath.local(image)
            } else {
                imagePath = ImagePath.url(path)
            }

            loginPageConfig = LoginPageConfig(imagePath: imagePath, projectName: projectName, description: description)
        }

        let observable = Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.presentLoginPage(type: loginType, account: account, supportAuthType: supportAuthTypeArray, socialLoginPrompt: socialLoginPrompt, config: loginPageConfig)
        }.map { userInfo in
            let userInfoJsonString = userInfo.jsonStringFullSnake()
            let newUserInfo = JSON(parseJSON: userInfoJsonString)
            return newUserInfo
        }

        subscribeAndCallback(observable: observable, callback: callback)
    }

    func sendPhoneCode(_ json: String, callback: @escaping ShareCallback) {
        let phone = json
        let observable = Single<Bool>.fromAsync { [weak self] in
            guard let self = self else { throw ParticleNetwork.ResponseError(code: nil, message: "self is nil") }
            return try await self.auth.sendPhoneCode(phone: phone)
        }
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func sendEmailCode(_ json: String, callback: @escaping ShareCallback) {
        let email = json

        let observable = Single<Bool>.fromAsync { [weak self] in
            guard let self = self else { throw ParticleNetwork.ResponseError(code: nil, message: "self is nil") }
            return try await self.auth.sendEmailCode(email: email)
        }
        subscribeAndCallback(observable: observable, callback: callback)
    }

    func connectWithCode(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let email = data["email"].stringValue
        let phone = data["phone"].stringValue
        let code = data["code"].stringValue
        if !email.isEmpty {
            let observable = Single<UserInfo>.fromAsync { [weak self] in
                guard let self = self else { throw ParticleNetwork.ResponseError(code: nil, message: "self is nil") }
                return try await self.auth.connect(type: LoginType.email, account: email, code: code)
            }.map { userInfo in
                let userInfoJsonString = userInfo.jsonStringFullSnake()
                let newUserInfo = JSON(parseJSON: userInfoJsonString)
                return newUserInfo
            }
            subscribeAndCallback(observable: observable, callback: callback)
        } else {
            let observable = Single<UserInfo>.fromAsync { [weak self] in
                guard let self = self else { throw ParticleNetwork.ResponseError(code: nil, message: "self is nil") }
                return try await self.auth.connect(type: LoginType.phone, account: phone, code: code)
            }.map { userInfo in
                let userInfoJsonString = userInfo.jsonStringFullSnake()
                let newUserInfo = JSON(parseJSON: userInfoJsonString)
                return newUserInfo
            }

            subscribeAndCallback(observable: observable, callback: callback)
        }
    }

    func disconnect(_ callback: @escaping ShareCallback) {
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.disconnect()
        }, callback: callback)
    }

    func isConnected(_ callback: @escaping ShareCallback) {
        subscribeAndCallback(observable: Single<Bool>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.isConnected()
        }, callback: callback)
    }

    func solanaSignMessage(_ json: String, callback: @escaping ShareCallback) {
        let message = json
        let serializedMessage = Base58.encode(message.data(using: .utf8)!)

        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.solana.signMessage(serializedMessage, chainInfo: chainInfo)
        }, callback: callback)
    }

    func solanaSignTransaction(_ json: String, callback: @escaping ShareCallback) {
        let transaction = json
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.solana.signTransaction(transaction, chainInfo: chainInfo)
        }, callback: callback)
    }

    func solanaSignAllTransactions(_ json: String, callback: @escaping ShareCallback) {
        let transactions = JSON(parseJSON: json).arrayValue.map { $0.stringValue }
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<[String]>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.solana.signAllTransactions(transactions, chainInfo: chainInfo)
        }, callback: callback)
    }

    func solanaSignAndSendTransaction(_ json: String, callback: @escaping ShareCallback) {
        let transaction = json
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.solana.signAndSendTransaction(transaction, chainInfo: chainInfo)
        }, callback: callback)
    }

    func evmPersonalSign(_ message: String, callback: @escaping ShareCallback) {
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.evm.personalSign(message, chainInfo: chainInfo)
        }, callback: callback)
    }

    func evmPersonalSignUnique(_ message: String, callback: @escaping ShareCallback) {
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.evm.personalSignUnique(message, chainInfo: chainInfo)
        }, callback: callback)
    }

    func evmSignTypedData(_ message: String, callback: @escaping ShareCallback) {
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.evm.signTypedData(message, chainInfo: chainInfo)
        }, callback: callback)
    }

    func evmSignTypedDataUnique(_ message: String, callback: @escaping ShareCallback) {
        let chainInfo = ParticleNetwork.getChainInfo()
        subscribeAndCallback(observable: Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.evm.signTypedDataUnique(message, chainInfo: chainInfo)
        }, callback: callback)
    }

    func evmSendTransaction(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let transaction = data["transaction"].stringValue
        let mode = data["fee_mode"]["option"].stringValue
        var feeMode: AA.FeeMode = .native
        if mode == "native" {
            feeMode = .native
        } else if mode == "gasless" {
            feeMode = .gasless
        } else if mode == "token" {
            let feeQuoteJson = JSON(data["fee_mode"]["fee_quote"].dictionaryValue)
            let tokenPaymasterAddress = data["fee_mode"]["token_paymaster_address"].stringValue
            let feeQuote = AA.FeeQuote(json: feeQuoteJson, tokenPaymasterAddress: tokenPaymasterAddress)

            feeMode = .token(feeQuote)
        }

        let wholeFeeQuoteData = (try? data["fee_mode"]["whole_fee_quote"].rawData()) ?? Data()
        let wholeFeeQuote = try? JSONDecoder().decode(AA.WholeFeeQuote.self, from: wholeFeeQuoteData)

        let aaService = ParticleNetwork.getAAService()
        var sendObservable: Single<String>
        let chainInfo = ParticleNetwork.getChainInfo()
        if aaService != nil, aaService!.isAAModeEnable() {
            sendObservable = aaService!.quickSendTransactions([transaction], feeMode: feeMode, messageSigner: self, wholeFeeQuote: wholeFeeQuote, chainInfo: chainInfo)
        } else {
            sendObservable = Single<String>.fromAsync { [weak self] in
                guard let self = self else { throw ParticleNetwork.ResponseError(code: nil, message: "self is nil") }
                return try await self.auth.evm.sendTransaction(transaction, feeMode: feeMode, chainInfo: chainInfo)
            }
        }

        subscribeAndCallback(observable: sendObservable, callback: callback)
    }

    func evmBatchSendTransactions(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let transactions = data["transactions"].arrayValue.map {
            $0.stringValue
        }
        let mode = data["fee_mode"]["option"].stringValue
        var feeMode: AA.FeeMode = .native
        if mode == "native" {
            feeMode = .native
        } else if mode == "gasless" {
            feeMode = .gasless
        } else if mode == "token" {
            let feeQuoteJson = JSON(data["fee_mode"]["fee_quote"].dictionaryValue)
            let tokenPaymasterAddress = data["fee_mode"]["token_paymaster_address"].stringValue
            let feeQuote = AA.FeeQuote(json: feeQuoteJson, tokenPaymasterAddress: tokenPaymasterAddress)

            feeMode = .token(feeQuote)
        }

        let wholeFeeQuoteData = (try? data["fee_mode"]["whole_fee_quote"].rawData()) ?? Data()
        let wholeFeeQuote = try? JSONDecoder().decode(AA.WholeFeeQuote.self, from: wholeFeeQuoteData)

        guard let aaService = ParticleNetwork.getAAService() else {
            print("aa service is not init")
            return
        }

        guard aaService.isAAModeEnable() else {
            print("aa service is not enable")
            return
        }

        let chainInfo = ParticleNetwork.getChainInfo()
        let sendObservable: Single<String> = aaService.quickSendTransactions(transactions, feeMode: feeMode, messageSigner: self, wholeFeeQuote: wholeFeeQuote, chainInfo: chainInfo)

        subscribeAndCallback(observable: sendObservable, callback: callback)
    }

    func solanaGetAddress() -> String {
        let address = self.auth.solana.getAddress()
        return address ?? ""
    }

    func evmGetAddress() -> String {
        let address = self.auth.evm.getAddress()
        return address ?? ""
    }

    func getUserInfo() -> String {
        guard let userInfo = auth.getUserInfo() else {
            return ""
        }
        let userInfoJsonString = userInfo.jsonStringFullSnake()
        let newUserInfo = JSON(parseJSON: userInfoJsonString)

        let data = try! JSONEncoder().encode(newUserInfo)
        let json = String(data: data, encoding: .utf8)!
        return json
    }

    func openAccountAndSecurity(_ callback: @escaping ShareCallback) {
        let observable = Single<Void>.fromAsync {
            [weak self] in
                guard let self = self else {
                    throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
                }
                try self.auth.openAccountAndSecurity()
        }.map {
            ""
        }

        subscribeAndCallback(observable: observable, callback: callback)
    }

    func hasPaymentPassword(_ callback: @escaping ShareCallback) {
        subscribeAndCallback(observable: Single<Bool>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try self.auth.hasPaymentPassword()
        }, callback: callback)
    }

    func hasMasterPassword(_ callback: @escaping ShareCallback) {
        subscribeAndCallback(observable: Single<Bool>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try self.auth.hasMasterPassword()
        }, callback: callback)
    }

    func changeMasterPassword(_ callback: @escaping ShareCallback) {
        subscribeAndCallback(observable: Single<Bool>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.changeMasterPassword()
        }, callback: callback)
    }

    func setBlindEnable(_ enable: Bool) {
        Auth.setBlindEnable(enable)
    }

    func getBlindEnable() -> Bool {
        let enable = Auth.getBlindEnable()
        return enable
    }
}

extension ShareAuthCore {
    private func subscribeAndCallback<T: Codable>(observable: Single<T>, callback: @escaping ShareCallback) {
        observable.subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = responseFromError(error)
                let statusModel = PNStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            case .success(let signedMessage):
                let statusModel = PNStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            }
        }.disposed(by: self.bag)
    }
}

extension ShareAuthCore: MessageSigner {
    public func signMessage(_ message: String, chainInfo: ChainInfo?) -> RxSwift.Single<String> {
        return Single<String>.fromAsync { [weak self] in
            guard let self = self else {
                throw ParticleNetwork.ResponseError(code: nil, message: "self is nil")
            }
            return try await self.auth.evm.personalSign(message, chainInfo: chainInfo)
        }
    }

    public func getEoaAddress() -> String {
        return self.auth.evm.getAddress() ?? ""
    }
}

extension Single {
    static func fromAsync<T>(_ fn: @escaping () async throws -> T) -> Single<T> {
        .create { observer in
            let task = Task {
                do { try await observer(.success(fn())) }
                catch { observer(.failure(error)) }
            }
            return Disposables.create { task.cancel() }
        }.observe(on: MainScheduler.instance)
    }
}

class ShareAA {
    static let shared: ShareAA = .init()

    let bag = DisposeBag()

    func initialize(_ json: String) {
        let data = JSON(parseJSON: json)

        let name = data["name"].stringValue.uppercased()
        let version = data["version"].stringValue.lowercased()

        let accountName = AA.AccountName(version: version, name: name)
        let all: [AA.AccountName] = AA.AccountName.allCases
        if all.contains(accountName) {
            AAService.initialize(name: accountName)
        } else {
            print("config aa error, wrong version and name")
        }
    }

    func isDeploy(_ json: String, callback: @escaping ShareCallback) {
        let eoaAddress = json
        let chainInfo = ParticleNetwork.getChainInfo()
        guard let aaService = ParticleNetwork.getAAService() else {
            let response = PNResponseError(code: nil, message: "aa service is not init", data: nil)
            let statusModel = PNStatusModel(status: false, data: response)
            let data = try! JSONEncoder().encode(statusModel)
            guard let json = String(data: data, encoding: .utf8) else { return }
            callback(json)
            return
        }
        subscribeAndCallback(observable: aaService.isDeploy(eoaAddress: eoaAddress, chainInfo: chainInfo), callback: callback)
    }

    func isAAModeEnable() -> Bool {
        return ParticleNetwork.getAAService()?.isAAModeEnable() ?? false
    }

    func enableAAMode() {
        ParticleNetwork.getAAService()?.enableAAMode()
    }

    func disableAAMode() {
        ParticleNetwork.getAAService()?.disableAAMode()
    }

    func rpcGetFeeQuotes(_ json: String, callback: @escaping ShareCallback) {
        let data = JSON(parseJSON: json)
        let eoaAddress = data["eoa_address"].stringValue
        let transactions = data["transactions"].arrayValue.map {
            $0.stringValue
        }
        let chainInfo = ParticleNetwork.getChainInfo()
        guard let aaService = ParticleNetwork.getAAService() else {
            let response = PNResponseError(code: nil, message: "aa service is not init", data: nil)
            let statusModel = PNStatusModel(status: false, data: response)
            let data = try! JSONEncoder().encode(statusModel)
            guard let json = String(data: data, encoding: .utf8) else { return }
            callback(json)
            return
        }

        subscribeAndCallback(observable: aaService.rpcGetFeeQuotes(eoaAddress: eoaAddress, transactions: transactions, chainInfo: chainInfo), callback: callback)
    }
}

extension ShareAA {
    private func subscribeAndCallback<T: Codable>(observable: Single<T>, callback: @escaping ShareCallback) {
        observable.subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = responseFromError(error)
                let statusModel = PNStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            case .success(let signedMessage):
                let statusModel = PNStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            }
        }.disposed(by: self.bag)
    }
}

class ShareWallet {
    static let shared: ShareWallet = .init()
    let bag = DisposeBag()

    func navigatorWallet(_ display: Int) {
        if display != 0 {
            PNRouter.navigatorWallet(display: .nft)
        } else {
            PNRouter.navigatorWallet(display: .token)
        }
    }

    func navigatorTokenReceive(_ json: String) {
        let tokenAddress = json
        PNRouter.navigatorTokenReceive(tokenReceiveConfig: tokenAddress.isEmpty ? nil : TokenReceiveConfig(tokenAddress: json))
    }

    func navigatorTokenSend(_ json: String) {
        let data = JSON(parseJSON: json)
        let tokenAddress = data["token_address"].stringValue
        let toAddress = data["to_address"].stringValue
        let amount = data["amount"].stringValue
        let config = TokenSendConfig(tokenAddress: tokenAddress.isEmpty ? nil : amount, toAddress: toAddress.isEmpty ? nil : toAddress, amountString: amount.isEmpty ? nil : amount)

        PNRouter.navigatorTokenSend(tokenSendConfig: config)
    }

    func navigatorTokenTransactionRecords(_ json: String) {
        let tokenAddress = json

        PNRouter.navigatorTokenTransactionRecords(tokenTransactionRecordsConfig: tokenAddress.isEmpty ? nil : TokenTransactionRecordsConfig(tokenAddress: json))
    }

    func navigatorNFTSend(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["mint"].stringValue
        let tokenId = data["token_id"].stringValue
        let toAddress = data["receiver_address"].stringValue
        let amount = data["amount"].stringValue
        let config = NFTSendConfig(address: address, toAddress: toAddress.isEmpty ? nil : toAddress, tokenId: tokenId, amountString: amount.isEmpty ? nil : amount)
        PNRouter.navigatorNFTSend(nftSendConfig: config)
    }

    func navigatorNFTDetails(_ json: String) {
        let data = JSON(parseJSON: json)
        let address = data["mint"].stringValue
        let tokenId = data["token_id"].stringValue
        let config = NFTDetailsConfig(address: address, tokenId: tokenId)
        PNRouter.navigatorNFTDetails(nftDetailsConfig: config)
    }

    func navigatorBuyCrypto(_ json: String) {
        let data = JSON(parseJSON: json)
        let walletAddress = data["wallet_address"].string
        let chainId = data["chain_info"]["chain_id"].intValue
        let chainName = data["chain_info"]["chain_name"].stringValue.lowercased()
        let chainType: ChainType = chainName == "solana" ? .solana : .evm

        let chainInfo = ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType) ?? ParticleNetwork.getChainInfo()

        let fiatCoin = data["fiat_coin"].string
        let fiatAmt = data["fiat_amt"].int
        let cryptoCoin = data["crypto_coin"].string
        let fixCryptoCoin = data["fix_crypto_coin"].boolValue
        let fixFiatAmt = data["fix_fiat_amt"].boolValue
        let fixFiatCoin = data["fix_fiat_coin"].boolValue
        let theme = data["theme"].stringValue.lowercased()
        let language = self.getLanguage(from: data["language"].stringValue.lowercased())

        var buyConfig = BuyCryptoConfig()
        buyConfig.network = chainInfo
        buyConfig.walletAddress = walletAddress
        buyConfig.cryptoCoin = cryptoCoin ?? chainInfo.nativeToken.symbol
        buyConfig.fiatAmt = fiatAmt
        if fiatCoin != nil {
            buyConfig.fiatCoin = fiatCoin!
        }
        buyConfig.fixCryptoCoin = fixCryptoCoin
        buyConfig.fixFiatCoin = fixFiatCoin
        buyConfig.fixFiatAmt = fixFiatAmt
        buyConfig.theme = theme
        buyConfig.language = language?.webString ?? Language.en.webString

        let modalStyleString = data["modal_style"].stringValue.lowercased()
        var modalStyle: ParticleGUIModalStyle
        if modalStyleString == "fullscreen" {
            modalStyle = .fullScreen
        } else {
            modalStyle = .pageSheet
        }

        PNRouter.navigatorBuy(buyCryptoConfig: buyConfig, modalStyle: modalStyle)
    }

    func navigatorSwap(_ json: String) {
        let data = JSON(parseJSON: json)
        let fromTokenAddress = data["from_token_address"].string
        let toTokenAddress = data["to_token_address"].string
        let amount = data["amount"].string
        let config = SwapConfig(fromTokenAddress: fromTokenAddress, toTokenAddress: toTokenAddress, fromTokenAmountString: amount)

        PNRouter.navigatorSwap(swapConfig: config)
    }

    func navigatorDappBrowser(_ json: String) {
        let data = JSON(parseJSON: json)
        let urlStr = data["url"].stringValue
        if let url = URL(string: urlStr) {
            PNRouter.navigatorDappBrowser(url: url)
        } else {
            PNRouter.navigatorDappBrowser(url: nil)
        }
    }

    func setShowTestNetwork(_ isShow: Bool) {
        ParticleWalletGUI.setShowTestNetwork(isShow)
    }

    func setShowSmartAccountSetting(_ isShow: Bool) {
        ParticleWalletGUI.setShowSmartAccountSetting(isShow)
    }

    func setShowManageWallet(_ isShow: Bool) {
        ParticleWalletGUI.setShowManageWallet(isShow)
    }

    func setSupportChain(_ json: String) {
        let chains = JSON(parseJSON: json).arrayValue.compactMap {
            let chainId = $0["chain_id"].intValue
            let chainName = $0["chain_name"].stringValue.lowercased()
            let chainType: ChainType = chainName == "solana" ? .solana : .evm
            return ParticleNetwork.searchChainInfo(by: chainId, chainType: chainType)
        }
        ParticleWalletGUI.setSupportChain(Set(chains))
    }

    func setPayDisabled(_ disabled: Bool) {
        ParticleWalletGUI.setPayDisabled(disabled)
    }

    func getPayDisabled() -> Bool {
        return ParticleWalletGUI.getPayDisabled()
    }

    func setSwapDisabled(_ disabled: Bool) {
        ParticleWalletGUI.setSwapDisabled(disabled)
    }

    func getSwapDisabled() -> Bool {
        return ParticleWalletGUI.getSwapDisabled()
    }

    func setBridgeDisabled(_ disabled: Bool) {
        ParticleWalletGUI.setBridgeDisabled(disabled)
    }

    func getBridgeDisabled() -> Bool {
        return ParticleWalletGUI.getBridgeDisabled()
    }

    func switchWallet(_ json: String) -> Bool {
        let data = JSON(parseJSON: json)
        let walletTypeString = data["wallet_type"].stringValue
        let publicAddress = data["public_address"].stringValue
        if let walletType = WalletType.fromString(walletTypeString) {
            let result = ParticleWalletGUI.switchWallet(walletType: walletType, publicAddress: publicAddress)
            return result
        } else {
            print("walletType \(walletTypeString) is not existed")
            return false
        }
    }

    private func getLanguage(from json: String) -> Language? {
        let languageString = json.lowercased()
        /*
         en,
         zh_hans, zh_cn
         zh_hant, zh_tw
         ja,
         ko
         */

        var language: Language?
        switch languageString {
        case "en":
            language = .en
        case "ja":
            language = .ja
        case "ko":
            language = .ko
        case "zh_hans", "zh_cn":
            language = .zh_Hans
        case "zh_hant", "zh_tw":
            language = .zh_Hant
        default:
            language = nil
        }

        return language
    }

    func setSupportDappBrowser(_ enable: Bool) {
        ParticleWalletGUI.setSupportDappBrowser(enable)
    }

    func setShowLanguageSetting(_ isShow: Bool) {
        ParticleWalletGUI.setShowLanguageSetting(isShow)
    }

    func setShowAppearanceSetting(_ isShow: Bool) {
        ParticleWalletGUI.setShowAppearanceSetting(isShow)
    }

    func setSupportAddToken(_ isShow: Bool) {
        ParticleWalletGUI.setSupportAddToken(isShow)
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

    func setPriorityTokenAddresses(_ json: String) {
        let data = JSON(parseJSON: json)
        let tokenAddresses = data.arrayValue.map {
            $0.stringValue
        }
        ParticleWalletGUI.setPriorityTokenAddresses(tokenAddresses)
    }

    func setPriorityNFTContractAddresses(_ json: String) {
        let data = JSON(parseJSON: json)
        let nftContractAddresses = data.arrayValue.map {
            $0.stringValue
        }
        ParticleWalletGUI.setPriorityNFTContractAddresses(nftContractAddresses)
    }

    func setSupportWalletConnect(_ enable: Bool) {
        ParticleWalletGUI.setSupportWalletConnect(enable)
    }

    func initializeWalletMetaData(_ json: String) {
        let data = JSON(parseJSON: json)

        let walletName = data["name"].stringValue
        let walletIconString = data["icon"].stringValue
        let walletUrlString = data["url"].stringValue
        let walletDescription = data["description"].stringValue

        let walletIconUrl = URL(string: walletIconString) != nil ? URL(string: walletIconString)! : URL(string: "https://connect.particle.network/icons/512.png")!

        let walletUrl = URL(string: walletUrlString) != nil ? URL(string: walletUrlString)! : URL(string: "https://connect.particle.network")!

        ParticleWalletConnect.initialize(.init(name: walletName, icon: walletIconUrl, url: walletUrl, description: walletDescription, redirectUniversalLink: nil))
        ParticleWalletGUI.setAdapters(ParticleConnect.getAllAdapters())
    }

    func setCustomWalletName(_ json: String) {
        let data = JSON(parseJSON: json)

        let name = data["name"].stringValue
        let icon = data["icon"].stringValue

        ConnectManager.setCustomWalletName(walletType: .authCore, name: .init(name: name, icon: icon))
    }

    func setCustomLocalizable(_ json: String) {
        let data = JSON(parseJSON: json).dictionaryValue

        var localizables: [Language: [String: String]] = [:]

        for (key, value) in data {
            let language = self.getLanguage(from: key.lowercased())
            if language == nil {
                continue
            }

            let itemLocalizables = value.dictionaryValue.mapValues { json in
                json.stringValue
            }
            localizables[language!] = itemLocalizables
        }

        ParticleWalletGUI.setCustomLocalizable(localizables)
    }

    func setWalletConnectProjectId(_ json: String) {
        let walletConnectProjectId = json
        ParticleWalletConnect.setWalletConnectV2ProjectId(walletConnectProjectId)
    }
}

extension ShareWallet {
    private func subscribeAndCallback<T: Codable>(observable: Single<T>, callback: @escaping ShareCallback) {
        observable.subscribe { [weak self] result in
            guard let self = self else { return }
            switch result {
            case .failure(let error):
                let response = responseFromError(error)
                let statusModel = PNStatusModel(status: false, data: response)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            case .success(let signedMessage):
                let statusModel = PNStatusModel(status: true, data: signedMessage)
                let data = try! JSONEncoder().encode(statusModel)
                guard let json = String(data: data, encoding: .utf8) else { return }
                callback(json)
            }
        }.disposed(by: self.bag)
    }
}

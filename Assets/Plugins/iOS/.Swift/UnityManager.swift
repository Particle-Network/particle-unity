//
//  UnityManager.swift
//  UnityFramework
//
//  Created by link on 2022/6/30.
//

import Foundation
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
        ufw?.sendMessageToGO(withName: unityName, functionName: methodName, message: message)
    }
}

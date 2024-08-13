#import <Foundation/Foundation.h>
#import "NativeCallProxy.h"

@implementation FrameworkLibAPI

id<NativeCallsProtocol> api = NULL;
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi
{
    api = aApi;
}

@end

// Particle Network Base
extern "C" {
    char* getChainInfo() {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api getChainInfo] UTF8String]);
    }

    bool setChainInfo(const char* chainInfo) {
        return [api setChainInfo:[NSString stringWithUTF8String: chainInfo]];
    }

    void initialize(const char* json) {
        [api initialize:[NSString stringWithUTF8String: json]];
    }

    long getDevEnv() {
        return [api getDevEnv];
    }

    void setLanguage(const char* json) {
        [api setLanguage:[NSString stringWithUTF8String: json]];
    }

    void setFiatCoin(const char* json) {
        [api setFiatCoin:[NSString stringWithUTF8String: json]];
    }
    
    void setAppearance(const char* json) {
        [api setAppearance:[NSString stringWithUTF8String: json]];
    }

    void setSecurityAccountConfig(const char* json) {
        [api setSecurityAccountConfig:[NSString stringWithUTF8String: json]];
    }
    
    void setCustomUIConfigJsonString(const char* json) {
        [api setCustomUIConfigJsonString:[NSString stringWithUTF8String: json]];
    }
}

// Particle Wallet GUI
extern "C" {
    void setPayDisabled(bool enable) {
        [api setPayDisabled:enable];
    }

    bool getPayDisabled() {
        return [api getPayDisabled];
    }
    
    void setSwapDisabled(bool enable) {
            [api setSwapDisabled:enable];
        }
    
    bool getSwapDisabled() {
        return [api getSwapDisabled];
    }

    void navigatorWallet(int display) {
        [api navigatorWallet:display];
    }

    void navigatorTokenReceive(const char* json) {
        [api navigatorTokenReceive:[NSString stringWithUTF8String: json]];
    }

    void navigatorDappBrowser(const char* json) {
        [api navigatorDappBrowser:[NSString stringWithUTF8String: json]];
    }

    void navigatorTokenSend(const char* json) {
        [api navigatorTokenSend:[NSString stringWithUTF8String: json]];
    }

    void navigatorTokenTransactionRecords(const char* json) {
        [api navigatorTokenTransactionRecords:[NSString stringWithUTF8String: json]];
    }

    void navigatorNFTSend(const char* json) {
        [api navigatorNFTSend:[NSString stringWithUTF8String: json]];
    }

    void navigatorNFTDetails(const char* json) {
        [api navigatorNFTDetails:[NSString stringWithUTF8String: json]];
    }
    
    void navigatorBuyCrypto(const char* json) {
        [api navigatorBuyCrypto:[NSString stringWithUTF8String: json]];
    }
    
    void navigatorSwap(const char* json) {
        [api navigatorSwap:[NSString stringWithUTF8String: json]];
    }
    
    void setShowTestNetwork(bool show) {
        [api setShowTestNetwork:show];
    }
    
    void setShowManageWallet(bool show) {
        [api setShowManageWallet:show];
    }
    
    void setSupportChain(const char* json) {
        [api setSupportChain:[NSString stringWithUTF8String: json]];
    }
    
    void switchWallet(const char* json) {
        [api switchWallet:[NSString stringWithUTF8String: json]];
    }

    void setShowLanguageSetting(bool show) {
        [api setShowLanguageSetting:show];
    }

    void setShowSmartAccountSetting(bool show) {
        [api setShowSmartAccountSetting:show];
    }

    void setShowAppearanceSetting(bool show) {
        [api setShowAppearanceSetting:show];
    }

    void setSupportWalletConnect(bool enable) {
        [api setSupportWalletConnect: enable];
    }
    
    void setSupportDappBrowser(bool enable) {
        [api setSupportDappBrowser: enable];
    }
    
    void particleWalletConnectInitialize(const char* json) {
        [api particleWalletConnectInitialize:[NSString stringWithUTF8String: json]];
    }

    void setSupportAddToken(bool enable) {
        [api setSupportWalletConnect: enable];
    }

    void setDisplayTokenAddresses(const char* json) {
        [api setDisplayTokenAddresses:[NSString stringWithUTF8String: json]];
    }

    void setDisplayNFTContractAddresses(const char* json) {
        [api setDisplayNFTContractAddresses:[NSString stringWithUTF8String: json]];
    }

    void setPriorityTokenAddresses(const char* json) {
        [api setPriorityTokenAddresses:[NSString stringWithUTF8String: json]];
    }

    void setPriorityNFTContractAddresses(const char* json) {
        [api setPriorityNFTContractAddresses:[NSString stringWithUTF8String: json]];
    }

    void setCustomWalletName(const char* json) {
        [api setCustomWalletName:[NSString stringWithUTF8String: json]];
    }

    void setCustomLocalizable(const char* json) {
        [api setCustomLocalizable:[NSString stringWithUTF8String: json]];
    }

}

// Particle Connect
extern "C" {
    void particleConnectInitialize(const char* json) {
        [api particleConnectInitialize:[NSString stringWithUTF8String: json]];
    }
    void setWalletConnectV2SupportChainInfos(const char* json) {
         [api setWalletConnectV2SupportChainInfos:[NSString stringWithUTF8String: json]];
    }
}

// Connect Service
extern "C" {
     char* adapterGetAccounts(const char* json) {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api adapterGetAccounts:[NSString stringWithUTF8String: json]] UTF8String]);
     }
     
     void adapterConnect(const char* json, const char* configJson) {
        [api adapterConnect:[NSString stringWithUTF8String: json] configJson:[NSString stringWithUTF8String: configJson]];
     }

    void adapterDisconnect(const char* json) {
        [api adapterDisconnect:[NSString stringWithUTF8String: json]];
    }
    
    bool adapterIsConnected(const char* json) {
        return [api adapterIsConnected:[NSString stringWithUTF8String: json]];
    }
    
    void adapterSignAndSendTransaction(const char* json) {
        [api adapterSignAndSendTransaction:[NSString stringWithUTF8String: json]];
    }
    void adapterSignTransaction(const char* json) {
        [api adapterSignTransaction:[NSString stringWithUTF8String: json]];
    }
    void adapterSignAllTransactions(const char* json) {
        [api adapterSignAllTransactions:[NSString stringWithUTF8String: json]];
    }

    void adapterBatchSendTransactions(const char* json) {
        [api adapterBatchSendTransactions:[NSString stringWithUTF8String: json]];
    }

    void adapterSignMessage(const char* json) {
        [api adapterSignMessage:[NSString stringWithUTF8String: json]];
    }
    
    void adapterSignTypedData(const char* json) {
        [api adapterSignTypedData:[NSString stringWithUTF8String: json]];
    }
    
    void adapterImportWalletFromPrivateKey(const char* json) {
        [api adapterImportWalletFromPrivateKey:[NSString stringWithUTF8String: json]];
    }
    
    void adapterImportWalletFromMnemonic(const char* json) {
        [api adapterImportWalletFromMnemonic:[NSString stringWithUTF8String: json]];
    }
    void adapterExportWalletPrivateKey(const char* json) {
        [api adapterExportWalletPrivateKey:[NSString stringWithUTF8String: json]];
    }

    void adapterLogin(const char* json) {
        [api adapterLogin:[NSString stringWithUTF8String: json]];
    }
    void adapterVerify(const char* json) {
        [api adapterVerify:[NSString stringWithUTF8String: json]];
    }

    char* adapterWalletReadyState(const char* json) {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api adapterWalletReadyState:[NSString stringWithUTF8String: json]] UTF8String]);
    }
    
}

extern "C" {
    void particleAAInitialize(const char* json) {
         [api particleAAInitialize:[NSString stringWithUTF8String: json]];
    }

    void enableAAMode() {
        [api enableAAMode];
    }

    void disableAAMode() {
        [api disableAAMode];
    }

    bool isAAModeEnable() {
        return [api isAAModeEnable];
    }

    void isDeploy(const char* json) {
        [api isDeploy:[NSString stringWithUTF8String: json]];
    }

    void rpcGetFeeQuotes(const char* json) {
        [api rpcGetFeeQuotes:[NSString stringWithUTF8String: json]];
    }

    bool isSupportChainInfo(const char* json) {
        return [api isSupportChainInfo:[NSString stringWithUTF8String: json]];
    }
}

extern "C" {

    void authCoreInitialize() {
         [api authCoreInitialize];
    }

    void authCoreSetBlindEnable(bool enable) {
        [api authCoreSetBlindEnable: enable];
    }

    bool authCoreGetBlindEnable() {
        return [api authCoreGetBlindEnable];
    }

    void authCoreConnect(const char* json) {
         [api authCoreConnect: [NSString stringWithUTF8String: json]];
    }

    void authCoreConnectWithCode(const char* json) {
        [api authCoreConnectWithCode: [NSString stringWithUTF8String: json]];
    }

    void authCoreSendEmailCode(const char* json) {
        [api authCoreSendEmailCode: [NSString stringWithUTF8String: json]];
    }

    void authCoreSendPhoneCode(const char* json) {
        [api authCoreSendPhoneCode: [NSString stringWithUTF8String: json]];
    }

    void authCoreDisconnect() {
         [api authCoreDisconnect];
    }

    void authCoreIsConnected() {
         [api authCoreIsConnected];
    }

    char* authCoreGetUserInfo() {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api authCoreGetUserInfo] UTF8String]);
    }

    void authCoreSwitchChain(const char* json) {
         [api authCoreSwitchChain: [NSString stringWithUTF8String: json]];
    }

    void authCoreChangeMasterPassword() {
        [api authCoreChangeMasterPassword];
    }

    bool authCoreHasMasterPassword() {
        return [api authCoreHasMasterPassword];
    }

    bool authCoreHasPaymentPassword() {
        return [api authCoreHasPaymentPassword];
    }

    void authCoreOpenAccountAndSecurity() {
        [api authCoreOpenAccountAndSecurity];
    }

    char* authCoreEvmGetAddress() {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api authCoreEvmGetAddress] UTF8String]);
    }

    void authCoreEvmPersonalSign(const char* json) {
        [api authCoreEvmPersonalSign: [NSString stringWithUTF8String: json]];
    }

    void authCoreEvmPersonalSignUnique(const char* json) {
        [api authCoreEvmPersonalSignUnique: [NSString stringWithUTF8String: json]];
    }

    void authCoreEvmSignTypedData(const char* json) {
        [api authCoreEvmSignTypedData: [NSString stringWithUTF8String: json]];
    }

    void authCoreEvmSignTypedDataUnique(const char* json) {
        [api authCoreEvmSignTypedDataUnique: [NSString stringWithUTF8String: json]];
    }

    void authCoreEvmSendTransaction(const char* json) {
        [api authCoreEvmSendTransaction: [NSString stringWithUTF8String: json]];
    }

    void authCoreEvmBatchSendTransactions(const char* json) {
        [api authCoreEvmBatchSendTransactions: [NSString stringWithUTF8String: json]];
    }

    char* authCoreSolanaGetAddress() {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api authCoreSolanaGetAddress] UTF8String]);
    }

    void authCoreSolanaSignMessage(const char* json) {
        [api authCoreSolanaSignMessage: [NSString stringWithUTF8String: json]];
    }

    void authCoreSolanaSignTransaction(const char* json) {
        [api authCoreSolanaSignTransaction: [NSString stringWithUTF8String: json]];
    }

    void authCoreSolanaSignAllTransactions(const char* json) {
        [api authCoreSolanaSignAllTransactions: [NSString stringWithUTF8String: json]];
    }

    void authCoreSolanaSignAndSendTransaction(const char* json) {
        [api authCoreSolanaSignAndSendTransaction: [NSString stringWithUTF8String: json]];
    }

}

// rename to cStringCopyPN in case meet same name function from other library.
char* cStringCopyPN(const char* string) {
     if (string == NULL){
          return NULL;
     }
     char* res = (char*)malloc(strlen(string)+1);
     strcpy(res, string);
     return res;
}



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

    void setInterfaceStyle(const char* json) {
        [api setInterfaceStyle:[NSString stringWithUTF8String: json]];
    }

    void setMediumScreen(bool isMedium) {
        [api setMediumScreen: isMedium];
    }

    void openWebWallet() {
        [api openWebWallet];
    }

    void openAccountAndSecurity() {
        [api openAccountAndSecurity];
    }

    void setSecurityAccountConfig(const char* json) {
        [api setSecurityAccountConfig:[NSString stringWithUTF8String: json]];
    }

}

// Particle Auth Service
extern "C" {
    void login(const char* json) {
        [api login:[NSString stringWithUTF8String: json]];
    }
    void setUserInfo(const char* json) {
        [api setUserInfo:[NSString stringWithUTF8String: json]];
    }

    void logout() {
        [api logout];
    }

    void fastLogout() {
        [api fastLogout];
    }

    bool isLogin() {
        return [api isLogin];
    }

    void isLoginAsync() {
        return [api isLoginAsync];
    }

    void signMessage(const char* message) {
        [api signMessage:[NSString stringWithUTF8String: message]];
    }

    void signTransaction(const char* transaction) {
        [api signTransaction:[NSString stringWithUTF8String: transaction]];
    }

    void signAllTransactions(const char* transactions) {
        [api signAllTransactions:[NSString stringWithUTF8String: transactions]];
    }

    void signAndSendTransaction(const char* message) {
        [api signAndSendTransaction:[NSString stringWithUTF8String: message]];
    }

    void signTypedData(const char* json) {
        [api signTypedData:[NSString stringWithUTF8String: json]];
    }

    char* getAddress() {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api getAddress] UTF8String]);
        
    }

    char* getUserInfo() {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api getUserInfo] UTF8String]);
        
    }

    void setChainInfoAsync(const char* chainInfo) {
        [api setChainInfoAsync:[NSString stringWithUTF8String: chainInfo]];
    }

    void setModalPresentStyle(const char* style) {
        [api setModalPresentStyle:[NSString stringWithUTF8String: style]];
    }
}


// Particle Wallet GUI
extern "C" {
    void enablePay(bool enable) {
        [api enablePay:enable];
    }

    bool getEnablePay() {
        return [api getEnablePay];
    }
    
    void enableSwap(bool enable) {
            [api enableSwap:enable];
        }
    
    bool getEnableSwap() {
        return [api getEnableSwap];
    }

    void navigatorWallet(int display) {
        [api navigatorWallet:display];
    }

    void navigatorTokenReceive(const char* json) {
        [api navigatorTokenReceive:[NSString stringWithUTF8String: json]];
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
    
    void navigatorLoginList(const char* json)  {
        [api navigatorLoginList:[NSString stringWithUTF8String: json]];
    }
    
    void showTestNetwork(bool show) {
        [api showTestNetwork:show];
    } 
    
    void showManageWallet(bool show) {
        [api showManageWallet:show];
    }
    
    void supportChain(const char* json) {
        [api supportChain:[NSString stringWithUTF8String: json]];
    }
    
    void switchWallet(const char* json) {
        [api switchWallet:[NSString stringWithUTF8String: json]];
    }

    void guiSetLanguage(const char* json) {
        [api guiSetLanguage:[NSString stringWithUTF8String: json]];
    }

    void showLanguageSetting(bool show) {
        [api showLanguageSetting:show];
    }

    void showAppearanceSetting(bool show) {
        [api showAppearanceSetting:show];
    }

    void supportWalletConnect(bool enable) {
        [api supportWalletConnect: enable];
    }
    
    void supportDappBrowser(bool enable) {
        [api supportDappBrowser: enable];
    }
    
    void particleWalletConnectInitialize(const char* json) {
        [api particleWalletConnectInitialize:[NSString stringWithUTF8String: json]];
    }

    void setSupportAddToken(bool enable) {
        [api supportWalletConnect: enable];
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

    void setFiatCoin(const char* json) {
        [api setFiatCoin:[NSString stringWithUTF8String: json]];
    }

    void loadCustomUIJsonString(const char* json) {
        [api loadCustomUIJsonString:[NSString stringWithUTF8String: json]];
    }
}

// Particle Connect
extern "C" {
    void particleConnectInitialize(const char* json) {
        [api particleConnectInitialize:[NSString stringWithUTF8String: json]];
    }
    
    bool particleConnectSetChainInfo(const char* chainInfo) {
         return [api particleConnectSetChainInfo:[NSString stringWithUTF8String: chainInfo]];
    }

    void particleConnectSetChainInfoAsync(const char* chainInfo) {
         return [api particleConnectSetChainInfoAsync:[NSString stringWithUTF8String: chainInfo]];
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
    
    void adapterSwitchEthereumChain(const char* json) {
        [api adapterSwitchEthereumChain:[NSString stringWithUTF8String: json]];
    }
    
    void adapterAddEthereumChain(const char* json) {
        [api adapterAddEthereumChain:[NSString stringWithUTF8String: json]];
    }

    char* adapterWalletReadyState(const char* json) {
        char* cStringCopyPN(const char* string);
        return cStringCopyPN([[api adapterWalletReadyState:[NSString stringWithUTF8String: json]] UTF8String]);
    }
    
    void setWalletConnectV2ProjectId(const char* json) {
         [api setWalletConnectV2ProjectId:[NSString stringWithUTF8String: json]];
    }
    
    void setWalletConnectV2SupportChainInfos(const char* json) {
         [api setWalletConnectV2SupportChainInfos:[NSString stringWithUTF8String: json]];
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


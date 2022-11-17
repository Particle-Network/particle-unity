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
}

// Particle Auth Service
extern "C" {
    void login(const char* json) {
        [api login:[NSString stringWithUTF8String: json]];
    }

    void logout() {
        [api logout];
    }

    bool isLogin() {
        return [api isLogin];
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

// Particle Wallet API
extern "C" {
 
    void solanaGetTokenList() {
        [api solanaGetTokenList];
    }

    void solanaGetTokensAndNFTs(const char* address) {
        [api solanaGetTokensAndNFTs: [NSString stringWithUTF8String: address]];
    }

    void solanaGetTokensAndNFTsFromDB(const char* address) {
        [api solanaGetTokensAndNFTsFromDB: [NSString stringWithUTF8String: address]];
    }

    void solanaAddCustomTokens(const char* json) {
        [api solanaAddCustomTokens: [NSString stringWithUTF8String: json]];
    }

    void solanaGetTransactions(const char* json) {
        [api solanaGetTransactions: [NSString stringWithUTF8String: json]];
    }

    void solanaGetTransactionsFromDB(const char* json) {
        [api solanaGetTransactionsFromDB: [NSString stringWithUTF8String: json]];
    }

    void solanaGetTokenTransactions(const char* json) {
        [api solanaGetTokenTransactions: [NSString stringWithUTF8String: json]];
    }

    void solanaGetTokenTransactionsFromDB(const char* json) {
        [api solanaGetTokenTransactionsFromDB: [NSString stringWithUTF8String: json]];
    }

    void evmGetTokenList() {
        [api evmGetTokenList];
    }

    void evmGetTokensAndNFTs(const char* json) {
        [api evmGetTokensAndNFTs: [NSString stringWithUTF8String: json]];
    }

    void evmGetTokensAndNFTsFromDB(const char* address) {
        [api evmGetTokensAndNFTsFromDB: [NSString stringWithUTF8String: address]];
    }

    void evmAddCustomTokens(const char* json) {
        [api evmGetTokensAndNFTsFromDB: [NSString stringWithUTF8String: json]];
    }

    void evmGetTransactions(const char* address) {
        [api evmGetTransactions:  [NSString stringWithUTF8String: address]];
    }

    void evmGetTransactionsFromDB(const char* address) {
        [api evmGetTransactionsFromDB:  [NSString stringWithUTF8String: address]];
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
    
    void navigatorPay() {
        [api navigatorPay];
    }
    
    void navigatorBuyCrypto(const char* json) {
        [api navigatorBuyCrypto:[NSString stringWithUTF8String: json]];
    }
    
    void navigatorSwap(const char* json) {
        [api navigatorSwap:[NSString stringWithUTF8String: json]];
    }
    
    void navigatorLoginList() {
         [api navigatorLoginList];
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
    
    void setLanguage(const char* json) {
        [api setLanguage:[NSString stringWithUTF8String: json]];
    }
    
    void setInterfaceStyle(const char* json) {
        [api setInterfaceStyle:[NSString stringWithUTF8String: json]];
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


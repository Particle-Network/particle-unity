// [!] important set UnityFramework in Target Membership for this file
// [!]           and set Public header visibility

#import <Foundation/Foundation.h>

// NativeCallsProtocol defines protocol with methods you want to be called from managed
@protocol NativeCallsProtocol
@required
// Regulation
// If return value is an object, dictionary or array, should convert to json string and return.
// If return value is a string, return this string.
// If return value is big int, int, double, big double or any other number, convert to string and return.
// If return value is bool, return bool.
// If return value is optional in swift, return empty string or default value.

// Particle Network Base
- (void) initialize:(NSString* _Nonnull)json;
- (NSString* _Nonnull) getChainInfo;
- (NSInteger) getDevEnv;
- (BOOL) setChainInfo:(NSString* _Nonnull)json;

// Particle Auth Service
- (void) login:(NSString* _Nonnull)json;
- (void) logout;
- (BOOL) isLogin;
- (void) signMessage:(NSString* _Nonnull)message;
- (void) signTransaction:(NSString* _Nonnull)transaction;
- (void) signAllTransactions:(NSString* _Nonnull)transactions;
- (void) signAndSendTransaction:(NSString* _Nonnull)message;
- (void) signTypedData:(NSString* _Nonnull)json;
- (NSString* _Nonnull) getAddress;
- (NSString* _Nonnull) getUserInfo;
- (void) setChainInfoAsync:(NSString* _Nonnull)json;
- (void) setModalPresentStyle:(NSString* _Nonnull)style;

// Particle Wallet API
- (void) solanaGetTokenList;
- (void) solanaGetTokensAndNFTs:(NSString* _Nonnull)address;
- (void) solanaGetTokensAndNFTsFromDB:(NSString* _Nonnull)address;
- (void) solanaAddCustomTokens:(NSString* _Nonnull)json;
- (void) solanaGetTransactions:(NSString* _Nonnull)json;
- (void) solanaGetTransactionsFromDB:(NSString* _Nonnull)json;
- (void) solanaGetTokenTransactions:(NSString* _Nonnull)json;
- (void) solanaGetTokenTransactionsFromDB:(NSString* _Nonnull)json;
- (void) evmGetTokenList;
- (void) evmGetTokensAndNFTs:(NSString* _Nonnull)json;
- (void) evmGetTokensAndNFTsFromDB:(NSString* _Nonnull)address;
- (void) evmAddCustomTokens:(NSString* _Nonnull)json;
- (void) evmGetTransactions:(NSString* _Nonnull)address;
- (void) evmGetTransactionsFromDB:(NSString* _Nonnull)address;

// Particle Wallet GUI
- (void) enablePay:(BOOL)enable;
- (BOOL) getEnablePay;
- (void) enableSwap:(BOOL)enable;
- (BOOL) getEnableSwap;
- (void) navigatorWallet:(NSInteger)display;
- (void) navigatorTokenReceive:(NSString* _Nullable)json;
- (void) navigatorTokenSend:(NSString* _Nullable)json;
- (void) navigatorTokenTransactionRecords:(NSString* _Nullable)json;
- (void) navigatorNFTSend:(NSString*_Nonnull)json;
- (void) navigatorNFTDetails:(NSString*_Nonnull)json;
- (void) navigatorPay;
- (void) navigatorSwap:(NSString* _Nullable)json;
- (void) navigatorLoginList;
- (void) showTestNetwork:(BOOL)show; 
- (void) showManageWallet:(BOOL)show;
- (void) supportChain:(NSString*_Nonnull)json;
- (void) switchWallet:(NSString*_Nonnull)json;
- (void) setLanguage:(NSString* _Nonnull)json;
- (void) setInterfaceStyle:(NSString* _Nonnull)json;

// Particle Connect
- (void) particleConnectInitialize:(NSString* _Nonnull)json;
- (BOOL) particleConnectSetChainInfo:(NSString* _Nonnull)json;
- (void) particleConnectSetChainInfoAsync:(NSString* _Nonnull)json;
// Particle Connect Service
- (NSString* _Nonnull) adapterGetAccounts:(NSString* _Nonnull)json;
- (void) adapterConnect:(NSString* _Nonnull)json configJson:(NSString* _Nonnull)account;
- (void) adapterDisconnect:(NSString* _Nonnull)json;
- (BOOL) adapterIsConnected:(NSString* _Nonnull)json;
- (void) adapterSignAndSendTransaction:(NSString* _Nonnull)json;
- (void) adapterSignTransaction:(NSString* _Nonnull)json;
- (void) adapterSignAllTransactions:(NSString* _Nonnull)json;
- (void) adapterSignMessage:(NSString* _Nonnull)json;
- (void) adapterSignTypedData:(NSString* _Nonnull)json;
- (void) adapterImportWalletFromPrivateKey:(NSString* _Nonnull)json;
- (void) adapterImportWalletFromMnemonic:(NSString* _Nonnull)json;
- (void) adapterExportWalletPrivateKey:(NSString* _Nonnull)json;
- (void) adapterLogin:(NSString* _Nonnull)json;
- (void) adapterVerify:(NSString* _Nonnull)json;
@end

__attribute__ ((visibility("default")))
@interface FrameworkLibAPI : NSObject
// call it any time after UnityFrameworkLoad to set object implementing NativeCallsProtocol methods
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>_Nonnull) aApi;

@end



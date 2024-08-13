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
- (void) setAppearance:(NSString* _Nonnull)json;
- (void) setLanguage:(NSString* _Nonnull)json;
- (void) setFiatCoin:(NSString*_Nonnull)json;
- (void) setSecurityAccountConfig:(NSString* _Nonnull)json;
- (void) setCustomUIConfigJsonString:(NSString* _Nonnull)json;

// Particle Wallet GUI
- (void) setPayDisabled:(BOOL)disabled;
- (BOOL) getPayDisabled;
- (void) setSwapDisabled:(BOOL)disabled;
- (BOOL) getSwapDisabled;
- (void) navigatorWallet:(NSInteger)display;
- (void) navigatorDappBrowser:(NSString* _Nonnull)json;
- (void) navigatorTokenReceive:(NSString* _Nullable)json;
- (void) navigatorTokenSend:(NSString* _Nullable)json;
- (void) navigatorTokenTransactionRecords:(NSString* _Nullable)json;
- (void) navigatorNFTSend:(NSString*_Nonnull)json;
- (void) navigatorNFTDetails:(NSString*_Nonnull)json;
- (void) navigatorBuyCrypto:(NSString*_Nonnull)json;
- (void) navigatorSwap:(NSString* _Nullable)json;
- (void) setShowTestNetwork:(BOOL)show;
- (void) setShowManageWallet:(BOOL)show;
- (void) setSupportChain:(NSString*_Nonnull)json;
- (void) switchWallet:(NSString*_Nonnull)json;
- (void) setShowAppearanceSetting:(BOOL)show;
- (void) setShowLanguageSetting:(BOOL)show;
- (void) setShowSmartAccountSetting:(BOOL)show;
- (void) setSupportWalletConnect:(BOOL)enable;
- (void) setSupportDappBrowser:(BOOL)enable;
- (void) particleWalletConnectInitialize:(NSString*_Nonnull)json;
- (void) setSupportAddToken:(BOOL)enable;
- (void) setDisplayTokenAddresses:(NSString*_Nonnull)json;
- (void) setDisplayNFTContractAddresses:(NSString*_Nonnull)json;
- (void) setPriorityTokenAddresses:(NSString*_Nonnull)json;
- (void) setPriorityNFTContractAddresses:(NSString*_Nonnull)json;
- (void) setCustomWalletName:(NSString*_Nonnull)json;
- (void) setCustomLocalizable:(NSString*_Nonnull)json;

// Particle Connect Service
- (void) particleConnectInitialize:(NSString* _Nonnull)json;
- (void) setWalletConnectV2SupportChainInfos:(NSString* _Nonnull)json;
- (NSString* _Nonnull) adapterGetAccounts:(NSString* _Nonnull)json;
- (void) adapterConnect:(NSString* _Nonnull)json configJson:(NSString* _Nonnull)account;
- (void) adapterDisconnect:(NSString* _Nonnull)json;
- (BOOL) adapterIsConnected:(NSString* _Nonnull)json;
- (void) adapterSignAndSendTransaction:(NSString* _Nonnull)json;
- (void) adapterBatchSendTransactions:(NSString* _Nonnull)json;
- (void) adapterSignTransaction:(NSString* _Nonnull)json;
- (void) adapterSignAllTransactions:(NSString* _Nonnull)json;
- (void) adapterSignMessage:(NSString* _Nonnull)json;
- (void) adapterSignTypedData:(NSString* _Nonnull)json;
- (void) adapterImportWalletFromPrivateKey:(NSString* _Nonnull)json;
- (void) adapterImportWalletFromMnemonic:(NSString* _Nonnull)json;
- (void) adapterExportWalletPrivateKey:(NSString* _Nonnull)json;
- (void) adapterLogin:(NSString* _Nonnull)json;
- (void) adapterVerify:(NSString* _Nonnull)json;
- (NSString* _Nonnull) adapterWalletReadyState:(NSString* _Nonnull)json;


// Particle AA
- (void) particleAAInitialize:(NSString* _Nonnull)json;
- (void) enableAAMode;
- (void) disableAAMode;
- (BOOL) isAAModeEnable;
- (void) isDeploy:(NSString* _Nonnull)json;
- (void) rpcGetFeeQuotes:(NSString* _Nonnull)json;
- (BOOL) isSupportChainInfo:(NSString* _Nonnull)json;


// Particle Auth Core
- (void) authCoreInitialize;
- (void) authCoreConnect:(NSString* _Nonnull)json;
- (void) authCoreConnectWithCode:(NSString* _Nonnull)json;
- (void) authCoreSendEmailCode:(NSString* _Nonnull)json;
- (void) authCoreSendPhoneCode:(NSString* _Nonnull)json;
- (void) authCoreSetBlindEnable:(BOOL)enbale;
- (BOOL) authCoreGetBlindEnable;
- (void) authCoreDisconnect;
- (void) authCoreIsConnected;
- (NSString* _Nonnull) authCoreGetUserInfo;
- (void) authCoreSwitchChain:(NSString* _Nonnull)json;
- (void) authCoreChangeMasterPassword;
- (BOOL) authCoreHasMasterPassword;
- (BOOL) authCoreHasPaymentPassword;
- (void) authCoreOpenAccountAndSecurity;

- (NSString* _Nonnull) authCoreEvmGetAddress;
- (void) authCoreEvmPersonalSign:(NSString* _Nonnull)json;
- (void) authCoreEvmPersonalSignUnique:(NSString* _Nonnull)json;
- (void) authCoreEvmSignTypedData:(NSString* _Nonnull)json;
- (void) authCoreEvmSignTypedDataUnique:(NSString* _Nonnull)json;
- (void) authCoreEvmSendTransaction:(NSString* _Nonnull)json;
- (void) authCoreEvmBatchSendTransactions:(NSString* _Nonnull)json;
- (NSString* _Nonnull) authCoreSolanaGetAddress;
- (void) authCoreSolanaSignMessage:(NSString* _Nonnull)json;
- (void) authCoreSolanaSignTransaction:(NSString* _Nonnull)json;
- (void) authCoreSolanaSignAllTransactions:(NSString* _Nonnull)json;
- (void) authCoreSolanaSignAndSendTransaction:(NSString* _Nonnull)json;
@end

__attribute__ ((visibility("default")))
@interface FrameworkLibAPI : NSObject
// call it any time after UnityFrameworkLoad to set object implementing NativeCallsProtocol methods
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>_Nonnull) aApi;

@end




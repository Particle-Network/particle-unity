#if UNITY_IOS
#nullable enable
using System.Runtime.InteropServices;
#endif

namespace Network.Particle.Scripts.Core
{
    public static class ParticleNetworkIOSBridge
    {
        // Particle Network Base
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void initialize(string json);

        [DllImport("__Internal")]
        public static extern bool setChainInfo(string chainInfo);

        [DllImport("__Internal")]
        public static extern string getChainInfo();

        [DllImport("__Internal")]
        public static extern int getDevEnv();

        [DllImport("__Internal")]
        public static extern void setAppearance(string json);

        [DllImport("__Internal")]
        public static extern void setLanguage(string json);

        [DllImport("__Internal")]
        public static extern void setFiatCoin(string json);

        [DllImport("__Internal")]
        public static extern void setSecurityAccountConfig(string json);

        [DllImport("__Internal")]
        public static extern void setWebAuthConfig(string json);

#endif

        // Particle Auth Service
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void login(string json);

        [DllImport("__Internal")]
        public static extern void logout();

        [DllImport("__Internal")]
        public static extern void fastLogout();

        [DllImport("__Internal")]
        public static extern bool isLogin();

        [DllImport("__Internal")]
        public static extern void isLoginAsync();

        [DllImport("__Internal")]
        public static extern void signMessage(string message);

        [DllImport("__Internal")]
        public static extern void signMessageUnique(string message);

        [DllImport("__Internal")]
        public static extern void signTransaction(string transaction);

        [DllImport("__Internal")]
        public static extern void signAllTransactions(string transactions);

        [DllImport("__Internal")]
        public static extern void signAndSendTransaction(string message);

        [DllImport("__Internal")]
        public static extern void batchSendTransactions(string message);

        [DllImport("__Internal")]
        public static extern void signTypedData(string json);

        [DllImport("__Internal")]
        public static extern string getAddress();

        [DllImport("__Internal")]
        public static extern string getUserInfo();

        [DllImport("__Internal")]
        public static extern void setChainInfoAsync(string chainInfo);

        [DllImport("__Internal")]
        public static extern void setModalPresentStyle(string style);

        [DllImport("__Internal")]
        public static extern void setMediumScreen(bool isMedium);

        [DllImport("__Internal")]
        public static extern void openWebWallet(string json);

        [DllImport("__Internal")]
        public static extern void openAccountAndSecurity();

        [DllImport("__Internal")]
        public static extern void getSecurityAccount();

#endif

        // Particle Wallet GUI
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void setPayDisabled(bool enable);

        [DllImport("__Internal")]
        public static extern bool getPayDisabled();

        [DllImport("__Internal")]
        public static extern void setSwapDisabled(bool enable);

        [DllImport("__Internal")]
        public static extern bool getSwapDisabled();

        [DllImport("__Internal")]
        public static extern void navigatorWallet(int display);

        [DllImport("__Internal")]
        public static extern void navigatorTokenReceive(string json);

        [DllImport("__Internal")]
        public static extern void navigatorTokenSend(string json);

        [DllImport("__Internal")]
        public static extern void navigatorTokenTransactionRecords(string json);

        [DllImport("__Internal")]
        public static extern void navigatorNFTSend(string json);

        [DllImport("__Internal")]
        public static extern void navigatorNFTDetails(string json);

        [DllImport("__Internal")]
        public static extern void navigatorBuyCrypto(string json);

        [DllImport("__Internal")]
        public static extern void navigatorLoginList(string json);

        [DllImport("__Internal")]
        public static extern void navigatorDappBrowser(string json);

        [DllImport("__Internal")]
        public static extern void setShowTestNetwork(bool show);

        [DllImport("__Internal")]
        public static extern void setShowManageWallet(bool show);

        [DllImport("__Internal")]
        public static extern void setShowAppearanceSetting(bool show);

        [DllImport("__Internal")]
        public static extern void setShowLanguageSetting(bool show);

        [DllImport("__Internal")]
        public static extern void setShowSmartAccountSetting(bool show);

        [DllImport("__Internal")]
        public static extern void setSupportChain(string json);

        [DllImport("__Internal")]
        public static extern void switchWallet(string json);

        [DllImport("__Internal")]
        public static extern void navigatorSwap(string json);

        [DllImport("__Internal")]
        public static extern void setSupportWalletConnect(bool enable);

        [DllImport("__Internal")]
        public static extern void setSupportDappBrowser(bool enable);

        [DllImport("__Internal")]
        public static extern void particleWalletConnectInitialize(string json);

        [DllImport("__Internal")]
        public static extern void setSupportAddToken(bool enable);

        [DllImport("__Internal")]
        public static extern void setDisplayTokenAddresses(string json);

        [DllImport("__Internal")]
        public static extern void setDisplayNFTContractAddresses(string json);

        [DllImport("__Internal")]
        public static extern void setPriorityTokenAddresses(string json);

        [DllImport("__Internal")]
        public static extern void setPriorityNFTContractAddresses(string json);

        [DllImport("__Internal")]
        public static extern void loadCustomUIJsonString(string json);


        [DllImport("__Internal")]
        public static extern void setCustomLocalizable(string json);

        [DllImport("__Internal")]
        public static extern void setCustomWalletName(string json);


#endif

        // Particle Connect
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void particleConnectInitialize(string json);

        [DllImport("__Internal")]
        public static extern void setWalletConnectV2SupportChainInfos(string json);

        [DllImport("__Internal")]
        public static extern string adapterGetAccounts(string json);

        [DllImport("__Internal")]
        public static extern void adapterConnect(string json, string configJson);

        [DllImport("__Internal")]
        public static extern void adapterDisconnect(string json);

        [DllImport("__Internal")]
        public static extern bool adapterIsConnected(string json);

        [DllImport("__Internal")]
        public static extern void adapterSignAndSendTransaction(string json);

        [DllImport("__Internal")]
        public static extern void adapterBatchSendTransactions(string json);

        [DllImport("__Internal")]
        public static extern void adapterSignTransaction(string json);

        [DllImport("__Internal")]
        public static extern void adapterSignAllTransactions(string json);

        [DllImport("__Internal")]
        public static extern void adapterSignMessage(string json);

        [DllImport("__Internal")]
        public static extern void adapterSignTypedData(string json);

        [DllImport("__Internal")]
        public static extern void adapterImportWalletFromPrivateKey(string json);

        [DllImport("__Internal")]
        public static extern void adapterImportWalletFromMnemonic(string json);

        [DllImport("__Internal")]
        public static extern void adapterExportWalletPrivateKey(string json);

        [DllImport("__Internal")]
        public static extern void adapterLogin(string json);

        [DllImport("__Internal")]
        public static extern void adapterVerify(string json);

        [DllImport("__Internal")]
        public static extern string adapterWalletReadyState(string json);
#endif

        // Particle AA
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void particleAAInitialize(string json);

        [DllImport("__Internal")]
        public static extern void enableAAMode();

        [DllImport("__Internal")]
        public static extern void disableAAMode();

        [DllImport("__Internal")]
        public static extern bool isAAModeEnable();

        [DllImport("__Internal")]
        public static extern void isDeploy(string json);

        [DllImport("__Internal")]
        public static extern void rpcGetFeeQuotes(string json);

        [DllImport("__Internal")]
        public static extern bool isSupportChainInfo(string json);

#endif

        // Particle Auth Core
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void authCoreInitialize();

        [DllImport("__Internal")]
        public static extern void authCoreConnect(string json);

        [DllImport("__Internal")]
        public static extern void authCoreConnectJWT(string json);

        [DllImport("__Internal")]
        public static extern void authCorePresentLoginPage(string json);

        [DllImport("__Internal")]
        public static extern void authCoreSendEmailCode(string json);

        [DllImport("__Internal")]
        public static extern void authCoreSendPhoneCode(string json);
        
        [DllImport("__Internal")]
        public static extern void authCoreSetBlindEnable(bool json);
        
        [DllImport("__Internal")]
        public static extern bool authCoreGetBlindEnable();
        
        [DllImport("__Internal")]
        public static extern void authCoreDisconnect();

        [DllImport("__Internal")]
        public static extern void authCoreIsConnected();

        [DllImport("__Internal")]
        public static extern string authCoreGetUserInfo();

        [DllImport("__Internal")]
        public static extern void authCoreSwitchChain(string json);

        [DllImport("__Internal")]
        public static extern void authCoreChangeMasterPassword();

        [DllImport("__Internal")]
        public static extern bool authCoreHasMasterPassword();

        [DllImport("__Internal")]
        public static extern bool authCoreHasPaymentPassword();

        [DllImport("__Internal")]
        public static extern void authCoreOpenAccountAndSecurity();


        [DllImport("__Internal")]
        public static extern string authCoreEvmGetAddress();

        [DllImport("__Internal")]
        public static extern void authCoreEvmPersonalSign(string json);

        [DllImport("__Internal")]
        public static extern void authCoreEvmPersonalSignUnique(string json);

        [DllImport("__Internal")]
        public static extern void authCoreEvmSignTypedData(string json);

        [DllImport("__Internal")]
        public static extern void authCoreEvmSignTypedDataUnique(string json);

        [DllImport("__Internal")]
        public static extern void authCoreEvmSendTransaction(string json);

        [DllImport("__Internal")]
        public static extern void authCoreEvmBatchSendTransactions(string json);

        [DllImport("__Internal")]
        public static extern string authCoreSolanaGetAddress();

        [DllImport("__Internal")]
        public static extern void authCoreSolanaSignMessage(string json);

        [DllImport("__Internal")]
        public static extern void authCoreSolanaSignTransaction(string json);

        [DllImport("__Internal")]
        public static extern void authCoreSolanaSignAllTransactions(string json);

        [DllImport("__Internal")]
        public static extern void authCoreSolanaSignAndSendTransaction(string json);

        [DllImport("__Internal")]
        public static extern void authCoreSetCustomUI(string json);


#endif
    }
}
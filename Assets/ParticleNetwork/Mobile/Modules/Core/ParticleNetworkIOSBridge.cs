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
        public static extern void setInterfaceStyle(string json);
        
        [DllImport("__Internal")]
        public static extern void setLanguage(string json);
#endif

        // Particle Auth Service
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void login(string json);

        [DllImport("__Internal")]
        public static extern void setUserInfo(string json);

        [DllImport("__Internal")]
        public static extern void logout();
        
        [DllImport("__Internal")]
        public static extern void fastLogout();

        [DllImport("__Internal")]
        public static extern bool isLogin();

        [DllImport("__Internal")]
        public static extern void signMessage(string message);

        [DllImport("__Internal")]
        public static extern void signTransaction(string transaction);

        [DllImport("__Internal")]
        public static extern void signAllTransactions(string transactions);

        [DllImport("__Internal")]
        public static extern void signAndSendTransaction(string message);

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
        public static extern void openWebWallet();
        
        [DllImport("__Internal")]
        public static extern void openAccountAndSecurity();
        
        [DllImport("__Internal")]
        public static extern void setSecurityAccountConfig(string json);
        
        
#endif

        // Particle Wallet GUI
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void enablePay(bool enable);
        
        [DllImport("__Internal")]
        public static extern bool getEnablePay();
        
        [DllImport("__Internal")]
        public static extern void enableSwap(bool enable);
        
        [DllImport("__Internal")]
        public static extern bool getEnableSwap();
        
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
        public static extern void navigatorPay();
        
        [DllImport("__Internal")]
        public static extern void navigatorBuyCrypto(string json);
        
        [DllImport("__Internal")]
        public static extern void navigatorLoginList();
        
        [DllImport("__Internal")]
        public static extern void showTestNetwork(bool show);
        
        [DllImport("__Internal")]
        public static extern void showManageWallet(bool show);
        
        [DllImport("__Internal")]
        public static extern void showAppearanceSetting(bool show);
        
        [DllImport("__Internal")]
        public static extern void showLanguageSetting(bool show);
        
        [DllImport("__Internal")]
        public static extern void supportChain(string json);
        
        [DllImport("__Internal")]
        public static extern void switchWallet(string json);
        
        [DllImport("__Internal")]
        public static extern void guiSetLanguage(string json);

        [DllImport("__Internal")]
        public static extern void navigatorSwap(string json);
        
        [DllImport("__Internal")]
        public static extern void supportWalletConnect(bool enable);
        
        [DllImport("__Internal")]
        public static extern void supportDappBrowser(bool enable);
        
        
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
        public static extern void setFiatCoin(string json);
        
        [DllImport("__Internal")]
        public static extern void loadCustomUIJsonString(string json);
        
#endif

            // Particle Connect
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void particleConnectInitialize(string json);
        
        [DllImport("__Internal")]
        public static extern bool particleConnectSetChainInfo(string json);
        
        [DllImport("__Internal")]
        public static extern bool particleConnectSetChainInfoAsync(string json);
        
#endif 
            
       // Connect Service
#if UNITY_IOS
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
        public static extern void adapterSwitchEthereumChain(string json);
        
        [DllImport("__Internal")]
        public static extern void adapterAddEthereumChain(string json);
        
        [DllImport("__Internal")]
        public static extern string adapterWalletReadyState(string json);
        

#endif

    }
}
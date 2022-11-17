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
#endif

        // Particle Auth Service
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void login(string json);

        [DllImport("__Internal")]
        public static extern void logout();

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
        public static extern void supportChain(string json);
        
        [DllImport("__Internal")]
        public static extern void switchWallet(string json);
        
        [DllImport("__Internal")]
        public static extern void setLanguage(string json);
        
        [DllImport("__Internal")]
        public static extern void setInterfaceStyle(string json);
        
        [DllImport("__Internal")]
        public static extern void navigatorSwap(string json);
        
        
        

#endif

        // Particle Wallet API
#if UNITY_IOS
        [DllImport("__Internal")]
        public static extern void solanaGetTokenList();

        [DllImport("__Internal")]
        public static extern void solanaGetTokensAndNFTs(string json);

        [DllImport("__Internal")]
        public static extern void solanaGetTokensAndNFTsFromDB(string json);

        [DllImport("__Internal")]
        public static extern void solanaAddCustomTokens(string json);

        [DllImport("__Internal")]
        public static extern void solanaGetTransactions(string json);

        [DllImport("__Internal")]
        public static extern void solanaGetTransactionsFromDB(string json);

        [DllImport("__Internal")]
        public static extern void solanaGetTokenTransactions(string json);

        [DllImport("__Internal")]
        public static extern void solanaGetTokenTransactionsFromDB(string json);

        [DllImport("__Internal")]
        public static extern void evmGetTokenList();

        [DllImport("__Internal")]
        public static extern void evmGetTokensAndNFTs(string json);

        [DllImport("__Internal")]
        public static extern void evmGetTokensAndNFTsFromDB(string json);

        [DllImport("__Internal")]
        public static extern void evmAddCustomTokens(string json);

        [DllImport("__Internal")]
        public static extern void evmGetTransactions(string json);

        [DllImport("__Internal")]
        public static extern void evmGetTransactionsFromDB(string json);

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

#endif

    }
}
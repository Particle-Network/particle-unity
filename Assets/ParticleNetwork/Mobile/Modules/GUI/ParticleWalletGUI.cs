
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public class ParticleWalletGUI : SingletonMonoBehaviour<ParticleWalletGUI>
    {
        private TaskCompletionSource<NativeResultData> switchWallet;
        
        private TaskCompletionSource<NativeResultData> loginListTask;

        /// <summary>
        /// Open Wallet page
        /// </summary>
        /// <param name="display">0->Token, 1->NFT</param>
        public static void NavigatorWallet(int display = 0)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
             ParticleNetwork.CallNative("navigatorWallet",display);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorWallet(display);
#else
#endif
        }

        /// <summary>
        /// Open Token Receive page
        /// </summary>
        /// <param name="tokenAddress">Token address, default value is empty</param>
        public static void NavigatorTokenReceive(string tokenAddress = "")
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorTokenReceive",tokenAddress);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorTokenReceive(tokenAddress);
#else
#endif
        }

        /// <summary>
        /// Open Token Send page
        /// </summary>
        /// <param name="tokenAddress">Token Address</param>
        /// <param name="toAddress">To address</param>
        /// <param name="amount">Token Amount</param>
        public static void NavigatorTokenSend(string tokenAddress = "", string toAddress = "", string amount = "")
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "token_address", tokenAddress },
                { "to_address", toAddress },
                { "amount", amount },
            });
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorTokenSend",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorTokenSend(json);
#else
#endif
        }

        /// <summary>
        /// Open Token Transaction Records page
        /// </summary>
        /// <param name="tokenAddress">Token address</param>
        public static void NavigatorTokenTransactionRecords(string tokenAddress = "")
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorTokenTransactionRecords",tokenAddress);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorTokenTransactionRecords(tokenAddress);
#else
#endif
        }

        /// <summary>
        /// Open NFT Send page
        /// </summary>
        /// <param name="mint">NFT mint address</param>
        /// <param name="tokenId">NFT token id</param>
        /// <param name="receiveAddress">Receiver address</param>
        /// <param name="amount">For solana nft or erc721 nft, it is a useless parameter, for erc1155 nft, you can pass amount, such as "1", "100", "10000", default value is "1"</param>
        public static void NavigatorNFTSend(string mint, string tokenId, [CanBeNull] string receiveAddress, [CanBeNull] string amount)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "mint", mint },
                { "receiver_address", receiveAddress },
                { "token_id", tokenId },
                { "amount", amount },
            });
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorNFTSend",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorNFTSend(json);
#else
#endif
        }

        /// <summary>
        /// Open NFT Details page
        /// </summary>
        /// <param name="mint">NFT mint address</param>
        /// <param name="tokenId">NFT token id</param>
        public static void NavigatorNFTDetails(string mint, string tokenId)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "mint", mint },
                { "token_id", tokenId },
            });
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorNFTDetails",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorNFTDetails(json);
#else
#endif
        }

        /// <summary>
        /// Enable pay feature
        /// </summary>
        /// <param name="enable">Enable, default value is true</param>
        public static void EnablePay(bool enable = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("enablePay",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.enablePay(enable);
#else
#endif
        }

        /// <summary>
        /// Get is pay feature enable
        /// </summary>
        /// <returns>If pay feature is enable, return true, vice versa</returns>
        public static bool GetEnablePay()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("getEnablePay");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getEnablePay();
#else
            return true;
#endif
        }

        /// <summary>
        /// Enable swap feature
        /// </summary>
        /// <param name="enable">Enable, default value is true</param>
        public static void EnableSwap(bool enable = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("enableSwap",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.enableSwap(enable);
#else
#endif
        }

        /// <summary>
        /// Get is swap feature enable
        /// </summary>
        /// <returns>If swap feature is enable, return true, vice versa</returns>
        public static bool GetEnableSwap()
        {
#if UNITY_ANDROID && !UNITY_EDITO
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("getEnableSwap");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getEnableSwap();
#else
            return true;
#endif
        }

        /// <summary>
        /// Open buy crypto page
        /// </summary>
        /// <param name="config">Buy crypto config</param>
        public static void NavigatorBuyCrypto([CanBeNull] BuyCryptoConfig config)
        {
            string json = "";
            if (config != null)
            {
                var jObject = new JObject
                {
                    { "wallet_address", config.WalletAddress },
                    { "network", config.Network.ToString() },
                    { "crypto_coin", config.CryptoCoin },
                    { "fiat_coin", config.FiatCoin },
                    { "fiat_amt", config.FiatAmt },
                    { "fix_crypto_coin", config.FixCryptoCoin },
                    { "fix_fiat_amt", config.FixFiatAmt },
                    { "fix_fiat_coin", config.FixFiatCoin },
                };
                if (config.Theme != null)
                {
                    jObject.Add(new JProperty("theme", config.Theme));
                }
                if (config.Language != null)
                {
                    jObject.Add(new JProperty("language", config.Language));
                }
                json = JsonConvert.SerializeObject(jObject);
            }
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("openBuy", json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorBuyCrypto(json);
#else

#endif
        }
        /// <summary>
        /// Open Swap page
        /// </summary>
        /// <param name="fromTokenAddress">From token address, default value is empty</param>
        /// <param name="toTokenAddress">To token address, default value is empty</param>
        /// <param name="amount">Swap amount, default value is empty</param>
        public static void NavigatorSwap(string fromTokenAddress = "", string toTokenAddress = "", string amount = "")
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "from_token_address", fromTokenAddress },
                { "to_token_address", toTokenAddress },
                { "amount", amount },
            });
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorSwap",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorSwap(json);
#else
#endif
        }


        /// <summary>
        /// Open Login List page
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> NavigatorLoginList(List<LoginListPageSupportType> supportTypes)
        {
            loginListTask = new TaskCompletionSource<NativeResultData>();
           
            var json = JsonConvert.SerializeObject(supportTypes.Select(x => x.ToString()));
#if UNITY_EDITOR
            DevModeService.Login();
#elif UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("navigatorLoginList");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorLoginList(json);
#endif
            return loginListTask.Task;
        }

        /// <summary>
        /// Login list call back
        /// </summary>
        /// <param name="json">Result</param>
        public void LoginListCallBack(string json)
        {
            Debug.Log($"LoginListCallBack:{json}");
#if UNITY_EDITOR
            var data = new NativeResultData(true, json);
            loginListTask?.TrySetResult(data);
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            loginListTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
        }

        /// <summary>
        /// Set Show Test Network
        /// </summary>
        /// <param name="show">Set false to hide test network, vice versa, default value is false</param>
        public static void ShowTestNetwork(bool show = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("showTestNetwork",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.showTestNetwork(show);
#else

#endif
        }

        /// <summary>
        /// Set Show Manage Wallet page
        /// </summary>
        /// <param name="show">Set true to show manage wallet page button in setting page, vice versa, default value is true</param>
        public static void ShowManageWallet(bool show = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("showManageWallet",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.showManageWallet(show);
#else

#endif
        }



        
        
        /// <summary>
        /// Set Show Appearance Setting in Setting page
        /// </summary>
        /// <param name="show">Set true to show appearance setting in setting page, vice versa, default value is false.</param>
        public static void ShowAppearanceSetting(bool show = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        ParticleNetwork.GetUnityBridgeClass().CallStatic("showSettingAppearance",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.showAppearanceSetting(show);
#else

#endif
        }
        
        /// <summary>
        /// Set Show Language Setting in Setting page
        /// </summary>
        /// <param name="show">Set true to show language setting in setting page, vice versa, default value is false.</param>
        public static void ShowLanguageSetting(bool show = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("showSettingLanguage",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.showLanguageSetting(show);
#else

#endif
        }

        /// <summary>
        /// Set Support Chain
        /// </summary>
        /// <param name="chainInfos">ChainInfo array</param>
        public static void SupportChain(ChainInfo[] chainInfos)
        {
            List<JObject> allInfos = new List<JObject>();
            foreach (var chainInfo in chainInfos)
            {
                if (!chainInfo.IsMainnet()) continue;
                var info = new JObject
                {
                    { "chain_name", chainInfo.getChainName() },
                    { "chain_id", chainInfo.getChainId() },
                    { "chain_id_name", chainInfo.getChainIdName() },
                };
                allInfos.Add(info);
            }

            var json = JsonConvert.SerializeObject(allInfos);
            Debug.Log($"SupportChain: {json}");
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setSupportChain",json);
#elif UNITY_IOS && !UNITY_EDITOR
            json = JsonConvert.SerializeObject(chainInfos.Select(x => x.getChainName()));
            ParticleNetworkIOSBridge.supportChain(json);
#else

#endif
        }

        /// <summary>
        /// Switch Wallet, when user switch to another wallet outside GUI, call this method to tell GUI sync with Connect.
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <returns></returns>
        public Task<NativeResultData> SwitchWallet(WalletType walletType, string publicAddress)
        {
            switchWallet = new TaskCompletionSource<NativeResultData>();
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
            });
            Debug.Log(json);

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("switchWallet",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.switchWallet(json);
#else

#endif
            return switchWallet.Task;
        }

        public void SwitchWalletCallBack(string json)
        {
            Debug.Log($"SwitchWalletCallBack:{json}");
#if UNITY_EDITOR
            var data = new NativeResultData(true, json);
            switchWallet?.TrySetResult(data);
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            switchWallet?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
        }

        /// <summary>
        /// Set Language
        /// </summary>
        /// <param name="language">Language</param>
        public static void SetLanguage(Language language)
        {
            Debug.Log(language);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("guiSetLanguage",language.ToString());
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.guiSetLanguage(language.ToString());
#else

#endif
        }

        /// <summary>
        /// Set support wallet connect as a wallet
        /// </summary>
        /// <param name="enable">Enable</param>
        public static void SupportWalletConnect(bool enable)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
           ParticleNetwork.GetUnityBridgeClass().CallStatic("supportWalletConnect",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.supportWalletConnect(enable);
#else

#endif
        }
        
        /// <summary>
        /// Set support dapp browser in wallet page
        /// </summary>
        /// <param name="enable">Enable</param>
        public static void SupportDappBrowser(bool enable)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
           ParticleNetwork.GetUnityBridgeClass().CallStatic("supportWalletConnect",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.supportDappBrowser(enable);
#else

#endif
        }

        /// <summary>
        /// Initialize particle wallet connect as a wallet 
        /// </summary>
        /// <param name="metaData">WalletMetaData</param>
        public static void ParticleWalletConnectInitialize(WalletMetaData metaData)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "name", metaData.name },
                { "icon", metaData.icon },
                { "url", metaData.url },
                { "description", metaData.description },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("particleWalletConnectInitialize",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.particleWalletConnectInitialize(json);
#else

#endif
        }

        /// <summary>
        /// Set support add token, true will show add token button, false will hide add token button.
        /// </summary>
        /// <param name="enable">Default value is true</param>
        public static void SetSupportAddToken(bool enable)
        {
           
#if UNITY_ANDROID && !UNITY_EDITOR
           ParticleNetwork.GetUnityBridgeClass().CallStatic("setSupportAddToken",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setSupportAddToken(enable);
#else

#endif
        }

        /// <summary>
        /// Set display token addresses,
        /// If you called this method, Wallet SDK will show these tokens in the token addresses.
        /// </summary>
        /// <param name="tokenAddresses"></param>
        public static void SetDisplayTokenAddresses(string[] tokenAddresses)
        {
            var json = JsonConvert.SerializeObject(tokenAddresses);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setDisplayTokenAddresses",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setDisplayTokenAddresses(json);
#else

#endif
            
        }
        
        /// <summary>
        /// Set display NFT contract addresses
        /// If you called this method, Wallet SDK will only show NFTs in the NFT contract addresses.
        /// </summary>
        /// <param name="nftContractAddresses"></param>
        public static void SetDisplayNFTContractAddresses(string[] nftContractAddresses)
        {
            
            var json = JsonConvert.SerializeObject(nftContractAddresses);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setDisplayNFTContractAddresses",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setDisplayNFTContractAddresses(json);
#else

#endif
            
        }
        
        /// <summary>
        /// Set display token addresses,
        /// If you called this method, Wallet SDK will show these tokens in the token addresses.
        /// </summary>
        /// <param name="tokenAddresses"></param>
        public static void SetPriorityTokenAddresses(string[] tokenAddresses)
        {
            var json = JsonConvert.SerializeObject(tokenAddresses);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setPriorityTokenAddresses",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setPriorityTokenAddresses(json);
#else

#endif
            
        }
        
        /// <summary>
        /// Set display NFT contract addresses
        /// If you called this method, Wallet SDK will only show NFTs in the NFT contract addresses.
        /// </summary>
        /// <param name="nftContractAddresses"></param>
        public static void SetPriorityNFTContractAddresses(string[] nftContractAddresses)
        {
            
            var json = JsonConvert.SerializeObject(nftContractAddresses);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setPriorityNFTContractAddresses",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setPriorityNFTContractAddresses(json);
#else

#endif
            
        }
        
        
        /// <summary>
        /// Set fait coin, currently support USD, CNY, JPY, HKD, INR, KRW.
        /// If you called this method, we will hide currency change button in setting page.
        /// You can pass nil to reset, after reset default fiat coin is USD and currency change button will show in setting page.
        /// </summary>
        /// <param name="fiatCoin">Fiat coin symbol.</param>
        public static void SetFiatCoin(string fiatCoin)
        {
            
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setFiatCoin",fiatCoin);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setFiatCoin(fiatCoin);
#else

#endif
            
        }
        
        /// <summary>
        /// Load custom ui json string
        /// </summary>
        /// <param name="json">Json string</param>
        public static void LoadCustomUIJsonString(string json)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// not support
            // ParticleNetwork.GetUnityBridgeClass().CallStatic("particleWalletConnectInitialize",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.loadCustomUIJsonString(json);
#else

#endif
            
        }

        
    }
}
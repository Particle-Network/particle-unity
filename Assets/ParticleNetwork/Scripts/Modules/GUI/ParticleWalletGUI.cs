using System;
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

        protected override void Awake()
        {
            base.Awake();
        }

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
        public static void NavigatorNFTSend(string mint, string tokenId, string receiveAddress = "")
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "mint", mint },
                { "receiver_address", receiveAddress },
                { "token_id", tokenId },
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
        /// Open Pay page
        /// </summary>
        public static void NavigatorPay()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("openBuy", "");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorPay();
#else

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
                json = JsonConvert.SerializeObject(new JObject
                {
                    { "wallet_address", config.walletAddress },
                    { "network", config.network.ToString() },
                    { "crypto_coin", config.cryptoCoin },
                    { "fiat_coin", config.fiatCoin },
                    { "fiat_amt", config.fiatAmt }
                });
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
        public Task<NativeResultData> NavigatorLoginList()
        {
            loginListTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            DevModeService.Login();
#elif UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorLoginList");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorLoginList();
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
            ParticleNetwork.GetUnityBridgeClass().CallStatic("showAppearanceSetting",show);
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
        /// Set Show Appearance page
        /// </summary>
        /// <param name="show">Set true to show Appearance wallet page button in setting page, vice versa, default value is true</param>
        public static void ShowSettingAppearance(bool show = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("showSettingAppearance",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.showSettingAppearance(show);
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
           ParticleNetwork.GetUnityBridgeClass().CallStatic("supportWalletConnect",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.supportWalletConnect(enable);
#else

#endif
        }

        /// <summary>
        /// Initialize particle wallet connect as a wallet 
        /// </summary>
        /// <param name="metaData">WalletMetdData</param>
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

        
    }
}
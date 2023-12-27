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
        /// <param name="modalStyle">Modal present style, default value is page sheet</param>
        public static void NavigatorTokenSend(string tokenAddress = "", string toAddress = "", string amount = "",
            iOSModalPresentStyle modalStyle = iOSModalPresentStyle.PageSheet)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "token_address", tokenAddress },
                { "to_address", toAddress },
                { "amount", amount },
                { "modal_style", modalStyle.ToString() }
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
        /// <param name="amount">Optional, For solana nft or erc721 nft, it is a useless parameter, for erc1155 nft, you can pass amount, such as "1", "100", "10000"</param>
        public static void NavigatorNFTSend(string mint, string tokenId, [CanBeNull] string receiveAddress,
            [CanBeNull] string amount)
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
        /// Set pay disabled
        /// </summary>
        /// <param name="disabled"></param>
        public static void SetPayDisabled(bool disabled)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setPayDisabled",disabled);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setPayDisabled(disabled);
#else
#endif
        }

        /// <summary>
        /// Get pay disabled
        /// </summary>
        /// <returns></returns>
        public static bool GetPayDisabled()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("getPayDisabled");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getPayDisabled();
#else
            return true;
#endif
        }

        /// <summary>
        /// Set swap disabled
        /// </summary>
        /// <param name="disabled"></param>
        public static void SetSwapDisabled(bool disabled)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setSwapDisabled",disabled);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setSwapDisabled(disabled);
#else
#endif
        }

        /// <summary>
        /// Get swap disabled
        /// </summary>
        /// <returns></returns>
        public static bool GetSwapDisabled()
        {
#if UNITY_ANDROID && !UNITY_EDITO
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("getSwapDisabled");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getSwapDisabled();
#else
            return true;
#endif
        }


        /// <summary>
        /// Open buy crypto page
        /// </summary>
        /// <param name="config">Buy crypto config</param>
        /// <param name="modalStyle">Modal present style, default value is page sheet</param>
        public static void NavigatorBuyCrypto([CanBeNull] BuyCryptoConfig config,
            iOSModalPresentStyle modalStyle = iOSModalPresentStyle.PageSheet)
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
                    { "modal_style", modalStyle.ToString() }
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
        /// <param name="modalStyle">Modal present style, default value is page sheet</param>
        public static void NavigatorSwap(string fromTokenAddress = "", string toTokenAddress = "", string amount = "",
            iOSModalPresentStyle modalStyle = iOSModalPresentStyle.PageSheet)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "from_token_address", fromTokenAddress },
                { "to_token_address", toTokenAddress },
                { "amount", amount },
                { "modal_style", modalStyle.ToString() }
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
        /// Open DappBrowser page
        /// </summary>
        public static void NavigatorDappBrowser([CanBeNull] string url)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "url", url },
            });
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("navigatorDappBrowser",url);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.navigatorDappBrowser(json);
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
            LoginListCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#elif UNITY_ANDROID && !UNITY_EDITOR
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
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            loginListTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Set Show Test Network
        /// </summary>
        /// <param name="show">Set false to hide test network, vice versa, default value is false</param>
        public static void SetShowTestNetwork(bool show = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setShowTestNetwork",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setShowTestNetwork(show);
#else

#endif
        }

        /// <summary>
        /// Set Show Manage Wallet page
        /// </summary>
        /// <param name="show">Set true to show manage wallet page button in setting page, vice versa, default value is true</param>
        public static void SetShowManageWallet(bool show = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setShowManageWallet",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setShowManageWallet(show);
#else

#endif
        }


        /// <summary>
        /// Set Show Appearance Setting in Setting page
        /// </summary>
        /// <param name="show">Set true to show appearance setting in setting page, vice versa, default value is false.</param>
        public static void SetShowAppearanceSetting(bool show = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todi
        ParticleNetwork.GetUnityBridgeClass().CallStatic("showSettingAppearance",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setShowAppearanceSetting(show);
#else

#endif
        }

        /// <summary>
        /// Set Show Language Setting in Setting page
        /// </summary>
        /// <param name="show">Set true to show language setting in setting page, vice versa, default value is false.</param>
        public static void SetShowLanguageSetting(bool show = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setShowLanguageSetting",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setShowLanguageSetting(show);
#else

#endif
        }
        
        /// <summary>
        /// Set Show Smart Account Setting in Setting page
        /// </summary>
        /// <param name="show">Set true to show smart account setting in setting page, vice versa, default value is true.</param>
        public static void SetShowSmartAccountSetting(bool show = true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("setShowSmartAccountSetting",show);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setShowSmartAccountSetting(show);
#else

#endif
        }

        /// <summary>
        /// Set Support Chain
        /// </summary>
        /// <param name="chainInfos">ChainInfo array</param>
        public static void SetSupportChain(ChainInfo[] chainInfos)
        {
            List<JObject> allInfos = new List<JObject>();
            foreach (var chainInfo in chainInfos)
            {
                if (!chainInfo.IsMainnet()) continue;
                var info = new JObject
                {
                    { "chain_name", chainInfo.Name },
                    { "chain_id", chainInfo.Id },
                    { "chain_id_name", chainInfo.Network },
                };
                allInfos.Add(info);
            }

            var json = JsonConvert.SerializeObject(allInfos);
            Debug.Log($"SupportChain: {json}");
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setSupportChain",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setSupportChain(json);
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
            SwitchWalletCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            return switchWallet.Task;
        }

        public void SwitchWalletCallBack(string json)
        {
            Debug.Log($"SwitchWalletCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            switchWallet?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Set support wallet connect as a wallet
        /// </summary>
        /// <param name="enable">Enable</param>
        public static void SetSupportWalletConnect(bool enable)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setSupportWalletConnect",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setSupportWalletConnect(enable);
#else

#endif
        }

        /// <summary>
        /// Set support dapp browser in wallet page
        /// </summary>
        /// <param name="enable">Enable</param>
        public static void SetSupportDappBrowser(bool enable)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
           ParticleNetwork.GetUnityBridgeClass().CallStatic("supportWalletConnect",enable);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setSupportDappBrowser(enable);
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
                { "walletConnectProjectId", metaData.walletConnectProjectId }
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("particleWalletConnectInitialize",json);
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
        /// Load custom ui json string
        /// Only works for iOS
        /// </summary>
        /// <param name="json">Json string</param>
        public static void LoadCustomUIJsonString(string json)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.loadCustomUIJsonString(json);
#else

#endif
        }

        /// <summary>
        /// Set custom localizable strings, should call before open any wallet page.
        /// Only works for iOS
        /// </summary>
        /// <param name="localizables">Localizables</param>
        public static void SetCustomLocalizable(Dictionary<Language, Dictionary<string, string>> localizables)
        {
            var json = JsonConvert.SerializeObject(localizables);
#if UNITY_ANDROID && !UNITY_EDITOR
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setCustomLocalizable(json);
#else

#endif
        }

        /// <summary>
        /// Set custom wallet name and icon, should call before login/connect, only support particle wallet.
        /// </summary>
        /// <param name="name">Wallet name</param>
        /// <param name="icon">Wallet Icon</param>
        public static void SetCustomWalletName(string name, string icon)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "name", name },
                { "icon", icon },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
// todo 
            // ParticleNetwork.GetUnityBridgeClass().CallStatic("setCustomWalletName",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setCustomWalletName(json);
#else

#endif
        }
    }
}
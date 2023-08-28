using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public static class ParticleConnectInteraction
    {
        /**
         * {
	        "chain_name": "BSC",
	        "chain_id": 56,
	        "chain_id_name": "Mainnet",
	        "env": "PRODUCTION",
	        "metadata": "{"name":"Particle Connect","icon":"https://static.particle.network/wallet-icons/Particle.png","url":"https://particle.network"}",
	        "rpc_url": null
        }
         */
        public static void Init(ChainInfo chainInfo, DAppMetaData dAppMetadata, [CanBeNull] RpcUrl rpcUrl = null,
            Env env = Env.DEV)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.Name },
                { "chain_id", chainInfo.Id },
                { "chain_id_name", chainInfo.Network },
                { "env", env.ToString() },
                { "metadata", JToken.FromObject(dAppMetadata) },
                { "rpc_url", rpcUrl == null ? null : JToken.FromObject(rpcUrl) }
            });
            Debug.Log("Init: " + json);
#if UNITY_ANDROID&& !UNITY_EDITOR
                ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("init",json);
#elif UNITY_IOS&& !UNITY_EDITOR
                ParticleNetworkIOSBridge.particleConnectInitialize(json);
#else
            Debug.Log($"ParticleConnect Init json: {json}");
#endif
        }

        public static void SetWalletConnectV2SupportChainInfos(ChainInfo[] chainInfos)
        {
            List<JObject> allInfos = new List<JObject>();
            foreach (var chainInfo in chainInfos)
            {
                var info = new JObject
                {
                    { "chain_name", chainInfo.Name },
                    { "chain_id", chainInfo.Id },
                    { "chain_id_name", chainInfo.Network },
                };
                allInfos.Add(info);
            }

            var json = JsonConvert.SerializeObject(allInfos);

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("setWalletConnectV2SupportChainInfos",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setWalletConnectV2SupportChainInfos(json);
#else

#endif
        }

        public static void Connect(WalletType walletType, [CanBeNull] ConnectConfig config)
        {
            string configJson = "";
            if (config != null)
            {
                var authTypeList = ParticleTools.GetSupportAuthTypeValues(config.supportAuthTypes);
                string accountNative = "";
                if (string.IsNullOrEmpty(config.account))
                    accountNative = "";
                else
                    accountNative = config.account;

                configJson = JsonConvert.SerializeObject(new JObject
                {
                    { "loginType", config.loginType.ToString() },
                    { "account", accountNative },
                    { "supportAuthTypeValues", JToken.FromObject(authTypeList) },
                    { "loginFormMode", config.loginFormMode },
                    { "socialLoginPrompt", config.socialLoginPrompt.ToString() }
                });
            }

            Debug.Log($"Connect-> walletType:{walletType} configJson:{configJson} ");
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("connect",walletType.ToString(),configJson);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterConnect(walletType.ToString(), configJson);
#else

#endif
        }

        public static void Disconnect(WalletType walletType, string publicAddress)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("disconnect",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterDisconnect(json);
#else

#endif
        }

        public static bool IsConnected(WalletType walletType, string publicAddress)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
             return ParticleNetwork.GetUnityConnectBridgeClass().CallStatic<int>("isConnect",json) == 1;
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.adapterIsConnected(json);
#else
            return false;
#endif
        }

        public static string GetAccounts(WalletType walletType)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityConnectBridgeClass().CallStatic<string>("getAccounts",walletType.ToString());
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.adapterGetAccounts(walletType.ToString());
#else
            return "";
#endif
        }

        public static void SignMessage(WalletType walletType, string publicAddress, string message)
        {
            string serializedMessage;
            if (ParticleNetwork.GetChainInfo().isEvmChain())
            {
                if (HexUtils.IsHexadecimal(message))
                {
                    serializedMessage = message;
                }
                else
                {
                    serializedMessage = HexUtils.ConvertHex(message);
                }
            }
            else
            {
                serializedMessage = message;
            }

            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "message", serializedMessage },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("signMessage",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSignMessage(json);
#else
            DevModeService.SolanaSignMessages(new[] { message });
#endif
        }

        public static void SignTransaction(WalletType walletType, string publicAddress, string transaction)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "transaction", transaction },
            });


#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("signTransaction",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSignTransaction(json);
#else

#endif
        }

        public static void SignAllTransactions(WalletType walletType, string publicAddress, string[] transactions)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "transactions", JToken.FromObject(transactions) },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("signAllTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSignAllTransactions(json);
#else

#endif
        }

        internal static void BatchSendTransactions(WalletType walletType, string publicAddress,
            List<string> transactions, [CanBeNull] AAFeeMode feeMode = null)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "transactions", JToken.FromObject(transactions) },
                { "fee_mode", feeMode == null ? null : JToken.FromObject(feeMode) },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("adapterBatchSendTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterBatchSendTransactions(json);
#else

#endif
        }

        public static void SignAndSendTransaction(WalletType walletType, string publicAddress, string transaction,
            [CanBeNull] AAFeeMode feeMode = null)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "transaction", transaction },
                { "fee_mode", feeMode == null ? null : JToken.FromObject(feeMode) },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("signAndSendTransaction",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSignAndSendTransaction(json);
#else

#endif
        }

        public static void SignTypedData(WalletType walletType, string publicAddress, string message)
        {
            string serializedMessage;

            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }


            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "message", serializedMessage },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
           ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("signTypedData",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSignTypedData(json);
#else

#endif
        }

        public static void ImportPrivateKey(WalletType walletType, string privateKey)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "private_key", privateKey },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("importPrivateKey",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterImportWalletFromPrivateKey(json);
#else

#endif
        }

        public static void ImportMnemonic(WalletType walletType, string mnemonic)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "mnemonic", mnemonic },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("importMnemonic",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterImportWalletFromMnemonic(json);
#else

#endif
        }

        public static void ExportPrivateKey(WalletType walletType, string publicAddress)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("exportPrivateKey",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterExportWalletPrivateKey(json);
#else

#endif
        }

        public static void Login(WalletType walletType, string publicAddress, string domain, string uri)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "domain", domain },
                { "uri", uri },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterLogin(json);
#else

#endif
        }

        public static void Verify(WalletType walletType, string publicAddress, string message, string signature)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "message", message },
                { "signature", signature },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("verify",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterVerify(json);
#else

#endif
        }

        public static void SwitchEthereumChain(WalletType walletType, string publicAddress, long chainId)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "chain_id", chainId },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("switchEthereumChain", json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSwitchEthereumChain(json);
#else

#endif
        }

        public static void AddEthereumChain(WalletType walletType, string publicAddress, long chainId,
            [CanBeNull] string chainName,
            [CanBeNull] NativeCurrency nativeCurrency, [CanBeNull] string rpcUrl, [CanBeNull] string blockExplorerUrl)
        {
            JToken nativeCurrencyJson = "";
            if (nativeCurrency != null)
            {
                nativeCurrencyJson = JToken.FromObject(nativeCurrency);
            }

            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "chain_id", chainId },
                { "chain_name", chainName },
                { "native_currency", nativeCurrencyJson },
                { "rpc_url", rpcUrl },
                { "block_explorer_url", blockExplorerUrl }
            });

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("addEthereumChain",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterAddEthereumChain(json);
#else

#endif
        }


        public static WalletReadyState GetWalletReadyState(WalletType walletType)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() }
            });
            var readyState = "";

#if UNITY_ANDROID && !UNITY_EDITOR
            // readyState = ParticleNetwork.GetUnityConnectBridgeClass().CallStatic<string>("adapterWalletReadyState",json);
#elif UNITY_IOS && !UNITY_EDITOR
            readyState = ParticleNetworkIOSBridge.adapterWalletReadyState(json);
#else
            readyState = "";
#endif

            WalletReadyState walletReadyState = WalletReadyState.undetectable;
            foreach (WalletReadyState item in Enum.GetValues(typeof(WalletReadyState)))
            {
                if (item.ToString() == readyState)
                {
                    walletReadyState = item;
                    break;
                }
            }

            return walletReadyState;
        }
    }
}
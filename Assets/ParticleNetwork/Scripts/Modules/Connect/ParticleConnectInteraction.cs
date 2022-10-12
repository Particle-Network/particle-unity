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
        public static void Init(ChainInfo chainInfo, DAppMetadata dAppMetadata, [CanBeNull] RpcUrl rpcUrl = null,
            Env env = Env.DEV)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.getChainName() },
                { "chain_id", chainInfo.getChainId() },
                { "chain_id_name", chainInfo.getChainIdName() },
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

        public static bool SetChainInfo(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.getChainName() },
                { "chain_id", chainInfo.getChainId() },
                { "chain_id_name", chainInfo.getChainIdName() },
            });
            Debug.Log("SetChainInfo: " + json);

#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityConnectBridgeClass().CallStatic<int>("setChainInfo", json) ==1;
#elif UNITY_IOS &&!UNITY_EDITOR
            return ParticleNetworkIOSBridge.particleConnectSetChainInfo(json);
#else
            Debug.Log($"ParticleConnectSetChainInfoSync json: {json}");
            return true;
#endif
        }

        public static void Connect(WalletType walletType)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("connect",walletType.ToString());
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterConnect(walletType.ToString());
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
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "message", message },
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
                {
                    "transactions",  JToken.FromObject(transactions)
                },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityConnectBridgeClass().CallStatic("signAllTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.adapterSignAllTransactions(json);
#else
            
#endif
        }

        public static void SignAndSendTransaction(WalletType walletType, string publicAddress, string transaction)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "transaction", transaction },
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
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "wallet_type", walletType.ToString() },
                { "public_address", publicAddress },
                { "message", message },
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
    }
}
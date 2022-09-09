using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public static class ParticleAuthServiceInteraction
    {
        
        public static void Login(LoginType loginType, [CanBeNull] string account, SupportAuthType supportAuthTypes)
        {
            var authTypeList = ParticleTools.GetSupportAuthTypeValues(supportAuthTypes);
            string accountNative = "";
            if (string.IsNullOrEmpty(account))
                accountNative = "";
            else
                accountNative = account;

#if UNITY_ANDROID && !UNITY_EDITOR
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "loginType", loginType.ToString() },
                { "account", accountNative },
                { "supportAuthTypeValues", JToken.FromObject(authTypeList) },
            });
            Debug.Log(json);
            ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            var json = JsonConvert.SerializeObject(authTypeList);
            ParticleNetworkIOSBridge.login(loginType.ToString().ToLower(), account, json);
#else

#endif
        }

        public static void Logout()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("logout");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.logout();
#else   
            
#endif
        }

        public static bool IsLogin()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<int>("isLogin")==1;
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.isLogin();
#else
            return false;
#endif
        }

        public static void SignMessage(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signMessage",message);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signMessage(message);
#else
            DevModeService.SolanaSignMessages(new[] { message });
#endif
        }

        public static void SignTransaction(string transaction)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signTransaction",transaction);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signTransaction(transaction);
#else

#endif
        }

        public static void SignAllTransactions(string[] transactions)
        {
            var json = JsonConvert.SerializeObject(transactions);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signAllTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signAllTransactions(json);
#else

#endif
        }

        public static void SignAndSendTransaction(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signAndSendTransaction",message);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signAndSendTransaction(message);
#else

#endif
        }

        public static void SignTypedData(string message, SignTypedDataVersion signTypedDataVersion)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "message", message },
                { "version", signTypedDataVersion.ToString() },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signTypedData",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signTypedData(json);
#else

#endif
        }

        public static string GetAddress()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<string>("getAddress");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getAddress();
#else
            return "";
#endif
        }

        /**
            {
	            "uuid": "",
	            "token": "",
	            "wallets": [{
		            "chain_name": "",
		            "public_address": "",
		            "uuid": ""
	            }]
            }
         */
        public static string GetUserInfo()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<string>("getUserInfo");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getUserInfo();
#else
            return PersistTools.GetUserInfoJson();
#endif
        }

        public static void SetChainInfoAsync(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.getChainName() },
                { "chain_id", chainInfo.getChainId() },
                { "chain_id_name", chainInfo.getChainIdName() },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setChainInfoAsync",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setChainInfoAsync(json);
#else
            Debug.Log($"SetChainInfoAsync json: {json}");
#endif
        }


        public static void SetModalPresentStyle(string style)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setModalPresentStyle(style);
#else
#endif
        }
    }
}
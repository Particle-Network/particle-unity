using System;
using System.Linq;
using Network.Particle.Scripts.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Network.Particle.Scripts.Core
{
    public static class ParticleNetwork
    {
        private static AndroidJavaObject activityObject;

        private static AndroidJavaClass unityBridge;
        private static AndroidJavaClass unityConnectBridge;
        private static AndroidJavaClass unityAuthCoreBridge;
        private static ChainInfo currChainInfo;

        public static void Init(ChainInfo chainInfo, Env env = Env.PRODUCTION)
        {
            currChainInfo = chainInfo;
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.Name },
                { "chain_id", chainInfo.Id },
                { "chain_id_name", chainInfo.Network },
                { "env", env.ToString() }
            });

#if UNITY_ANDROID&& !UNITY_EDITOR
                ParticleNetwork.CallNative("init",json);
#elif UNITY_IOS&& !UNITY_EDITOR
                ParticleNetworkIOSBridge.initialize(json);
#else
            Debug.Log($"Init json: {json}");
#endif
        }

        public static bool SetChainInfo(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.Name },
                { "chain_id", chainInfo.Id },
                { "chain_id_name", chainInfo.Network },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<int>("setChainInfo", json) ==1?true:false;
#elif UNITY_IOS &&!UNITY_EDITOR
            return ParticleNetworkIOSBridge.setChainInfo(json);
#else
            UnityInnerChainInfo.SetChainInfo(chainInfo);
            Debug.Log($"SetChainInfoSync json: {json}");
            return true;
#endif
        }

        /// <summary>
        /// Get chain info
        /// </summary>
        /// <returns>ChainInfo</returns>
        public static ChainInfo GetChainInfo()
        {
            Assert.IsNotNull(currChainInfo, "currChainInfo is null,you must call ParticleNetwork.Init() first");
            var resultJson = "";
#if UNITY_ANDROID && !UNITY_EDITOR
            resultJson = ParticleNetwork.GetUnityBridgeClass().CallStatic<string>("getChainInfo");
#elif UNITY_IOS && !UNITY_EDITOR
            resultJson = ParticleNetworkIOSBridge.getChainInfo();
#else
            return currChainInfo;
#endif

            Debug.Log($"GetChainInfo json: {resultJson}");
            var data = JObject.Parse(resultJson);
            var chainInfo = ChainUtils.FindChain(data["chain_name"].ToString(), (int)data["chain_id"]);
            return chainInfo;
        }

        public static int GetEnv()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<int>("getEnv");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getDevEnv();
#else
            return 0;
#endif
        }


        /// <summary>
        /// Set Interface Style
        /// </summary>
        /// <param name="style">Style</param>
        public static void SetAppearance(Appearance style)
        {
            Debug.Log(style);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setAppearance",style.ToString());
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setAppearance(style.ToString());
#else

#endif
        }

        /// <summary>
        /// Set language
        /// </summary>
        /// <param name="language">Language</param>
        public static void SetLanguage(Language language)
        {
            Debug.Log(language);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.GetUnityBridgeClass().CallStatic("setLanguage",language.ToString());
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setLanguage(language.ToString());
#else

#endif
        }

        /// <summary>
        /// Set fiat coin, currently support USD, CNY, JPY, HKD, INR, KRW.
        /// </summary>
        /// <param name="fiatCoin">Fiat coin</param>
        public static void SetFiatCoin(FiatCoin fiatCoin)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setFiatCoin",fiatCoin.ToString());
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.setFiatCoin(fiatCoin.ToString());
#else

#endif
        }

        public static void SetSecurityAccountConfig(SecurityAccountConfig config)
        {
            var json = JsonConvert.SerializeObject(config);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setSecurityAccountConfig",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setSecurityAccountConfig(json);
#else
#endif
        }

        public static void SetWebAuthConfig(bool displayWallet, Appearance appearance)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "display_wallet", displayWallet },
                { "appearance", appearance.ToString() },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setWebAuthConfig",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setWebAuthConfig(json);
#else
#endif
        }

        public static void CallNative(string methodName, params object[] args)
        {
            Debug.Log("CallNative_methodName " + methodName );

            GetAndroidJavaObject().Call("runOnUiThread",
                new AndroidJavaRunnable(() => { GetUnityBridgeClass().CallStatic(methodName, args); }));
        }


        public static void CallConnectNative(string methodName, params object[] args)
        {
            Debug.Log("CallConnectNative_methodName " + methodName);

            GetAndroidJavaObject().Call("runOnUiThread",
                new AndroidJavaRunnable(() => { GetUnityConnectBridgeClass().CallStatic(methodName, args); }));
        }

        public static void CallAuthCoreNative<T>(string methodName, params object[] args)
        {
            Debug.Log("CallAuthCoreNative_methodName " + methodName);

            GetAndroidJavaObject().Call("runOnUiThread",
                new AndroidJavaRunnable(() => { GetAuthCoreBridgeClass().CallStatic<T>(methodName, args); }));
        }

        public static void CallAuthCoreNative(string methodName, params object[] args)
        {
            Debug.Log("CallAuthCoreNative_methodName " + methodName);

            GetAndroidJavaObject().Call("runOnUiThread",
                new AndroidJavaRunnable(() => { GetAuthCoreBridgeClass().CallStatic(methodName, args); }));
        }


        private static AndroidJavaObject GetAndroidJavaObject()
        {
            if (activityObject != null)
            {
                return activityObject;
            }

            var jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            activityObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
            return activityObject;
        }


        public static AndroidJavaClass GetUnityBridgeClass()
        {
            if (unityBridge != null)
            {
                return unityBridge;
            }

            unityBridge = new AndroidJavaClass("network.particle.unity.UnityBridge");
            return unityBridge;
        }

        public static AndroidJavaClass GetUnityConnectBridgeClass()
        {
            if (unityConnectBridge != null)
            {
                return unityConnectBridge;
            }

            unityConnectBridge = new AndroidJavaClass("network.particle.unity.UnityBridgeConnect");
            return unityConnectBridge;
        }

        public static AndroidJavaClass GetAuthCoreBridgeClass()
        {
            if (unityAuthCoreBridge != null)
            {
                return unityAuthCoreBridge;
            }

            unityAuthCoreBridge = new AndroidJavaClass("network.particle.unity.UnityBridgeAuthCore");
            return unityAuthCoreBridge;
        }

        public static string GetPrivateKey()
        {
            Assert.IsNotNull(currChainInfo, "currChainInfo is null,you must call ParticleNetwork.Init() first");
#if UNITY_EDITOR
            var userInfo = PersistTools.GetUserInfo();
            var wallets = userInfo.Wallets;

            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                var evmPrivateKey = wallets.FirstOrDefault(x => x.ChainName == "evm_chain")?.PrivateKey;
                return evmPrivateKey;
            }
            else
            {
                var solanaPrivateKey = wallets.FirstOrDefault(x => x.ChainName == "solana")?.PrivateKey;
                return solanaPrivateKey;
            }
#else
            return "";
#endif
        }
    }
}
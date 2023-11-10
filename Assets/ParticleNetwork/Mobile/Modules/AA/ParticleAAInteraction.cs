using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Core
{
    public static class ParticleAAInteraction
    {
        public static void Init(Dictionary<int, string> biconomyApiKeys)
        {
            var obj = new JObject
            {
                { "biconomy_api_keys", JObject.FromObject(biconomyApiKeys) },
            };

            var json = JsonConvert.SerializeObject(obj);

            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("particleAAInitialize",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.particleAAInitialize(json);
#else

#endif
        }

        public static void EnableAAMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("enableAAMode");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.enableAAMode();
#else

#endif
        }

        public static void DisableAAMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("disableAAMode");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.disableAAMode();
#else

#endif
        }

        public static bool IsAAModeEnable()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("isAAModeEnable");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.isAAModeEnable();
#else
            return false;
#endif
        }

        public static void IsDeploy(string eoaAddress)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("isDeploy",eoaAddress);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.isDeploy(eoaAddress);
#else

#endif
        }

        public static void RpcGetFeeQuotes(string eoaAddress, List<string> transactions)
        {
            var obj = new JObject
            {
                { "eoa_address", eoaAddress },
                { "transactions", JToken.FromObject(transactions) },
            };

            var json = JsonConvert.SerializeObject(obj);

            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("rpcGetFeeQuotes",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.rpcGetFeeQuotes(json);
#else

#endif
        }

        public static bool IsSupportChainInfo(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.Name },
                { "chain_id", chainInfo.Id },
                { "chain_id_name", chainInfo.Network },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("isSupportChainInfo",json);
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.isSupportChainInfo(json);
#else
            return false;
#endif
        }
    }
}
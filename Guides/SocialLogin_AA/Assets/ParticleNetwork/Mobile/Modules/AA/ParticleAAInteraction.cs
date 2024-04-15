using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Core
{
    public static class ParticleAAInteraction
    {
        public static void Init(AAAccountName accountName,
            Dictionary<int, string> biconomyApiKeys)
        {
            var obj = new JObject
            {
                { "biconomy_api_keys", JObject.FromObject(biconomyApiKeys) },
                { "name", accountName.name },
                { "version", accountName.version },
            };

            var json = JsonConvert.SerializeObject(obj);

            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("particleAAInitialize",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.particleAAInitialize(json);
#else

#endif
        }

        public static void EnableAAMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("enableAAMode");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.enableAAMode();
#else

#endif
        }

        public static void DisableAAMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("disableAAMode");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.disableAAMode();
#else

#endif
        }

        public static bool IsAAModeEnable()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
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
            ParticleNetwork.CallNative("rpcGetFeeQuotes",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.rpcGetFeeQuotes(json);
#else

#endif
        }
    }
}
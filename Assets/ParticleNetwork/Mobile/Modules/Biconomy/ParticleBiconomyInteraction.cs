
using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Core
{
    public static class ParticleBiconomyInteraction
    {
        public static void Init(BiconomyVersion version, Dictionary<int, string> dappApiKeys)
        {
            var versionString = "";
            if (version == BiconomyVersion.V1_0_0)
            {
                versionString = "1.0.0";
            }
            var obj = new JObject
            {
                { "version", versionString },
                { "dapp_api_keys", JObject.FromObject(dappApiKeys) },
            };
            
            var json = JsonConvert.SerializeObject(obj);
            
            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            // ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.particleBiconomyInitialize(json);
#else

#endif
            
        }

        public static void EnableBiconomyMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            // ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.enableBiconomyMode();
#else

#endif
        }
        
        public static void DisableBiconomyMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            // ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.disableBiconomyMode();
#else

#endif
        }
        
        public static bool IsBiconomyModeEnable()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
return false;
            // ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.isBiconomyModeEnable();
#else
            return false;
#endif
        }

        public static void IsDeploy(string eoaAddress)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            // ParticleNetwork.CallNative("login",json);
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
            // ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.rpcGetFeeQuotes(json);
#else

#endif
        }

        public static bool IsSupportChainInfo(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.getChainName() },
                { "chain_id", chainInfo.getChainId() },
                { "chain_id_name", chainInfo.getChainIdName() },
            });
            
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
return false;
            // ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.isSupportChainInfo(json);
#else
            return false;
#endif

        }
    }
}
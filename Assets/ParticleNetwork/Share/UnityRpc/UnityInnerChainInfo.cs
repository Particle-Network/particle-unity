using Network.Particle.Scripts.Core.Utils;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Network.Particle.Scripts.Core
{
    public static class UnityInnerChainInfo
    {
        private static ChainInfo currChainInfo;

        public static ChainInfo GetChainInfo()
        {
            var resultJson = "";
            
#if UNITY_ANDROID && !UNITY_EDITOR
            Assert.IsNotNull(currChainInfo, "currChainInfo is null, you must call ParticleNetwork.Init() first");
            resultJson = ParticleNetwork.GetUnityBridgeClass().CallStatic<string>("getChainInfo");

            Debug.Log($"GetChainInfo json: {resultJson}");
            var data = JObject.Parse(resultJson);
            var chainInfo = ChainUtils.FindChain(data["chain_name"].ToString(), (int)data["chain_id"]);
            return chainInfo;
#elif UNITY_IOS && !UNITY_EDITOR
            Assert.IsNotNull(currChainInfo, "currChainInfo is null, you must call ParticleNetwork.Init() first");
            resultJson = ParticleNetworkIOSBridge.getChainInfo();
            
            Debug.Log($"GetChainInfo json: {resultJson}");
            var data = JObject.Parse(resultJson);
            var chainInfo = ChainUtils.FindChain(data["chain_name"].ToString(), (int)data["chain_id"]);
            return chainInfo;
#else
            
            return currChainInfo;
#endif
            
        }

        public static void SetChainInfo(ChainInfo chainInfo)
        {
            currChainInfo = chainInfo;
        }
    }
}
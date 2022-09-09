using System;
using Network.Particle.Scripts.Core.Utils;
using Network.Particle.Scripts.Model;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class ParticleNetworkSDKDemo:MonoBehaviour
    {

        public void TestChainChoice()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"xxhong {chainInfo.getChainName()} {chainInfo.getChainId()} {chainInfo.getChainIdName()}");
            });
        }
    }
}
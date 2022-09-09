using System;
using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Core.Utils
{
    public class ChainUtils
    {
        public static ChainInfo CreateChain(string chainName, int chainId)
        {
            var chainInfo = (ChainInfo)Activator.CreateInstance(
                Type.GetType($"Network.Particle.Scripts.Model.{chainName}Chain"),
                chainId);
            return chainInfo;
        }
      
    }
}
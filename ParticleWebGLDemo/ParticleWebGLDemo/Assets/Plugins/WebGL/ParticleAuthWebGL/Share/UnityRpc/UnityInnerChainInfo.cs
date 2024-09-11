using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Core
{
    public static class UnityInnerChainInfo
    {
        private static ChainInfo currChainInfo;

        public static ChainInfo GetChainInfo()
        {
            return currChainInfo;
        }

        public static void SetChainInfo(ChainInfo chainInfo)
        {
            currChainInfo = chainInfo;
        }
    }
}
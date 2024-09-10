using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Core.Utils
{
    public class ChainUtils
    {
        public static ChainInfo FindChain(string chainNameString, long chainId)
        {
            var chainInfo = ChainInfo.GetEvmChain(chainId);
            if (chainInfo != null)
            {
                return chainInfo;
            }

            chainInfo = ChainInfo.GetSolanaChain(chainId);

            if (chainInfo != null)
            {
                return chainInfo;
            }

            return ChainInfo.Ethereum;
        }
    }
}
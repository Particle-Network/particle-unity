
using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class BuyCryptoConfig
    {
        [CanBeNull] public string walletAddress;
        public OpenBuyNetwork? network;
        [CanBeNull] public string cryptoCoin;
        [CanBeNull] public string fiatCoin;
        public int? fiatAmt;
        
        
        public BuyCryptoConfig([CanBeNull] string walletAddress, OpenBuyNetwork? network, [CanBeNull] string cryptoCoin, [CanBeNull] string fiatCoin, int? fiatAmt)
        {
            this.walletAddress = walletAddress;
            this.network = network;
            this.cryptoCoin = cryptoCoin;
            this.fiatCoin = fiatCoin;
            this.fiatAmt = fiatAmt;
        }
    }
}
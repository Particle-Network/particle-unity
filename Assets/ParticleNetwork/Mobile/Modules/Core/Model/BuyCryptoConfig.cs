
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
        
        
        /// <summary>
        /// Buy crypto config
        /// </summary>
        /// <param name="walletAddress">A wallet address to receive the purchased crypto</param>
        /// <param name="network">Choose a chain network to receive crypto</param>
        /// <param name="cryptoCoin">Coin symbol you want to bug, for example "USDT", "ETH", "SOL"</param>
        /// <param name="fiatCoin">Fiat symbol you pay, for example "USD", "GBP", "HKD"</param>
        /// <param name="fiatAmt">how much you want to pay</param>
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
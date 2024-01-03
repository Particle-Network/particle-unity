using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class BuyCryptoConfig
    {
        [CanBeNull] public string WalletAddress;
        public OpenBuyNetwork? Network;
        [CanBeNull] public string CryptoCoin;
        [CanBeNull] public string FiatCoin;
        public int? FiatAmt;
        public bool FixFiatCoin = false;
        public bool FixFiatAmt = false;
        public bool FixCryptoCoin = false;
        public Theme? Theme = null;
        public Language? Language = null;

        /// <summary>
        /// Buy crypto config
        /// </summary>
        /// <param name="walletAddress">A wallet address to receive the purchased crypto</param>
        /// <param name="network">Choose a chain network to receive crypto</param>
        /// <param name="cryptoCoin">Coin symbol you want to buy, for example "USDT", "ETH", "SOL"</param>
        /// <param name="fiatCoin">Fiat symbol you pay, for example "USD", "GBP", "HKD"</param>
        /// <param name="fiatAmt">How much you want to pay</param>
        public BuyCryptoConfig([CanBeNull] string walletAddress,
            OpenBuyNetwork? network,
            [CanBeNull] string cryptoCoin,
            [CanBeNull] string fiatCoin,
            int? fiatAmt)
        {
            this.WalletAddress = walletAddress;
            this.Network = network;
            this.CryptoCoin = cryptoCoin;
            this.FiatCoin = fiatCoin;
            this.FiatAmt = fiatAmt;
        }

        /// <summary>
        /// Buy crypto config
        /// </summary>
        /// <param name="walletAddress">A wallet address to receive the purchased crypto</param>
        /// <param name="network">Choose a chain network to receive crypto</param>
        /// <param name="cryptoCoin">Coin symbol you want to buy, for example "USDT", "ETH", "SOL"</param>
        /// <param name="fiatCoin">Fiat symbol you pay, for example "USD", "GBP", "HKD"</param>
        /// <param name="fiatAmt">How much you want to pay</param>
        /// <param name="fixFiatCoin">Fix fiat coin</param>
        /// <param name="fixFiatAmt">Fix fiat amount</param>
        /// <param name="fixCryptoCoin">Fix crypto coin</param>
        /// <param name="theme">Theme</param>
        /// <param name="language">Language</param>
        public BuyCryptoConfig([CanBeNull] string walletAddress,
            OpenBuyNetwork? network,
            [CanBeNull] string cryptoCoin,
            [CanBeNull] string fiatCoin,
            int? fiatAmt,
            bool fixFiatCoin,
            bool fixFiatAmt,
            bool fixCryptoCoin,
            Theme theme,
            Language language)
        {
            this.WalletAddress = walletAddress;
            this.Network = network;
            this.CryptoCoin = cryptoCoin;
            this.FiatCoin = fiatCoin;
            this.FiatAmt = fiatAmt;
            this.FixFiatCoin = fixFiatCoin;
            this.FixFiatAmt = fixFiatAmt;
            this.FixCryptoCoin = fixCryptoCoin;
            this.Theme = theme;
            this.Language = language;
        }
    }
}
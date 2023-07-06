using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class BiconomyFeeMode
    {
        public string option;
        [CanBeNull] public object feeQuote;
        
        /// <summary>
        /// Buy crypto config
        /// </summary>
        /// <param name="option">A wallet address to receive the purchased crypto</param>
        /// <param name="feeQuote">Choose a chain network to receive crypto</param>
        private BiconomyFeeMode(string option, [CanBeNull] object feeQuote)
        {
            this.option = option;
            this.feeQuote = feeQuote;
        }

        /// <summary>
        /// Auto mode, use native to pay gas fee.
        /// </summary>
        /// <returns></returns>
        public static BiconomyFeeMode Auto()
        {
            return new BiconomyFeeMode("auto", null);
        }


        /// <summary>
        /// Gasless mode, user doesn't pay gas fee.
        /// </summary>
        /// <returns></returns>
        public static BiconomyFeeMode Gasless()
        {
            return new BiconomyFeeMode("gasless", null);
        }
        
        /// <summary>
        /// Custom gas fee mode, select one fee quote from RpcGetFeeQuotes to pay gas fee.
        /// </summary>
        /// <param name="feeQuote">Fee quote, get from RpcGetFeeQuotes</param>
        /// <returns></returns>
        public static BiconomyFeeMode Custom(object feeQuote)
        {
            return new BiconomyFeeMode("custom", feeQuote);
        }
    }
}
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    public class AAFeeMode
    {
        public string option;
        [JsonProperty(PropertyName = "fee_quote")] 
        [CanBeNull] public object feeQuote;
        
        /// <summary>
        /// Buy crypto config
        /// </summary>
        /// <param name="option">A wallet address to receive the purchased crypto</param>
        /// <param name="feeQuote">Choose a chain network to receive crypto</param>
        private AAFeeMode(string option, [CanBeNull] object feeQuote)
        {
            this.option = option;
            this.feeQuote = feeQuote;
        }

        /// <summary>
        /// Auto mode, use native to pay gas fee.
        /// </summary>
        /// <returns></returns>
        public static AAFeeMode Auto()
        {
            return new AAFeeMode("auto", null);
        }


        /// <summary>
        /// Gasless mode, user doesn't pay gas fee.
        /// </summary>
        /// <returns></returns>
        public static AAFeeMode Gasless()
        {
            return new AAFeeMode("gasless", null);
        }
        
        /// <summary>
        /// Custom gas fee mode, select one fee quote from RpcGetFeeQuotes to pay gas fee.
        /// </summary>
        /// <param name="feeQuote">Fee quote, get from RpcGetFeeQuotes</param>
        /// <returns></returns>
        public static AAFeeMode Custom(object feeQuote)
        {
            return new AAFeeMode("custom", feeQuote);
        }
    }
}
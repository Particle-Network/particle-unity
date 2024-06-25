using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Particle.Scripts.Model
{
    public class AAFeeMode
    {
        public string option;

        [JsonProperty(PropertyName = "fee_quote")] [CanBeNull]
        public object feeQuote;

        [JsonProperty(PropertyName = "token_paymaster_address")] [CanBeNull]
        public string tokenPaymasterAddress;

        [JsonProperty(PropertyName = "whole_fee_quote")] [CanBeNull]
        public object wholeFeeQuote;

        /// <summary>
        /// Buy crypto config
        /// </summary>
        /// <param name="option">A wallet address to receive the purchased crypto</param>
        /// <param name="feeQuote">Choose a chain network to receive crypto</param>
        private AAFeeMode(string option, [CanBeNull] object feeQuote, [CanBeNull] string tokenPaymasterAddress,
            [CanBeNull] object wholeFeeQuote)
        {
            this.option = option;
            this.feeQuote = feeQuote;
            this.tokenPaymasterAddress = tokenPaymasterAddress;
            this.wholeFeeQuote = wholeFeeQuote;
        }

        /// <summary>
        /// select native for fee
        /// There are two use cases.
        /// pass null as wholeFeeQuote, to send a user paid transaction, use native as gas fee.
        /// specify wholeFeeQuote, that you can get from rpcGetFeeQuotes, to send a user paid transaction, use native as gas fee.
        /// </summary>
        /// <param name="wholeFeeQuote"></param>
        /// <returns></returns>
        public static AAFeeMode Native([CanBeNull] object wholeFeeQuote)
        {
            return new AAFeeMode("native", null, null, wholeFeeQuote);
        }


        /// <summary>
        /// gasless
        /// There are two use cases.
        /// pass null as wholeFeeQuote, to send a gasless transaction.
        /// specify wholeFeeQuote, that you can get from rpcGetFeeQuotes, to send a gasless transaction
        /// </summary>
        /// <param name="wholeFeeQuote"></param>
        /// <returns></returns>
        public static AAFeeMode Gasless([CanBeNull] object wholeFeeQuote)
        {
            return new AAFeeMode("gasless", null, null, wholeFeeQuote);
        }

        /// <summary>
        /// Select token for fee
        /// specify feeQuote and tokenPaymasterAddress, that you can get from rpcGetFeeQuotes, to send a user paid transaction, use token as gas fee.
        /// </summary>
        /// <param name="feeQuote">Fee quote, get from RpcGetFeeQuotes</param>
        /// <param name="tokenPaymasterAddress">TokenPaymasterAddress</param>
        /// <returns></returns>
        public static AAFeeMode Token(object feeQuote, string tokenPaymasterAddress)
        {
            return new AAFeeMode("token", feeQuote, tokenPaymasterAddress, null);
        }
    }
}
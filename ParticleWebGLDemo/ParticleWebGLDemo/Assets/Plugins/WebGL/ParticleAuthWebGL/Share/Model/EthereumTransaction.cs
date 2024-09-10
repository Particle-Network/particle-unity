#nullable enable
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class EthereumTransaction
    {
        public string from;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? to;

        public string data;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? gasLimit;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? gasPrice;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? value;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? nonce;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? type;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? chainId;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? maxPriorityFeePerGas;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? maxFeePerGas;


        public EthereumTransaction(string from, string? to, string data, string? gasLimit, string? gasPrice,
            string? value, string? nonce, string? type, string? chainId, string? maxPriorityFeePerGas,
            string? maxFeePerGas)
        {
            this.from = from;
            this.to = to;
            this.data = data;
            this.gasLimit = gasLimit;
            this.gasPrice = gasPrice;
            this.value = value;
            this.nonce = nonce;
            this.type = type;
            this.chainId = chainId;
            this.maxPriorityFeePerGas = maxPriorityFeePerGas;
            this.maxFeePerGas = maxFeePerGas;
        }
    }
}
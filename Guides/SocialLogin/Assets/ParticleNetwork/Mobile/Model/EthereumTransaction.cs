#nullable enable
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class EthereumTransaction
    {

        public string from;

        public string? to;

        public string data;
        
        public string? gasLimit;
        
        public string? gasPrice;

        public string? value;

        public string? nonce;

        public string? type;

        public string? chainId;

        public string? maxPriorityFeePerGas;

        public string? maxFeePerGas;


        public EthereumTransaction(string from, string? to, string data, string? gasLimit, string? gasPrice, string? value, string? nonce, string? type, string? chainId, string? maxPriorityFeePerGas, string? maxFeePerGas)
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
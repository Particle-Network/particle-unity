using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    public class RpcUrl
    {
        [JsonProperty(PropertyName = "evm_url")]
        public string evmUrl;

        [JsonProperty(PropertyName = "sol_url")]
        public string solUrl;

        public RpcUrl(string evmUrl, string solUrl)
        {
            this.evmUrl = evmUrl;
            this.solUrl = solUrl;
        }
    }
}
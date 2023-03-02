using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class NativeCurrency
    {
        [JsonProperty(PropertyName = "name")] 
        public string Name;
        [JsonProperty(PropertyName = "symbol")] 
        public string Symbol;
        [JsonProperty(PropertyName = "decimals")] 
        public uint Decimals;

        public NativeCurrency(string name, string symbol, uint decimals)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Decimals = decimals;
        }
    }
    
}
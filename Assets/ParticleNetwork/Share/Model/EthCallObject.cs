using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class EthCallObject
    {
        [JsonProperty(PropertyName = "from")] public string from;
        [JsonProperty(PropertyName = "data")] public string data;
        [JsonProperty(PropertyName = "value")] public string value;
        [JsonProperty(PropertyName = "to")] public string to;

        public EthCallObject(string from, string to, string value, string data)
        {
            this.from = from;
            this.to = to;
            this.data = data;
            this.value = value;
        }
    }
}
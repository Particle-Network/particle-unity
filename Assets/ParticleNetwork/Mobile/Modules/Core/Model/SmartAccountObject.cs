using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class SmartAccountObject
    {
        [JsonProperty(PropertyName = "name")] public string name;

        [JsonProperty(PropertyName = "version")]
        public string version;

        [JsonProperty(PropertyName = "ownerAddress")]
        public string ownerAddress;

        public SmartAccountObject(string name, string version, string ownerAddress)
        {
            this.name = name;
            this.version = version;
            this.ownerAddress = ownerAddress;
        }
    }
}
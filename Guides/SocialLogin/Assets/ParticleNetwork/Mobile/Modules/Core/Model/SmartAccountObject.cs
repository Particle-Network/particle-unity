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

        public SmartAccountObject(AAAccountName accountName, string ownerAddress)
        {
            this.name = accountName.name;
            this.version = accountName.version;
            this.ownerAddress = ownerAddress;
        }
    }
}
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class NativeErrorData
    {
        [JsonProperty(PropertyName = "message")]
        public string Message;
        [JsonProperty(PropertyName = "code")]
        public int Code;
    }
}
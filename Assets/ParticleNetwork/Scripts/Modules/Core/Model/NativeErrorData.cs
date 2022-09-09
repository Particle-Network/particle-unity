
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
        [JsonProperty(PropertyName = "data")] 
        public string Data;
    }
}
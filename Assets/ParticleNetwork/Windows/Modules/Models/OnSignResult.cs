#if !UNITY_ANDROID && !UNITY_IOS
using Newtonsoft.Json;

namespace Particle.Windows.Modules.Models
{
    [JsonObject]
    public class OnSignResult
    {
        [JsonProperty(PropertyName = "method")] 
        public string Method;
        
        [JsonProperty(PropertyName = "signature")] 
        public string Signature;
        
        [JsonProperty(PropertyName = "error")] 
        public OnSignResultError Error;
    }
    
    [JsonObject]
    public class OnSignResultError
    {
        [JsonProperty(PropertyName = "code")] 
        public int Code;
        
        [JsonProperty(PropertyName = "message")] 
        public string Message;
        
    }
}
#endif
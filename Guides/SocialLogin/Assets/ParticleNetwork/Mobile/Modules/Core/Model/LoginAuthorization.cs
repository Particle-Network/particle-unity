using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    public class LoginAuthorization
    {
        [JsonProperty(PropertyName = "message")] 
        public string Message;
        [JsonProperty(PropertyName = "uniq")] 
        public bool Uniq;
        
        public LoginAuthorization(string message,
            bool uniq = false)
        {
            this.Message = message;
            this.Uniq = uniq;
        }
        
    }
}
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class SimplifyTransaction
    {
        public string to;

        public string data;

        public string value;

        public SimplifyTransaction(string to, string data, string value)
        {
            this.to = to;
            this.data = data;
            this.value = value;
        }
    }
}
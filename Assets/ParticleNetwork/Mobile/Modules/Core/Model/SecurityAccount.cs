using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class SecurityAccount
    {
        [JsonProperty(PropertyName = "has_set_master_password")] 
        public bool HasMasterPassword;

        [JsonProperty(PropertyName = "has_set_payment_password")] 
        public bool HasPaymentPassword;

        [JsonProperty(PropertyName = "email")] 
        [CanBeNull] public string Email;
        
        [JsonProperty(PropertyName = "phone")] 
        [CanBeNull] public string Phone;

        public SecurityAccount(bool hasMasterPassword, bool hasPaymentPassword, [CanBeNull] string email, [CanBeNull] string phone)
        {
            this.HasMasterPassword = hasMasterPassword;
            this.HasPaymentPassword = hasPaymentPassword;
            this.Email = email;
            this.Phone = phone;
        }
    }
}
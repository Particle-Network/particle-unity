using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [JsonObject]
    public class SecurityAccountConfig
    {
        [JsonProperty(PropertyName = "prompt_setting_when_sign")]
        public int promptSettingWhenSign;

        /// <summary>
        /// Security account config
        /// </summary>
        /// <param name="promptSettingWhenSign">
        /// you can choose one of 0, 1, 2.
        /// 
        /// 0 don't show prompt when sign in web.
        /// 
        /// 1 show prompt when first sign only.
        /// 
        /// 2 show prompt when sign every time.
        ///
        /// default value is 1.
        /// </param>
        public SecurityAccountConfig(int promptSettingWhenSign)
        {
            this.promptSettingWhenSign = promptSettingWhenSign;
        }
    }
}
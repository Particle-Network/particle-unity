using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ConnectConfig
    {
        public LoginType loginType;
        public bool loginFormMode;
        [CanBeNull] public string account;
        public SupportAuthType supportAuthTypes;
        public SocialLoginPrompt? socialLoginPrompt;

        /// <summary>
        /// Particle connect configuration
        /// </summary>
        /// <param name="loginType">Login type, support email, phone, json web token, google, apple and more</param>
        /// <param name="account">Account, such as phone number, email, json web token.</param>
        /// <param name="supportAuthTypes">Controls whether third-party login buttons are displayed.</param>
        /// <param name="loginFormMode">Controls whether show light UI in web, default is false.</param>
        /// <param name="socialLoginPrompt">Controls whether show light UI in web, default is false.</param>
        public ConnectConfig(LoginType loginType, [CanBeNull] string account, SupportAuthType supportAuthTypes,
            bool loginFormMode = false, SocialLoginPrompt? socialLoginPrompt = null)
        {
            this.loginType = loginType;
            this.account = account;
            this.supportAuthTypes = supportAuthTypes;
            this.loginFormMode = loginFormMode;
            this.socialLoginPrompt = socialLoginPrompt;
        }
    }
}
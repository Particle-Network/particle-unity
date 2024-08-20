using System.Collections.Generic;
using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ConnectConfig
    {
        public LoginType loginType;
        [CanBeNull] public string account;
        [CanBeNull] public string code;
        [CanBeNull] public List<SupportLoginType> supportLoginTypes;
        public SocialLoginPrompt? socialLoginPrompt;
        [CanBeNull] public LoginPageConfig loginPageConfig;

        /// <summary>
        /// Particle connect configuration
        /// </summary>
        /// <param name="loginType">Login type, support email, phone, json web token, google, apple and more</param>
        /// <param name="account">Optional, phone number, email or json web token.</param>
        /// <param name="code">Optional, email or phone code, used with auth core sdk</param>
        /// <param name="supportLoginTypes">Controls whether third-party login buttons are displayed.</param>
        /// <param name="socialLoginPrompt">Optional, controls whether show light UI in web, default is false.</param>
        /// <param name="authCoreLoginPageConfig">Optional, LoginPageConfig, for customize login page when use ParticleAuthCore</param>
        public ConnectConfig(LoginType loginType, [CanBeNull] string account = null, [CanBeNull] string code = null,
            [CanBeNull] List<SupportLoginType> supportLoginTypes = null,
            SocialLoginPrompt? socialLoginPrompt = null, [CanBeNull] LoginPageConfig authCoreLoginPageConfig = null)
        {
            this.loginType = loginType;
            this.account = account;
            this.code = code;
            this.supportLoginTypes = supportLoginTypes;
            this.socialLoginPrompt = socialLoginPrompt;
            this.loginPageConfig = authCoreLoginPageConfig;
        }
    }
}
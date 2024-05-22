
using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ConnectConfig
    {
        public LoginType loginType;
        [CanBeNull] public string account;
        [CanBeNull] public string code;
        public SupportAuthType supportAuthTypes;
        public SocialLoginPrompt? socialLoginPrompt;
        [CanBeNull] public LoginAuthorization authorization;
        [CanBeNull] public LoginPageConfig loginPageConfig;

        /// <summary>
        /// Particle connect configuration
        /// </summary>
        /// <param name="loginType">Login type, support email, phone, json web token, google, apple and more</param>
        /// <param name="account">Optional, phone number, email or json web token.</param>
        /// <param name="code">Optional, email or phone code, used with auth core sdk</param>
        /// <param name="supportAuthTypes">Controls whether third-party login buttons are displayed.</param>
        /// <param name="socialLoginPrompt">Optional, controls whether show light UI in web, default is false.</param>
        /// <param name="authorization">Optional, LoginAuthorization, , login and sign message, its message requires hex in evm, base58 in solana </param>
        public ConnectConfig(LoginType loginType, [CanBeNull] string account, [CanBeNull] string code,
            SupportAuthType supportAuthTypes,  [CanBeNull] LoginAuthorization authorization = null,
            SocialLoginPrompt? socialLoginPrompt = null, [CanBeNull] LoginPageConfig authCoreLoginPageConfig = null)
        {
            this.loginType = loginType;
            this.account = account;
            this.code = code;
            this.supportAuthTypes = supportAuthTypes;
            this.socialLoginPrompt = socialLoginPrompt;
            this.authorization = authorization;
            this.loginPageConfig = authCoreLoginPageConfig;
        }
    }
}
using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ParticleAuthConnectConfig
    {
        public LoginType loginType;
        [CanBeNull] public string account;
        public SupportAuthType supportAuthTypes;
        public SocialLoginPrompt? socialLoginPrompt;
        [CanBeNull] public LoginAuthorization authorization;

        /// <summary>
        /// Particle connect configuration
        /// </summary>
        /// <param name="loginType">Login type, support email, phone, json web token, google, apple and more</param>
        /// <param name="account">Account, such as phone number, email, json web token.</param>
        /// <param name="supportAuthTypes">Controls whether third-party login buttons are displayed.</param>
        /// <param name="socialLoginPrompt">Controls whether show light UI in web, default is false.</param>
        /// <param name="authorization">LoginAuthorization, optional, login and sign message, its message requires hex in evm, base58 in solana </param>
        public ParticleAuthConnectConfig(LoginType loginType, [CanBeNull] string account, SupportAuthType supportAuthTypes, [CanBeNull] LoginAuthorization authorization = null, SocialLoginPrompt? socialLoginPrompt = null)
        {
            this.loginType = loginType;
            this.account = account;
            this.supportAuthTypes = supportAuthTypes;
            this.socialLoginPrompt = socialLoginPrompt;
            this.authorization = authorization;
        }
    }
}
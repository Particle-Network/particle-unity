using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ConnectConfig
    {
        private LoginType loginType;
        private bool loginFormMode;
        [CanBeNull] private string account;
        private SupportAuthType supportAuthTypes;

        /// <summary>
        /// Particle connect configuration
        /// </summary>
        /// <param name="loginType">Login type, support email, phone, json web token, google, apple and more</param>
        /// <param name="account">Account, such as phone number, email, json web token.</param>
        /// <param name="supportAuthTypes">Controls whether third-party login buttons are displayed.</param>
        /// <param name="loginFormMode">Controls whether show light UI in web, default is false.</param>
        public ConnectConfig(LoginType loginType, [CanBeNull] string account, SupportAuthType supportAuthTypes, bool loginFormMode = false)
        {
            this.loginType = loginType;
            this.account = account;
            this.supportAuthTypes = supportAuthTypes;
            this.loginFormMode = loginFormMode;
        }

        public LoginType LoginType
        {
            get => loginType;
            set => loginType = value;
        }

        [CanBeNull]
        public string Account
        {
            get => account;
            set => account = value;
        }

        public SupportAuthType SupportAuthTypes
        {
            get => supportAuthTypes;
            set => supportAuthTypes = value;
        }

        public bool LoginFormMode
        {
            get => loginFormMode;
            set => loginFormMode = value;
        }
    }
}
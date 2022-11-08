using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ConnectConfig
    {
        private LoginType loginType;
        
        [CanBeNull] private string account;
        private SupportAuthType supportAuthTypes;

        public ConnectConfig(LoginType loginType, [CanBeNull] string account, SupportAuthType supportAuthTypes)
        {
            this.loginType = loginType;
            this.account = account;
            this.supportAuthTypes = supportAuthTypes;
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
    }
}
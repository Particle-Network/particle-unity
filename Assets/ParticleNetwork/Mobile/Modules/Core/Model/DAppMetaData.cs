using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class DAppMetaData
    {
        public string walletConnectProjectId;
        public string name; //your app name
        public string icon; //your dapp icon url
        public string url; //your dapp website url
        public string description; // your dapp description
        [CanBeNull] public string redirect;
        [CanBeNull] public string verifyUrl;

        public DAppMetaData(string walletConnectProjectId, string name, string icon, string url, string description,
            [CanBeNull] string redirect = null, [CanBeNull] string verifyUrl = null)
        {
            this.walletConnectProjectId = walletConnectProjectId;
            this.name = name;
            this.icon = icon;
            this.url = url;
            this.description = description;
            this.redirect = redirect;
            this.verifyUrl = verifyUrl;
        }
    }
}
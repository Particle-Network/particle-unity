namespace Network.Particle.Scripts.Model
{
    public class WalletMetaData
    {
        public string name; // your app name
        public string icon; // your dapp icon url
        public string url; // your dapp website url
        public string description; // your dapp description
        public string walletConnectProjectId;

        public WalletMetaData(string name, string icon, string url, string description, string walletConnectProjectId)
        {
            this.name = name;
            this.icon = icon;
            this.url = url;
            this.description = description;
            this.walletConnectProjectId = walletConnectProjectId;
        }
    }
}
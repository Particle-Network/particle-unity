namespace Network.Particle.Scripts.Model
{
    public class DAppMetaData
    {
        public string name; //your app name
        public string icon; //your dapp icon url
        public string url; //your dapp website url

        public DAppMetaData(string name, string icon, string url)
        {
            this.name = name;
            this.icon = icon;
            this.url = url;
        }
    }
}
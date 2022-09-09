namespace Network.Particle.Scripts.Model
{
    public class DAppMetadata
    {
        public string name; //your app name
        public string icon; //your dapp icon url
        public string url; //your dapp website url

        public DAppMetadata(string name, string icon, string url)
        {
            this.name = name;
            this.icon = icon;
            this.url = url;
        }

        public static DAppMetadata Create(string name, string icon, string url)
        {
            return new DAppMetadata(name, icon, url);
        }
    }
}
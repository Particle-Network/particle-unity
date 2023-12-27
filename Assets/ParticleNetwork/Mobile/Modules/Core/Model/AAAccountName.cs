namespace Network.Particle.Scripts.Model
{
    public class AAAccountName
    {
        public string name;
        public string version;


        private AAAccountName(string name, string version)
        {
            this.name = name;
            this.version = version;
        }

        public static AAAccountName BICONOMY_V1()
        {
            return new AAAccountName("BICONOMY", "1.0.0");
        }

        public static AAAccountName BICONOMY_V2()
        {
            return new AAAccountName("BICONOMY", "2.0.0");
        }

        public static AAAccountName SIMPLE()
        {
            return new AAAccountName("SIMPLE", "1.0.0");
        }

        public static AAAccountName CYBERCONNECT()
        {
            return new AAAccountName("CYBERCONNECT", "1.0.0");
        }
    }
}
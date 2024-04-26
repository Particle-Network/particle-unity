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

        /// <summary>
        /// Biconomy Account 1.0.0
        /// </summary>
        /// <returns></returns>
        public static AAAccountName BICONOMY_V1()
        {
            return new AAAccountName("BICONOMY", "1.0.0");
        }

        /// <summary>
        /// Biconomy Account 2.0.0
        /// </summary>
        /// <returns></returns>
        public static AAAccountName BICONOMY_V2()
        {
            return new AAAccountName("BICONOMY", "2.0.0");
        }

        /// <summary>
        /// Simple Account 1.0.0
        /// </summary>
        /// <returns></returns>
        public static AAAccountName SIMPLE()
        {
            return new AAAccountName("SIMPLE", "1.0.0");
        }

        /// <summary>
        /// Cyber Account 1.0.0
        /// </summary>
        /// <returns></returns>
        public static AAAccountName CYBERCONNECT()
        {
            return new AAAccountName("CYBERCONNECT", "1.0.0");
        }

        /// <summary>
        /// Alchemy Light Account 1.0.2
        /// </summary>
        /// <returns></returns>
        public static AAAccountName LIGHT()
        {
            return new AAAccountName("LIGHT", "1.0.2");
        }
        
        /// <summary>
        /// Xterio Account 1.0.0
        /// </summary>
        /// <returns></returns>
        public static AAAccountName XTERIO()
        {
            return new AAAccountName("XTERIO", "1.0.0");
        }
        
        
    }
}
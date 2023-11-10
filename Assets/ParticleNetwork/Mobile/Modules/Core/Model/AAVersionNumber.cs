namespace Network.Particle.Scripts.Model
{
    public class AAVersionNumber
    {
        public string version;
        
        private AAVersionNumber(string version)
        {
            this.version = version;
        }

        public static AAVersionNumber V1_0_0()
        {
            return new AAVersionNumber("1.0.0");
        }
        
        
    }
}
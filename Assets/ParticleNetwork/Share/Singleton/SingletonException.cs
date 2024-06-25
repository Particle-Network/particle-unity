using System;

namespace Network.Particle.Scripts.Singleton
{
    public class SingletonException : Exception
    {
        public SingletonException(string msg)
            : base(msg)
        {
        }
    }
}
using System;

namespace Plugins.WebGL.ParticleAuthWebGL.Share
{
    public class ErrorException : Exception
    {
        public int Code { get; }

        public ErrorException(int code, string message) : base(message)
        {
            Code = code;
        }

        public ErrorException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
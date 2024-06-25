using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class Account
    {
        public string publicAddress;
        [CanBeNull] public string name;
        [CanBeNull] public string url;
        [CanBeNull] public string description;
        [CanBeNull] public List<string> icons = null;
        [CanBeNull] public string mnemonic;
        [CanBeNull] public long? chainId;
    }
}
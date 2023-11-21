using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core;
using UnityEngine;

namespace Network.Particle.Scripts.Model
{
    [Serializable]
    public class ParticleRpcRequest<T>
    {
        [SerializeField] public string id;
        [SerializeField] public string jsonrpc = "2.0";
        [SerializeField] [CanBeNull] public string method;
        [SerializeField] public T[] @params;
        [SerializeField] public long chainId;

        public ParticleRpcRequest()
        {
        }

        public ParticleRpcRequest(string method, T[] parameters)
        {
            id = Guid.NewGuid().ToString();
            this.method = method;
            @params = parameters;
            chainId = ParticleNetwork.GetChainInfo().Id;
        }
        
        public ParticleRpcRequest(string method, List<T> parameters)
        {
            id = Guid.NewGuid().ToString();
            this.method = method;
            @params = parameters.ToArray();
            chainId = ParticleNetwork.GetChainInfo().Id;
        }
    }
}
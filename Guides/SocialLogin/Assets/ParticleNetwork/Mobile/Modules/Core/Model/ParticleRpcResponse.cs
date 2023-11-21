using System;
using UnityEngine;

namespace Network.Particle.Scripts.Model
{
    [Serializable]
    internal class ParticleRpcResponse<T>
    {
        [SerializeField] internal string id;
        [SerializeField] internal string jsonrpc = "2.0";
        [SerializeField] internal T result;
        [SerializeField] internal Error error;

        public ParticleRpcResponse(string id, string jsonrpc, T result)
        {
            this.id = id;
            this.jsonrpc = jsonrpc;
            this.result = result;
        }
    }

    [Serializable]
    public class Error
    {
        public int code;
        public string message;
    }
}
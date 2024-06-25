using System.Collections.Generic;
using Network.Particle.Scripts.Core;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class UnityRPCDemo : MonoBehaviour
    {
        public async void GetTokensAndNFTs()
        {
            var json = await EvmService.GetTokensAndNFTs("0x529C4A6074Ce9Ea052eCde1AC8E4B935FCd78539",
                new List<string>());
            Debug.Log(json);
        }

        public async void GetTokens()
        {
            var json = await EvmService.GetTokens("0x529C4A6074Ce9Ea052eCde1AC8E4B935FCd78539",
                new List<string>());
            Debug.Log(json);
        }

        public async void GetNFTs()
        {
            var json = await EvmService.GetNFTs("0x529C4A6074Ce9Ea052eCde1AC8E4B935FCd78539",
                new List<string>());
            Debug.Log(json);
        }

        public async void GetPrice()
        {
            var json = await EvmService.GetPrice(new List<string> { "native" }, new List<string> { "usd" });
            Debug.Log(json);
        }

        public async void GetTransactionsByAddress()
        {
            var json = await EvmService.GetTransactionsByAddress("0x529C4A6074Ce9Ea052eCde1AC8E4B935FCd78539");
            Debug.Log(json);
        }

        public async void GetTokensByTokenAddresses()
        {
            var json = await EvmService.GetTokensByTokenAddresses("0x529C4A6074Ce9Ea052eCde1AC8E4B935FCd78539",
                new List<string> { "0x2C89bbc92BD86F8075d1DEcc58C7F4E0107f286b" });
            Debug.Log(json);
        }

        public async void Rpc_eth_getBalance()
        {
            var json = await EvmService.Rpc("eth_getBalance",
                new List<object> { "0x529C4A6074Ce9Ea052eCde1AC8E4B935FCd78539", "latest" });
            Debug.Log(json);
        }
    }
}
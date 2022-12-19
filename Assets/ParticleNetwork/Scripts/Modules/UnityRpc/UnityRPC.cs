using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Network.Particle.Scripts.Core
{

    /// <summary>
    /// evm-chain api 
    /// </summary>
    public class EvmService
    {
        public async static Task<string> Rpc(string method, List<object> parameters)
        {
            string path = "evm-chain/rpc";
            ParticleRpcRequest<object> request = new ParticleRpcRequest<object>(method, parameters);
            return await NetService.Request(path, request);
        }

        public async static Task<string> GetPrice(List<string> addresses, List<string> currencies)
        {
            return await Rpc(EvmReqBodyMethod.particleGetPrice, new List<object> { addresses, currencies });
        }

        public async static Task<string> GetTokensAndNFTs(string address)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTokensAndNFTs, new List<object> { address });
        }

        public async static Task<string> GetTransactionsByAddress(string address)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTransactionsByAddress, new List<object> { address });
        }

        public async static Task<string> SuggestedGasFees()
        {
            return await Rpc(EvmReqBodyMethod.particleSuggestedGasFees, new List<object> { });
        }

        public async static Task<string> EstimateGas(string from, string to, string value, string data)
        {
            var obj = new EthCallObject(from, to, value, data);
            return await Rpc(EvmReqBodyMethod.ethEstimateGas, new List<object> { obj });
        }

        public async static Task<string> Erc20Transfer(string contractAddress, string to, BigInteger amount)
        {
            var list2 = new List<string> { to, amount.ToString() };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc20Transfer, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        public async static Task<string> Erc20Approve(string contractAddress, string sender, BigInteger amount)
        {
            var list2 = new List<string> { sender, amount.ToString() };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc20Approve, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        public async static Task<string> Erc20TransferFrom(string contractAddress, string from, string to,
            BigInteger amount)
        {
            var list2 = new List<string> { from, to, amount.ToString() };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc20TransferFrom, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        public async static Task<string> Erc721SafeTransferFrom(string contractAddress, string from, string to,
            string tokenId)
        {
            var list2 = new List<string> { from, to, tokenId };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc721SafeTransferFrom, list2 };
            return await AbiEncodeFunctionCall(list1);
        }


        public async static Task<string> Erc1155SafeTransferFrom(string contractAddress, string from, string to,
            string id,
            BigInteger amount,
            string data)
        {
            var list2 = new List<string> { from, to, id, amount.ToString(), data };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc1155SafeTransferFrom, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        public async static Task<string> AbiEncodeFunctionCall(List<object> parameters)
        {
            return await Rpc(EvmReqBodyMethod.particleAbiEncodeFunctionCall, parameters);
        }

        public async static Task<string> AbiEncodeFunctionCall(string contractAddress, string methodName,
            List<object> parameters,
            string abiJsonString)
        {
            var list = new List<object> { contractAddress, methodName, parameters };
            if (!string.IsNullOrEmpty(abiJsonString)) list.Add(abiJsonString);
            return await Rpc(EvmReqBodyMethod.particleAbiEncodeFunctionCall, list);
        }

        //getTokensByTokenAddresses
        public async static Task<string> GetTokensByTokenAddresses(string address, List<string> tokenAddresses)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTokensByTokenAddresses,
                new List<object> { address, tokenAddresses });
        }
    }

    /// <summary>
    /// solana-chain api
    /// </summary>
    public class SolanaService
    {
        public async static Task<string> Rpc(string method, List<object> parameters)
        {
            string path = "solana/rpc";
            ParticleRpcRequest<object> request = new ParticleRpcRequest<object>(method, parameters);
            return await NetService.Request(path, request);
        }

        public async static Task<string> GetPrice(List<string> addresses, List<string> currencies)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetPrice, new List<object> { addresses, currencies });
        }

        public async static Task<string> GetTokensAndNFTs(string address)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetTokensAndNFTs, new List<object> { address });
        }

        public async static Task<string> GetTransactionsByAddress(string address)
        {
            Dictionary<string, bool> map = new Dictionary<string, bool>();
            map["parseMetadataUri"] = true;
            return await Rpc(SolanaReqBodyMethod.enhancedGetTransactionsByAddress,
                new List<object> { address, map });
        }

        public async static Task<string> GetTokenTransactionsByAddress(string address)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetTokenTransactionsByAddress,
                new List<object> { address });
        }

        public async static Task<string> SerializeSOLTransaction(SerializeSOLTransReq req)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedSerializeTransaction,
                new List<object> { SerializeTransactionParams.transferSol, req });
        }

        public async static Task<string> SerializeTokenTransaction(SerializeTokenTransReq req)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedSerializeTransaction,
                new List<object> { SerializeTransactionParams.transferToken, req });
        }

        public async static Task<string> GetTokensByTokenAddresses(string pubKey, List<string> tokenAddresses)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetTokensByTokenAddresses,
                new List<object> { pubKey, tokenAddresses });
        }
    }
}
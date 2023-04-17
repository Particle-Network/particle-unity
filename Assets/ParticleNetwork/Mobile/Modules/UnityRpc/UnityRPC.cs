using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json.Linq;


namespace Network.Particle.Scripts.Core
{
    /// <summary>
    /// evm-chain api 
    /// </summary>
    public class EvmService
    {
        
        /// <summary>
        /// Call rpc method
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="parameters">Method parameters</param>
        /// <returns></returns>
        public static async Task<string> Rpc(string method, List<object> parameters)
        {
            string path = "evm-chain";
            ParticleRpcRequest<object> request = new ParticleRpcRequest<object>(method, parameters);
            return await NodeService.Request(path, request);
        }

        /// <summary>
        /// Get token price
        /// </summary>
        /// <param name="addresses">Token addresses</param>
        /// <param name="currencies">Currencies</param>
        /// <returns></returns>
        public static async Task<string> GetPrice(List<string> addresses, List<string> currencies)
        {
            return await Rpc(EvmReqBodyMethod.particleGetPrice, new List<object> { addresses, currencies });
        }

        /// <summary>
        /// Get tokens and nfts
        /// </summary>
        /// <param name="address">Public address</param>
        /// <param name="tokenAddresses">Token addresses, pass empty to get all tokens, pass specified token addresses to get specified tokens</param>
        /// <returns></returns>
        public static async Task<string> GetTokensAndNFTs(string address, List<string> tokenAddresses)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTokensAndNFTs, new List<object> { address, tokenAddresses });
        }
        
        /// <summary>
        /// Get tokens 
        /// </summary>
        /// <param name="address">Public address</param>
        /// <param name="tokenAddresses">Token addresses, pass empty to get all tokens, pass specified token addresses to get specified tokens</param>
        /// <returns></returns>
        public static async Task<string> GetTokens(string address, List<string> tokenAddresses)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTokens, new List<object> { address, tokenAddresses });
        }
        
        /// <summary>
        /// Get tokens 
        /// </summary>
        /// <param name="address">Public address</param>
        /// <param name="nftContractAddresses">NFT contract addresses to filter, default get all NFTs</param>
        /// <returns></returns>
        public static async Task<string> GetNFTs(string address, List<string> nftContractAddresses)
        {
            return await Rpc(EvmReqBodyMethod.particleGetNFTs, new List<object> { address, nftContractAddresses });
        }

        /// <summary>
        /// Get transactions by public address
        /// </summary>
        /// <param name="address">Public address</param>
        /// <returns></returns>
        public static async Task<string> GetTransactionsByAddress(string address)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTransactionsByAddress, new List<object> { address });
        }

        /// <summary>
        /// Get suggested gas fees
        /// </summary>
        /// <returns></returns>
        public static async Task<string> SuggestedGasFees()
        {
            return await Rpc(EvmReqBodyMethod.particleSuggestedGasFees, new List<object> { });
        }

        /// <summary>
        /// Estimate gas
        /// </summary>
        /// <param name="from">From public address</param>
        /// <param name="to">To address</param>
        /// <param name="value">Value</param>
        /// <param name="data">Data</param>
        /// <returns></returns>
        public static async Task<string> EstimateGas(string from, string to, string value, string data)
        {
            var obj = new EthCallObject(from, to, value, data);
            return await Rpc(EvmReqBodyMethod.ethEstimateGas, new List<object> { obj });
        }

        /// <summary>
        /// Call erc20 transfer
        /// </summary>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="to">To address, usually its receiver public address</param>
        /// <param name="amount">Amount, transfer token amount, in minimum unit</param>
        /// <returns>Data</returns>
        public static async Task<string> Erc20Transfer(string contractAddress, string to, BigInteger amount)
        {
            var list2 = new List<string> { to, amount.ToString() };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc20Transfer, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        /// <summary>
        /// Call erc20 approve
        /// </summary>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="spender">Spender</param>
        /// <param name="amount">Amount, approve token amount, in minimum unit</param>
        /// <returns>Data</returns>
        public static async Task<string> Erc20Approve(string contractAddress, string spender, BigInteger amount)
        {
            var list2 = new List<string> { spender, amount.ToString() };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc20Approve, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        /// <summary>
        /// Call erc20 transfer from
        /// </summary>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="from">From public address</param>
        /// <param name="to">To public address</param>
        /// <param name="amount">Amount, transfer token amount, in minimum unit</param>
        /// <returns>Data</returns>
        public static async Task<string> Erc20TransferFrom(string contractAddress, string from, string to,
            BigInteger amount)
        {
            var list2 = new List<string> { from, to, amount.ToString() };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc20TransferFrom, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="from">From public address</param>
        /// <param name="to">To public address</param>
        /// <param name="tokenId">Token id</param>
        /// <returns>Data</returns>
        public static async Task<string> Erc721SafeTransferFrom(string contractAddress, string from, string to,
            string tokenId)
        {
            var list2 = new List<string> { from, to, tokenId };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc721SafeTransferFrom, list2 };
            return await AbiEncodeFunctionCall(list1);
        }


        /// <summary>
        /// Call erc1155 safe transfer from
        /// </summary>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="from">From public address</param>
        /// <param name="to">To public address</param>
        /// <param name="id">Token id</param>
        /// <param name="amount">Token amount</param>
        /// <param name="data">Data, could be "0x"</param>
        /// <returns>Data</returns>
        public static async Task<string> Erc1155SafeTransferFrom(string contractAddress, string from, string to,
            string id,
            BigInteger amount,
            string data)
        {
            var list2 = new List<string> { from, to, id, amount.ToString(), data };
            var list1 = new List<object> { contractAddress, AbiEncodeFunction.erc1155SafeTransferFrom, list2 };
            return await AbiEncodeFunctionCall(list1);
        }

        /// <summary>
        /// Call custom abi encode function
        /// </summary>
        /// <param name="parameters">Parameters in a list.
        /// order in list
        /// 0, contract address.
        /// 1, method name, you should add "custom_" before your method name, like "custom_balanceOf", "custom_mint".
        /// 2. method parameters.
        /// 3. optional, method abi json string. you can get it from your contract developer.
        /// </param>
        /// <returns>Data</returns>
        public static async Task<string> AbiEncodeFunctionCall(List<object> parameters)
        {
            return await Rpc(EvmReqBodyMethod.particleAbiEncodeFunctionCall, parameters);
        }

        /// <summary>
        /// Call custom abi encode function
        /// </summary>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="methodName">Method name, you should add "custom_" before your method name, like "custom_balanceOf", "custom_mint"</param>
        /// <param name="parameters">Method parameters</param>
        /// <param name="abiJsonString">Abi json string</param>
        /// <returns></returns>
        public static async Task<string> AbiEncodeFunctionCall(string contractAddress, string methodName,
            List<object> parameters,
            string abiJsonString = "")
        {
            var list = new List<object> { contractAddress, methodName, parameters };
            if (!string.IsNullOrEmpty(abiJsonString)) list.Add(abiJsonString);
            return await Rpc(EvmReqBodyMethod.particleAbiEncodeFunctionCall, list);
        }


        /// <summary>
        /// Get tokens by token addresses
        /// </summary>
        /// <param name="address">Public address</param>
        /// <param name="tokenAddresses">Token addresses</param>
        /// <returns></returns>
        public static async Task<string> GetTokensByTokenAddresses(string address, List<string> tokenAddresses)
        {
            return await Rpc(EvmReqBodyMethod.particleGetTokensByTokenAddresses,
                new List<object> { address, tokenAddresses });
        }
        
        /// <summary>
        /// Read contract
        /// </summary>
        /// <param name="from">Your public address</param>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="methodName">Method name, you should add "custom_" before your method name, like "custom_balanceOf", "custom_mint"</param>
        /// <param name="parameters">Method parameters</param>
        /// <param name="abiJsonString">Abi json string</param>
        /// <returns></returns>
        public static async Task<string> ReadContract(string from, string contractAddress, string methodName, List<object> parameters, string abiJsonString = "")
        {
            // Combine above into a ordered list
            var list = new List<object> { contractAddress, methodName, parameters };
            if (!string.IsNullOrEmpty(abiJsonString)) list.Add(abiJsonString);
            
            string dataResult = await EvmService.AbiEncodeFunctionCall(list);
            var data = (string)JObject.Parse(dataResult)["result"];

            var obj = new JObject
            {
                { "from", from },
                { "to", contractAddress },
                { "data", data },
                { "value", "0x0"}
            };
            
            // read contract
            var result = await EvmService.Rpc("eth_call", new List<object>{obj, "latest"});
            return result;
        }
    }

    /// <summary>
    /// solana-chain api
    /// </summary>
    public class SolanaService
    {
        /// <summary>
        /// Call rpc method
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="parameters">Method parameters</param>
        /// <returns></returns>
        public static async Task<string> Rpc(string method, List<object> parameters)
        {
            string path = "solana";
            ParticleRpcRequest<object> request = new ParticleRpcRequest<object>(method, parameters);
            return await NodeService.Request(path, request);
        }

        /// <summary>
        /// Get price
        /// </summary>
        /// <param name="addresses">Token addresses, the native token has no contract address, so it's value is native</param>
        /// <param name="currencies">Currencies, array of supported currencies</param>
        /// <returns></returns>
        public static async Task<string> GetPrice(List<string> addresses, List<string> currencies)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetPrice, new List<object> { addresses, currencies });
        }

        /// <summary>
        /// Get tokens and nfts
        /// </summary>
        /// <param name="address">Public address</param>
        /// <returns>Tokens, nfts, native balance</returns>
        public static async Task<string> GetTokensAndNFTs(string address)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetTokensAndNFTs, new List<object> { address });
        }

        /// <summary>
        /// Get transactions by address
        /// </summary>
        /// <param name="address">Public address</param>
        /// <returns>Transactions</returns>
        public static async Task<string> GetTransactionsByAddress(string address)
        {
            Dictionary<string, bool> map = new Dictionary<string, bool>();
            map["parseMetadataUri"] = true;
            return await Rpc(SolanaReqBodyMethod.enhancedGetTransactionsByAddress,
                new List<object> { address, map });
        }

        /// <summary>
        /// Get token transaction by address
        /// </summary>
        /// <param name="address">Public address</param>
        /// <param name="mintAddress">Token address</param>
        /// <returns>Transactions</returns>
        public static async Task<string> GetTokenTransactionsByAddress(string address, string mintAddress)
        {
            JObject obj = new JObject
            {
                { "address", address },
                { "mint", mintAddress },
            };

            return await Rpc(SolanaReqBodyMethod.enhancedGetTokenTransactionsByAddress,
                new List<object> { obj });
        }


        /// <summary>
        /// Serialize Sol transaction
        /// </summary>
        /// <param name="req">Request</param>
        /// <returns>Transaction</returns>
        public static async Task<string> SerializeSOLTransaction(SerializeSOLTransReq req)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedSerializeTransaction,
                new List<object> { SerializeTransactionParams.transferSol, req });
        }

        /// <summary>
        /// Serialize Spl token transaction
        /// </summary>
        /// <param name="req">Request</param>
        /// <returns>Transaction</returns>
        public static async Task<string> SerializeTokenTransaction(SerializeTokenTransReq req)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedSerializeTransaction,
                new List<object> { SerializeTransactionParams.transferToken, req });
        }

        /// <summary>
        /// Get tokens by token address
        /// </summary>
        /// <param name="publicAddress">Public address</param>
        /// <param name="tokenAddresses">Token addresses</param>
        /// <returns>Tokens</returns>
        public static async Task<string> GetTokensByTokenAddresses(string publicAddress, List<string> tokenAddresses)
        {
            return await Rpc(SolanaReqBodyMethod.enhancedGetTokensByTokenAddresses,
                new List<object> { publicAddress, tokenAddresses });
        }
    }
}
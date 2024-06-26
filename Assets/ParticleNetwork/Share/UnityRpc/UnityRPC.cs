using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;


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
        /// <returns>Contract function return value</returns>
        public static async Task<string> ReadContract(string from, string contractAddress, string methodName,
            List<object> parameters, string abiJsonString = "")
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
                { "value", "0x0" }
            };

            // read contract
            var result = await EvmService.Rpc("eth_call", new List<object> { obj, "latest" });
            return result;
        }

        /// <summary>
        /// Write contract 
        /// </summary>
        /// <param name="from">Your public address</param>
        /// <param name="contractAddress">Contract address</param>
        /// <param name="methodName">Method name, you should add "custom_" before your method name, like "custom_balanceOf", "custom_mint"</param>
        /// <param name="parameters">Method parameters</param>
        /// <param name="abiJsonString">Abi json string</param>
        /// <param name="gasFeeLevel">Gas fee level, default is high</param>
        /// <returns>Serialized transaction</returns>
        public static async Task<string> WriteContract(string from, string contractAddress, string methodName,
            List<object> parameters, [CanBeNull] string abiJsonString, GasFeeLevel gasFeeLevel = GasFeeLevel.High)
        {
            var dataResult = await AbiEncodeFunctionCall(contractAddress, methodName, parameters, abiJsonString);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await CreateTransaction(from, data, 0, contractAddress, gasFeeLevel);
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <param name="from">Your public address</param>
        /// <param name="data">Transaction data, if you are sending native, data should be "0x", if your are sending tokens, you should get data from other methods, such as
        /// "AbiEncodeFunctionCall", "Erc20Transfer" and so on</param>
        /// <param name="value">Native value </param>
        /// <param name="to">If your are sending native, it is receiver address, if you are sending token, it is the token contract address</param>
        /// <param name="gasFeeLevel">Gas fee level, default is high</param>
        /// <returns>Serialized transaction </returns>
        public static async Task<string> CreateTransaction(string from, string data, BigInteger value, string to,
            GasFeeLevel gasFeeLevel = GasFeeLevel.High)
        {
            var valueHex = "0x" + value.ToString("x");
            var gasLimitResult = await EstimateGas(from, to, "0x0", data);
            var gasLimit = (string)JObject.Parse(gasLimitResult)["result"];
            var gasFeesResult = await SuggestedGasFees();
            var level = "";
            switch (gasFeeLevel)
            {
                case GasFeeLevel.High:
                    level = "high";
                    break;
                case GasFeeLevel.Medium:
                    level = "medium";
                    break;
                case GasFeeLevel.Low:
                    level = "low";
                    break;
            }

            var maxFeePerGas = (double)JObject.Parse(gasFeesResult)["result"][level]["maxFeePerGas"];
            var maxFeePerGasHex = "0x" + ((BigInteger)(maxFeePerGas * Mathf.Pow(10, 9))).ToString("x");

            var maxPriorityFeePerGas = (double)JObject.Parse(gasFeesResult)["result"][level]["maxPriorityFeePerGas"];
            var maxPriorityFeePerGasHex = "0x" + ((BigInteger)(maxPriorityFeePerGas * Mathf.Pow(10, 9))).ToString("x");
            var chainInfo = UnityInnerChainInfo.GetChainInfo();
            var chainId = chainInfo.Id;

            EthereumTransaction transaction;

            if (chainInfo.IsEIP1559Supported())
            {
                transaction = new EthereumTransaction(from, to, data, gasLimit, gasPrice: null,
                    value: valueHex,
                    nonce: null,
                    type: "0x2",
                    chainId: "0x" + chainId.ToString("x"),
                    maxPriorityFeePerGasHex,
                    maxFeePerGasHex);
            }
            else
            {
                transaction = new EthereumTransaction(from, to, data, gasLimit, gasPrice: maxFeePerGasHex,
                    value: valueHex,
                    nonce: null,
                    type: "0x0",
                    chainId: "0x" + chainId.ToString("x"),
                    null,
                    null);
            }

            var json = JsonConvert.SerializeObject(transaction);
            var serialized = BitConverter.ToString(Encoding.Default.GetBytes(json));
            serialized = serialized.Replace("-", "");

            Debug.Log($"transaction {"0x" + serialized}");
            return "0x" + serialized;
        }

        /// <summary>
        /// Get smart account 
        /// </summary>
        /// <param name="objects">Smart account object list</param>
        /// <returns>Smart account</returns>
        public static async Task<string> GetSmartAccount(List<SmartAccountObject> objects)
        {
            var result = await Rpc("particle_aa_getSmartAccount", objects.Cast<object>().ToList());
            return result;
        }

        /// <summary>
        /// Get fee quotes
        /// </summary>
        /// <param name="obj">Smart account object</param>
        /// <param name="transactions">Transaction List</param>
        /// <returns>Whole fee quotes</returns>
        public static async Task<string> GetFeeQuotes(SmartAccountObject obj, List<SimplifyTransaction> transactions)
        {
            var result = await Rpc("particle_aa_getFeeQuotes", new List<object> { obj, transactions });
            return result;
        }

        /// <summary>
        /// Create user op
        /// </summary>
        /// <param name="obj">Smart account object</param>
        /// <param name="transactions">Transaction List</param>
        /// <param name="feeQuoteObject">Fee quote object</param>
        /// <param name="tokenPaymasterAddress">Token paymaster address</param>
        /// <param name="biconomyApiKey">Optional, biconomy api key</param>
        /// <returns>User op and hash</returns>
        public static async Task<string> CreateUserOp(SmartAccountObject obj,
            List<SimplifyTransaction> transactions, object feeQuoteObject, string tokenPaymasterAddress,
            [CanBeNull] string biconomyApiKey = null)
        {
            var accountConfig = new JObject
            {
                { "name", obj.name },
                { "version", obj.version },
                { "ownerAddress", obj.ownerAddress },
            };

            if (biconomyApiKey != null)
            {
                accountConfig[biconomyApiKey] = biconomyApiKey;
            }


            var result = await Rpc("particle_aa_createUserOp",
                new List<object> { accountConfig, transactions, feeQuoteObject, tokenPaymasterAddress });
            return result;
        }

        /// <summary>
        /// Send user op
        /// </summary>
        /// <param name="obj">Smart account object</param>
        /// <param name="userOp">User op</param>
        /// <returns>Signature</returns>
        public static async Task<string> SendUserOp(SmartAccountObject obj, object userOp,
            [CanBeNull] List<object> sessions = null,
            [CanBeNull] object targetSession = null)
        {
            var accountConfig = new JObject
            {
                { "name", obj.name },
                { "version", obj.version },
                { "ownerAddress", obj.ownerAddress },
            };


            var result = "";
            if (sessions != null && targetSession != null)
            {
                var sessionObj = new JObject()
                {
                    { "sessions", JToken.FromObject(sessions) },
                    { "targetSession", JToken.FromObject(targetSession) }
                };
                result = await Rpc("particle_aa_sendUserOp", new List<object> { accountConfig, userOp, sessionObj });
            }
            else
            {
                result = await Rpc("particle_aa_sendUserOp", new List<object> { accountConfig, userOp });
            }

            return result;
        }

        /// <summary>
        /// Create sessions
        /// </summary>
        /// <param name="obj">Smart account object</param>
        /// <param name="sessions">Sessions</param>
        /// <returns>Result</returns>
        public static async Task<string> CreateSessions(SmartAccountObject obj, List<object> sessions)
        {
            var accountConfig = new JObject
            {
                { "name", obj.name },
                { "version", obj.version },
                { "ownerAddress", obj.ownerAddress },
            };

            var result = await Rpc("particle_aa_createSessions", new List<object> { accountConfig, sessions });
            return result;
        }

        /// <summary>
        /// Validate session
        /// </summary>
        /// <param name="obj">Smart account object</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="targetSession">Target session</param>
        /// <returns></returns>
        public static async Task<string> ValidateSession(SmartAccountObject obj, List<object> sessions,
            object targetSession)
        {
            var accountConfig = new JObject
            {
                { "name", obj.name },
                { "version", obj.version },
                { "ownerAddress", obj.ownerAddress },
            };

            var sessionObj = new JObject()
            {
                { "sessions", JToken.FromObject(sessions) },
                { "targetSession", JToken.FromObject(targetSession) }
            };

            var result = await Rpc("particle_aa_validateSession", new List<object> { accountConfig, sessionObj });
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
using System;
using System.Numerics;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Particle.Scripts.Core
{
     public class EvmReqBodyMethod
    {
        public const string particleSuggestedGasFees = "particle_suggestedGasFees";
        public const string particleGetPrice = "particle_getPrice";
        public const string particleGetTokensAndNFTs = "particle_getTokensAndNFTs";
        public const string particleDeserializeTransaction = "particle_deserializeTransaction";
        public const string particleGetTransactionsByAddress = "particle_getTransactionsByAddress";
        public const string particleAbiEncodeFunctionCall = "particle_abi_encodeFunctionCall";
        public const string ethEstimateGas = "eth_estimateGas";
        public const string particleGetTokensByTokenAddresses = "particle_getTokensByTokenAddresses";
    }

    public class AbiEncodeFunction
    {
        public const string erc20Transfer = "erc20_transfer";
        public const string erc20Approve = "erc20_approve";
        public const string erc20TransferFrom = "erc20_transferFrom";
        public const string erc721SafeTransferFrom = "erc721_safeTransferFrom";
        public const string erc1155SafeTransferFrom = "erc1155_safeTransferFrom";
    }

    public class SolanaReqBodyMethod
    {
        public const string enhancedGetTokensAndNFTs = "enhancedGetTokensAndNFTs";
        public const string enhancedGetPrice = "enhancedGetPrice";
        public const string enhancedGetTransactionsByAddress = "enhancedGetTransactionsByAddress";
        public const string enhancedGetTokenTransactionsByAddress = "enhancedGetTokenTransactionsByAddress";
        public const string enhancedSerializeTransaction = "enhancedSerializeTransaction";
        public const string enhancedGetTokensByTokenAddresses = "enhancedGetTokensByTokenAddresses";
    }

    public class SerializeTransactionParams
    {
        public const string transferSol = "transfer-sol";
        public const string transferToken = "transfer-token";
    }

    [JsonObject]
    public class SerializeSOLTransReq
    {
        [SerializeField] public string sender;
        [SerializeField] public string receiver;
        [SerializeField] public BigInteger lamports;
        
        public SerializeSOLTransReq(string sender, string receiver, BigInteger lamports)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.lamports = lamports;
        }

        public SerializeSOLTransReq()
        {
        }
    }

    [JsonObject]
    public class SerializeTokenTransReq
    {
        [SerializeField] public string sender;
        [SerializeField] public string receiver;
        [SerializeField] public string mint;
        [SerializeField] public BigInteger amount;

        public SerializeTokenTransReq(string sender, string receiver, string mint, BigInteger amount)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.mint = mint;
            this.amount = amount;
        }

        public SerializeTokenTransReq()
        {
        }
    }

}
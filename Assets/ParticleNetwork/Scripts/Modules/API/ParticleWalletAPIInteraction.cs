using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Network.Particle.Scripts.Core
{
    public static class ParticleWalletAPIInteraction
    {
        /**
             [{
	            "chainId": 101,
	            "address": "3SghkPdBSrpF9bzdAy5LwR4nGgFbqNcC6ZSq8vtZdj91",
	            "symbol": "EV1",
	            "name": "EveryOne Coin",
	            "decimals": 9,
	            "logoURI": "https://static.particle.network/token-list/solana/3SghkPdBSrpF9bzdAy5LwR4nGgFbqNcC6ZSq8vtZdj91.png"
            }]
         */
        public static void SolanaGetTokenList()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("solanaGetTokenList");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTokenList();
#else

#endif
        }


        /**
 {
    "tokens":[{
        "mintAddress":"",
        "amount":"",
        "decimals":18,
        "updateAt":1321313131132,
        "symbol":"",
        "logoURI":""
    }],
    "nfts":[
       {
        "mintAddress":"",
        "image":"",
        "symbol":"",
        "name":"",
        "sellerFeeBasisPoints":11,
        "description":"",
        "externalUrl":"",
        "animationUrl":"",
        "data":"xxxxxxxxx",

        "isSemiFungible":false,
        "tokenId":"",
        "tokenBalance":111
       } 
    ]

}
 
 */
        public static void SolanaGetTokensAndNFTs(string address)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("solanaGetTokensAndNFTs",address);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTokensAndNFTs(address);
#else
#endif
        }

        public static void SolanaGetTokensAndNFTsFromDB(string address)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("solanaGetTokensAndNFTsFromDB",address);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTokensAndNFTsFromDB(address);
#else
#endif
        }


        public static void SolanaAddCustomTokens(string address, string[] tokenAddresses)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "token_addresses", JsonConvert.SerializeObject(tokenAddresses) },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("solanaAddCustomTokens",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaAddCustomTokens(json);
#else
#endif
        }

        /**
       [
       {
         "from":"",
         "to":"",
         "type":"",
         "lamportsChange":1212121,
         "lamportsFee":1212121,
         "signature":"xxx",
         "blockTime":1212121,
         "status":"",
         "data":"",
         "mint":""
       }
    ]
     */
        public static void SolanaGetTransactions(string address, [CanBeNull] string beforeSignature,
            [CanBeNull] string untilSignature, int limit)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "before", beforeSignature },
                { "until", untilSignature },
                { "limit", limit },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("solanaGetTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTransactions(json);
#else
#endif
        }

        public static void SolanaGetTransactionsFromDB(string address, int limit)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "limit", limit },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("solanaGetTransactionsFromDB",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTransactionsFromDB(json);
#else
#endif
        }

        public static void SolanaGetTokenTransactions(string address, string mintAddress,
            [CanBeNull] string beforeSignature,
            [CanBeNull] string untilSignature, int limit)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "mint_address", mintAddress },
                { "before", beforeSignature },
                { "until", untilSignature },
                { "limit", limit },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTokenTransactions(json);
#else
#endif
        }

        public static void SolanaGetTokenTransactionsFromDB(string address, string mintAddress, int limit)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "mint_address", mintAddress },
                { "limit", limit },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.solanaGetTokenTransactionsFromDB(json);
#else
#endif
        }


        public static void EvmGetTokenList()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("evmGetTokenList");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.evmGetTokenList();
#else
#endif
        }


        public static void EvmGetTokensAndNFTs(string address, string[] tokenAddresses)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "token_addresses", JsonConvert.SerializeObject(tokenAddresses) },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("evmGetTokensAndNFTs",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.evmGetTokensAndNFTs(json);
#else
#endif
        }

        public static void EvmGetTokensAndNFTsFromDB(string address)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("evmGetTokensAndNFTsFromDB",address);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.evmGetTokensAndNFTsFromDB(address);
#else
#endif
        }


        public static void EvmAddCustomTokens(string address, string[] tokenAddresses)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "address", address },
                { "token_addresses", JsonConvert.SerializeObject(tokenAddresses) },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("evmAddCustomTokens",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.evmAddCustomTokens(json);
#else
#endif
        }


        /**
 [
    {
        "from":"",
        "to":"",
        "hash":"",
        "value":"",
        "data":"",
        "gasLimit":"",
        "gasSpent":"",
        "gasPrice":"",
        "fees":"",
        "type":1,
        "nonce":"",
        "maxPriorityFeePerGas":"",
        "maxFeePerGas":"",
        "timestamp":1234567980,
        "status":1
    }
 ]
 */
        public static void EvmGetTransactions(string address)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("evmGetTransactions",address);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.evmGetTransactions(address);
#else

#endif
        }

        public static void EvmGetTransactionsFromDB(string address)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("evmGetTransactionsFromDB",address);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.evmGetTransactionsFromDB(address);
#else
#endif
        }
    }
}
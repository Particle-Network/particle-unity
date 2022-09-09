using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Singleton;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public class ParticleWalletAPI : SingletonMonoBehaviour<ParticleWalletAPI>
    {
        protected override void Awake()
        {
            base.Awake();
            Debug.Log($"Unity Game Build Debug = {Debug.isDebugBuild}");
        }

        private TaskCompletionSource<NativeResultData> solanaGetTokenListTask;
        private TaskCompletionSource<NativeResultData> solanaGetTokensAndNFTsTask;
        private TaskCompletionSource<NativeResultData> solanaGetTokensAndNFTsFromDBTask;
        private TaskCompletionSource<NativeResultData> solanaAddCustomTokensTask;
        private TaskCompletionSource<NativeResultData> solanaGetTransactionsTask;
        private TaskCompletionSource<NativeResultData> solanaGetTransactionsFromDBTask;
        private TaskCompletionSource<NativeResultData> solanaGetTokenTransactionsTask;
        private TaskCompletionSource<NativeResultData> solanaGetTokenTransactionsFromDBTask;

        private TaskCompletionSource<NativeResultData> evmGetTokenListTask;
        private TaskCompletionSource<NativeResultData> evmGetTokensAndNFTsTask;
        private TaskCompletionSource<NativeResultData> evmGetTokensAndNFTsFromDBTask;
        private TaskCompletionSource<NativeResultData> evmAddCustomTokensTask;
        private TaskCompletionSource<NativeResultData> evmGetTransactionsTask;
        private TaskCompletionSource<NativeResultData> evmGetTransactionsFromDBTask;


        public Task<NativeResultData> SolanaGetTokenList()
        {
            solanaGetTokenListTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTokenList();
#if UNITY_EDITOR
            SolanaGetTokenListCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTokenListTask.Task;
        }

        public void SolanaGetTokenListCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTokenListTask?.TrySetResult(new NativeResultData(status == 1, json));
        }


        public Task<NativeResultData> SolanaGetTokensAndNFTs(string address)
        {
            solanaGetTokensAndNFTsTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTokensAndNFTs(address);
#if UNITY_EDITOR
            SolanaGetTokensAndNFTsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTokensAndNFTsTask.Task;
        }

        public void SolanaGetTokensAndNFTsCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTokensAndNFTsTask?.TrySetResult(new NativeResultData(status == 1, json));
        }

        public Task<NativeResultData> SolanaGetTokensAndNFTsFromDB(string address)
        {
            solanaGetTokensAndNFTsFromDBTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTokensAndNFTsFromDB(address);
#if UNITY_EDITOR
            SolanaGetTokensAndNFTsFromDBCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTokensAndNFTsFromDBTask.Task;
        }

        public void SolanaGetTokensAndNFTsFromDBCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTokensAndNFTsFromDBTask?.TrySetResult(new NativeResultData(status == 1, json));
        }


        public Task<NativeResultData> SolanaAddCustomTokens(string address, string[] tokenAddresses)
        {
            solanaAddCustomTokensTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaAddCustomTokens(address, tokenAddresses);
#if UNITY_EDITOR
            SolanaAddCustomTokensCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaAddCustomTokensTask.Task;
        }

        public void SolanaAddCustomTokensCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaAddCustomTokensTask?.TrySetResult(new NativeResultData(status == 1, json));
        }

        public Task<NativeResultData> SolanaGetTransactions(string address, [CanBeNull] string beforeSignature,
            [CanBeNull] string untilSignature, int limit)
        {
            solanaGetTransactionsTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTransactions(address, beforeSignature, untilSignature, limit);
#if UNITY_EDITOR
            SolanaGetTransactionsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTransactionsTask.Task;
        }

        public void SolanaGetTransactionsCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTransactionsTask?.TrySetResult(new NativeResultData(status == 1, json));
        }

        public Task<NativeResultData> SolanaGetTransactionsFromDB(string address, [CanBeNull] string beforeSignature,
            [CanBeNull] string untilSignature, int limit)
        {
            solanaGetTransactionsFromDBTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTransactionsFromDB(address, limit);
#if UNITY_EDITOR
            SolanaGetTransactionsFromDBCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTransactionsFromDBTask.Task;
        }

        public void SolanaGetTransactionsFromDBCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTransactionsFromDBTask?.TrySetResult(new NativeResultData(status == 1,
                (string)resultData["data"]));
        }

        public Task<NativeResultData> SolanaGetTokenTransactions(string address, string mintAddress,
            [CanBeNull] string beforeSignature,
            [CanBeNull] string untilSignature, int limit)
        {
            solanaGetTokenTransactionsTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTokenTransactions(address, mintAddress, beforeSignature,
                untilSignature, limit);
#if UNITY_EDITOR
            SolanaGetTransactionsFromDBCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTokenTransactionsTask.Task;
        }

        public void SolanaGetTokenTransactionsCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTokenTransactionsTask?.TrySetResult(new NativeResultData(status == 1, (string)resultData["data"]));
        }

        public Task<NativeResultData> SolanaGetTokenTransactionsFromDB(string address, string mintAddress, int limit)
        {
            solanaGetTokenTransactionsFromDBTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.SolanaGetTokenTransactionsFromDB(address, mintAddress, limit);
#if UNITY_EDITOR
            SolanaGetTransactionsFromDBCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return solanaGetTokenTransactionsFromDBTask.Task;
        }

        public void SolanaGetTokenTransactionsFromDBCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaGetTokenTransactionsFromDBTask?.TrySetResult(new NativeResultData(status == 1,
                (string)resultData["data"]));
        }

        public Task<NativeResultData> EvmGetTokenList()
        {
            evmGetTokenListTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.EvmGetTokenList();
#if UNITY_EDITOR
            EvmGetTokenListCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return evmGetTokenListTask.Task;
        }

        public void EvmGetTokenListCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmGetTokenListTask?.TrySetResult(new NativeResultData(status == 1, json));
        }


        public Task<NativeResultData> EvmGetTokensAndNFTs(string address, string[] tokenAddresses)
        {
            evmGetTokensAndNFTsTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.EvmGetTokensAndNFTs(address, tokenAddresses);
#if UNITY_EDITOR
            EvmGetTokensAndNFTsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return evmGetTokensAndNFTsTask.Task;
        }

        public void EvmGetTokensAndNFTsCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmGetTokensAndNFTsTask?.TrySetResult(new NativeResultData(status == 1, json));
        }

        public Task<NativeResultData> EvmGetTokensAndNFTsFromDB(string address)
        {
            evmGetTokensAndNFTsFromDBTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.EvmGetTokensAndNFTsFromDB(address);
#if UNITY_EDITOR
            SolanaGetTokensAndNFTsFromDBCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return evmGetTokensAndNFTsFromDBTask.Task;
        }

        public void EvmGetTokensAndNFTsFromDBCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmGetTokensAndNFTsFromDBTask?.TrySetResult(new NativeResultData(status == 1, json));
        }

        public async Task<string> EvmGetTokensByTokenAddresses(string address, List<string> tokenAddresses)
        {
            return await EvmService.GetTokensByTokenAddresses(address, tokenAddresses);
        }


        public Task<NativeResultData> EvmAddCustomTokens(string address, string[] tokenAddresses)
        {
            evmAddCustomTokensTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.EvmAddCustomTokens(address, tokenAddresses);
#if UNITY_EDITOR
            SolanaAddCustomTokensCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return evmAddCustomTokensTask.Task;
        }

        public void EvmAddCustomTokensCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmAddCustomTokensTask?.TrySetResult(new NativeResultData(status == 1, json));
        }


        public Task<NativeResultData> EvmGetTransactions(string address)
        {
            evmGetTransactionsTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.EvmGetTransactions(address);
#if UNITY_EDITOR
            EvmGetTransactionsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return evmGetTransactionsTask.Task;
        }

        public void EvmGetTransactionsCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmGetTransactionsTask?.TrySetResult(new NativeResultData(status == 1, json));
        }

        public Task<NativeResultData> EvmGetTransactionsFromDB(string address)
        {
            evmGetTransactionsFromDBTask = new TaskCompletionSource<NativeResultData>();
            ParticleWalletAPIInteraction.EvmGetTransactionsFromDB(address);
#if UNITY_EDITOR
            EvmGetTransactionsFromDBCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            return evmGetTransactionsFromDBTask.Task;
        }

        public void EvmGetTransactionsFromDBCallBack(string json)
        {
            Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmGetTransactionsFromDBTask?.TrySetResult(new NativeResultData(status == 1,
                (string)resultData["data"]));
        }
    }
}
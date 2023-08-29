using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using Network.Particle.Scripts.Utils;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public partial class ParticleAuthCore : SingletonMonoBehaviour<ParticleAuthCore>
    {
        private TaskCompletionSource<NativeResultData> connectTask;
        private TaskCompletionSource<NativeResultData> disconnectTask;
        private TaskCompletionSource<NativeResultData> isConnectedTask;
        private TaskCompletionSource<NativeResultData> switchChainTask;
        private TaskCompletionSource<NativeResultData> changeMasterPasswordTask;
        private TaskCompletionSource<NativeResultData> openAccountAndSecurityTask;


        /// <summary>
        /// Connect jwt
        /// </summary>
        /// <returns>User info json string</returns>
        public Task<NativeResultData> Connect(string jwt)
        {
            connectTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            ConnectCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.Connect(jwt);
            return connectTask.Task;
        }

        /// <summary>
        /// Connect call back
        /// </summary>
        /// <param name="json">Result</param>
        public void ConnectCallBack(string json)
        {
            Debug.Log($"ConnectCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            connectTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }


        /// <summary>
        /// Disconnect
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> Disconnect()
        {
            disconnectTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            DisconnectCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.Disconnect();
            return disconnectTask.Task;
        }

        /// <summary>
        /// Disconnect call back
        /// </summary>
        /// <param name="json">Result</param>
        public void DisconnectCallBack(string json)
        {
            Debug.Log($"DisconnectCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            disconnectTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Is connected
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> IsConnected()
        {
            isConnectedTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            IsConnectedCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.IsConnected();
            return isConnectedTask.Task;
        }

        /// <summary>
        /// Is connected call back
        /// </summary>
        /// <param name="json">Result</param>
        public void IsConnectedCallBack(string json)
        {
            Debug.Log($"IsConnectedCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            isConnectedTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }


        /// <summary>
        /// Switch chain info
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> SwitchChain(ChainInfo chainInfo)
        {
            switchChainTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SwitchChainCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.SwitchChain(chainInfo);

            return switchChainTask.Task;
        }

        /// <summary>
        /// Switch chain call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SwitchChainCallBack(string json)
        {
            Debug.Log($"SwitchChainCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            switchChainTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Change master password
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> ChangeMasterPassword()
        {
            changeMasterPasswordTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR

            ChangeMasterPasswordCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.ChangeMasterPassword();

            return changeMasterPasswordTask.Task;
        }

        /// <summary>
        /// Change master password call back
        /// </summary>
        /// <param name="json">Result</param>
        public void ChangeMasterPasswordCallBack(string json)
        {
            Debug.Log($"ChangeMasterPasswordCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            changeMasterPasswordTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }


        /// <summary>
        /// Open account and security page.
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> OpenAccountAndSecurity()
        {
            openAccountAndSecurityTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            OpenAccountAndSecurityCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif

            ParticleAuthCoreInteraction.OpenAccountAndSecurity();
            return openAccountAndSecurityTask.Task;
        }

        /// <summary>
        /// Open account and security call back,only failed will return data.
        /// </summary>
        /// <param name="json">Result</param>
        public void OpenAccountAndSecurityCallBack(string json)
        {
            Debug.Log($"OpenAccountAndSecurityCallBack:{json}");
#if UNITY_EDITOR
            openAccountAndSecurityTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            openAccountAndSecurityTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
        }
    }

    public partial class ParticleAuthCore
    {
        private TaskCompletionSource<NativeResultData> evmPersonalSignTask;
        private TaskCompletionSource<NativeResultData> evmPersonalSignUniqueTask;
        private TaskCompletionSource<NativeResultData> evmSignTypedDataTask;
        private TaskCompletionSource<NativeResultData> evmSignTypedDataUniqueTask;
        private TaskCompletionSource<NativeResultData> evmSendTransactionTask;

        /// <summary>
        /// Evm Personal Sign
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> EvmPersonalSign(string message)
        {
            evmPersonalSignTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            EvmPersonalSignCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.EvmPersonalSign(message);
            return evmPersonalSignTask.Task;
        }

        /// <summary>
        /// Evm Personal Sign call back
        /// </summary>
        /// <param name="json">Result</param>
        public void EvmPersonalSignCallBack(string json)
        {
            Debug.Log($"EvmPersonalSignCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmPersonalSignTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Evm Personal Sign Unique
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> EvmPersonalSignUnique(string message)
        {
            evmPersonalSignUniqueTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            EvmPersonalSignUniqueCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.EvmPersonalSignUnique(message);
            return evmPersonalSignUniqueTask.Task;
        }

        /// <summary>
        /// Evm Personal Sign Unique call back
        /// </summary>
        /// <param name="json">Result</param>
        public void EvmPersonalSignUniqueCallBack(string json)
        {
            Debug.Log($"EvmPersonalSignUniqueCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmPersonalSignUniqueTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Evm sign typed data
        /// </summary>
        /// <param name="message">typed data</param>
        /// <returns></returns>
        public Task<NativeResultData> EvmSignTypedData(string message)
        {
            evmSignTypedDataTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            EvmSignTypedDataCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.EvmSignTypedData(message);
            return evmSignTypedDataTask.Task;
        }

        /// <summary>
        /// Personal Sign Unique call back
        /// </summary>
        /// <param name="json">Result</param>
        public void EvmSignTypedDataCallBack(string json)
        {
            Debug.Log($"EvmSignTypedDataCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmSignTypedDataTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Evm sign typed data unique
        /// </summary>
        /// <param name="message">typed data</param>
        /// <returns></returns>
        public Task<NativeResultData> EvmSignTypedDataUnique(string message)
        {
            evmSignTypedDataTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            EvmSignTypedDataUniqueCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.EvmSignTypedDataUnique(message);
            return evmSignTypedDataTask.Task;
        }

        /// <summary>
        /// Personal Sign Unique call back
        /// </summary>
        /// <param name="json">Result</param>
        public void EvmSignTypedDataUniqueCallBack(string json)
        {
            Debug.Log($"EvmSignTypedDataUniqueCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmSignTypedDataTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Evm send transaction
        /// </summary>
        /// <param name="message">typed data</param>
        /// <returns></returns>
        public Task<NativeResultData> EvmSendTransaction(string transaction)
        {
            evmSendTransactionTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            EvmSendTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.EvmSendTransaction(transaction);
            return evmSendTransactionTask.Task;
        }

        /// <summary>
        /// Evm send transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void EvmSendTransactionCallBack(string json)
        {
            Debug.Log($"EvmSendTransactionCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            evmSendTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Batch Send Transaction, should init and enable particle aa.
        /// </summary>
        /// <param name="transactions">Transactions</param>
        /// /// <param name="feeMode">AAFeeMode, default is auto</param>
        /// <returns></returns>
        public Task<NativeResultData> BatchSendTransactions(List<string> transactions,
            [CanBeNull] AAFeeMode feeMode = null)
        {
            batchSendTransactionTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            BatchSendTransactionsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.BatchSendTransactions(transactions, feeMode);

            return batchSendTransactionTask.Task;
        }

        /// <summary>
        /// Batch Send Transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void BatchSendTransactionsCallBack(string json)
        {
            Debug.Log($"BatchSendTransactionsCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            batchSendTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
    }
    
    public partial class ParticleAuthCore
    {
        private TaskCompletionSource<NativeResultData> solanaSignMessageTask;
        private TaskCompletionSource<NativeResultData> solanaSignTransactionTask;
        private TaskCompletionSource<NativeResultData> solanaSignAllTransactionsTask;
        private TaskCompletionSource<NativeResultData> solanaSignAndSendTransactionTask;
        private TaskCompletionSource<NativeResultData> batchSendTransactionTask;

        /// <summary>
        /// Solana Sign message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> SolanaSignMessage(string message)
        {
            solanaSignMessageTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SolanaSignMessageCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.SolanaSignMessage(message);
            return solanaSignMessageTask.Task;
        }

        /// <summary>
        /// Solana Sign message call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SolanaSignMessageCallBack(string json)
        {
            Debug.Log($"SolanaSignMessageCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaSignMessageTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Solana Sign Transaction
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> SolanaSignTransaction(string message)
        {
            solanaSignTransactionTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SolanaSignTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.SolanaSignTransaction(message);
            return solanaSignTransactionTask.Task;
        }

        /// <summary>
        /// Solana Sign Transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SolanaSignTransactionCallBack(string json)
        {
            Debug.Log($"SolanaSignTransactionCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaSignTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Solana sign all transactions
        /// </summary>
        /// <param name="transactions">Transactions</param>
        /// <returns></returns>
        public Task<NativeResultData> SolanaSignAllTransactions(string[] transactions)
        {
            solanaSignAllTransactionsTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SolanaSignAllTransactionsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.SolanaSignAllTransactions(transactions);
            return solanaSignAllTransactionsTask.Task;
        }

        /// <summary>
        /// Solana sign all transacitons call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SolanaSignAllTransactionsCallBack(string json)
        {
            Debug.Log($"SolanaSignAllTransactionsCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaSignAllTransactionsTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Solana sign and send transaction
        /// </summary>
        /// <param name="transaction">Solana transaction, base58 string</param>
        /// <returns></returns>
        public Task<NativeResultData> SolanaSignAndSendTransaction(string transaction)
        {
            solanaSignAndSendTransactionTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SolanaSignAndSendTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleAuthCoreInteraction.SolanaSendAndSendTransaction(transaction);
            return solanaSignAndSendTransactionTask.Task;
        }

        /// <summary>
        /// Solana sign and send transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SolanaSignAndSendTransactionCallBack(string json)
        {
            Debug.Log($"SolanaSignAndSendTransactionCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            solanaSignAndSendTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
    }
}
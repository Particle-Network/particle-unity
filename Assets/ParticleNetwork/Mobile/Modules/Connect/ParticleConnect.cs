using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using Newtonsoft.Json;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public class ParticleConnect : SingletonMonoBehaviour<ParticleConnect>
    {
        private TaskCompletionSource<NativeResultData> connectTask;
        private TaskCompletionSource<NativeResultData> disconnectTask;
        private TaskCompletionSource<NativeResultData> signMessageTask;
        private TaskCompletionSource<NativeResultData> signTransactionTask;
        private TaskCompletionSource<NativeResultData> signAllTransactionsTask;
        private TaskCompletionSource<NativeResultData> signAndSendTransactionTask;
        private TaskCompletionSource<NativeResultData> batchSendTransactionsTask;
        private TaskCompletionSource<NativeResultData> signTypedDataTask;
        private TaskCompletionSource<NativeResultData> loginTask;
        private TaskCompletionSource<NativeResultData> verifyTask;

        private TaskCompletionSource<NativeResultData> importPrivateKeyTask;
        private TaskCompletionSource<NativeResultData> importMnemonicTask;
        private TaskCompletionSource<NativeResultData> exportPrivateKeyTask;


        /// <summary>
        /// Connect wallet
        /// </summary>
        /// <param name="walletType">Wallet Type</param>
        /// <returns></returns>
        public Task<NativeResultData> Connect(WalletType walletType, [CanBeNull] ConnectConfig config = null)
        {
            connectTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            ConnectCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.Connect(walletType, config);
            return connectTask.Task;
        }

        /// <summary>
        /// Connect wallet call back
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
        /// Disconnect wallet
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <returns></returns>
        public Task<NativeResultData> Disconnect(WalletType walletType, string publicAddress)
        {
            disconnectTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            DisconnectCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.Disconnect(walletType, publicAddress);

            return disconnectTask.Task;
        }

        /// <summary>
        /// Disconnect wallet call back
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
        /// Sign message
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> SignMessage(WalletType walletType, string publicAddress, string message)
        {
            signMessageTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SignMessageCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.SignMessage(walletType, publicAddress, message);


            return signMessageTask.Task;
        }

        /// <summary>
        /// Sign message call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignMessageCallBack(string json)
        {
            Debug.Log($"SignMessageCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signMessageTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }


        /// <summary>
        /// Sign transaction
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="transaction">Transaction</param>
        /// <returns></returns>
        public Task<NativeResultData> SignTransaction(WalletType walletType, string publicAddress, string transaction)
        {
            signTransactionTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            SignTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.SignTransaction(walletType, publicAddress, transaction);


            return signTransactionTask.Task;
        }

        /// <summary>
        /// Sign transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignTransactionCallBack(string json)
        {
            Debug.Log($"SignTransactionCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Sign all transactions
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="transactions">Transactions</param>
        /// <returns></returns>
        public Task<NativeResultData> SignAllTransactions(WalletType walletType, string publicAddress,
            string[] transactions)
        {
            signAllTransactionsTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SignAllTransactionsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 1 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.SignAllTransactions(walletType, publicAddress, transactions);

            return signAllTransactionsTask.Task;
        }

        /// <summary>
        /// Sign all transactions call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignAllTransactionsCallBack(string json)
        {
            Debug.Log($"SignAllTransactionsCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signAllTransactionsTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Sign and send transaction
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="transactions">Transactions</param>
        /// <param name="feeMode">AAFeeMode, works with aa mode, default value is auto</param>
        /// <returns></returns>
        public Task<NativeResultData> BatchSendTransactions(WalletType walletType, string publicAddress,
            List<string> transactions, [CanBeNull] AAFeeMode feeMode = null)
        {
            batchSendTransactionsTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            BatchSendTransactionsCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.BatchSendTransactions(walletType, publicAddress, transactions, feeMode);

            return batchSendTransactionsTask.Task;
        }

        /// <summary>
        /// Sign and send transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void BatchSendTransactionsCallBack(string json)
        {
            Debug.Log($"BatchSendTransactionsCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            batchSendTransactionsTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Sign and send transaction
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="transaction">Transaction</param>
        /// <param name="feeMode">AAFeeMode, works with aa mode, default value is auto</param>
        /// <returns></returns>
        public Task<NativeResultData> SignAndSendTransaction(WalletType walletType, string publicAddress,
            string transaction, [CanBeNull] AAFeeMode feeMode = null)
        {
            signAndSendTransactionTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            SignAndSendTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.SignAndSendTransaction(walletType, publicAddress, transaction, feeMode);

            return signAndSendTransactionTask.Task;
        }

        /// <summary>
        /// Sign and send transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignAndSendTransactionCallBack(string json)
        {
            Debug.Log($"signAndSendTransactionCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signAndSendTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Sign typed data, Particle Connect support V4
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="typedData">Typed Data</param>
        /// <returns></returns>
        public Task<NativeResultData> SignTypedData(WalletType walletType, string publicAddress, string typedData)
        {
            signTypedDataTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            SignTypedDataCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.SignTypedData(walletType, publicAddress, typedData);

            return signTypedDataTask.Task;
        }

        /// <summary>
        /// Sign typed data call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignTypedDataCallBack(string json)
        {
            Debug.Log($"signTypedDataCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signTypedDataTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Login, Sign-in with Ethereum, For more information on SIWE check out https://docs.login.xyz
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="domain">Domain</param>
        /// <param name="uri">Uri</param>
        /// <returns></returns>
        public Task<NativeResultData> Login(WalletType walletType, string publicAddress, string domain, string uri)
        {
            loginTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            LoginCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.Login(walletType, publicAddress, domain, uri);
            return loginTask.Task;
        }

        /// <summary>
        /// Login call back
        /// </summary>
        /// <param name="json">Result</param>
        public void LoginCallBack(string json)
        {
            Debug.Log($"LoginCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            loginTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Verify locally, works with Login.
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="message">Message</param>
        /// <param name="signature">Signature</param>
        /// <returns></returns>
        public Task<NativeResultData> Verify(WalletType walletType, string publicAddress, string message,
            string signature)
        {
            verifyTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            VerifyCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.Verify(walletType, publicAddress, message, signature);
            return verifyTask.Task;
        }

        /// <summary>
        /// Verify call back
        /// </summary>
        /// <param name="json">Result</param>
        public void VerifyCallBack(string json)
        {
            Debug.Log($"VerifyCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            verifyTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Import wallet from private key
        /// </summary>
        /// <param name="walletType">Wallet type, must be EthereumPrivateKey or SolanaPrivateKey</param>
        /// <param name="privateKey">Private key string</param>
        /// <returns></returns>
        public Task<NativeResultData> ImportWalletFromPrivateKey(WalletType walletType, string privateKey)
        {
            importPrivateKeyTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            ImportWalletFromPrivateKeyCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.ImportPrivateKey(walletType, privateKey);

            return importPrivateKeyTask.Task;
        }

        /// <summary>
        /// Import wallet from private key call back 
        /// </summary>
        /// <param name="json">Result</param>
        public void ImportWalletFromPrivateKeyCallBack(string json)
        {
            Debug.Log($"importPrivateKeyCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            importPrivateKeyTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Import wallet from mnemonic
        /// </summary>
        /// <param name="walletType">Wallet type, must be EthereumPrivateKey or SolanaPrivateKey</param>
        /// <param name="mnemonic">Mnemonic</param>
        /// <returns></returns>
        public Task<NativeResultData> ImportWalletFromMnemonic(WalletType walletType, string mnemonic)
        {
            importMnemonicTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            ImportWalletFromMnemonicCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.ImportMnemonic(walletType, mnemonic);

            return importMnemonicTask.Task;
        }

        /// <summary>
        /// Import wallet from mnemonic call back
        /// </summary>
        /// <param name="json">Result</param>
        public void ImportWalletFromMnemonicCallBack(string json)
        {
            Debug.Log($"importMnemonicCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            importMnemonicTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Export wallet private key
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <returns></returns>
        public Task<NativeResultData> ExportWalletPrivateKey(WalletType walletType, string publicAddress)
        {
            exportPrivateKeyTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            ExportWalletPrivateKeyCallBack(JsonConvert.SerializeObject(new JObject
            {
                { "status", 0 },
                { "data", "" },
            }));
#endif
            ParticleConnectInteraction.ExportPrivateKey(walletType, publicAddress);
            return exportPrivateKeyTask.Task;
        }

        /// <summary>
        /// Export wallet private key call back
        /// </summary>
        /// <param name="json">Result</param>
        public void ExportWalletPrivateKeyCallBack(string json)
        {
            Debug.Log($"exportPrivateKeyCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            exportPrivateKeyTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
    }
}
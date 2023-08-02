using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
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
        private TaskCompletionSource<NativeResultData> setChainTask;

        private TaskCompletionSource<NativeResultData> importPrivateKeyTask;
        private TaskCompletionSource<NativeResultData> importMnemonicTask;
        private TaskCompletionSource<NativeResultData> exportPrivateKeyTask;
        private TaskCompletionSource<NativeResultData> loginListTask;
        
        private TaskCompletionSource<NativeResultData> switchEthereumChainTask;
        private TaskCompletionSource<NativeResultData> addEthereumChainTask;

        /// <summary>
        /// Set Chain Info Async call back
        /// </summary>
        /// <param name="json"></param>
        public void SetChainInfoAsyncCallBack(string json)
        {
            Debug.Log($"SetChainCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            setChainTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Connect wallet
        /// </summary>
        /// <param name="walletType">Wallet Type</param>
        /// <returns></returns>
        public Task<NativeResultData> Connect(WalletType walletType, [CanBeNull] ConnectConfig config = null)
        {
            connectTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR

#else
            ParticleConnectInteraction.Connect(walletType,config);
#endif
            return connectTask.Task;
        }

        /// <summary>
        /// Connect wallet call back
        /// </summary>
        /// <param name="json">Result</param>
        public void ConnectCallBack(string json)
        {
            Debug.Log($"ConnectCallBack:{json}");
#if UNITY_EDITOR
            connectTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            connectTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
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

#else
            ParticleConnectInteraction.Disconnect(walletType,publicAddress);
#endif
            return disconnectTask.Task;
        }

        /// <summary>
        /// Disconnect wallet call back
        /// </summary>
        /// <param name="json">Result</param>
        public void DisconnectCallBack(string json)
        {
            Debug.Log($"DisconnectCallBack:{json}");
#if UNITY_EDITOR
            disconnectTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            disconnectTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
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
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                DevModeService.EvmSignMessages(new[] { message });
            }
            else
            {
                DevModeService.SolanaSignMessages(new[] { message });
            }
#else
            ParticleConnectInteraction.SignMessage(walletType,publicAddress,message);
#endif

            return signMessageTask.Task;
        }

        /// <summary>
        /// Sign message call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignMessageCallBack(string json)
        {
            Debug.Log($"SignMessageCallBack:{json}");
#if UNITY_EDITOR
            signMessageTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signMessageTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
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
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                DevModeService.EvmSignTransactions(new[] { transaction });
            }
            else
            {
                DevModeService.SolanaSignTransactions(new[] { transaction });
            }
#else
             ParticleConnectInteraction.SignTransaction(walletType,publicAddress,transaction);
#endif

            return signTransactionTask.Task;
        }

        /// <summary>
        /// Sign transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignTransactionCallBack(string json)
        {
            Debug.Log($"SignTransactionCallBack:{json}");
#if UNITY_EDITOR
            signTransactionTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            signTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
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
#else
            ParticleConnectInteraction.SignAllTransactions(walletType,publicAddress,transactions);
#endif
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
        /// <param name="feeMode">BiconomyFeeMode, works with biconomy mode, default value is auto</param>
        /// <returns></returns>
        public Task<NativeResultData> BatchSendTransactions(WalletType walletType, string publicAddress,
            List<string> transactions, [CanBeNull] BiconomyFeeMode feeMode = null)
        {
            batchSendTransactionsTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            SignAndSendTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                {"status", 0},
                {"data", ""},
            }));
#else
            ParticleConnectInteraction.BatchSendTransactions(walletType,publicAddress, transactions, feeMode);
#endif
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
        /// <param name="feeMode">BiconomyFeeMode, works with biconomy mode, default value is auto</param>
        /// <returns></returns>
        public Task<NativeResultData> SignAndSendTransaction(WalletType walletType, string publicAddress,
            string transaction, [CanBeNull] BiconomyFeeMode feeMode = null)
        {
            signAndSendTransactionTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            SignAndSendTransactionCallBack(JsonConvert.SerializeObject(new JObject
            {
                {"status", 0},
                {"data", ""},
            }));
#else
            ParticleConnectInteraction.SignAndSendTransaction(walletType,publicAddress, transaction, feeMode);
#endif
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
#else
            ParticleConnectInteraction.SignTypedData(walletType,publicAddress,typedData);
#endif
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
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> Login(WalletType walletType, string publicAddress, string domain, string uri)
        {
            loginTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR

#else
            ParticleConnectInteraction.Login(walletType, publicAddress, domain, uri);
#endif

            return loginTask.Task;
        }

        /// <summary>
        /// Login call back
        /// </summary>
        /// <param name="json">Result</param>
        public void LoginCallBack(string json)
        {
            Debug.Log($"LoginCallBack:{json}");
#if UNITY_EDITOR
            loginTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            loginTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
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
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                DevModeService.EvmSignMessages(new[] { message });
            }
            else
            {
                DevModeService.SolanaSignMessages(new[] { message });
            }
#else
            ParticleConnectInteraction.Verify(walletType,publicAddress,message, signature);
#endif

            return verifyTask.Task;
        }

        /// <summary>
        /// Verify call back
        /// </summary>
        /// <param name="json">Result</param>
        public void VerifyCallBack(string json)
        {
            Debug.Log($"VerifyCallBack:{json}");
#if UNITY_EDITOR
            verifyTask?.TrySetResult(new NativeResultData(true, json));
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            verifyTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
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
#else
            ParticleConnectInteraction.ImportPrivateKey(walletType,privateKey);
#endif
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
#else
            ParticleConnectInteraction.ImportMnemonic(walletType,mnemonic);
#endif
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
#else
            ParticleConnectInteraction.ExportPrivateKey(walletType,publicAddress);
#endif
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

        
        /// <summary>
        /// Call wallet_switchEthereumChain, only support wallet type MetaMask.
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="chainId">Chain id</param>
        /// <returns></returns>
        public Task<NativeResultData> SwitchEthereumChain(WalletType walletType, string publicAddress, int chainId)
        {
            switchEthereumChainTask = new TaskCompletionSource<NativeResultData>();

            // Wallet type WalletConnect contains metamask when connect with show qrcode.
            if (walletType != WalletType.MetaMask && walletType != WalletType.WalletConnect)
            {
                NativeErrorData errorData = new NativeErrorData("not support wallet type", 0, "");
                var data = new NativeResultData(false, JsonConvert.SerializeObject(errorData));
                switchEthereumChainTask.TrySetResult(data);
                return switchEthereumChainTask.Task;
            }
            else
            {
#if UNITY_EDITOR
#else
            ParticleConnectInteraction.SwitchEthereumChain(walletType,publicAddress,chainId);
#endif
                return switchEthereumChainTask.Task;  
            }
        }

        
        public void SwitchEthereumChainCallBack(string json)
        {
            Debug.Log($"switchEthereumChainCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            switchEthereumChainTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Call wallet_addEthereumChain, only support wallet type MetaMask.
        /// </summary>
        /// <param name="walletType">Wallet type</param>
        /// <param name="publicAddress">Public address</param>
        /// <param name="chainId">Chain id</param>
        /// <param name="chainName">Chain name</param>
        /// <param name="nativeCurrency">Chain native curreny</param>
        /// <param name="rpcUrl">Rpc url</param>
        /// <param name="blockExplorerUrl">Block explorer url</param>
        /// <returns></returns>
        public Task<NativeResultData> AddEthereumChain(WalletType walletType, string publicAddress, int chainId, [CanBeNull] string chainName = null, 
            [CanBeNull] NativeCurrency nativeCurrency = null, [CanBeNull] string rpcUrl = null, [CanBeNull] string blockExplorerUrl = null)
        {
            
            addEthereumChainTask = new TaskCompletionSource<NativeResultData>();

            // Wallet type WalletConnect contains metamask when connect with show qrcode.
            if (walletType != WalletType.MetaMask && walletType != WalletType.WalletConnect)
            {
                NativeErrorData errorData = new NativeErrorData("not support wallet type", 0, "");
                var data = new NativeResultData(false, JsonConvert.SerializeObject(errorData));
                addEthereumChainTask.TrySetResult(data);
                return addEthereumChainTask.Task;
            }
            else
            {
#if UNITY_EDITOR
#else
            ParticleConnectInteraction.AddEthereumChain(walletType,publicAddress,chainId, chainName, nativeCurrency, rpcUrl, blockExplorerUrl);
#endif
                return addEthereumChainTask.Task;
            }
        }

        public void AddEthereumChainCallBack(string json)
        {
            Debug.Log($"addEthereumChainTask:{json}");
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            addEthereumChainTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        
    }
}
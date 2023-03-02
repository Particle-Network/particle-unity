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
    public class ParticleAuthService : SingletonMonoBehaviour<ParticleAuthService>
    {
        
        private TaskCompletionSource<NativeResultData> loginTask;
        private TaskCompletionSource<NativeResultData> logoutTask;
        private TaskCompletionSource<NativeResultData> fastLogoutTask;
        private TaskCompletionSource<NativeResultData> setChainTask;

        private TaskCompletionSource<NativeResultData> signMessageTask;
        private TaskCompletionSource<NativeResultData> signTransactionTask;
        private TaskCompletionSource<NativeResultData> signAllTransactionsTask;
        private TaskCompletionSource<NativeResultData> signAndSendTransactionTask;
        private TaskCompletionSource<NativeResultData> signTypedDataTask;

        private TaskCompletionSource<NativeResultData> openAccountAndSecurityTask;
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginType">Login type</param>
        /// <param name="account">Account, default value is empty</param>
        /// <param name="supportAuthTypes">Support auth types, default value is all</param>
        /// <param name="loginFormMode">Login form mode</param>
        /// <param name="socialLoginPrompt">Social login prompt</param>
        /// <returns></returns>
        public Task<NativeResultData> Login(LoginType loginType, string account = "",
            SupportAuthType supportAuthTypes = SupportAuthType.ALL, bool loginFormMode = false, SocialLoginPrompt? socialLoginPrompt = null)
        {
            loginTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            DevModeService.Login();
#else
            ParticleAuthServiceInteraction.Login(loginType, account, supportAuthTypes, loginFormMode, socialLoginPrompt);
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
            var data = new NativeResultData(true, json);
            loginTask?.TrySetResult(data);
            PersistTools.SaveUserInfo(json);
#else
            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            loginTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
#endif
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> Logout()
        {
            logoutTask = new TaskCompletionSource<NativeResultData>();
            ParticleAuthServiceInteraction.Logout();
#if UNITY_EDITOR
              DevModeService.Logout();
            logoutTask?.TrySetResult(new NativeResultData(true, ""));
#endif
            return logoutTask.Task;
        }

        /// <summary>
        /// Logout call back
        /// </summary>
        /// <param name="json">Result</param>
        public void LogoutCallBack(string json)
        {
            Debug.Log($"LogoutCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int) resultData["status"];
            logoutTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        /// <summary>
        /// Fast logout, silently
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> FastLogout()
        {
            fastLogoutTask = new TaskCompletionSource<NativeResultData>();
            ParticleAuthServiceInteraction.FastLogout();
#if UNITY_EDITOR
            DevModeService.Logout();
            fastLogoutTask?.TrySetResult(new NativeResultData(true, ""));
#endif
            return fastLogoutTask.Task;
        }

        /// <summary>
        /// Logout call back
        /// </summary>
        /// <param name="json">Result</param>
        public void FastLogoutCallBack(string json)
        {
            Debug.Log($"FastLogoutCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int) resultData["status"];
            fastLogoutTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Set Chain Info Sync
        /// </summary>
        /// <param name="chainInfo">Chain info</param>
        /// <returns></returns>
        public bool SetChainInfoSync(ChainInfo chainInfo)
        {
            return ParticleNetwork.SetChainInfo(chainInfo);
        }

        /// <summary>
        /// Sign Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public Task<NativeResultData> SignMessage(string message)
        {
            signMessageTask = new TaskCompletionSource<NativeResultData>();
#if UNITY_EDITOR
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                DevModeService.EvmSignMessages(new[] {message});
            }
            else
            {
                DevModeService.SolanaSignMessages(new[] {message});
            }
#else
            ParticleAuthServiceInteraction.SignMessage(message);
#endif
            return signMessageTask.Task;
        }

        /// <summary>
        /// Sign Message call back
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
        /// Set Chain Info Async, because ParticleAuthService support both solana and evm, if switch to solana from evm,
        /// Auth Service will create a evm address if the user doesn't has a evm address.
        /// </summary>
        /// <param name="chainInfo">Chain info</param>
        /// <returns></returns>
        public Task<NativeResultData> SetChainInfoAsync(ChainInfo chainInfo)
        {
            Debug.Log(chainInfo);
            setChainTask = new TaskCompletionSource<NativeResultData>();
            ParticleAuthServiceInteraction.SetChainInfoAsync(chainInfo);
#if UNITY_EDITOR
            LoginCallBack(JsonConvert.SerializeObject(new JObject
            {
                {"status", 1},
                {"data", ""},
            }));
#endif
            return setChainTask.Task;
        }

        /// <summary>
        /// Set Chain Info Async call back
        /// </summary>
        /// <param name="json"></param>
        public void SetChainInfoAsyncCallBack(string json)
        {
            Debug.Log($"SetChainCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int) resultData["status"];
            setChainTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }


        /// <summary>
        /// Sign Transaction
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns></returns>
        public Task<NativeResultData> SignTransaction(string transaction)
        {
            signTransactionTask = new TaskCompletionSource<NativeResultData>();

#if UNITY_EDITOR
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                DevModeService.EvmSignTransactions(new[] {transaction});
            }
            else
            {
                DevModeService.SolanaSignTransactions(new[] {transaction});
            }
#else
             ParticleAuthServiceInteraction.SignTransaction(transaction);
#endif
            return signTransactionTask.Task;
        }

        /// <summary>
        /// Sign Transaction call back
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
        /// Sign All Transations
        /// </summary>
        /// <param name="transactions">Transactions</param>
        /// <returns></returns>
        public Task<NativeResultData> SignAllTransactions(string[] transactions)
        {
            signAllTransactionsTask = new TaskCompletionSource<NativeResultData>();
            ParticleAuthServiceInteraction.SignAllTransactions(transactions);
#if UNITY_EDITOR
            LoginCallBack(JsonConvert.SerializeObject(new JObject
            {
                {"status", 0},
                {"data", ""},
            }));
#endif
            return signAllTransactionsTask.Task;
        }

        /// <summary>
        /// Sign All Transactions call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignAllTransactionsCallBack(string json)
        {
            Debug.Log($"SignAllTransactionsCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int) resultData["status"];
            signAllTransactionsTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Sign And Send Transaction
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns></returns>
        public Task<NativeResultData> SignAndSendTransaction(string transaction)
        {
            signAndSendTransactionTask = new TaskCompletionSource<NativeResultData>();
            ParticleAuthServiceInteraction.SignAndSendTransaction(transaction);
#if UNITY_EDITOR
            LoginCallBack(JsonConvert.SerializeObject(new JObject
            {
                {"status", 0},
                {"data", ""},
            }));
#endif
            return signAndSendTransactionTask.Task;
        }

        /// <summary>
        /// Sign And Send Transaction call back
        /// </summary>
        /// <param name="json">Result</param>
        public void SignAndSendTransactionCallBack(string json)
        {
            Debug.Log($"signAndSendTransactionCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int) resultData["status"];
            signAndSendTransactionTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        /// <summary>
        /// Sign Typed Data
        /// </summary>
        /// <param name="typedData">Typed data</param>
        /// <param name="signTypedDataVersion">Sign typed data version, Particle Auth Service support V1, V3, V4</param>
        /// <returns></returns>
        public Task<NativeResultData> SignTypedData(string typedData, SignTypedDataVersion signTypedDataVersion)
        {
            signTypedDataTask = new TaskCompletionSource<NativeResultData>();
            ParticleAuthServiceInteraction.SignTypedData(typedData, signTypedDataVersion);
#if UNITY_EDITOR
            LoginCallBack(JsonConvert.SerializeObject(new JObject
            {
                {"status", 0},
                {"data", ""},
            }));
#endif
            return signTypedDataTask.Task;
        }

        /// <summary>
        /// Sign Typed Data call back
        /// </summary>
        /// <param name="json"></param>
        public void SignTypedDataCallBack(string json)
        {
            Debug.Log($"signTypedDataCallBack:{json}");
            var resultData = JObject.Parse(json);
            var status = (int) resultData["status"];
            signTypedDataTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
        
        
        /// <summary>
        /// Open account and security page.
        /// </summary>
        /// <returns></returns>
        public Task<NativeResultData> OpenAccountAndSecurity()
        {
            openAccountAndSecurityTask = new TaskCompletionSource<NativeResultData>();

            ParticleAuthServiceInteraction.OpenAccountAndSecurity();

            return openAccountAndSecurityTask.Task;
        }

        /// <summary>
        /// Open account and security call back
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
}
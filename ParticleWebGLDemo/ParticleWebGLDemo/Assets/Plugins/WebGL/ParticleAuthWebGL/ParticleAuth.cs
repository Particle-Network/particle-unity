using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class ParticleAuth : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void InitParticleAuth(string options);

    [DllImport("__Internal")]
    private static extern void SetParticleLanguage(string language);

    [DllImport("__Internal")]
    private static extern void SetParticleFiatCoin(string fiatCoin);

    [DllImport("__Internal")]
    private static extern void SetParticleAuthTheme(string options);

    [DllImport("__Internal")]
    private static extern void SetParticleERC4337(string json);

    [DllImport("__Internal")]
    private static extern void EnableParticleERC4337(bool enable);

    [DllImport("__Internal")]
    private static extern void LoginWithParticle(string options);

    [DllImport("__Internal")]
    private static extern void LogoutParticle();

    [DllImport("__Internal")]
    private static extern int IsParticleLoggedIn();

    [DllImport("__Internal")]
    private static extern string GetParticleUserInfo();

    [DllImport("__Internal")]
    private static extern void OpenParticleAccountAndSecurity();

    [DllImport("__Internal")]
    private static extern void GetParticleSecurityAccount();

    [DllImport("__Internal")]
    private static extern void OpenParticleWallet();

    [DllImport("__Internal")]
    private static extern void OpenParticleBuy(string options);

    [DllImport("__Internal")]
    private static extern string GetParticleWalletAddress();

    [DllImport("__Internal")]
    private static extern void ParticleSwitchChain(string options);

    [DllImport("__Internal")]
    private static extern void ParticleEVMSendTransaction(string transaction);

    [DllImport("__Internal")]
    private static extern void ParticleEVMPersonalSign(string message);

    [DllImport("__Internal")]
    private static extern void ParticleEVMPersonalSignUniq(string message);

    [DllImport("__Internal")]
    private static extern void ParticleEVMSignTypedData(string message);

    [DllImport("__Internal")]
    private static extern void ParticleEVMSignTypedDataUniq(string message);

    [DllImport("__Internal")]
    private static extern void ParticleSolanaSignAndSendTransaction(string transaction);

    [DllImport("__Internal")]
    private static extern void ParticleSolanaSignMessage(string message);

    [DllImport("__Internal")]
    private static extern void ParticleSolanaSignTransaction(string transaction);

    [DllImport("__Internal")]
    private static extern void ParticleSolanaSignAllTransactions(string json);


    public static ParticleAuth Instance;

    private TaskCompletionSource<string> loginTask;
    private TaskCompletionSource<bool> logoutTask;
    private TaskCompletionSource<string> getSecurityAccountTask;
    private TaskCompletionSource<string> switchChainTask;
    private TaskCompletionSource<string> evmSendTransactionTask;
    private TaskCompletionSource<string> evmPersonalSignTask;
    private TaskCompletionSource<string> evmPersonalSignUniqTask;
    private TaskCompletionSource<string> evmSignTypedDataTask;
    private TaskCompletionSource<string> evmSignTypedDataUniqTask;
    private TaskCompletionSource<string> solanaSignAndSendTransactionTask;
    private TaskCompletionSource<string> solanaSignMessageTask;
    private TaskCompletionSource<string> solanaSignTransactionTask;
    private TaskCompletionSource<List<string>> solanaSignAllTransactionsTask;


    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    /// <summary>
    /// Init with a config object
    /// </summary>
    /// <param name="config"></param>
    public void Init(InitConfig config)
    {
        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        SetInnerConfig(config.projectId, config.clientKey, config.appId, config.chainInfo);
        InitParticleAuth(json);
    }

    /// <summary>
    /// Init with a json string, must contains chainName and chainId
    /// </summary>
    /// <param name="json">config ison string</param>
    public void InitWithJsonString(string json)
    {
        var chainName = (string)JObject.Parse(json)["chainName"];
        var chainId = (long)JObject.Parse(json)["chainId"];
        var chainInfo = ChainInfo.GetChain(chainId, chainName);
        var projectId = (string)JObject.Parse(json)["projectId"];
        var clientKey = (string)JObject.Parse(json)["clientKey"];
        var appId = (string)JObject.Parse(json)["appId"];
        SetInnerConfig(projectId, clientKey, appId, chainInfo);
        InitParticleAuth(json);
    }

    private void SetInnerConfig(string projectId, string clientKey, string appId, ChainInfo chainInfo)
    {
        if (projectId == "" || clientKey == "" || appId == "")
        {
            throw new ErrorException(0, "Init config is wrong");
        }

        ParticleUnityRpc.projectId = projectId;
        ParticleUnityRpc.appId = appId;
        ParticleUnityRpc.clientKey = clientKey;
        UnityInnerChainInfo.SetChainInfo(chainInfo);
    }

    /// <summary>
    /// Set language, support en, zh-CN, zh-TW, zh-HK, ja, ko
    /// </summary>
    /// <param name="language"></param>
    public void SetLanguage(string language)
    {
        SetParticleLanguage(language);
    }

    /// <summary>
    /// Set fiatCoin, support  'USD' | 'CNY' | 'JPY' | 'HKD' | 'INR' | 'KRW'
    /// </summary>
    /// <param name="fiatCoin"></param>
    public void SetFiatCoin(string fiatCoin)
    {
        SetParticleFiatCoin(fiatCoin);
    }

    /// <summary>
    /// Set auth theme
    /// </summary>
    /// <param name="uiSettings"></param>
    public void SetAuthTheme(UISettings uiSettings)
    {
        var json = JsonConvert.SerializeObject(uiSettings);
        SetParticleAuthTheme(json);
    }

    /// <summary>
    /// Set account abstraction 
    /// </summary>
    /// <param name="accountName">AccountName</param>
    public void SetERC4337(AAAccountName accountName)
    {
        var json = JsonConvert.SerializeObject(accountName);
        SetParticleERC4337(json);
    }

    /// <summary>
    /// Enable ERC4337 
    /// </summary>
    /// <param name="enable">true is enable, false is disable</param>
    public void EnableERC4337(bool enable)
    {
        EnableParticleERC4337(enable);
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="config">login config</param>
    /// <returns></returns>
    public Task<string> Login(LoginConfig? config)
    {
        var json = JsonConvert.SerializeObject(config);
        loginTask = new TaskCompletionSource<string>();
        LoginWithParticle(json);
        return loginTask.Task;
    }

    // Called from browser
    public void OnLogin(string json)
    {
        HandleLoginResult(json, loginTask);
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    public Task<bool> Logout()
    {
        logoutTask = new TaskCompletionSource<bool>();
        LogoutParticle();
        return logoutTask.Task;
    }
    
    // Called from browser
    public void OnLogout()
    {
        logoutTask?.TrySetResult(true);
    }

    public bool IsLoggedIn()
    {
        return IsParticleLoggedIn() == 1;
    }


    /// <summary>
    /// Get user info after login
    /// </summary>
    /// <returns></returns>
    public string GetUserInfo()
    {
        return GetParticleUserInfo();
    }

    /// <summary>
    /// Open accoun and security page
    /// </summary>
    public void OpenAccountAndSecurity()
    {
        OpenParticleAccountAndSecurity();
    }

    public Task<string> GetSecurityAccount()
    {
        getSecurityAccountTask = new TaskCompletionSource<string>();
        GetParticleSecurityAccount();
        return getSecurityAccountTask.Task;
    }

    // Called from browser
    public void OnGetSecurityAccount(string json)
    {
        getSecurityAccountTask?.TrySetResult(json);
    }

    /// <summary>
    /// Open wallet page
    /// </summary>
    /// <param name="accountName">Optional, if you are using smart account, should provide this value</param>
    public void OpenWallet([CanBeNull] AAAccountName accountName = null)
    {
        if (accountName != null)
        {
            SetERC4337(accountName);
        }

        OpenParticleWallet();
    }

    public void OpenBuy(string options)
    {
        OpenParticleBuy(options);
    }
    
    /// <summary>
    /// Get address base on current chainInfo
    /// </summary>
    /// <returns></returns>
    public string GetWalletAddress()
    {
        return GetParticleWalletAddress();
    }

    /// <summary>
    /// Switch chain
    /// </summary>
    /// <param name="chainInfo"></param>
    /// <returns></returns>
    public Task<string> SwitchChain(ChainInfo chainInfo)
    {
        switchChainTask = new TaskCompletionSource<string>();
        var json = JsonConvert.SerializeObject(chainInfo);
        ParticleSwitchChain(json);
        return switchChainTask.Task;
    }

    public void OnSwitchChain(string json)
    {
        switchChainTask?.TrySetResult(json);
    }

    /// <summary>
    /// EVM send transaction
    /// </summary>
    /// <param name="transaction">The EVM transaction requires a hexadecimal string.</param>
    /// <returns></returns>
    public Task<string> EVMSendTransaction(string transaction)
    {
        var jsonString = "";
        if (transaction.StartsWith("0x"))
        {
            jsonString = HexToString(transaction);
        }
        else
        {
            jsonString = transaction;
        }

        evmSendTransactionTask = new TaskCompletionSource<string>();
        ParticleEVMSendTransaction(jsonString);
        return evmSendTransactionTask.Task;
    }


    public void OnEVMSendTransaction(string json)
    {
        HandleSignResult(json, evmSendTransactionTask);
    }

    /// <summary>
    /// EVM personal sign
    /// </summary>
    /// <param name="message">The message requires a hexadecimal string.</param>
    /// <param name="accountName">Optional, if you are using smart account, should provide this value</param>
    /// <returns></returns>
    public Task<string> EVMPersonalSign(string message, [CanBeNull] AAAccountName accountName = null)
    {
        if (accountName != null)
        {
            SetERC4337(accountName);
        }

        evmPersonalSignTask = new TaskCompletionSource<string>();
        ParticleEVMPersonalSign(message);
        return evmPersonalSignTask.Task;
    }

    public void OnEVMPersonalSign(string json)
    {
        HandleSignResult(json, evmPersonalSignTask);
    }


    /// <summary>
    /// EVM personal sign unique
    /// </summary>
    /// <param name="message">The message requires a hexadecimal string.</param>
    /// <returns></returns>
    public Task<string> EVMPersonalSignUniq(string message)
    {
        evmPersonalSignUniqTask = new TaskCompletionSource<string>();
        ParticleEVMPersonalSignUniq(message);
        return evmPersonalSignUniqTask.Task;
    }

    public void OnEVMPersonalSignUniq(string json)
    {
        HandleSignResult(json, evmPersonalSignUniqTask);
    }

    /// <summary>
    /// EVM sign typed data
    /// </summary>
    /// <param name="message">The message requires a json string.</param>
    /// <returns></returns>
    public Task<string> EVMSignTypedData(string message)
    {
        evmSignTypedDataTask = new TaskCompletionSource<string>();
        ParticleEVMSignTypedData(message);
        return evmSignTypedDataTask.Task;
    }

    public void OnEVMSignTypedData(string json)
    {
        HandleSignResult(json, evmSignTypedDataTask);
    }

    /// <summary>
    /// EVM sign typed data unique
    /// </summary>
    /// <param name="message">The message requires a json string.</param>
    /// <returns></returns>
    public Task<string> EVMSignTypedDataUniq(string message)
    {
        evmSignTypedDataUniqTask = new TaskCompletionSource<string>();
        ParticleEVMSignTypedDataUniq(message);
        return evmSignTypedDataUniqTask.Task;
    }

    public void OnEVMSignTypedDataUniq(string json)
    {
        HandleSignResult(json, evmSignTypedDataUniqTask);
    }

    /// <summary>
    /// Solana sign and send transaction
    /// </summary>
    /// <param name="transaction">The Solana transaction requires a base58 string.</param>
    /// <returns></returns>
    public Task<string> SolanaSignAndSendTransaction(string transaction)
    {
        solanaSignAndSendTransactionTask = new TaskCompletionSource<string>();
        ParticleSolanaSignAndSendTransaction(transaction);
        return solanaSignAndSendTransactionTask.Task;
    }

    public void OnSolanaSignAndSendTransaction(string json)
    {
        HandleSignResult(json, solanaSignAndSendTransactionTask);
    }

    /// <summary>
    /// Solana sign message
    /// </summary>
    /// <param name="transaction">The message requires a base58 string.</param>
    /// <returns></returns>
    public Task<string> SolanaSignMessage(string transaction)
    {
        solanaSignMessageTask = new TaskCompletionSource<string>();
        ParticleSolanaSignMessage(transaction);
        return solanaSignMessageTask.Task;
    }

    public void OnSolanaSignMessage(string json)
    {
        HandleSignResult(json, solanaSignMessageTask);
    }

    /// <summary>
    /// Solana sign transaction
    /// </summary>
    /// <param name="transaction">The Solana transaction requires a base58 string.</param>
    /// <returns></returns>
    public Task<string> SolanaSignTransaction(string transaction)
    {
        solanaSignTransactionTask = new TaskCompletionSource<string>();
        ParticleSolanaSignTransaction(transaction);
        return solanaSignTransactionTask.Task;
    }

    public void OnSolanaSignTransaction(string json)
    {
        HandleSignResult(json, solanaSignTransactionTask);
    }

    /// <summary>
    /// Solana sign all transactions
    /// </summary>
    /// <param name="transactions">Each Solana transaction requires a base58 string.</param>
    /// <returns></returns>
    public Task<List<string>> SolanaSignAllTransactions(string[] transactions)
    {
        solanaSignAllTransactionsTask = new TaskCompletionSource<List<string>>();
        var json = JsonConvert.SerializeObject(transactions);
        ParticleSolanaSignAllTransactions(json);
        return solanaSignAllTransactionsTask.Task;
    }

    public void OnSolanaSignAllTransactions(string json)
    {
        HandleSignResult(json, solanaSignAllTransactionsTask);
    }

    private void HandleLoginResult<T>(string json, TaskCompletionSource<T> task)
    {
        HandleResult(json, task, "");
    }

    private void HandleSignResult<T>(string json, TaskCompletionSource<T> task)
    {
        HandleResult(json, task, "signature");
    }

    private void HandleResult<T>(string json, TaskCompletionSource<T> task, string key)
    {
        Debug.Log($"handle result {json}");
        var jsonObject = JObject.Parse(json);
        
        if (jsonObject.ContainsKey("error"))
        {
            var error = jsonObject.GetValue("error");

            if (error != null && error is JObject errorObj)
            {
                var codeToken = errorObj.GetValue("code");
                var messageToken = errorObj.GetValue("message");

                if (codeToken != null && messageToken != null)
                {
                    var code = (int)codeToken;
                    var message = (string)messageToken;

                    task.SetException(new ErrorException(code, message));
                }
                else
                {
                    task.SetException(new ErrorException(0, "Unknown error: Missing 'code' or 'message' fields"));
                }
            }
            else
            {
                task.SetException(new ErrorException(0, "Unknown error: 'error' object missing"));
            }
        }
        else if (jsonObject.ContainsKey(key))
        {
            var value = jsonObject[key]!.ToObject<T>();
            task?.TrySetResult(value);
        }
        else
        {
            task?.TrySetResult((T)(object)json);
        }
    }

    private string HexToString(string hex)
    {
        if (hex.StartsWith("0x"))
        {
            hex = hex.Substring(2);
        }

        hex = hex.Replace("-", "");
        byte[] raw = new byte[hex.Length / 2];
        for (int i = 0; i < raw.Length; i++)
        {
            raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }

        return Encoding.ASCII.GetString(raw);
    }
}
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json;


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
    private static extern void SetParticleERC4337(bool enable);

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
    private static extern void ParticleEVMSendTransaction(string options);

    [DllImport("__Internal")]
    private static extern void ParticleEVMPersonalSign(string options);

    [DllImport("__Internal")]
    private static extern void ParticleEVMPersonalSignUniq(string options);

    [DllImport("__Internal")]
    private static extern void ParticleEVMSignTypedData(string options);

    [DllImport("__Internal")]
    private static extern void ParticleEVMSignTypedDataUniq(string options);

    [DllImport("__Internal")]
    private static extern void ParticleSolanaSignAndSendTransaction(string options);

    [DllImport("__Internal")]
    private static extern void ParticleSolanasSignMessage(string options);

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
    private TaskCompletionSource<string> solanasSignMessageTask;


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
        InitParticleAuth(json);
    }
    
    /// <summary>
    /// Init with a json string
    /// </summary>
    /// <param name="json"></param>
    public void InitWithJsonString(string json)
    {
        InitParticleAuth(json);
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

    public void SetERC4337(bool enable)
    {
        SetParticleERC4337(enable);
    }

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
        loginTask?.TrySetResult(json);
    }

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

    public void OnGetSecurityAccount(string json)
    {
        getSecurityAccountTask?.TrySetResult(json);
    }

    public void OpenWallet()
    {
        OpenParticleWallet();
    }

    public void OpenBuy(string options)
    {
        OpenParticleBuy(options);
    }

    public string GetWalletAddress()
    {
        return GetParticleWalletAddress();
    }

    public Task<string> SwitchChain(Chain chain)
    {
        switchChainTask = new TaskCompletionSource<string>();
        var json = JsonConvert.SerializeObject(chain);
        ParticleSwitchChain(json);
        return switchChainTask.Task;
    }

    public void OnSwitchChain(string json)
    {
        switchChainTask?.TrySetResult(json);
    }

    public Task<string> EVMSendTransaction(string transaction)
    {
        evmSendTransactionTask = new TaskCompletionSource<string>();
        ParticleEVMSendTransaction(transaction);
        return evmSendTransactionTask.Task;
    }

    public void OnEVMSendTransaction(string json)
    {
        evmSendTransactionTask?.TrySetResult(json);
    }

    public Task<string> EVMPersonalSign(string message)
    {
        evmPersonalSignTask = new TaskCompletionSource<string>();
        ParticleEVMPersonalSign(message);
        return evmPersonalSignTask.Task;
    }

    public void OnEVMPersonalSign(string json)
    {
        evmPersonalSignTask?.TrySetResult(json);
    }

    public Task<string> EVMPersonalSignUniq(string message)
    {
        evmPersonalSignUniqTask = new TaskCompletionSource<string>();
        ParticleEVMPersonalSignUniq(message);
        return evmPersonalSignUniqTask.Task;
    }

    public void OnEVMPersonalSignUniq(string json)
    {
        evmPersonalSignUniqTask?.TrySetResult(json);
    }

    public Task<string> EVMSignTypedData(string message)
    {
        evmSignTypedDataTask = new TaskCompletionSource<string>();
        ParticleEVMSignTypedData(message);
        return evmSignTypedDataTask.Task;
    }

    public void OnEVMSignTypedData(string json)
    {
        evmSignTypedDataTask?.TrySetResult(json);
    }

    public Task<string> EVMSignTypedDataUniq(string message)
    {
        evmSignTypedDataUniqTask = new TaskCompletionSource<string>();
        ParticleEVMSignTypedDataUniq(message);
        return evmSignTypedDataUniqTask.Task;
    }

    public void OnEVMSignTypedDataUniq(string json)
    {
        evmSignTypedDataUniqTask?.TrySetResult(json);
    }

    public Task<string> SolanaSignAndSendTransaction(string transaction)
    {
        solanaSignAndSendTransactionTask = new TaskCompletionSource<string>();
        ParticleSolanaSignAndSendTransaction(transaction);
        return solanaSignAndSendTransactionTask.Task;
    }

    public void OnSolanaSignAndSendTransaction(string json)
    {
        solanaSignAndSendTransactionTask?.TrySetResult(json);
    }

    public Task<string> SolanasSignMessage(string message)
    {
        solanasSignMessageTask = new TaskCompletionSource<string>();
        ParticleSolanasSignMessage(message);
        return solanasSignMessageTask.Task;
    }

    public void OnSolanasSignMessage(string json)
    {
        solanasSignMessageTask?.TrySetResult(json);
    }
}
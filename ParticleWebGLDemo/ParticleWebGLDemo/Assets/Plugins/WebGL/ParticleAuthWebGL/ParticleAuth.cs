using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;


public class ParticleAuth : MonoBehaviour {

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

    public string config;
    

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


    
    void Awake() {
        Instance = this;
        // replace xxxx with particle project config
        // {"projectId":"34c6b829-5b89-44e8-90a9-6d982787b9c9","clientKey":"c6Z44Ml4TQeNhctvwYgdSv6DBzfjf6t6CB0JDscR","appId":"64f36641-b68c-4b19-aa10-5c5304d0eab3","chainName":"Ethereum","chainId":1}'
        InitParticleAuth(config);
    }

    void OnDestroy() {
        Instance = null;
    }

    public void SetLanguage(string language) {
        SetParticleLanguage(language);
    }

    public void SetFiatCoin(string fiatCoin) {
        SetParticleFiatCoin(fiatCoin);
    }

    public void SetAuthTheme(string options) {
        SetParticleAuthTheme(options);
    }

    public void SetERC4337(bool enable)
    {
        SetParticleERC4337(enable);
    }

    public Task<string> Login(string options) {
        loginTask = new TaskCompletionSource<string>();
        LoginWithParticle(options);
        return loginTask.Task;
    }

    // Called from browser
    public void OnLogin(string json) {
        loginTask?.TrySetResult(json);
    }

    public Task<bool> Logout() {
        logoutTask = new TaskCompletionSource<bool>();
        LogoutParticle();
        return logoutTask.Task;
    }

    // Called from browser
    public void OnLogout() {
        logoutTask?.TrySetResult(true);
    }

    public bool IsLoggedIn() {
        return IsParticleLoggedIn() == 1;
    }

    public string GetUserInfo() {
        return GetParticleUserInfo();
    }

    public void OpenAccountAndSecurity() {
        OpenParticleAccountAndSecurity();
    }

    public Task<string> GetSecurityAccount() {
        getSecurityAccountTask = new TaskCompletionSource<string>();
        GetParticleSecurityAccount();
        return getSecurityAccountTask.Task;
    }

    public void OnGetSecurityAccount(string json) {
        getSecurityAccountTask?.TrySetResult(json);
    }

    public void OpenWallet() {
        OpenParticleWallet();
    }

    public void OpenBuy(string options) {
        OpenParticleBuy(options);
    }

    public string GetWalletAddress() {
        return GetParticleWalletAddress();
    }

    public Task<string> SwitchChain(string options) {
        switchChainTask = new TaskCompletionSource<string>();
        ParticleSwitchChain(options);
        return switchChainTask.Task;
    }

    public void OnSwitchChain(string json) {
        switchChainTask?.TrySetResult(json);
    }

    public Task<string> EVMSendTransaction(string options) {
        evmSendTransactionTask = new TaskCompletionSource<string>();
        ParticleEVMSendTransaction(options);
        return evmSendTransactionTask.Task;
    }

    public void OnEVMSendTransaction(string json) {
        evmSendTransactionTask?.TrySetResult(json);
    }

    public Task<string> EVMPersonalSign(string options) {
        evmPersonalSignTask = new TaskCompletionSource<string>();
        ParticleEVMPersonalSign(options);
        return evmPersonalSignTask.Task;
    }

    public void OnEVMPersonalSign(string json) {
        evmPersonalSignTask?.TrySetResult(json);
    }

    public Task<string> EVMPersonalSignUniq(string options) {
        evmPersonalSignUniqTask = new TaskCompletionSource<string>();
        ParticleEVMPersonalSignUniq(options);
        return evmPersonalSignUniqTask.Task;
    }

    public void OnEVMPersonalSignUniq(string json) {
        evmPersonalSignUniqTask?.TrySetResult(json);
    }

    public Task<string> EVMSignTypedData(string options) {
        evmSignTypedDataTask = new TaskCompletionSource<string>();
        ParticleEVMSignTypedData(options);
        return evmSignTypedDataTask.Task;
    }

    public void OnEVMSignTypedData(string json) {
        evmSignTypedDataTask?.TrySetResult(json);
    }

    public Task<string> EVMSignTypedDataUniq(string options) {
        evmSignTypedDataUniqTask = new TaskCompletionSource<string>();
        ParticleEVMSignTypedDataUniq(options);
        return evmSignTypedDataUniqTask.Task;
    }

    public void OnEVMSignTypedDataUniq(string json) {
        evmSignTypedDataUniqTask?.TrySetResult(json);
    }

    public Task<string> SolanaSignAndSendTransaction(string options) {
        solanaSignAndSendTransactionTask = new TaskCompletionSource<string>();
        ParticleSolanaSignAndSendTransaction(options);
        return solanaSignAndSendTransactionTask.Task;
    }

    public void OnSolanaSignAndSendTransaction(string json) {
        solanaSignAndSendTransactionTask?.TrySetResult(json);
    }

    public Task<string> SolanasSignMessage(string options) {
        solanasSignMessageTask = new TaskCompletionSource<string>();
        ParticleSolanasSignMessage(options);
        return solanasSignMessageTask.Task;
    }

    public void OnSolanasSignMessage(string json) {
        solanasSignMessageTask?.TrySetResult(json);
    }
}
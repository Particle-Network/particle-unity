using System.Threading.Tasks;
using System.Web;
using Network.Particle.Scripts.Singleton;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Particle.Windows.Modules.Models;
using UnityEngine;
using Vuplex.WebView;

namespace Windows.Modules
{
    public class ParticleSystem : SingletonMonoBehaviour<ParticleSystem>
    {
        public CanvasWebViewPrefab _canvasWebViewPrefab;

        private string _theme;
        private string _language;
        private string _chainName;
        private long _chainId;
        private string _config;
        private readonly string _walletURL = "https://auth-bridge-debug.particle.network/";

        private TaskCompletionSource<string> LoginTask;
        
        private TaskCompletionSource<string> SignMessageTask;
        private TaskCompletionSource<string> SignAndSendTransactionTask;
        private TaskCompletionSource<string> SignTypedDataTask;
        private TaskCompletionSource<string> SignTransactionTask;
        private TaskCompletionSource<string> SignAllTransactionsTask;
        async void Start()
        {
            // _canvasWebViewPrefab.gameObject.SetActive(false);

            await _canvasWebViewPrefab.WaitUntilInitialized();
            
            _canvasWebViewPrefab.WebView.MessageEmitted += (sender, eventArgs) => {
                // > JSON received: { "type": "greeting", "message": "Hello from JavaScript!" }
                Debug.Log("JSON received: " + eventArgs.Value);
                var type = JObject.Parse(eventArgs.Value)["type"]?.ToString();
                if (type == "onlogin")
                {
                    var jsonString = JObject.Parse(eventArgs.Value)["message"]?.ToString();
                    OnLogin(jsonString);
                } else if (type == "onsign")
                {
                    var jsonString = JObject.Parse(eventArgs.Value)["message"]?.ToString();
                    OnSign(jsonString);
                }
            };
            
            _canvasWebViewPrefab.WebView.UrlChanged += (sender, eventArgs) =>
            {
                Debug.Log("[CanvasWebViewDemo] URL changed: " + eventArgs.Url);
            };
        }

        public void Init(string config, string theme, string language, string chainName, long chainId)
        {
            this._config = config;
            this._theme = theme;
            this._language = language;
            this._chainName = chainName;
            this._chainId = chainId;
        }

        public Task<string> Login(string preferredAuthType, string account)
        {
            LoginTask = new TaskCompletionSource<string>();
            
            if (_canvasWebViewPrefab == null)
            {
                LoginTask.TrySetResult("");
                return LoginTask.Task;;
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainName}&chainId={_chainId}&preferredAuthType={preferredAuthType}&account={account}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "login";
            var uri = $"{_walletURL}{path}?{temp}";


            Debug.Log($"Particle login URI = {uri}");

            _canvasWebViewPrefab.gameObject.SetActive(true);
            
            _canvasWebViewPrefab.WebView.LoadUrl(uri);
            

            return LoginTask.Task;
        }

        public Task<string> SignMessage(string message)
        {
            SignMessageTask = new TaskCompletionSource<string>();

            if (_canvasWebViewPrefab == null)
            {
                SignMessageTask.TrySetResult("");
                return SignMessageTask.Task;
                ;
            }

            string method;
            if (_chainName.ToLower() == "solana" && (_chainId == 101 || _chainId == 102 || _chainId == 103))
            {
                method = SignMethod.signMessage.ToString();
            }
            else
            {
                method = SignMethod.personal_Sign.ToString();
            }

            var queryStr = $"config={_config}&theme={_theme}&language={_language}&chainName={_chainName}&chainId={_chainId}&method={method}&message={message}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";
            
            Debug.Log($"Particle SignMessage URI = {uri}");

            _canvasWebViewPrefab.WebView.LoadUrl(uri);

            _canvasWebViewPrefab.gameObject.SetActive(true);

            return SignMessageTask.Task;
        }
        
        private void OnLogin(string jsonString)
        {
            if (_canvasWebViewPrefab == null)
                return;
            LoginTask?.TrySetResult(jsonString);

            _canvasWebViewPrefab.gameObject.SetActive(false);
            
            Debug.Log($"Particle OnLogin JsonString = {jsonString}");
            
        }
        
        private void OnSign(string jsonString) {
            if (_canvasWebViewPrefab == null)
                return;
            var result = JsonConvert.DeserializeObject<OnSignResult>(jsonString);

            
            if (result.Method == SignMethod.personal_Sign.ToString() || result.Method == SignMethod.signMessage.ToString())
            {
                SignMessageTask.TrySetResult(jsonString);
            }
            else if (result.Method == SignMethod.eth_SignTypedData.ToString()
                     || result.Method == SignMethod.eth_SignTypedData1.ToString()
                     || result.Method == SignMethod.eth_SignTypedData3.ToString()
                     || result.Method == SignMethod.eth_SignTypedData4.ToString())
            {
                SignTypedDataTask.TrySetResult(jsonString);
            }
            else if (result.Method == SignMethod.eth_SendTransaction.ToString() || result.Method == SignMethod.signAndSendTransaction.ToString())
            {
                SignAndSendTransactionTask.TrySetResult(jsonString);
            }
            else if (result.Method == SignMethod.signTransaction.ToString())
            {
                SignTransactionTask.TrySetResult(jsonString);
            }
            else if (result.Method == SignMethod.signAllTransactions.ToString())
            {
                SignAllTransactionsTask.TrySetResult(jsonString);
            }
        }
    }
}
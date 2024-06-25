#if !UNITY_ANDROID && !UNITY_IOS
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Web;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Particle.Windows.Modules.Models;
using UnityEngine;
using Vuplex.WebView;
using SignTypedDataVersion = Particle.Windows.Modules.Models.SignTypedDataVersion;

namespace Particle.Windows
{
    public class ParticleSystem : SingletonMonoBehaviour<ParticleSystem>
    {
        public CanvasWebViewPrefab canvasWebViewPrefab;

        private string _theme;
        private string _language;
        private ChainInfo _chainInfo;
        private string _config;
        private readonly string _walletURL = "https://auth-bridge.particle.network/";

        private TaskCompletionSource<string> _loginTask;

        private TaskCompletionSource<string> _signMessageTask;
        private TaskCompletionSource<string> _signAndSendTransactionTask;
        private TaskCompletionSource<string> _signTypedDataTask;
        private TaskCompletionSource<string> _signTransactionTask;
        private TaskCompletionSource<List<string>> _signAllTransactionsTask;

        async void Start()
        {
            await canvasWebViewPrefab.WaitUntilInitialized();

            canvasWebViewPrefab.WebView.MessageEmitted += (sender, eventArgs) =>
            {
                // > JSON received: { "type": "greeting", "message": "Hello from JavaScript!" }
                Debug.Log("JSON received: " + eventArgs.Value);
                var type = JObject.Parse(eventArgs.Value)["type"]?.ToString();
                if (type == "onlogin")
                {
                    var jsonString = JObject.Parse(eventArgs.Value)["message"]?.ToString();
                    OnLogin(jsonString);
                }
                else if (type == "onsign")
                {
                    var jsonString = JObject.Parse(eventArgs.Value)["message"]?.ToString();
                    OnSign(jsonString);
                }
                else if (type == "PARTICLE_WALLET_CLOSE_IFRAME")
                {
                    OnCloseWallet();
                }
            };

            Debug.Log("[CanvasWebViewDemo] Initialized finished");

            canvasWebViewPrefab.WebView.UrlChanged += (sender, eventArgs) =>
            {
                Debug.Log("[CanvasWebViewDemo] URL changed: " + eventArgs.Url);
            };
        }

        /// <summary>
        /// Particle Windows Init, required
        /// </summary>
        /// <param name="config">Config json string</param>
        /// <param name="theme">Theme json string</param>
        /// <param name="language">Language</param>
        /// <param name="chainInfo">Chain info</param>
        public void Init(string config, string theme, string language, ChainInfo chainInfo)
        {
            this._config = config;
            this._theme = theme;
            this._language = language;
            this._chainInfo = chainInfo;
            UnityInnerChainInfo.SetChainInfo(chainInfo);

            Debug.Log(
                $"Particle SDK init, Config = {config}, ChainName = {chainInfo.Name}, ChainId = {chainInfo.Id}, Language = {language}, Theme = {theme}");
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="preferredAuthType">email, phone or jwt</param>
        /// <param name="account">for email or phone, it is optional. required for jwt</param>
        /// <returns>Login result json string</returns>
        public Task<string> Login(PreferredAuthType preferredAuthType, string account)
        {
            _loginTask = new TaskCompletionSource<string>();

            if (canvasWebViewPrefab == null)
            {
                _loginTask.TrySetResult("");
                return _loginTask.Task;
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&preferredAuthType={preferredAuthType.ToString()}&account={account}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "login";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle login URI = {uri}");

            canvasWebViewPrefab.gameObject.SetActive(true);

            canvasWebViewPrefab.WebView.LoadUrl(uri);

            return _loginTask.Task;
        }

        /// <summary>
        /// Sign message, support evm and solana.
        /// </summary>
        /// <param name="message">In evm, request plain text, like "hello world", in solana, request base58 string.</param>
        /// <returns></returns>
        public Task<string> SignMessage(string message)
        {
            _signMessageTask = new TaskCompletionSource<string>();

            if (canvasWebViewPrefab == null)
            {
                _signMessageTask.TrySetResult("");
                return _signMessageTask.Task;
            }

            string method;
            if (_chainInfo.IsSolanaChain())
            {
                method = SignMethod.signMessage.ToString();
            }
            else
            {
                method = SignMethod.personal_sign.ToString();
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&method={method}&message={message}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignMessage URI = {uri}");

            canvasWebViewPrefab.WebView.LoadUrl(uri);

            canvasWebViewPrefab.gameObject.SetActive(true);

            return _signMessageTask.Task;
        }

        /// <summary>
        /// Sign message unique, support evm and solana.
        /// </summary>
        /// <param name="message">In evm, request plain text, like "hello world", in solana, request base58 string.</param>
        /// <returns></returns>
        public Task<string> SignMessageUnique(string message)
        {
            _signMessageTask = new TaskCompletionSource<string>();

            if (canvasWebViewPrefab == null)
            {
                _signMessageTask.TrySetResult("");
                return _signMessageTask.Task;
            }

            string method;
            if (_chainInfo.IsSolanaChain())
            {
                var errorData = new JObject
                {
                    { "code", 0 },
                    { "message", "unsupported method in solana" },
                };

                _signMessageTask.TrySetResult(errorData.ToString());
                return _signMessageTask.Task;
            }
            else
            {
                method = SignMethod.personal_sign_uniq.ToString();
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&method={method}&message={message}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignMessageUnique URI = {uri}");

            canvasWebViewPrefab.WebView.LoadUrl(uri);

            canvasWebViewPrefab.gameObject.SetActive(true);

            return _signMessageTask.Task;
        }

        /// <summary>
        /// Sign and send transaction, support evm and solana.
        /// </summary>
        /// <param name="transaction">In evm, request json string, in solana, request base58 string.</param>
        /// <returns></returns>
        public Task<string> SignAndSendTransaction(string transaction)
        {
            _signAndSendTransactionTask = new TaskCompletionSource<string>();

            if (canvasWebViewPrefab == null)
            {
                _signAndSendTransactionTask.TrySetResult("");
                return _signAndSendTransactionTask.Task;
            }

            string method;
            if (_chainInfo.IsSolanaChain())
            {
                method = SignMethod.signAndSendTransaction.ToString();
            }
            else
            {
                method = SignMethod.eth_sendTransaction.ToString();
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&method={method}&message={transaction}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignAndSendTransaction URI = {uri}");

            canvasWebViewPrefab.WebView.LoadUrl(uri);

            canvasWebViewPrefab.gameObject.SetActive(true);

            return _signAndSendTransactionTask.Task;
        }

        /// <summary>
        /// Sign typed data, only support evm.
        /// </summary>
        /// <param name="message">Typed data string.</param>
        /// <param name="version">Support v1, v3, v4, default is v4.</param>
        /// <returns></returns>
        public Task<string> SignTypedData(string message, SignTypedDataVersion version)
        {
            _signTypedDataTask = new TaskCompletionSource<string>();

            if (canvasWebViewPrefab == null)
            {
                _signTypedDataTask.TrySetResult("");
                return _signTypedDataTask.Task;
            }

            string method;
            if (_chainInfo.IsSolanaChain())
            {
                var errorData = new JObject
                {
                    { "code", 0 },
                    { "message", "unsupported method in solana" },
                };

                _signTypedDataTask.TrySetResult(errorData.ToString());
                return _signTypedDataTask.Task;
            }
            else
            {
                switch (version)
                {
                    case SignTypedDataVersion.Default:
                        method = SignMethod.eth_signTypedData.ToString();
                        break;
                    case SignTypedDataVersion.v1:
                        method = SignMethod.eth_signTypedData_v1.ToString();
                        break;
                    case SignTypedDataVersion.v3:
                        method = SignMethod.eth_signTypedData_v3.ToString();
                        break;
                    case SignTypedDataVersion.v4:
                        method = SignMethod.eth_signTypedData_v4.ToString();
                        break;
                    case SignTypedDataVersion.v4Unique:
                        method = SignMethod.eth_signTypedData_v4_uniq.ToString();
                        break;
                    default:
                        method = SignMethod.eth_signTypedData.ToString();
                        break;
                }
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&method={method}&message={message}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignTypedData URI = {uri}");

            canvasWebViewPrefab.WebView.LoadUrl(uri);

            canvasWebViewPrefab.gameObject.SetActive(true);

            return _signTypedDataTask.Task;
        }

        /// <summary>
        /// Sign transaction, only support solana.
        /// </summary>
        /// <param name="transaction">Request base58 string.</param>
        /// <returns></returns>
        public Task<string> SignTransaction(string transaction)
        {
            _signTransactionTask = new TaskCompletionSource<string>();

            if (canvasWebViewPrefab == null)
            {
                _signTransactionTask.TrySetResult("");
                return _signTransactionTask.Task;
            }

            string method;
            if (_chainInfo.IsSolanaChain())
            {
                method = SignMethod.signTransaction.ToString();
            }
            else
            {
                var errorData = new JObject
                {
                    { "code", 0 },
                    { "message", "unsupported method in evm" },
                };

                _signTypedDataTask.TrySetResult(errorData.ToString());
                return _signTypedDataTask.Task;
            }


            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&method={method}&message={transaction}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignTransaction URI = {uri}");

            canvasWebViewPrefab.WebView.LoadUrl(uri);

            canvasWebViewPrefab.gameObject.SetActive(true);

            return _signTransactionTask.Task;
        }

        /// <summary>
        /// Sign all transactions, only support solana.
        /// </summary>
        /// <param name="transactions">Request base58 string list</param>
        /// <returns></returns>
        public Task<List<string>> SignAllTransactions(List<string> transactions)
        {
            _signAllTransactionsTask = new TaskCompletionSource<List<string>>();

            var message = JsonConvert.SerializeObject(transactions);
            if (canvasWebViewPrefab == null)
            {
                _signAllTransactionsTask.TrySetResult(new List<string>());
                return _signAllTransactionsTask.Task;
            }

            string method;
            if (_chainInfo.IsSolanaChain())
            {
                method = SignMethod.signAllTransactions.ToString();
            }
            else
            {
                var errorData = new JObject
                {
                    { "code", 0 },
                    { "message", "unsupported method in evm" },
                };

                _signAllTransactionsTask.TrySetResult(new List<string> { errorData.ToString() });
                return _signAllTransactionsTask.Task;
            }

            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}&method={method}&message={message}";
            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "sign";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignAllTransactions URI = {uri}");
            canvasWebViewPrefab.WebView.LoadUrl(uri);
            canvasWebViewPrefab.gameObject.SetActive(true);
            return _signAllTransactionsTask.Task;
        }

        private void OnLogin(string jsonString)
        {
            if (canvasWebViewPrefab == null)
                return;
            _loginTask?.TrySetResult(jsonString);

            canvasWebViewPrefab.gameObject.SetActive(false);

            Debug.Log($"Particle OnLogin JsonString = {jsonString}");
        }

        private void OnSign(string jsonString)
        {
            if (canvasWebViewPrefab == null)
                return;

            Debug.Log($"Particle OnSign JsonString = {jsonString}");

            var result = JsonConvert.DeserializeObject<OnSignResult>(jsonString);

            if (result.Method == SignMethod.personal_sign.ToString() ||
                result.Method == SignMethod.signMessage.ToString())
            {
                _signMessageTask.TrySetResult(result.Signature);
            }
            else if (result.Method == SignMethod.eth_signTypedData.ToString()
                     || result.Method == SignMethod.eth_signTypedData_v1.ToString()
                     || result.Method == SignMethod.eth_signTypedData_v3.ToString()
                     || result.Method == SignMethod.eth_signTypedData_v4.ToString())
            {
                _signTypedDataTask.TrySetResult(result.Signature);
            }
            else if (result.Method == SignMethod.eth_sendTransaction.ToString() ||
                     result.Method == SignMethod.signAndSendTransaction.ToString())
            {
                _signAndSendTransactionTask.TrySetResult(result.Signature);
            }
            else if (result.Method == SignMethod.signTransaction.ToString())
            {
                _signTransactionTask.TrySetResult(result.Signature);
            }
            else if (result.Method == SignMethod.signAllTransactions.ToString())
            {
                var signatureList = JsonConvert.DeserializeObject<string[]>(result.Signature).ToList();
                _signAllTransactionsTask.TrySetResult(signatureList);
            }

            canvasWebViewPrefab.gameObject.SetActive(false);
        }

        private void OnCloseWallet()
        {
            if (canvasWebViewPrefab == null)
                return;

            canvasWebViewPrefab.WebView.LoadHtml("<html><body></body></html>");
            canvasWebViewPrefab.gameObject.SetActive(false);
        }

        /// <summary>
        /// Easy method to make a evm transaction
        /// </summary>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="data">Data</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public string MakeEvmTransaction(string from, string to, string data, string value)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "from", from },
                { "to", to },
                { "data", data },
                { "value", value },
                { "chainId", _chainInfo.Id },
            });

            return json;
        }

        /// <summary>
        /// Update Chain info
        /// </summary>
        /// <param name="chainInfo">Chain info</param>
        public void UpdateChainInfo(ChainInfo chainInfo)
        {
            this._chainInfo = chainInfo;
        }

        /// <summary>
        /// Open web wallet 
        /// </summary>
        /// <param name="accountName">Optional, used when you prefer to open aa wallet</param>
        public void OpenWebWallet([CanBeNull] AAAccountName accountName)
        {
            var queryStr =
                $"config={_config}&theme={_theme}&language={_language}&chainName={_chainInfo.Name}&chainId={_chainInfo.Id}";

            if (accountName != null)
            {
                var erc4337Params = JsonConvert.SerializeObject(accountName);
                queryStr += $"&erc4337={erc4337Params}";
            }

            var temp = HttpUtility.UrlEncode(queryStr);
            var path = "wallet";
            var uri = $"{_walletURL}{path}?{temp}";

            Debug.Log($"Particle SignMessage URI = {uri}");

            canvasWebViewPrefab.WebView.LoadUrl(uri);
            canvasWebViewPrefab.gameObject.SetActive(true);
        }
    }
}

#endif
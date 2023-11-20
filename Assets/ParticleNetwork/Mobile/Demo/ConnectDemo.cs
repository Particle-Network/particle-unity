using System;
using System.Reflection;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using CommonTip.Script;

namespace Network.Particle.Scripts.Test
{
    public class ConnectDemo : MonoBehaviour
    {
        private ChainInfo _chainInfo = ChainInfo.EthereumGoerli;

        private string loginSourceMessage = "";
        private string loginSignature = "";

        // before test in devices, preset wallet and test account
        private WalletType _walletType;
        private TestAccount _account;

        private string publicAddress = "";

        private void Start()
        {
            this._walletType = WalletType.MetaMask;
            this._account = TestAccount.EVM;
        }

        public void SelectChain()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"select chain {chainInfo.Name} {chainInfo.Id} {chainInfo.Network}");
                this._chainInfo = chainInfo;
            });
        }

        public void SelectWallet()
        {
            WalletChoice.Instance.Show(walletType =>
            {
                Debug.Log($"xxhong {walletType}");
                this._walletType = walletType;
            });
        }

        public void Init()
        {
            var metadata = new DAppMetaData(TestConfig.walletConnectProjectId, "Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network",
                "");
            ParticleNetwork.Init(_chainInfo);
            ParticleConnectInteraction.Init(_chainInfo, metadata);
            // List<ChainInfo> chainInfos = new List<ChainInfo>{new EthereumChain(EthereumChainId.Mainnet), new PolygonChain(PolygonChainId.Mainnet), new EthereumChain(EthereumChainId.Sepolia)};
            // ParticleConnectInteraction.SetWalletConnectV2SupportChainInfos(chainInfos.ToArray());
        }

        /// <summary>
        /// Before test connect to wallet connect, like metamask wallet, you should login metamask with our evm test account.
        /// </summary>
        public async void Connect()
        {
            try
            {
                var nativeResultData = await ParticleConnect.Instance.Connect(this._walletType);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    publicAddress = JObject.Parse(nativeResultData.data)["publicAddress"].ToString();
                    Tips.Instance.Show(
                        $"{MethodBase.GetCurrentMethod()?.Name} publicAddress:{publicAddress}  Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void ConnectWithParticleParams()
        {
            try
            {
                ConnectConfig config = null;
                if (_walletType == WalletType.Particle)
                {
                    config = new ConnectConfig(LoginType.GOOGLE, null, SupportAuthType.NONE);
                }

                var nativeResultData = await ParticleConnect.Instance.Connect(this._walletType, config);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    publicAddress = JObject.Parse(nativeResultData.data)["publicAddress"].ToString();
                    Tips.Instance.Show(
                        $"{MethodBase.GetCurrentMethod()?.Name} publicAddress:{publicAddress} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void Disconnect()
        {
            try
            {
                // Test public address
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");

                var nativeResultData = await ParticleConnect.Instance.Disconnect(this._walletType, publicAddress);

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public void IsConnected()
        {
            if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
            var isConnect = ParticleConnectInteraction.IsConnected(this._walletType, publicAddress);
            Tips.Instance.Show(
                $"Particle Connect is Connect = {isConnect}, publicAddress = {publicAddress}, walletType = {this._walletType.ToString()}");
            Debug.Log(
                $"Particle Connect is Connect = {isConnect}, publicAddress = {publicAddress}, walletType = {this._walletType.ToString()}");
        }

        public async void SignAndSendTransaction()
        {
            try
            {
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                string transaction;
                if (!_account.PublicAddress.StartsWith("0x"))
                {
                    transaction = await TransactionHelper.GetSolanaTransacion(publicAddress);
                }
                else
                {
                    transaction = await TransactionHelper.GetEVMTransacion(publicAddress);
                }

                Debug.Log("transaction = " + transaction);
                var nativeResultData =
                    await ParticleConnect.Instance.SignAndSendTransaction(this._walletType, publicAddress, transaction);
                Debug.Log(nativeResultData.data);
                Tips.Instance.Show(nativeResultData.data);
                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }


        public async void SignTransaction()
        {
            try
            {
                // sign transaction doesn't support evm.
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                var transaction = await TransactionHelper.GetSolanaTransacion(publicAddress);
                Debug.Log("transaction = " + transaction);
                var nativeResultData =
                    await ParticleConnect.Instance.SignTransaction(this._walletType, publicAddress, transaction);
                Debug.Log(nativeResultData.data);
                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignAllTransactions()
        {
            try
            {
                // sign all transactions doesn't support evm.
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                var transaction1 = await TransactionHelper.GetSolanaTransacion(publicAddress);
                var transaction2 = await TransactionHelper.GetSolanaTransacion(publicAddress);
                Debug.Log("transaction1 = " + transaction1);
                Debug.Log("transaction2 = " + transaction2);
                string[] transactions = new[] { transaction1, transaction2 };
                var nativeResultData =
                    await ParticleConnect.Instance.SignAllTransactions(this._walletType, publicAddress, transactions);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignMessage()
        {
            try
            {
                // string publicAddress;
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                var message = "Hello world";
                Debug.Log($"SignMessage-> publicAddress:{publicAddress} message:{message}");
                var nativeResultData =
                    await ParticleConnect.Instance.SignMessage(this._walletType, publicAddress, message);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignTypedData()
        {
            try
            {
                // sign typed data doesn't support solana
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                var txtAsset = Resources.Load<TextAsset>("TypedDataV4");
                string typedData = txtAsset.text;

                var chainId = ParticleNetwork.GetChainInfo().Id;
                JObject json = JObject.Parse(typedData);
                json["domain"]["chainId"] = chainId;
                string newTypedData = json.ToString();

                var nativeResultData =
                    await ParticleConnect.Instance.SignTypedData(this._walletType, publicAddress, newTypedData);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void Login()
        {
            try
            {
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                var domain = "login.xyz";
                var uri = "https://login.xyz/demo#login";
                var nativeResultData =
                    await ParticleConnect.Instance.Login(this._walletType, publicAddress, domain, uri);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");

                    var message = (string)JObject.Parse(nativeResultData.data)["message"];
                    var signature = (string)JObject.Parse(nativeResultData.data)["signature"];

                    this.loginSourceMessage = message;
                    this.loginSignature = signature;

                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void Verify()
        {
            try
            {
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");

                var message = this.loginSourceMessage;
                var signature = this.loginSignature;
                var nativeResultData =
                    await ParticleConnect.Instance.Verify(this._walletType, publicAddress, message, signature);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void ImportPrivateKey()
        {
            try
            {
                string privateKey;

                if (this._account.PublicAddress == TestAccount.Solana.PublicAddress)
                {
                    privateKey = TestAccount.Solana.PrivateKey;
                }
                else
                {
                    privateKey = TestAccount.EVM.PrivateKey;
                }

                var nativeResultData =
                    await ParticleConnect.Instance.ImportWalletFromPrivateKey(this._walletType, privateKey);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void ImportMnemonic()
        {
            try
            {
                string mnemonic;

                if (this._account.PublicAddress == TestAccount.Solana.PublicAddress)
                {
                    mnemonic = TestAccount.Solana.Mnemonic;
                }
                else
                {
                    mnemonic = TestAccount.EVM.Mnemonic;
                }

                var nativeResultData =
                    await ParticleConnect.Instance.ImportWalletFromMnemonic(this._walletType, mnemonic);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void ExportPrivateKey()
        {
            try
            {
                string publicAddress;

                if (this._account.PublicAddress == TestAccount.Solana.PublicAddress)
                {
                    publicAddress = TestAccount.Solana.PublicAddress;
                }
                else
                {
                    publicAddress = TestAccount.EVM.PublicAddress;
                }

                var nativeResultData =
                    await ParticleConnect.Instance.ExportWalletPrivateKey(this._walletType, publicAddress);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }


        public void GetWalletReadyState()
        {
            var walletReadyState = ParticleConnectInteraction.GetWalletReadyState(WalletType.Rainbow);
            Debug.Log($"walletReadyState = {walletReadyState}");
        }

        public void GetChainInfo()
        {
            var chainInfo = ParticleNetwork.GetChainInfo();
            Debug.Log(chainInfo);
            Debug.Log(chainInfo.Id);
            Debug.Log(chainInfo.Name);
            Debug.Log(chainInfo.Network);
        }

        public void ShowToast(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            Toast.CallStatic<AndroidJavaObject>("makeText", currentActivity, message, Toast.GetStatic<int>("LENGTH_LONG")).Call("show");
        }));
#elif UNITY_IOS && !UNITY_EDITOR
            ToastTip.Instance.OnShow(message);
#endif
        }
    }
}
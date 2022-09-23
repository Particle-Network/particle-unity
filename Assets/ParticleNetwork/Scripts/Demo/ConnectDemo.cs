using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Core.Utils;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class ConnectDemo : MonoBehaviour
    {
        private ChainInfo _chainInfo = new EthereumChain(EthereumChainId.Goerli);

        private string loginSourceMessage = "";
        private string loginSignature = "";

        // before test in devices, preset wallet and test account
        private WalletType _walletType;
        private TestAccount _account;

        private void Start()
        {
            this._walletType = WalletType.MetaMask;
            this._account = TestAccount.EVM;
        }

        public void SelectChain()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"xxhong {chainInfo.getChainName()} {chainInfo.getChainId()} {chainInfo.getChainIdName()}");
                this._chainInfo = ChainUtils.CreateChain(chainInfo.getChainName(), chainInfo.getChainId());
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
            var metadata = DAppMetadata.Create("Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network");
            ParticleNetwork.Init(_chainInfo);
            ParticleConnectInteraction.Init(_chainInfo, metadata);
        }

        /// <summary>
        /// Before test connect to wallet connect, like metamask wallet, you should login metamask with our evm test account.
        /// </summary>
        public async void Connect()
        {
            var nativeResultData = await ParticleConnect.Instance.Connect(this._walletType);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }


        public async void Disconnect()
        {
            // Test public address
            string publicAddress = TestAccount.EVM.PublicAddress;
            var nativeResultData = await ParticleConnect.Instance.Disconnect(this._walletType, publicAddress);

            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public void IsConnected()
        {
            // Test public address
            string publicAddress = TestAccount.EVM.PublicAddress;
            var isConnect = ParticleConnectInteraction.IsConnected(this._walletType, publicAddress);
            Debug.Log(
                $"Particle Connect is Connect = {isConnect}, publicAddress = {publicAddress}, walletType = {this._walletType.ToString()}");
        }

        public async void SignAndSendTransaction()
        {
            // Test public address
            string publicAddress;
            string transaction;
            if (this._account.PublicAddress == TestAccount.Solana.PublicAddress)
            {
                publicAddress = TestAccount.Solana.PublicAddress;
                transaction = await GetSolanaTransacion();
            }
            else
            {
                publicAddress = TestAccount.EVM.PublicAddress;
                transaction = await GetEVMTransacion();
            }

            Debug.Log("transaction = " + transaction);
            var nativeResultData =
                await ParticleConnect.Instance.SignAndSendTransaction(this._walletType, publicAddress, transaction);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void SignTransaction()
        {
            // sign transaction doesn't support evm.
            // Test public address
            string publicAddress = TestAccount.Solana.PublicAddress;
            var transaction = await GetSolanaTransacion();
            Debug.Log("transaction = " + transaction);
            var nativeResultData =
                await ParticleConnect.Instance.SignTransaction(this._walletType, publicAddress, transaction);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void SignAllTransactions()
        {
            // sign all transactions doesn't support evm.
            var publicAddress = TestAccount.Solana.PublicAddress;
            var transaction1 = await GetSolanaTransacion();
            var transaction2 = await GetSolanaTransacion();
            Debug.Log("transaction1 = " + transaction1);
            Debug.Log("transaction2 = " + transaction2);
            string[] transactions = new[] { transaction1, transaction2 };
            var nativeResultData =
                await ParticleConnect.Instance.SignAllTransactions(this._walletType, publicAddress, transactions);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void SignMessage()
        {
            // Test public address
            string publicAddress;

            if (this._account.PublicAddress == TestAccount.Solana.PublicAddress)
            {
                publicAddress = TestAccount.Solana.PublicAddress;
            }
            else
            {
                publicAddress = TestAccount.EVM.PublicAddress;
            }

            var message = "Hello world";
            var nativeResultData =
                await ParticleConnect.Instance.SignMessage(this._walletType, publicAddress, message);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void SignTypedData()
        {
            // sign typed data doesn't support solana
            // Test public address
            string publicAddress = TestAccount.EVM.PublicAddress;

            var txtAsset = Resources.Load<TextAsset>("TypedDataV4");
            string typedData = txtAsset.text;
            Debug.Log(typedData);

            var nativeResultData =
                await ParticleConnect.Instance.SignTypedData(this._walletType, publicAddress, typedData);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void Login()
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
            
            var domain = "login.xyz";
            var uri = "https://login.xyz/demo#login";
            var nativeResultData =
                await ParticleConnect.Instance.Login(this._walletType, publicAddress, domain, uri);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");

                var message = (string)JObject.Parse(nativeResultData.data)["message"];
                var signature = (string)JObject.Parse(nativeResultData.data)["signature"];

                this.loginSourceMessage = message;
                this.loginSignature = signature;

                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void Verify()
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
            
            var message = this.loginSourceMessage;
            var signature = this.loginSignature;
            var nativeResultData =
                await ParticleConnect.Instance.Verify(this._walletType, publicAddress, message, signature);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void ImportPrivateKey()
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
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void ImportMnemonic()
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
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void ExportPrivateKey()
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
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
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
#endif
        }

        async Task<string> GetSolanaTransacion()
        {
            string sender = TestAccount.Solana.PublicAddress;
            string receiver = TestAccount.Solana.ReceiverAddress;
            BigInteger amount = TestAccount.Solana.Amount;
            SerializeSOLTransReq req = new SerializeSOLTransReq(sender, receiver, amount);
            var result = await SolanaService.SerializeSOLTransaction(req);

            var resultData = JObject.Parse(result);
            var transaction = (string)resultData["result"]["transaction"]["serialized"];
            return transaction;
        }

        async Task<string> GetEVMTransacion()
        {
            // mock send some chain link token from send to receiver.
            string sender = TestAccount.EVM.PublicAddress;
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            var gasLimitResult = await EvmService.EstimateGas(sender, contractAddress, "0x0", data);
            var gasLimit = (string)JObject.Parse(gasLimitResult)["result"];
            var gasFeesResult = await EvmService.SuggestedGasFees();
            var maxFeePerGas = (double)JObject.Parse(gasFeesResult)["result"]["high"]["maxFeePerGas"];
            var maxFeePerGasHex = "0x" + ((BigInteger)(maxFeePerGas * Mathf.Pow(10, 9))).ToString("x");

            var maxPriorityFeePerGas = (double)JObject.Parse(gasFeesResult)["result"]["high"]["maxPriorityFeePerGas"];
            var maxPriorityFeePerGasHex = "0x" + ((BigInteger)(maxPriorityFeePerGas * Mathf.Pow(10, 9))).ToString("x");
            var chainId = TestAccount.EVM.ChainId;
            ;

            var transaction = new EthereumTransaction(sender, contractAddress, data, gasLimit, gasPrice: null,
                value: "0x0",
                nonce: null, type: "0x2",
                chainId: "0x" + chainId.ToString("x"), maxPriorityFeePerGasHex, maxFeePerGasHex);
            var json = JsonConvert.SerializeObject(transaction);
            var serialized = BitConverter.ToString(Encoding.Default.GetBytes(json));
            serialized = serialized.Replace("-", "");
            return "0x" + serialized;
        }
    }
}
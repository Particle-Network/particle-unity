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
            var metadata = new DAppMetaData("Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network");
            ParticleNetwork.Init(_chainInfo);
            ParticleConnectInteraction.Init(_chainInfo, metadata);
        }
        string publicAddress = "";
        /// <summary>
        /// Before test connect to wallet connect, like metamask wallet, you should login metamask with our evm test account.
        /// </summary>
        public async void Connect()
        {
            var nativeResultData = await ParticleConnect.Instance.Connect(this._walletType);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                publicAddress = JObject.Parse(nativeResultData.data)["publicAddress"].ToString();
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} publicAddress:{publicAddress}  Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void ConnectWithParticleParams()
        {
            ConnectConfig config = null;
            if (_walletType == WalletType.Particle)
            {
                config = new ConnectConfig(LoginType.GOOGLE, null, SupportAuthType.NONE, false);
            }

            var nativeResultData = await ParticleConnect.Instance.Connect(this._walletType, config);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                publicAddress = JObject.Parse(nativeResultData.data)["publicAddress"].ToString();
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} publicAddress:{publicAddress} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
            }
            else
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void Disconnect()
        {
            // Test public address
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
            
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

        public void IsConnected()
        {
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
            var isConnect = ParticleConnectInteraction.IsConnected(this._walletType, publicAddress);
            Tips.Instance.Show(
                $"Particle Connect is Connect = {isConnect}, publicAddress = {publicAddress}, walletType = {this._walletType.ToString()}");
            Debug.Log(
                $"Particle Connect is Connect = {isConnect}, publicAddress = {publicAddress}, walletType = {this._walletType.ToString()}");
        }

        public async void SignAndSendTransaction()
        {
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
            string transaction;
            if (!_account.PublicAddress.StartsWith("0x"))
            {
                transaction = await GetSolanaTransacion();
            }
            else
            {
                transaction = await GetEVMTransacion();
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

        public async void SignTransaction()
        {
            // sign transaction doesn't support evm.
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
            var transaction = await GetSolanaTransacion();
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

        public async void SignAllTransactions()
        {
            // sign all transactions doesn't support evm.
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
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

        public async void SignMessage()
        {
            // string publicAddress;
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
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

        public async void SignTypedData()
        {
            // sign typed data doesn't support solana
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
            var txtAsset = Resources.Load<TextAsset>("TypedDataV4");
            string typedData = txtAsset.text;
            Debug.Log(typedData);

            var nativeResultData =
                await ParticleConnect.Instance.SignTypedData(this._walletType, publicAddress, typedData);
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

        public async void Login()
        {
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
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

        public async void Verify()
        {
            if (String.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");

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

        public void SetChainInfoSync()
        {
            var result = ParticleConnectInteraction.SetChainInfo(_chainInfo);
            Debug.Log("SetChainInfoSync:" + result);
            Tips.Instance.Show("SetChainInfoSync:" + result);
        }

        public async void SetChainInfoAsync()
        {
            var nativeResultData =
                await ParticleConnect.Instance.SetChainInfoAsync(_chainInfo);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log("SetChainInfoAsync:" + nativeResultData.data);
                Tips.Instance.Show(nativeResultData.data);
            }
            else
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void SwitchEthereumChain()
        {
            var nativeResultData =
                await ParticleConnect.Instance.SwitchEthereumChain(_walletType, publicAddress, (int)EthereumChainId.Goerli);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log("SwitchEthereumChain:" + nativeResultData.data);
                Tips.Instance.Show(nativeResultData.data);
            }
            else
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        
        public async void AddEthereumChain()
        {
            // add chain example
            // define chain id, chain name, native currency, block explorer url and rpc url.
            var nativeResultData =
                await ParticleConnect.Instance.AddEthereumChain(_walletType, publicAddress, (int)PolygonChainId.Mumbai, 
                    "Polygon Mumbai", 
                    new NativeCurrency("Polygon Mumbai", "Matic", 18), 
                    "https://matic-mumbai.chainstacklabs.com", "https://mumbai.polygonscan.com");
            // define chain id, other parameters will auto configure in SDK.
            // var nativeResultData =
                // await ParticleConnect.Instance.AddEthereumChain(_walletType, publicAddress, (int)PolygonChainId.Mumbai);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log("AddEthereumChain:" + nativeResultData.data);
                Tips.Instance.Show(nativeResultData.data);
            }
            else
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
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
            Debug.Log(chainInfo.getChainId());
            Debug.Log(chainInfo.getChainName());
            Debug.Log(chainInfo.getChainIdName());
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
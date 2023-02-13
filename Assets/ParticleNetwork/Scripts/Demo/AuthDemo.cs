using System;
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
    public class AuthDemo : MonoBehaviour
    {
        private ChainInfo _chainInfo = new EthereumChain(EthereumChainId.Goerli);

        public void SelectChain()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"xxhong {chainInfo.getChainName()} {chainInfo.getChainId()} {chainInfo.getChainIdName()}");
                this._chainInfo = ChainUtils.CreateChain(chainInfo.getChainName(), chainInfo.getChainId());
            });
        }

        public void Init()
        {
            ParticleNetwork.Init(this._chainInfo);
        }

        public async void Login()
        {
            // login email
            var nativeResultData = await ParticleAuthService.Instance.Login(LoginType.PHONE, null, SupportAuthType.ALL, false, SocialLoginPrompt.Select_account);
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

        public async void Logout()
        {
            var nativeResultData = await ParticleAuthService.Instance.Logout();
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
        
        public async void FastLogout()
        {
            var nativeResultData = await ParticleAuthService.Instance.FastLogout();
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

        public void IsLogin()
        {
            Debug.Log(ParticleAuthServiceInteraction.IsLogin());
        }

        private string GetAddress()
        {
            return ParticleAuthServiceInteraction.GetAddress();
        }


        public async void SignAndSendTransaction()
        {
            var transaction = await GetEVMTransacion();
            Debug.Log("transaction = " + transaction);
            var nativeResultData =
                await ParticleAuthService.Instance.SignAndSendTransaction(transaction);
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
            var transaction = await GetSolanaTransacion();
            Debug.Log("transaction = " + transaction);
            var nativeResultData =
                await ParticleAuthService.Instance.SignTransaction(transaction);
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
            var transaction1 = await GetSolanaTransacion();
            var transaction2 = await GetSolanaTransacion();
            Debug.Log("transaction1 = " + transaction1);
            Debug.Log("transaction2 = " + transaction2);
            string[] transactions = new[] { transaction1, transaction2 };
            var nativeResultData =
                await ParticleAuthService.Instance.SignAllTransactions(transactions);
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
            var message = "Hello world";
            var nativeResultData =
                await ParticleAuthService.Instance.SignMessage(message);
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
            var publicAddress = GetAddress();
            var txtAsset = Resources.Load<TextAsset>("TypedDataV1");
            string typedData = txtAsset.text;

            var nativeResultData =
                await ParticleAuthService.Instance.SignTypedData(typedData,
                    SignTypedDataVersion.V1);
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

        public void SetLanguage()
        {
            Language language = Language.JA;
            ParticleNetwork.SetLanguage(language);
        }

        public void SetInterfaceStyle()
        {
            UserInterfaceStyle style = UserInterfaceStyle.DARK;
            ParticleNetwork.SetInterfaceStyle(style);
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
            string sender = GetAddress();
            string receiver = "BBBsMq9cEgRf9jeuXqd6QFueyRDhNwykYz63s1vwSCBZ";
            long amount = 10000000;
            SerializeSOLTransReq req = new SerializeSOLTransReq(sender, receiver, amount);
            var result = await SolanaService.SerializeSOLTransaction(req);

            var resultData = JObject.Parse(result);
            var transaction = (string)resultData["result"]["transaction"]["serialized"];
            return transaction;
        }

        async Task<string> GetEVMTransacion()
        {
            // mock send some chain link token from send to receiver.
            string sender = GetAddress();
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

        public async void SetChainInfoASync()
        {
            // call this method to change chain info and auto create public address if that is not existed.
            var nativeResultData =
                await ParticleAuthService.Instance.SetChainInfoAsync(_chainInfo);
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

        public void GetPnAddress()
        {
            var address = GetAddress();
            Tips.Instance.Show("GetPnAddress:" + address);
        }

        public void SetChainInfoSync()
        {
            // call this method to change chain info. 
            ParticleNetwork.SetChainInfo(_chainInfo);
        }

        public void SetiOSModalStyle()
        {
            ParticleAuthServiceInteraction.SetiOSModalPresentStyle(iOSModalPresentStyle.FullScreen);
        }

        public void SetiOSMediumScreen()
        {
            ParticleAuthServiceInteraction.SetiOSMediumScreen(true);
        }

        /// <summary>
        /// Set browser height percent, only support android.
        /// </summary>
        public void SetBrowserHeightPercent()
        {
            ParticleAuthServiceInteraction.SetBrowserHeightPercent(0.7f);
        }

        public async void OpenAccountAndSecurity()
        {
            var nativeResultData =
                await ParticleAuthService.Instance.OpenAccountAndSecurity();
            
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

        public void OpenWebWallet()
        {
            ParticleAuthServiceInteraction.OpenWebWallet();
        }
    }
}
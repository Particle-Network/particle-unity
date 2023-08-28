using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
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
                this._chainInfo = chainInfo;
            });
        }

        public void Init()
        {
            ParticleNetwork.Init(this._chainInfo);
        }

        public async void Login()
        {
            // login email
            var nativeResultData = await ParticleAuthService.Instance.Login(LoginType.PHONE, null, SupportAuthType.ALL,
                SocialLoginPrompt.SelectAccount);
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

        public async void IsLoginAsync()
        {
            var nativeResultData = await ParticleAuthService.Instance.IsLoginAsync();
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

        private string GetAddress()
        {
            return ParticleAuthServiceInteraction.GetAddress();
        }
        
        public void GetAddressBtn()
        {
            var address=  ParticleAuthServiceInteraction.GetAddress();
            Debug.Log("address = " + address);
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
            var txtAsset = Resources.Load<TextAsset>("TypedDataV4");
            string typedData = txtAsset.text;

            var chainId = ParticleNetwork.GetChainInfo().getChainId();
            JObject json = JObject.Parse(typedData);
            json["domain"]["chainId"] = chainId;
            string newTypedData = json.ToString();
            
            var nativeResultData =
                await ParticleAuthService.Instance.SignTypedData(newTypedData,
                    SignTypedDataVersion.V4);
            
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

        public void SetAppearance()
        {
            ParticleNetwork.SetAppearance(Appearance.DARK);
        }

        public void SetFiatCoin()
        {
            ParticleNetwork.SetFiatCoin(FiatCoin.KRW);
        }

        public void SetWebAuthConfig()
        {
            ParticleNetwork.SetWebAuthConfig(true, Appearance.DARK);
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
            string from = GetAddress();
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
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

        public void SetChainInfoSync()
        {
            // call this method to change chain info. 
            ParticleNetwork.SetChainInfo(_chainInfo);
        }

        public void GetChainInfo()
        {
            var chainInfo = ParticleNetwork.GetChainInfo();
            Debug.Log(
                $"chain name {chainInfo.getChainName()}, chain id {chainInfo.getChainId()}, chain id name {chainInfo.getChainIdName()}");
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
            var jsonString = "";
            ParticleAuthServiceInteraction.OpenWebWallet(jsonString);
        }

        public void HasMasterPassword()
        {
            var result = ParticleAuthServiceInteraction.HasMasterPassword();
            Debug.Log($"HasMasterPassword {result}");
        }

        public void HasPaymentPassword()
        {
            var result = ParticleAuthServiceInteraction.HasPaymentPassword();
            Debug.Log($"HasPaymentPassword {result}");
        }

        public void HasSecurityAccount()
        {
            var result = ParticleAuthServiceInteraction.HasSecurityAccount();
            Debug.Log($"HasSecurityAccount {result}");
        }

        public async void GetSecurityAccount()
        {
            var nativeResultData = await ParticleAuthService.Instance.GetSecurityAccount();
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
                var securityAccount = JsonConvert.DeserializeObject<SecurityAccount>(nativeResultData.data);
                var hasSecurityAccount = !string.IsNullOrEmpty(securityAccount.Email) ||
                                         !string.IsNullOrEmpty(securityAccount.Phone);
                Debug.Log(securityAccount);
                Debug.Log(
                    $"HasMasterPassword {securityAccount.HasMasterPassword}, HasPaymentPassword {securityAccount.HasPaymentPassword}, HasSecurityAccount {hasSecurityAccount}");
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
    }
}
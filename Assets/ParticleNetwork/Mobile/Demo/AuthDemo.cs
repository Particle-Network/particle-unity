using System;
using System.Reflection;
using CommonTip.Script;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class AuthDemo : MonoBehaviour
    {
        private ChainInfo _chainInfo = ChainInfo.EthereumGoerli;

        public void SelectChain()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"select chain {chainInfo.Name} {chainInfo.Id} {chainInfo.Network}");
                this._chainInfo = chainInfo;
            });
        }

        public void Init()
        {
            ParticleNetwork.Init(this._chainInfo);
        }

        public async void Login()
        {
            try
            {
                // login email
                var nativeResultData = await ParticleAuthService.Instance.Login(LoginType.PHONE, null,
                    SupportAuthType.ALL,
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void Logout()
        {
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void FastLogout()
        {
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public void IsLogin()
        {
            var isLogin = ParticleAuthServiceInteraction.IsLogin();
            Debug.Log($"isLogin {isLogin}");
            ShowToast($"isLogin {isLogin}");
        }

        public async void IsLoginAsync()
        {
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public void GetAddress()
        {
            var address = ParticleAuthServiceInteraction.GetAddress();
            Debug.Log($"address {address}");
        }
        
        public void GetAddressBtn()
        {
            var address=  ParticleAuthServiceInteraction.GetAddress();
            Debug.Log("address = " + address);
        }


        public async void SignAndSendTransaction()
        {
            try
            {
                var address = ParticleAuthServiceInteraction.GetAddress();
                var transaction = await TransactionHelper.GetEVMTransacion(address);
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignTransaction()
        {
            try
            {
                var address = ParticleAuthServiceInteraction.GetAddress();
                var transaction = await TransactionHelper.GetSolanaTransacion(address);
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignAllTransactions()
        {
            try
            {
                var address = ParticleAuthServiceInteraction.GetAddress();
                var transaction1 = await TransactionHelper.GetSolanaTransacion(address);
                var transaction2 = await TransactionHelper.GetSolanaTransacion(address);
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignMessage()
        {
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void SignTypedData()
        {
            try
            {
                var txtAsset = Resources.Load<TextAsset>("TypedDataV4");
                string typedData = txtAsset.text;

                var chainId = ParticleNetwork.GetChainInfo().Id;
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
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

        public void SetAAAccountName()
        {
            ParticleNetwork.SetAAAccountName(AAAccountName.BICONOMY);
        }

        public void SetAAVersionNumber()
        {
            ParticleNetwork.SetAAVersionNumber(AAVersionNumber.V1_0_0());
        }

        public void GetAAAccountName()
        {
           var accountName = ParticleNetwork.GetAAAccountName();
           Debug.Log(accountName.ToString());
           ShowToast(accountName.ToString());
        }

        public void GetAAVersionNumber()
        {
            var versionNumber = ParticleNetwork.GetAAVersionNumber();
            Debug.Log(versionNumber.version);
            ShowToast(versionNumber.version);
        }


        public void ShowToast(string message)
        {

#if UNITY_EDITOR
            
#elif UNITY_ANDROID && !UNITY_EDITOR
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


        public async void SetChainInfoAsync()
        {
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
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
                $"chain name {chainInfo.Name}, chain id {chainInfo.Id}, chain id name {chainInfo.Network}");
            ShowToast($"chain name {chainInfo.Name}, chain id {chainInfo.Id}, chain id name {chainInfo.Network}");
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
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
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
            try
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }
    }
}
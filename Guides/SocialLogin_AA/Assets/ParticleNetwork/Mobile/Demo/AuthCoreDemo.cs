using System;
using System.Collections.Generic;
using System.Reflection;
using CommonTip.Script;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class AuthCoreDemo : MonoBehaviour
    {
        private ChainInfo _chainInfo = ChainInfo.EthereumSepolia;

        private LoginType _loginType;

        [SerializeField] private GameObject emailLoginObject;
        [SerializeField] private GameObject phoneLoginObject;

        public void SelectChain()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"select chain {chainInfo.Name} {chainInfo.Id} {chainInfo.Network}");
                this._chainInfo = chainInfo;
            });
        }

        public void SelectLoginType()
        {
            LoginTypeChoice.Instance.Show(loginType =>
            {
                Debug.Log($"select {loginType.ToString()}");
                this._loginType = loginType;
            });
        }

        public void Init()
        {
            ParticleNetwork.Init(this._chainInfo);
            ParticleAuthCoreInteraction.Init();
            // control how to show set master password and payment password.
            ParticleNetwork.SetSecurityAccountConfig(new SecurityAccountConfig(0, 0));
        }

        public async void Connect()
        {
            try
            {
                List<SupportLoginType> allSupportLoginTypes = new List<SupportLoginType>(Enum.GetValues(typeof(SupportLoginType)) as SupportLoginType[]);
                var nativeResultData =
                    await ParticleAuthCore.Instance.Connect(_loginType, null, allSupportLoginTypes,
                        SocialLoginPrompt.SelectAccount,
                        new LoginPageConfig("Particle Unity Example", "Welcome to login", 
                            "https://connect.particle.network/icons/512.png"));

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

        public async void ConnectJWT()
        {
            try
            {
                var jwt = "";
                var nativeResultData = await ParticleAuthCore.Instance.ConnectJWT(jwt);

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

        public void ConnectEmail()
        {
            emailLoginObject.SetActive(true);
        }

        public void ConnectPhone()
        {
            phoneLoginObject.SetActive(true);
        }


        public async void Disconnect()
        {
            try
            {
                var nativeResultData = await ParticleAuthCore.Instance.Disconnect();
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

        public async void IsConnected()
        {
            try
            {
                var nativeResultData = await ParticleAuthCore.Instance.IsConnected();
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

        public void GetUserInfo()
        {
            var userInfo = ParticleAuthCoreInteraction.GetUserInfo();
            Debug.Log($"get user info {userInfo}");
        }

        public async void SwitchChain()
        {
            try
            {
                var nativeResultData = await ParticleAuthCore.Instance.SwitchChain(this._chainInfo);
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


        public async void ChangeMasterPassword()
        {
            try
            {
                var nativeResultData =
                    await ParticleAuthCore.Instance.ChangeMasterPassword();
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


        public void HasMasterPassword()
        {
            var result = ParticleAuthCoreInteraction.HasMasterPassword();
            Debug.Log($"has master password {result}");
        }

        public void HasPaymentPassword()
        {
            var result = ParticleAuthCoreInteraction.HasPaymentPassword();
            Debug.Log($"has master password {result}");
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


        public void SetChainInfo()
        {
            // call this method to change chain info. 
            ParticleNetwork.SetChainInfo(_chainInfo);
        }

        public void GetChainInfo()
        {
            var chainInfo = ParticleNetwork.GetChainInfo();
            Debug.Log(
                $"chain name {chainInfo.Name}, chain id {chainInfo.Id}, chain id name {chainInfo.Network}");
        }

        public async void OpenAccountAndSecurity()
        {
            try
            {
                var nativeResultData =
                    await ParticleAuthCore.Instance.OpenAccountAndSecurity();

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

        public void EvmGetAddress()
        {
            var address = ParticleAuthCoreInteraction.EvmGetAddress();
            Debug.Log($"evm address: {address}");
        }

        public async void EvmPersonalSign()
        {
            try
            {
                var message = "Hello world";
                var nativeResultData =
                    await ParticleAuthCore.Instance.EvmPersonalSign(message);

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

        public async void EvmPersonalSignUnique()
        {
            try
            {
                var message = "Hello world";
                var nativeResultData =
                    await ParticleAuthCore.Instance.EvmPersonalSignUnique(message);

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

        public async void EvmSignTypedData()
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
                    await ParticleAuthCore.Instance.EvmSignTypedData(newTypedData);

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

        public async void EvmSignTypedDataUnique()
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
                    await ParticleAuthCore.Instance.EvmSignTypedDataUnique(newTypedData);

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

        public async void EvmSendTransaction()
        {
            try
            {
                var address = ParticleAuthCoreInteraction.EvmGetAddress();
                var transaction = await TransactionHelper.GetEVMTransacion(address);

                var nativeResultData =
                    await ParticleAuthCore.Instance.EvmSendTransaction(transaction);

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

        public void SolanaGetAddress()
        {
            var address = ParticleAuthCoreInteraction.SolanaGetAddress();
            Debug.LogError($"solana address: {address}");
        }

        public async void SolanaSignMessage()
        {
            try
            {
                var message = "Hello Particle!";

                var nativeResultData =
                    await ParticleAuthCore.Instance.SolanaSignMessage(message);

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

        public async void SolanaSignTransaction()
        {
            try
            {
                var address = ParticleAuthCoreInteraction.SolanaGetAddress();
                var transaction = await TransactionHelper.GetSolanaTransacion(address);

                var nativeResultData =
                    await ParticleAuthCore.Instance.SolanaSignTransaction(transaction);

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

        public async void SolanaSignAllTransactions()
        {
            try
            {
                var address = ParticleAuthCoreInteraction.SolanaGetAddress();
                var transaction1 = await TransactionHelper.GetSolanaTransacion(address);
                var transaction2 = await TransactionHelper.GetSolanaTransacion(address);

                var nativeResultData =
                    await ParticleAuthCore.Instance.SolanaSignAllTransactions(new[] { transaction1, transaction2 });

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

        public async void SolanaSignAndSendTransaction()
        {
            try
            {
                var address = ParticleAuthCoreInteraction.SolanaGetAddress();
                var transaction = await TransactionHelper.GetSolanaTransacion(address);

                var nativeResultData =
                    await ParticleAuthCore.Instance.SolanaSignAndSendTransaction(transaction);

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

        public void SetBlindEnable()
        {
            ParticleAuthCoreInteraction.SetBlindEnable(true);
        }

        public void SetCustomUI()
        {
            // Only works for iOS
            // your custom ui json
            var txtAsset = Resources.Load<TextAsset>("customUIConfig");
            string json = txtAsset.text;
            ParticleAuthCoreInteraction.SetCustomUI(json);
        }
    }
}
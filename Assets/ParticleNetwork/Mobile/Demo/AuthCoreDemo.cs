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
    public class AuthCoreDemo : MonoBehaviour
    {
        private ChainInfo _chainInfo = ChainInfo.EthereumGoerli;

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
                var nativeResultData =
                    await ParticleAuthCore.Instance.Connect(_loginType, null, null,
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

        public async void ConnectJWT()
        {
            try
            {
                var jwt =
                    "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IndVUE05RHNycml0Sy1jVHE2OWNKcCJ9.eyJlbWFpbCI6InBhbnRhb3ZheUBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsImlzcyI6Imh0dHBzOi8vZGV2LXFyNi01OWVlLnVzLmF1dGgwLmNvbS8iLCJhdWQiOiJFVmpLMVpaUFN0UWNkV3VoandQZGRBdGdSaXdwNTRWUSIsImlhdCI6MTcwMzY2MTA3MywiZXhwIjoxNzAzNjk3MDczLCJzdWIiOiJhdXRoMHw2MzAzMjE0YjZmNjE1NjM2YWM5MTdmMWIiLCJzaWQiOiJZQ3Z5c2R3WWFRMXRlTlF0Ty1TYVp4ZVVURHJ4V0FCRSJ9.gpgDqfNGnXA2RohGB65EuFm1pDMh_QYeplvSlT7ol6RwKrut9MQWQxm_dPOqvnt4r0xtzXJWWqmN07TjPxtJaVtNnmtx0l99_c8MQvQYW04tw7iduBX3NOahPwHIRfSa6mhlbau5nnBJwYoMz99uI2VfoQjTaZ8xEDfijP8BLsCVyQDL69ZRULgBXs-hFIo1pVS7JPLD7wyhmPscClwZ6qNii1uUE03IhL3GPBpun3AhPZBTcC5kqF8aOO_52TAJYpUSMJDu2mawbZm29g6OmCR_ZrVFqzjQHXy5xEFLAb0XXa-cAkYUTA9AuHbyfKvpaylgO-z4IcjRmVaucXGC3Q";
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

        public async void PresentLoginPage()
        {
            try
            {
                var nativeResultData =
                    await ParticleAuthCore.Instance.PresentLoginPage(LoginType.EMAIL, null, SupportAuthType.ALL,
                        SocialLoginPrompt.SelectAccount,
                        new LoginPageConfig("Particle Unity Example", "Welcome to login", ImageType.Base64,
                            "data:image/png;base64,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"));

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
            // for more detail, please explore
            https: //docs.particle.network/developers/wallet-service/sdks/web#custom-particle-wallet-style
            var txtAsset = Resources.Load<TextAsset>("customUIConfig");
            string json = txtAsset.text;
            ParticleAuthCoreInteraction.SetCustomUI(json);
        }
    }
}
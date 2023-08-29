using System;
using System.Reflection;
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
            ParticleAuthCoreInteraction.Init();
        }

        public async void Connect()
        {
            try
            {
                var jwt =
                    "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IndVUE05RHNycml0Sy1jVHE2OWNKcCJ9.eyJlbWFpbCI6InBhbnRhb3ZheUBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsImlzcyI6Imh0dHBzOi8vZGV2LXFyNi01OWVlLnVzLmF1dGgwLmNvbS8iLCJhdWQiOiJFVmpLMVpaUFN0UWNkV3VoandQZGRBdGdSaXdwNTRWUSIsImlhdCI6MTY5MzI3NjE1OSwiZXhwIjoxNjkzMzEyMTU5LCJzdWIiOiJhdXRoMHw2MzAzMjE0YjZmNjE1NjM2YWM5MTdmMWIiLCJzaWQiOiJCcjlQUG1rSEdTT3NraF9aNnlWVlpYcldsRjVZOVRQQSJ9.CT7hekpLVAP_3lqTCWKfPeFFB75UhGO7nMws23OO3ZlTUheU3Q2OqCSG-YIFIz348ii98VmEMYPPjml_OXBLcFiRoZbwUCV5j5sWaQVE1SWmTl1aBJfJn9nbSdee0nle3r83wdZBEERDPaWbLHJFC6Md0Zs_V4fpHHjbh94oXnihjsSiuPoMDb7FPH38_1d5RfvuDl-b8Ibwz-yXhkrsQqrJVE029SX3007_5ssfBl11wjzvuf0ME1G7I5ds6u_t1ZycqJozm68ppCRMcKw6KW1-Sqclk8Wor-G5lInNGWf-mSucqBg8dZuK_VAxjao3Ly5Pif6t4ZmFABrsDifN1A";
                var nativeResultData = await ParticleAuthCore.Instance.Connect(jwt);

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

        public void OpenWebWallet()
        {
            var jsonString = "";
            ParticleAuthCoreInteraction.OpenWebWallet(jsonString);
        }

        public void EvmGetAddress()
        {
            var address = ParticleAuthCoreInteraction.EvmGetAddress();
            Debug.LogError($"evm address: {address}");
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
    }
}
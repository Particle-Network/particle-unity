using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CommonTip.Script;

namespace Network.Particle.Scripts.Test
{
    public class ConnectedWalletOprate : MonoBehaviour
    {
        public UnityAction<bool> unityAction; //refresh wallet list
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Button btnBack;
        [SerializeField] private GameObject authCoreOprate;

        private string loginSourceMessage = "";
        private string loginSignature = "";

        private WalletType _walletType;
        private Account _account;
        private string publicAddress;

        private void Start()
        {
            btnBack.onClick.AddListener(Hidden);
        }

        public void Show(WalletType walletType, Account account, UnityAction<bool> unityAction)
        {
            this.unityAction = unityAction;
            this._walletType = walletType;
            this._account = account;
            this.publicAddress = account.publicAddress;
            title.text = walletType.ToString();
            gameObject.SetActive(true);
            authCoreOprate.SetActive(walletType == WalletType.AuthCore);
        }

        private void Hidden()
        {
            gameObject.SetActive(false);
        }

        public void AuthCoreGetUserInfo()
        {
            var userInfo = ParticleAuthCoreInteraction.GetUserInfo();
            ShowToast($"get user info {userInfo}");
            Debug.Log($"get user info {userInfo}");
        }

        public async void AuthCoreChangeMasterPassword()
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


        public async void AuthCoreHasMasterPassword()
        {
            try
            {
                var nativeResultData =
                    await ParticleAuthCore.Instance.HasMasterPassword();
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

        public async void AuthCoreHasPaymentPassword()
        {
            try
            {
                var nativeResultData =
                    await ParticleAuthCore.Instance.HasPaymentPassword();
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

        public async void AuthCoreOpenAccountAndSecurity()
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

        public void AuthCoreSetBlindEnable()
        {
            ParticleAuthCoreInteraction.SetBlindEnable(true);
        }


        public async void Disconnect()
        {
            try
            {
                var nativeResultData = await ParticleConnect.Instance.Disconnect(this._walletType, publicAddress);

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                    unityAction.Invoke(true);
                    Hidden();
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
                if (!_account.publicAddress.StartsWith("0x"))
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
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                Debug.Log("SignTransaction only support solana");
                return;
            }

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
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                Debug.Log("SignAllTransactions only support solana");
                return;
            }

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
            if (!ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                Debug.Log("SignTypedData only support evm");
                return;
            }

            try
            {
                // sign typed data doesn't support solana
                if (string.IsNullOrEmpty(publicAddress)) throw new Exception("publicAddress is null, connect first");
                var txtAsset = Resources.Load<TextAsset>("Share/TypedDataV4");
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
                var domain = "particle.network";
                var uri = "https://demo.particle.network";
                var nativeResultData =
                    await ParticleConnect.Instance.SignInWithEthereum(this._walletType, publicAddress, domain, uri);
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
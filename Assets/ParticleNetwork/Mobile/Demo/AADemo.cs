using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Core.Utils;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class AADemo : MonoBehaviour
    {
        private static AndroidJavaObject activityObject;

        private ChainInfo _chainInfo = new EthereumChain(EthereumChainId.Goerli);
        
        private static AndroidJavaObject GetAndroidJavaObject()
        {
            if (activityObject != null)
            {
                return activityObject;
            }

            var jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            activityObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
            return activityObject;
        }

        public void Init()
        {
            var dappApiKeys = new Dictionary<int, string>
            {
                { 1, "your ethereum mainnet key" },
                { 5, "your ethereum goerli key" },
                { 137, "your polygon mainnet key" },
                { 80001, "hYZIwIsf2.e18c790b-cafb-4c4e-a438-0289fc25dba1" }
            };

            ParticleAAInteraction.Init(dappApiKeys);
        }

        public void EnableAAMode()
        {
            ParticleAAInteraction.EnableAAMode();
        }

        public void DisableAAMode()
        {
            ParticleAAInteraction.DisableAAMode();
        }

        public void IsAAModeEnable()
        {
            var result = ParticleAAInteraction.IsAAModeEnable();
            Debug.Log($"IsAAModeEnable {result}");
        }

        public async void IsDeploy()
        {
            var eoaAddress = ParticleAuthServiceInteraction.GetAddress();
            var nativeResultData = await ParticleAA.Instance.IsDepoly(eoaAddress);

            Debug.Log("result" + nativeResultData.data);
            if (nativeResultData.isSuccess)
            {
                var isDeploy = Convert.ToBoolean(nativeResultData.data);
                Debug.Log($"isDeploy {isDeploy}");
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
            }
            else
            {
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void RpcGetFeeQuotes()
        {
            var eoaAddress = ParticleAuthServiceInteraction.GetAddress();
            var transaction = await GetEVMTransacion();
            var nativeResultData = await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string>{transaction});

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

        public async void SendTransactionWithNativeByAuth()
        {
            var eoaAddress = ParticleAuthServiceInteraction.GetAddress();
            var transaction = await GetEVMTransacion();
            var nativeResultData =
                await ParticleAuthService.Instance.SignAndSendTransaction(transaction, AAFeeMode.Auto());

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

        public async void SendTransactionWithGaslessByAuth()
        {
            var eoaAddress = ParticleAuthServiceInteraction.GetAddress();
            var transaction = await GetEVMTransacion();
            var nativeResultData =
                await ParticleAuthService.Instance.SignAndSendTransaction(transaction, AAFeeMode.Gasless());

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

        public async void SendTransactionWithTokenByAuth()
        {
            var eoaAddress = ParticleAuthServiceInteraction.GetAddress();
            var transaction = await GetEVMTransacion();

            var feeQuotesResult =
                await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

            List<object> feeQuotes = JsonConvert.DeserializeObject<List<object>>(feeQuotesResult.data);

            var nativeResultData =
                await ParticleAuthService.Instance.SignAndSendTransaction(transaction,
                    AAFeeMode.Custom(feeQuotes[0]));

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

        public async void BatchSendTransactionsByAuth()
        {
            var transaction = await GetEVMTransacion();

            List<string> transactions = new List<string> { transaction, transaction };
            Debug.Log($"BatchSendTransactionsWithAuth {transactions}");

            var nativeResultData = await ParticleAuthService.Instance.BatchSendTransactions(transactions);
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

        public async void SendTransactionWithNativeByConnect()
        {
            var eoaAddress = "0x498c9b8379E2e16953a7b1FF94ea11893d09A3Ed";
            var transaction = await GetEVMTransactionWithConnect();
            var nativeResultData =
                await ParticleConnect.Instance.SignAndSendTransaction(WalletType.MetaMask, eoaAddress, transaction,
                    AAFeeMode.Auto());

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

        public async void SendTransactionWithGaslessByConnect()
        {
            var eoaAddress = "0x498c9b8379E2e16953a7b1FF94ea11893d09A3Ed";
            var transaction = await GetEVMTransactionWithConnect();
            var nativeResultData =
                await ParticleConnect.Instance.SignAndSendTransaction(WalletType.MetaMask, eoaAddress, transaction,
                    AAFeeMode.Gasless());

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

        public async void SendTransactionWithTokenByConnect()
        {
            var eoaAddress = "0x498c9b8379E2e16953a7b1FF94ea11893d09A3Ed";
            var transaction = await GetEVMTransactionWithConnect();

            var feeQuotesResult =
                await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

            List<object> feeQuotes = JsonConvert.DeserializeObject<List<object>>(feeQuotesResult.data);

            var nativeResultData =
                await ParticleConnect.Instance.SignAndSendTransaction(WalletType.MetaMask, eoaAddress, transaction,
                    AAFeeMode.Custom(feeQuotes[0]));

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

        public async void BatchSendTransactionsByConnect()
        {
            var eoaAddress = "0x498c9b8379E2e16953a7b1FF94ea11893d09A3Ed";
            var transaction = await GetEVMTransactionWithConnect();

            List<string> transactions = new List<string> { transaction, transaction };
            var nativeResultData = await ParticleConnect.Instance.BatchSendTransactions(WalletType.MetaMask, eoaAddress,
                transactions, AAFeeMode.Auto());
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


        async Task<string> GetEVMTransacion()
        {
            // mock send some chain link token from send to receiver.
            string from = ParticleAuthServiceInteraction.GetAddress();
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
        }

        async Task<string> GetEVMTransactionWithConnect()
        {
            string from = "0x498c9b8379E2e16953a7b1FF94ea11893d09A3Ed";
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
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
    }
}
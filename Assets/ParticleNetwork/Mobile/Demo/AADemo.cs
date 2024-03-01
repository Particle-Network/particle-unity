using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using CommonTip.Script;

namespace Network.Particle.Scripts.Test
{
    public class AADemo : MonoBehaviour
    {
        private static AndroidJavaObject activityObject;

        private AAAccountName accountName = AAAccountName.LIGHT();

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

        private string GetEoaAddress()
        {
            var eoaAddress = ParticleAuthCoreInteraction.EvmGetAddress();
            // return ParticleAuthServiceInteraction.GetAddress();

            ShowToast($"eoaAddress {eoaAddress}");
            Debug.Log($"eoaAddress {eoaAddress}");
            return eoaAddress;
        }

        private async Task<string> GetSmartAccountAddress(string eoaAddress)
        {
            var smartAccountResult = await EvmService.GetSmartAccount(new[]
            {
                new SmartAccountObject(accountName, eoaAddress)
            });

            var smartAccountAddress =
                (string)JObject.Parse(smartAccountResult)["result"][0]["smartAccountAddress"];

            ShowToast($"smartAccountAddress {smartAccountAddress}");
            return smartAccountAddress;
        }

        public void Init()
        {
            var biconomyApiKeys = new Dictionary<int, string>
            {
                { 1, "" }, // your ethereum mainnet key
                { 5, "" }, // your ethereum goerli key
                { 137, "" }, // your polygon mainnet key
                { 80001, "" } // your polygon mumbai key
            };

            ParticleAAInteraction.Init(accountName, biconomyApiKeys);
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
            try
            {
                var eoaAddress = GetEoaAddress();
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
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }


        public async void GetSmartAccountAddress()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                Debug.Log($"smartAccountAddress {smartAccountAddress}");
            }

            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void RpcGetFeeQuotes()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);
                var nativeResultData =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

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

        public async void SendTransactionWithNativeByAuth()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                // check if enough native for gas fee
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                var verifyingPaymasterNative = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterNative"];
                var feeQuote = verifyingPaymasterNative["feeQuote"];

                var fee = BigInteger.Parse((string)feeQuote["fee"]);
                var balance = BigInteger.Parse((string)feeQuote["balance"]);

                if (balance < fee)
                {
                    Debug.Log("native balance is not enough for gas fee");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send pay with native
                var nativeResultData =
                    await ParticleAuthService.Instance.SignAndSendTransaction(transaction,
                        AAFeeMode.Native(JObject.Parse(feeQuotesResult.data)));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithGaslessByAuth()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                // check if gasless available
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                var verifyingPaymasterGasless = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterGasless"];

                if (verifyingPaymasterGasless == null)
                {
                    Debug.Log("gasless is not available");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send gasless
                var nativeResultData =
                    await ParticleAuthService.Instance.SignAndSendTransaction(transaction,
                        AAFeeMode.Gasless(JObject.Parse(feeQuotesResult.data)));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithTokenByAuth()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                // select a feeQuote
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                JArray feeQuotes = (JArray)(JObject.Parse(feeQuotesResult.data)["tokenPaymaster"]["feeQuotes"]);

                var overFeeQuotes = feeQuotes
                    .Where(jt =>
                    {
                        var fee = BigInteger.Parse(jt["fee"].Value<string>());
                        var balance = BigInteger.Parse((string)jt["balance"].Value<string>());

                        return balance >= fee;
                    })
                    .ToList();


                if (overFeeQuotes.Count == 0)
                {
                    Debug.Log("no valid token for gas fee");
                    return;
                }

                // select the first feeQuote
                var feeQuote = overFeeQuotes[0];
                var tokenPaymasterAddress =
                    JObject.Parse(feeQuotesResult.data)["tokenPaymaster"]["tokenPaymasterAddress"].Value<string>();

                Debug.Log($"feeQuote {feeQuote}");
                Debug.Log($"tokenPaymasterAddress {tokenPaymasterAddress}");

                var nativeResultData =
                    await ParticleAuthService.Instance.SignAndSendTransaction(transaction,
                        AAFeeMode.Token(feeQuote, tokenPaymasterAddress));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void BatchSendTransactionsByAuth()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                var transactions = new List<string> { transaction, transaction };
                // check if enough native for gas fee
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, transactions);

                var verifyingPaymasterNative = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterNative"];
                var feeQuote = verifyingPaymasterNative["feeQuote"];

                var fee = BigInteger.Parse((string)feeQuote["fee"]);
                var balance = BigInteger.Parse((string)feeQuote["balance"]);

                if (balance < fee)
                {
                    Debug.Log("native balance is not enough for gas fee");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send pay with native
                var nativeResultData = await ParticleAuthService.Instance.BatchSendTransactions(transactions,
                    AAFeeMode.Native(JObject.Parse(feeQuotesResult.data)));
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithNativeByConnect()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransactionWithConnect(smartAccountAddress);

                // check if enough native for gas fee
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                var verifyingPaymasterNative = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterNative"];
                var feeQuote = verifyingPaymasterNative["feeQuote"];

                var fee = BigInteger.Parse((string)feeQuote["fee"]);
                var balance = BigInteger.Parse((string)feeQuote["balance"]);

                if (balance < fee)
                {
                    Debug.Log("native balance is not enough for gas fee");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send pay with native
                var nativeResultData =
                    await ParticleConnect.Instance.SignAndSendTransaction(WalletType.MetaMask, eoaAddress,
                        transaction,
                        AAFeeMode.Native(JObject.Parse(feeQuotesResult.data)));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithGaslessByConnect()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransactionWithConnect(smartAccountAddress);

                // check if gasless available
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                var verifyingPaymasterGasless = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterGasless"];

                if (verifyingPaymasterGasless == null)
                {
                    Debug.Log("gasless is not available");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send gasless

                var nativeResultData =
                    await ParticleConnect.Instance.SignAndSendTransaction(WalletType.MetaMask, eoaAddress,
                        transaction,
                        AAFeeMode.Gasless(JObject.Parse(feeQuotesResult.data)));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithTokenByConnect()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransactionWithConnect(smartAccountAddress);

                // select a feeQuote
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                JArray feeQuotes = (JArray)(JObject.Parse(feeQuotesResult.data)["tokenPaymaster"]["feeQuotes"]);

                var overFeeQuotes = feeQuotes
                    .Where(jt =>
                    {
                        var fee = BigInteger.Parse(jt["fee"].Value<string>());
                        var balance = BigInteger.Parse((string)jt["balance"].Value<string>());

                        return balance >= fee;
                    })
                    .ToList();


                if (overFeeQuotes.Count == 0)
                {
                    Debug.Log("no valid token fro gas fee");
                    return;
                }

                // select the first feeQuote
                var feeQuote = overFeeQuotes[0];
                var tokenPaymasterAddress =
                    JObject.Parse(feeQuotesResult.data)["tokenPaymaster"]["tokenPaymasterAddress"].Value<string>();

                Debug.Log($"feeQuote {feeQuote}");
                Debug.Log($"tokenPaymasterAddress {tokenPaymasterAddress}");

                var nativeResultData =
                    await ParticleConnect.Instance.SignAndSendTransaction(WalletType.MetaMask, eoaAddress,
                        transaction,
                        AAFeeMode.Token(feeQuote, tokenPaymasterAddress));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void BatchSendTransactionsByConnect()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                var transactions = new List<string> { transaction, transaction };
                // check if enough native for gas fee
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, transactions);

                var verifyingPaymasterNative = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterNative"];
                var feeQuote = verifyingPaymasterNative["feeQuote"];

                var fee = BigInteger.Parse((string)feeQuote["fee"]);
                var balance = BigInteger.Parse((string)feeQuote["balance"]);

                if (balance < fee)
                {
                    Debug.Log("native balance is not enough for gas fee");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send pay with native
                var nativeResultData = await ParticleConnect.Instance.BatchSendTransactions(WalletType.MetaMask,
                    eoaAddress,
                    transactions, AAFeeMode.Native(JObject.Parse(feeQuotesResult.data)));
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithNativeByAuthCore()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                // check if enough native for gas fee
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                var verifyingPaymasterNative = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterNative"];
                var feeQuote = verifyingPaymasterNative["feeQuote"];

                var fee = BigInteger.Parse((string)feeQuote["fee"]);
                var balance = BigInteger.Parse((string)feeQuote["balance"]);

                if (balance < fee)
                {
                    Debug.Log("native balance is not enough for gas fee");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send pay with native
                var nativeResultData =
                    await ParticleAuthCore.Instance.EvmSendTransaction(transaction,
                        AAFeeMode.Native(JObject.Parse(feeQuotesResult.data)));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithGaslessByAuthCore()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                // check if gasless available
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                var verifyingPaymasterGasless = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterGasless"];

                if (verifyingPaymasterGasless == null)
                {
                    Debug.Log("gasless is not available");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send gasless
                var nativeResultData =
                    await ParticleAuthCore.Instance.EvmSendTransaction(transaction,
                        AAFeeMode.Gasless(JObject.Parse(feeQuotesResult.data)));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void SendTransactionWithTokenByAuthCore()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                // select a feeQuote
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, new List<string> { transaction });

                JArray feeQuotes = (JArray)(JObject.Parse(feeQuotesResult.data)["tokenPaymaster"]["feeQuotes"]);

                var overFeeQuotes = feeQuotes
                    .Where(jt =>
                    {
                        var fee = BigInteger.Parse(jt["fee"].Value<string>());
                        var balance = BigInteger.Parse((string)jt["balance"].Value<string>());

                        return balance >= fee;
                    })
                    .ToList();


                if (overFeeQuotes.Count == 0)
                {
                    Debug.Log("no valid token for gas fee");
                    return;
                }

                // select the first feeQuote
                var feeQuote = overFeeQuotes[0];
                var tokenPaymasterAddress =
                    JObject.Parse(feeQuotesResult.data)["tokenPaymaster"]["tokenPaymasterAddress"].Value<string>();

                Debug.Log($"feeQuote {feeQuote}");
                Debug.Log($"tokenPaymasterAddress {tokenPaymasterAddress}");

                var nativeResultData =
                    await ParticleAuthCore.Instance.EvmSendTransaction(transaction,
                        AAFeeMode.Token(feeQuote, tokenPaymasterAddress));

                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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

        public async void BatchSendTransactionsByAuthCore()
        {
            try
            {
                var eoaAddress = GetEoaAddress();
                var smartAccountAddress = await GetSmartAccountAddress(eoaAddress);
                var transaction = await TransactionHelper.GetEVMTransacion(smartAccountAddress);

                var transactions = new List<string> { transaction, transaction };
                // check if enough native for gas fee
                var feeQuotesResult =
                    await ParticleAA.Instance.RpcGetFeeQuotes(eoaAddress, transactions);

                var verifyingPaymasterNative = JObject.Parse(feeQuotesResult.data)["verifyingPaymasterNative"];
                var feeQuote = verifyingPaymasterNative["feeQuote"];

                var fee = BigInteger.Parse((string)feeQuote["fee"]);
                var balance = BigInteger.Parse((string)feeQuote["balance"]);

                if (balance < fee)
                {
                    Debug.Log("native balance is not enough for gas fee");
                    return;
                }

                // pass result from rpcGetFeeQuotes to send pay with native
                var nativeResultData = await ParticleAuthCore.Instance.BatchSendTransactions(
                    transactions, AAFeeMode.Native(JObject.Parse(feeQuotesResult.data)));
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{nativeResultData.data}");
                    Debug.Log($"signature {nativeResultData.data}");
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
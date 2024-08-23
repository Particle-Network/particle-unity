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

namespace Network.Particle.Scripts.Test
{
    public class AADemo : MonoBehaviour
    {
        private static AndroidJavaObject activityObject;

        private AAAccountName accountName = AAAccountName.BICONOMY_V2();


        private string sessionSignerPublicAddress = TestAccount.EVM.PublicAddress;
        private List<object> sessions = new List<object>();

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

            ShowToast($"eoaAddress {eoaAddress}");
            Debug.Log($"eoaAddress {eoaAddress}");
            return eoaAddress;
        }

        private async Task<string> GetSmartAccountAddress(string eoaAddress)
        {
            var smartAccountResult = await EvmService.GetSmartAccount(new List<SmartAccountObject>
                { new SmartAccountObject(accountName, eoaAddress) });

            var smartAccountAddress =
                (string)JObject.Parse(smartAccountResult)["result"][0]["smartAccountAddress"];

            ShowToast($"smartAccountAddress {smartAccountAddress}");
            return smartAccountAddress;
        }

        public void Init()
        {
            ParticleAAInteraction.Init(accountName);
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

                if (verifyingPaymasterGasless == null || verifyingPaymasterGasless.Type == JTokenType.Null)
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

                if (verifyingPaymasterGasless == null || verifyingPaymasterGasless.Type == JTokenType.Null)
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

        public async void CreateSessions()
        {
            try
            {
                var erc1155MintToModuleAddr = "0x8788379F96B5dbF5B787Bd8c616079B958da2538";
                var receiver = TestAccount.EVM.ReceiverAddress;
                var eoaAddress = GetEoaAddress();
                var smartAccountObject = new SmartAccountObject(accountName, eoaAddress);
                // Now we need a sessionSigner, we use a private key for this role, and you should use your server for this role
                // create a sessionSigner
                
                var sessionSignerPrivateKey = TestAccount.EVM.PrivateKey;
                var signerAccountResultData =
                    await ParticleConnect.Instance.ImportWalletFromPrivateKey(WalletType.EvmPrivateKey,
                        sessionSignerPrivateKey);
                
                if (signerAccountResultData.isSuccess)
                {
                    this.sessionSignerPublicAddress =
                        JObject.Parse(signerAccountResultData.data)["publicAddress"].ToString();
                }
                else
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{signerAccountResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(signerAccountResultData.data);
                    Debug.Log(errorData);
                }
                
                if (this.sessionSignerPublicAddress == "")
                {
                    Debug.Log("Create session signer failed");
                    return;
                }
                // get a session signer


                var session = new JObject
                {
                    { "validUntil", 0 },
                    { "validAfter", 0 },
                    { "sessionValidationModule", erc1155MintToModuleAddr },
                    {
                        "sessionKeyDataInAbi", new JArray
                        {
                            new JArray("address", "address", "uint256"),
                            new JArray(this.sessionSignerPublicAddress, receiver, 1)
                        }
                    }
                };

                // you can store the sessions
                this.sessions = new List<object> { session };

                var createSessionsResultData = await EvmService.CreateSessions(smartAccountObject, this.sessions);

                var createSessionUserOp =
                    JObject.Parse(createSessionsResultData)["result"]["verifyingPaymasterGasless"]["userOp"];
                var createSessionUserOpHash =
                    JObject.Parse(createSessionsResultData)["result"]["verifyingPaymasterGasless"]["userOpHash"]
                        .ToString();

                var createSessionUserOpResultData = await ParticleConnect.Instance.SignMessage(WalletType.AuthCore,
                    eoaAddress, createSessionUserOpHash);

                Debug.Log($"createSessionUserOpResultData {createSessionUserOpResultData}");
                if (createSessionUserOpResultData.isSuccess)
                {
                    var signature = createSessionUserOpResultData.data;
                    createSessionUserOp["signature"] = signature;
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{signature}");
                    Debug.Log($"signature {signature}");
                }
                else
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{createSessionUserOpResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(createSessionUserOpResultData.data);
                    Debug.Log(errorData);
                }

                var sendCreateSessionUserOpResult =
                    await EvmService.SendUserOp(smartAccountObject, createSessionUserOp);
                var sendCreateSessionUserOpHash = JObject.Parse(sendCreateSessionUserOpResult)["result"].ToString();

                Debug.Log($"send create session user op hash {sendCreateSessionUserOpHash}");
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }

        public async void ValidateSessions()
        {
            var eoaAddress = GetEoaAddress();
            var smartAccountObject = new SmartAccountObject(accountName, eoaAddress);
            var validateSessionResult = await EvmService.ValidateSession(smartAccountObject, sessions, sessions[0]);
            var isPassingValidate = JObject.Parse(validateSessionResult)["result"].Value<bool>();
            Debug.Log($"isPassingValidate {isPassingValidate}");
            ShowToast($"{MethodBase.GetCurrentMethod()?.Name} isPassingValidate:{isPassingValidate}");
        }

        public async void SendSessionUserOp()
        {
            try
            {
                var erc1155TokenAddress = "0x210f16F8B69Cb8f2429942e573e19ba3C855b53b";
                var receiver = TestAccount.EVM.ReceiverAddress;
                var eoaAddress = GetEoaAddress();
                var smartAccountObject = new SmartAccountObject(accountName, eoaAddress);
                var abiJsonString =
                    "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"mintTo\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
                List<object> objects = new List<object>
                {
                    erc1155TokenAddress, 
                    "custom_mintTo", 
                    new List<object> { receiver, "0x1", "0x1" }, 
                    abiJsonString
                };
                var dataResult = await EvmService.AbiEncodeFunctionCall(objects);

                var data = JObject.Parse(dataResult)["result"].ToString();
                
                Debug.Log($"data {data}");
                var transaction = new SimplifyTransaction(erc1155TokenAddress, data, "0x0");
                var feeQuotes = await EvmService.GetFeeQuotes(smartAccountObject,
                    new List<SimplifyTransaction> { transaction });
                
                
                var verifyingPaymasterGasless = JObject.Parse(feeQuotes)["result"]["verifyingPaymasterGasless"];

                if (verifyingPaymasterGasless == null || verifyingPaymasterGasless.Type == JTokenType.Null)
                {
                    Debug.Log("gasless is not available");
                    return;
                }
                else
                {
                    Debug.Log($"verifyingPaymasterGasless {verifyingPaymasterGasless}");
                }

                var userOpHash = verifyingPaymasterGasless["userOpHash"].ToString();
                var userOp = verifyingPaymasterGasless["userOp"];

                var signUserOpHashResultData = await ParticleConnect.Instance.SignMessage(WalletType.EvmPrivateKey,
                    this.sessionSignerPublicAddress, userOpHash);

                if (signUserOpHashResultData.isSuccess)
                {
                    var signature = signUserOpHashResultData.data;
                    userOp["signature"] = signature;
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{signature}");
                    Debug.Log($"signature {signature}");
                }
                else
                {
                    ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Failed:{signUserOpHashResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(signUserOpHashResultData.data);
                    Debug.Log(errorData);
                }

                var sendUserOpResult =
                    await EvmService.SendUserOp(smartAccountObject, userOp, this.sessions, this.sessions[0]);
                var sendUserOpHash = JObject.Parse(sendUserOpResult)["result"].ToString();

                Debug.Log($"send user op hash {sendUserOpHash}");
                ShowToast($"{MethodBase.GetCurrentMethod()?.Name} Success:{sendUserOpHash}");
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
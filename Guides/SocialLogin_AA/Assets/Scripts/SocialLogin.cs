using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SocialLogin : MonoBehaviour
{
    [CanBeNull] private string _publicAddress = null;
    private WalletType? _walletType = null;

    [CanBeNull] private string _smartAccountAddress = null;

    // Start is called before the first frame update
    void Start()
    {
        ParticleNetwork.Init(ChainInfo.PolygonMumbai);
        var walletConnectProjectId = "f431aaea6e4dea6a669c0496f9c009c1";
        ParticleConnectInteraction.Init(ChainInfo.PolygonMumbai,
            new DAppMetaData(walletConnectProjectId, "Guide Wallet", "Guide Icon", "Guide Url", "Guide description"));
        ParticleWalletGUI.ParticleWalletConnectInitialize(new WalletMetaData("Guide Wallet", "Guide Icon", "Guide Url",
            "Guide description",
            walletConnectProjectId));
        ParticleAAInteraction.Init(AAAccountName.SIMPLE, AAVersionNumber.V1_0_0(), new Dictionary<int, string>());
        ParticleAAInteraction.EnableAAMode();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public async void ConnectGoogle()
    {
        try
        {
            var config = new ConnectConfig(LoginType.GOOGLE, "", SupportAuthType.ALL,
                socialLoginPrompt: SocialLoginPrompt.SelectAccount);
            // var config = new ConnectConfig(LoginType.JWT, "Your JWT", SupportAuthType.ALL);
            var nativeResultData =
                await ParticleConnect.Instance.Connect(WalletType.Particle, config);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var accountJson = JObject.Parse(nativeResultData.data);

                this._publicAddress = accountJson["publicAddress"].ToString();
                this._walletType = WalletType.Particle;
                Debug.Log($"publicAddress: {_publicAddress}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void ConnectMetamask()
    {
        try
        {
            var nativeResultData =
                await ParticleConnect.Instance.Connect(WalletType.MetaMask);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var accountJson = JObject.Parse(nativeResultData.data);

                this._publicAddress = accountJson["publicAddress"].ToString();
                this._walletType = WalletType.MetaMask;
                Debug.Log($"publicAddress: {_publicAddress}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void GetSmartAccount()
    {
        try
        {
            var smartAccountResult = await EvmService.GetSmartAccount(new[]
            {
                new SmartAccountObject(AAAccountName.SIMPLE.ToString(), AAVersionNumber.V1_0_0().version,
                    this._publicAddress)
            });
            this._smartAccountAddress = (string)JObject.Parse(smartAccountResult)["result"][0]["smartAccountAddress"];
            Debug.Log($"_smartAccountAddress: {_smartAccountAddress}");
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void Disconnect()
    {
        if (this._publicAddress == null || this._walletType == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        this._publicAddress = null;
        this._walletType = null;

        try
        {
            var nativeResultData =
                await ParticleConnect.Instance.Disconnect(WalletType.Particle, this._publicAddress);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void SendTransaction()
    {
        if (this._publicAddress == null || this._walletType == null || this._smartAccountAddress == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        try
        {
            // use smart account to create transaction
            var address = this._smartAccountAddress;
            const string to = "0x0000000000000000000000000000000000000000";
            var transaction =
                await EvmService.CreateTransaction(address, "0x", BigInteger.Parse("10000000000000000"), to);
            // call SignAndSendTransaction must use Account.publicAddress (Your EOA address) to send transaction
            var nativeResultData =
                await ParticleConnect.Instance.SignAndSendTransaction((WalletType)this._walletType, _publicAddress,
                    transaction);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var result = nativeResultData.data;
                Debug.Log($"result: {result}");
            }
            else
            {
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
        if (this._publicAddress == null || this._walletType == null || this._smartAccountAddress == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        try
        {
            // call SignMessage must use Account.publicAddress (Your EOA address) to send transaction
            var nativeResultData =
                await ParticleConnect.Instance.SignMessage((WalletType)this._walletType, _publicAddress,
                    "Hello Particle");
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var result = nativeResultData.data;
                Debug.Log($"result: {result}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void BatchSendTransactions()
    {
        if (this._publicAddress == null || this._walletType == null || this._smartAccountAddress == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        try
        {
            // use smart account to create transaction
            var address = this._smartAccountAddress;
            const string to = "0x0000000000000000000000000000000000000000";
            // Now, create two transactions
            var transaction1 =
                await EvmService.CreateTransaction(address, "0x", BigInteger.Parse("10000000000000000"), to);
            var transaction2 =
                await EvmService.CreateTransaction(address, "0x", BigInteger.Parse("20000000000000000"), to);

            // Before send
            // check if pay native available
            var feeQuotesResultData = await ParticleAA.Instance.RpcGetFeeQuotes(this._publicAddress,
                new List<string> { transaction1, transaction2 });

            var verifyingPaymasterNative = JObject.Parse(feeQuotesResultData.data)["verifyingPaymasterNative"];
            var nativeFeeQuote = verifyingPaymasterNative["feeQuote"];

            var fee = BigInteger.Parse((string)nativeFeeQuote["fee"]);
            var balance = BigInteger.Parse((string)nativeFeeQuote["balance"]);

            if (balance < fee)
            {
                Debug.Log("native balance if not enough for gas fee");
            }
            else
            {
                Debug.Log("could send pay native");
            }
            // is balance >= fee, you can send with native
            // here is the code
            // var batchSendResultData =
            // await ParticleConnect.Instance.BatchSendTransactions((WalletType)this._walletType, _publicAddress,
            // new List<string> { transaction1, transaction2 }, AAFeeMode.Native(feeQuotesResultData));

            // check if pay gasless available
            var verifyingPaymasterGasless = JObject.Parse(feeQuotesResultData.data)["verifyingPaymasterGasless"];

            if (verifyingPaymasterGasless == null)
            {
                Debug.Log("gasless is not available");
            }
            else
            {
                Debug.Log("could send gasless");
            }
            // is verifyingPaymasterGasless is not null, you can send gasless
            // here is the code
            // var batchSendResultData =
            // await ParticleConnect.Instance.BatchSendTransactions((WalletType)this._walletType, _publicAddress,
            // new List<string> { transaction1, transaction2 }, AAFeeMode.Gasless(feeQuotesResultData));

            // Now, let's check if you can send pay token
            // check if pay token available
            var tokenPaymaster = JObject.Parse(feeQuotesResultData.data)["tokenPaymaster"];
            Debug.Log($"TokenPaymaster = {tokenPaymaster}");

            if (tokenPaymaster == null)
            {
                Debug.Log("pay token is not available");
                return;
            }
            else
            {
            }

            JArray feeQuotes = (JArray)(tokenPaymaster["feeQuotes"]);

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

            Debug.Log("prepare send pay token");

            // select the first feeQuote
            var feeQuote = overFeeQuotes[0];
            var tokenPaymasterAddress =
                JObject.Parse(feeQuotesResultData.data)["tokenPaymaster"]["tokenPaymasterAddress"].Value<string>();

            // call BatchSendTransactions must use Account.publicAddress (Your EOA address) to send transaction
            var batchSendResultData =
                await ParticleConnect.Instance.BatchSendTransactions((WalletType)this._walletType, _publicAddress,
                    new List<string> { transaction1, transaction2 }, AAFeeMode.Token(feeQuote, tokenPaymasterAddress));
            if (batchSendResultData.isSuccess)
            {
                var result = batchSendResultData.data;
                Debug.Log($"result: {result}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(batchSendResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }


    public void OpenWallet()
    {
        ParticleWalletGUI.NavigatorWallet();
    }
}
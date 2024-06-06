#if !UNITY_ANDROID && !UNITY_IOS
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json.Linq;
using Particle.Windows.Modules.Models;
using UnityEngine;
using SignTypedDataVersion = Particle.Windows.Modules.Models.SignTypedDataVersion;

namespace Particle.Windows.Demo
{
    public class ParticleWindowsDemo : MonoBehaviour
    {
        public Canvas webCanvas;


        private string _evmAddress = "";
        private string _solanaAddress = "";
        private string _smartAccountAddress = "";

        public void Init()
        {
            var config = new ParticleConfig(new ParticleConfigSecurityAccount(1, 1), null);

            var theme = new ParticleTheme
            {
                UiMode = "dark",
                DisplayWallet = true,
                DisplayCloseButton = true
            };

            var language = "en-US";

            var chainInfo = ChainInfo.Polygon;

            ParticleSystem.Instance.Init(config.ToString(), theme.ToString(), language, chainInfo);
        }

        public async void Login()
        {
            webCanvas.sortingOrder = 2;
            var loginResult = await ParticleSystem.Instance.Login(PreferredAuthType.email, "");
            Debug.Log($"Login result {loginResult}");
            var userInfo = JObject.Parse(loginResult);
            var wallets = userInfo["wallets"];

            var evmWallet = wallets.Children<JObject>()
                .FirstOrDefault(wallet => wallet["chain_name"].ToString() == "evm_chain");
            if (evmWallet != null) _evmAddress = evmWallet["public_address"].ToString();

            var solanaWallet = wallets.Children<JObject>()
                .FirstOrDefault(wallet => wallet["chain_name"].ToString() == "solana_chain");
            if (solanaWallet != null) _solanaAddress = solanaWallet["public_address"]!.ToString();

            Debug.Log($"evm address: {_evmAddress}, solana address: {_solanaAddress}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignMessage()
        {
            webCanvas.sortingOrder = 2;
            var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            Debug.Log($"SignMessage result {signMessageResult}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignAndSendTransaction()
        {
            webCanvas.sortingOrder = 2;

            // make a test transaction,
            // you need to update it parameters before trying.
            var transaction = ParticleSystem.Instance.MakeEvmTransaction("0x16380a03f21e5a5e339c15ba8ebe581d194e0db3",
                "0xA719d8C4C94C1a877289083150f8AB96AD0C6aa1", "0x",
                "0x123123");
            var signMessageResult = await ParticleSystem.Instance.SignAndSendTransaction(transaction);
            Debug.Log($"SignAndSendTransaction result {signMessageResult}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignTypedData()
        {
            webCanvas.sortingOrder = 2;
            // only support evm
            // pass your typedDataV4 here.
            string typedDataV4 = "";
            var signMessageResult =
                await ParticleSystem.Instance.SignTypedData(typedDataV4, SignTypedDataVersion.Default);
            Debug.Log($"SignTypedData result {signMessageResult}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignTransaction()
        {
            webCanvas.sortingOrder = 2;
            // only support solana
            // pass your solana transaction here, request base58 string.
            string transaction = "";
            var signMessageResult = await ParticleSystem.Instance.SignTransaction(transaction);
            Debug.Log($"SignTransaction result {signMessageResult}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignAllTransactions()
        {
            webCanvas.sortingOrder = 2;
            // only support solana
            // pass your solana transactions here, request base58 string list.
            List<string> transactions = new List<string> { "" };
            var signMessageResult = await ParticleSystem.Instance.SignAllTransactions(transactions);
            Debug.Log($"SignAllTransactions result {signMessageResult}");
            webCanvas.sortingOrder = 0;
        }

        public void GetChainInfo()
        {
            var chainInfo = UnityInnerChainInfo.GetChainInfo();
            Debug.Log($"chainName: {chainInfo.Name}, chainId: {chainInfo.Id}");
        }

        public async void GetSmartAccount()
        {
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            var result = await EvmService.GetSmartAccount(new List<SmartAccountObject>
                { new SmartAccountObject(AAAccountName.BICONOMY_V2(), _evmAddress) });
            _smartAccountAddress = (string)JObject.Parse(result)["result"][0]["smartAccountAddress"];
            Debug.Log($"smart account address: {_smartAccountAddress}");
        }

        public async void SendTransactionWithGasless()
        {
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            var transaction = new SimplifyTransaction("0xc7099b5CaE9c4c9Bc64febe8fce0321C69F15417", "0x", "0");

            var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), _evmAddress);

            var feeQuotes = await EvmService.GetFeeQuotes(smartAccountObject,
                new List<SimplifyTransaction> { transaction });

            var verifyingPaymasterGasless = JObject.Parse(feeQuotes)["result"]["verifyingPaymasterGasless"];

            if (verifyingPaymasterGasless == null || verifyingPaymasterGasless.Type == JTokenType.Null)
            {
                Debug.Log("gasless is not available");
                return;
            }

            var userOpHash = verifyingPaymasterGasless["userOpHash"].ToString();
            var userOp = verifyingPaymasterGasless["userOp"];
            
            webCanvas.sortingOrder = 2;
            var signMessageResult = await ParticleSystem.Instance.SignMessage(userOpHash);
            var signature = JObject.Parse(signMessageResult)["signature"].ToString();
            webCanvas.sortingOrder = 0;

            userOp["signature"] = signature;

            var result = await EvmService.SendUserOp(smartAccountObject, userOp);
            var txHash = JObject.Parse(result)["result"].ToString();
            Debug.Log($"txHash {txHash}");
        }

        public async void SendTransactionWithNative()
        {
           
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            var transaction = new SimplifyTransaction("0xc7099b5CaE9c4c9Bc64febe8fce0321C69F15417", "0x", "0x0");

            var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), _evmAddress);

            var feeQuotes = await EvmService.GetFeeQuotes(smartAccountObject,
                new List<SimplifyTransaction> { transaction });


            var verifyingPaymasterNative = JObject.Parse(feeQuotes)["result"]["verifyingPaymasterNative"];
            var feeQuote = verifyingPaymasterNative["feeQuote"];

            var fee = BigInteger.Parse((string)feeQuote["fee"]);
            var balance = BigInteger.Parse((string)feeQuote["balance"]);

            if (balance < fee)
            {
                Debug.Log("native balance is not enough for gas fee");
                return;
            }


            var userOpHash = verifyingPaymasterNative["userOpHash"].ToString();
            var userOp = verifyingPaymasterNative["userOp"];

            webCanvas.sortingOrder = 2;
            var signMessageResult = await ParticleSystem.Instance.SignMessage(userOpHash);
            var signature = JObject.Parse(signMessageResult)["signature"].ToString();
            webCanvas.sortingOrder = 0;
            
            userOp["signature"] = signature;
            
            var result = await EvmService.SendUserOp(smartAccountObject, userOp);
            var txHash = JObject.Parse(result)["result"].ToString();
            Debug.Log($"txHash {txHash}");
        }

        public async void SendTransactionWithToken()
        {
            _evmAddress = "0xbD578922d613A9bAc9525aAc70685B7C5236692D";
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            var transaction = new SimplifyTransaction("0xc7099b5CaE9c4c9Bc64febe8fce0321C69F15417", "0x", "0x0");

            var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), _evmAddress);

            var feeQuotes = await EvmService.GetFeeQuotes(smartAccountObject,
                new List<SimplifyTransaction> { transaction });


            JArray tokenFeeQuotes = (JArray)(JObject.Parse(feeQuotes)["result"]["tokenPaymaster"]["feeQuotes"]);

            var overFeeQuotes = tokenFeeQuotes
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
                JObject.Parse(feeQuotes)["result"]["tokenPaymaster"]["tokenPaymasterAddress"].Value<string>();
            
            var creatUserOpResult = await EvmService.CreateUserOp(smartAccountObject, new List<SimplifyTransaction> { transaction }, feeQuote,
                tokenPaymasterAddress, null);

            var userOpHash = JObject.Parse(creatUserOpResult)["result"]["userOpHash"].ToString();
            var userOp = JObject.Parse(creatUserOpResult)["result"]["userOp"];

            webCanvas.sortingOrder = 2;
            var signMessageResult = await ParticleSystem.Instance.SignMessage(userOpHash);
            var signature = JObject.Parse(signMessageResult)["signature"].ToString();
            webCanvas.sortingOrder = 0;

            userOp["signature"] = signature;
            var result = await EvmService.SendUserOp(smartAccountObject, userOp);
            var txHash = JObject.Parse(result)["result"].ToString();
            Debug.Log($"txHash {txHash}");
        }
    }
}
#endif
#if !UNITY_ANDROID && !UNITY_IOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Test;
using Newtonsoft.Json;
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

        private string sessionSignerPublicAddress = "";
        private List<object> sessions = new List<object>();

        public void Init()
        {
            var supportChains = new List<SupportChain>
                { new SupportChain(ChainInfo.EthereumSepolia.Name, ChainInfo.EthereumSepolia.Id) };
            var config = new ParticleConfig(new ParticleConfigSecurityAccount(1, 1),
                new ParticleConfigWallet(true, supportChains, null));

            var theme = new ParticleTheme
            {
                UiMode = "dark",
                DisplayWallet = true,
                DisplayCloseButton = true
            };

            var language = "en-US";

            var chainInfo = ChainInfo.EthereumSepolia;

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
                .FirstOrDefault(wallet => wallet["chain_name"].ToString() == "solana");
            if (solanaWallet != null) _solanaAddress = solanaWallet["public_address"]!.ToString();

            Debug.Log($"evm address: {_evmAddress}, solana address: {_solanaAddress}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignMessage()
        {
            var message = "";
            if (UnityInnerChainInfo.GetChainInfo().IsSolanaChain())
            {
                // "hello world" convert to base58 string,
                message = "StV1DL6CwTryKyV";
            }
            else
            {
                message = "hello world";
            }

            webCanvas.sortingOrder = 2;
            var signature = await ParticleSystem.Instance.SignMessage(message);
            Debug.Log($"SignMessage signature: {signature}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignAndSendTransaction()
        {
            webCanvas.sortingOrder = 2;

            // make a test transaction,
            // you need to update it parameters before trying.
            var transaction = "";
            if (UnityInnerChainInfo.GetChainInfo().IsSolanaChain())
            {
                transaction = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
            }
            else
            {
                transaction = await TransactionHelper.GetEVMTransacion(_evmAddress);
            }

            var signature = await ParticleSystem.Instance.SignAndSendTransaction(transaction);
            Debug.Log($"SignAndSendTransaction signature: {signature}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignTypedData()
        {
            // only support evm.
            if (UnityInnerChainInfo.GetChainInfo().IsSolanaChain())
            {
                return;
            }

            webCanvas.sortingOrder = 2;
            // only support evm
            // pass your typedDataV4 here.
            var txtAsset = Resources.Load<TextAsset>("Share/TypedDataV4");
            string typedData = txtAsset.text;

            var chainId = UnityInnerChainInfo.GetChainInfo().Id;
            JObject json = JObject.Parse(typedData);
            json["domain"]["chainId"] = chainId;
            string newTypedData = json.ToString();

            var signature =
                await ParticleSystem.Instance.SignTypedData(newTypedData, SignTypedDataVersion.v4);
            Debug.Log($"SignTypedData signature: {signature}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignTransaction()
        {
            // only support solana
            // pass your solana transaction here, request base58 string.
            var transaction = "";
            if (UnityInnerChainInfo.GetChainInfo().IsSolanaChain())
            {
                transaction = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
            }
            else
            {
                return;
            }

            webCanvas.sortingOrder = 2;

            var signature = await ParticleSystem.Instance.SignTransaction(transaction);
            Debug.Log($"SignTransaction signature: {signature}");
            webCanvas.sortingOrder = 0;
        }

        public async void SignAllTransactions()
        {
            // only support solana
            // pass your solana transactions here, request base58 string list.

            List<string> transactions = new List<string>();
            if (UnityInnerChainInfo.GetChainInfo().IsSolanaChain())
            {
                var transaction = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
                transactions.Add(transaction);
                transactions.Add(transaction);
            }
            else
            {
                return;
            }

            webCanvas.sortingOrder = 2;
            var signatureList = await ParticleSystem.Instance.SignAllTransactions(transactions);

            Debug.Log($"SignAllTransactions signature: {string.Join(", ", signatureList)}");
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
            var signature = await ParticleSystem.Instance.SignMessage(userOpHash, AAAccountName.BICONOMY_V2());
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
            var signature = await ParticleSystem.Instance.SignMessage(userOpHash, AAAccountName.BICONOMY_V2());
            webCanvas.sortingOrder = 0;

            userOp["signature"] = signature;

            var result = await EvmService.SendUserOp(smartAccountObject, userOp);
            var txHash = JObject.Parse(result)["result"].ToString();
            Debug.Log($"txHash {txHash}");
        }

        public async void SendTransactionWithToken()
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

            var creatUserOpResult = await EvmService.CreateUserOp(smartAccountObject,
                new List<SimplifyTransaction> { transaction }, feeQuote,
                tokenPaymasterAddress, null);

            var userOpHash = JObject.Parse(creatUserOpResult)["result"]["userOpHash"].ToString();
            var userOp = JObject.Parse(creatUserOpResult)["result"]["userOp"];

            webCanvas.sortingOrder = 2;
            var signature = await ParticleSystem.Instance.SignMessage(userOpHash, AAAccountName.BICONOMY_V2());
            webCanvas.sortingOrder = 0;

            userOp["signature"] = signature;
            var result = await EvmService.SendUserOp(smartAccountObject, userOp);
            var txHash = JObject.Parse(result)["result"].ToString();
            Debug.Log($"txHash {txHash}");
        }

        public void OpenWebWallet()
        {
            webCanvas.sortingOrder = 2;
            ParticleSystem.Instance.OpenWebWallet(AAAccountName.BICONOMY_V2());
        }

        public async void CreateSessions()
        {
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            try
            {
                var erc1155MintToModuleAddr = "0x8788379F96B5dbF5B787Bd8c616079B958da2538";
                var receiver = TestAccount.EVM.ReceiverAddress;
                var eoaAddress = _evmAddress;
                var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), eoaAddress);
                // Now we need a sessionSigner, at most time, you should use your server for this role.
                var signerPrivateKey = "";
                var signerPublicAddress = "";

                this.sessionSignerPublicAddress = signerPublicAddress;
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
                    
                var signature  = await ParticleSystem.Instance.SignMessage(createSessionUserOpHash, AAAccountName.BICONOMY_V2());
                Debug.Log($"signature {signature}");

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
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            var eoaAddress = _evmAddress;
            var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), eoaAddress);
            var validateSessionResult = await EvmService.ValidateSession(smartAccountObject, sessions, sessions[0]);
            var isPassingValidate = JObject.Parse(validateSessionResult)["result"].Value<bool>();
            Debug.Log($"isPassingValidate {isPassingValidate}");
        }

        public async void SendSessionUserOp()
        {
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }
            
            try
            {
                var erc1155TokenAddress = "0x210f16F8B69Cb8f2429942e573e19ba3C855b53b";
                var receiver = TestAccount.EVM.ReceiverAddress;
                var eoaAddress = _evmAddress;
                var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), eoaAddress);
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
                
                // use your session signer to sign the userOp hash
                var signature = "";
                userOp["signature"] = signature;
                
                var sendUserOpResult =
                    await EvmService.SendUserOp(smartAccountObject, userOp, this.sessions, this.sessions[0]);
                var sendUserOpHash = JObject.Parse(sendUserOpResult)["result"].ToString();

                Debug.Log($"send user op hash {sendUserOpHash}");
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }
    }
}
#endif
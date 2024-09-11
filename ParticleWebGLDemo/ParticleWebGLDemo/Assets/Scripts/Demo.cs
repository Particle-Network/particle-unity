using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Test;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Demo : MonoBehaviour
    {
        private string _evmAddress;
        private string _solanaAddress;
        private string _smartAccountAddress;
        private string sessionSignerPublicAddress;
        private List<object> sessions;

        private void Start()
        {
        }

        public void Init()
        {
            var projectId = "be36dab3-cefc-4345-9f80-c218f618febc";
            var clientKey = "cGzYoS9EKLSmniB0kD5Z7Q6Hv9t4GPqW99w7Jw4i";
            var appId = "252b2631-b2ad-48ad-b53f-c7091d1a475b";
            var securityAccount = new SecurityAccount
            {
                promptSettingWhenSign = 0,
                promptMasterPasswordSettingWhenLogin = 0
            };
            var wallet = new Wallet
            {
                displayWalletEntry = false,
                defaultWalletEntryPosition = new WalletEntryPosition { x = 0.0f, y = 0.0f },
                supportChains = new List<ChainInfo>
                {
                    ChainInfo.SolanaDevnet
                }
            };
            var config = new InitConfig(projectId, clientKey, appId, ChainInfo.SolanaDevnet, securityAccount,
                wallet);

            ParticleAuth.Instance.Init(config);
        }

        public async void Login()
        {
            // google login
            var userInfo = await ParticleAuth.Instance.Login(new LoginConfig(preferredAuthType: "google", account: "",
                null, "select_account", null));
            // jwt login
            // var userInfo = await ParticleAuth.Instance.Login(new LoginConfig(preferredAuthType: "jwt", account: "your jwt", null, "select_account", null));

            // open login page
            // var userInfo = await ParticleAuth.Instance.Login(null);

            // get userInfo after login
            // var userInfo = await ParticleAuth.Instance.Login(null);
            Debug.Log($"userInfo {userInfo}");

            // get current chain's public address, all evm chains' address is the same.
            var address = ParticleAuth.Instance.GetWalletAddress();
            Debug.Log($"current chainInfo, address {address}");

            getAddressFromUserInfo(userInfo);
        }

        public async void Logout()
        {
            var result = await ParticleAuth.Instance.Logout();
            Debug.Log($"logout result {result}");
        }

        public void GetUserInfo()
        {
            // get userInfo at anywhere
            var userInfo = ParticleAuth.Instance.GetUserInfo();
            Debug.Log($"userInfo {userInfo}");

            getAddressFromUserInfo(userInfo);
        }

        private void getAddressFromUserInfo(string userInfo)
        {
            var wallets = JObject.Parse(userInfo)["wallets"];

            var evmWallet = wallets.Children<JObject>()
                .FirstOrDefault(wallet => wallet["chain_name"].ToString() == "evm_chain");
            if (evmWallet != null) _evmAddress = evmWallet["public_address"].ToString();

            var solanaWallet = wallets.Children<JObject>()
                .FirstOrDefault(wallet => wallet["chain_name"].ToString() == "solana");
            if (solanaWallet != null) _solanaAddress = solanaWallet["public_address"]!.ToString();

            Debug.Log($"evm address {this._evmAddress}, solana address {this._solanaAddress}");
        }

        public async void EVMPersonalSign()
        {
            try
            {
                var message = "0x48656c6c6f205061727469636c6521"; //"Hello Particle!"
                var signature = await ParticleAuth.Instance.EVMPersonalSign(message, AAAccountName.BICONOMY_V2());

                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void EVMPersonalSignUnique()
        {
            try
            {
                var message = "0x48656c6c6f205061727469636c6521"; //"Hello Particle!"
                var signature = await ParticleAuth.Instance.EVMPersonalSignUniq(message);

                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void EVMSignTypedData()
        {
            try
            {
                var txtAsset = Resources.Load<TextAsset>("Share/TypedDataV4");
                string typedData = txtAsset.text;

                var chainId = UnityInnerChainInfo.GetChainInfo().Id;
                JObject json = JObject.Parse(typedData);
                json["domain"]["chainId"] = chainId;
                string newTypedData = json.ToString();


                var signature = await ParticleAuth.Instance.EVMSignTypedData(newTypedData);

                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void EVMSignTypedDataUnique()
        {
            try
            {
                var txtAsset = Resources.Load<TextAsset>("Share/TypedDataV4");
                string typedData = txtAsset.text;

                var chainId = UnityInnerChainInfo.GetChainInfo().Id;
                JObject json = JObject.Parse(typedData);
                json["domain"]["chainId"] = chainId;
                string newTypedData = json.ToString();


                var signature = await ParticleAuth.Instance.EVMSignTypedDataUniq(newTypedData);

                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void EVMSendTransaction()
        {
            try
            {
                var transaction = await TransactionHelper.GetEVMTransacion(_evmAddress);
                var signature = await ParticleAuth.Instance.EVMSendTransaction(transaction);
                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void SolanaSignMessage()
        {
            try
            {
                // "hello world" convert to base58 string,
                var message = "StV1DL6CwTryKyV";
                var signature = await ParticleAuth.Instance.SolanaSignMessage(message);
                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void SolanaSignTransaction()
        {
            try
            {
                // "hello world" convert to base58 string,
                var transaction = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
                var signature = await ParticleAuth.Instance.SolanaSignTransaction(transaction);
                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void SolanaSignAllTransactions()
        {
            try
            {
                // "hello world" convert to base58 string,
                var transaction1 = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
                var transaction2 = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
                var signatures =
                    await ParticleAuth.Instance.SolanaSignAllTransactions(new[] { transaction1, transaction2 });
                Debug.Log($"signatures: {string.Join(", ", signatures)}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void SolanaSignAndSendTransaction()
        {
            try
            {
                // "hello world" convert to base58 string,
                var transaction = await TransactionHelper.GetSolanaTransacion(_solanaAddress);
                var signature = await ParticleAuth.Instance.SolanaSignAndSendTransaction(transaction);
                Debug.Log($"signature {signature}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public void EnableERC4337()
        {
            ParticleAuth.Instance.EnableERC4337(true);
            ParticleAuth.Instance.SetERC4337(AAAccountName.BICONOMY_V2());
        }

        public void DisableERC4337()
        {
            ParticleAuth.Instance.EnableERC4337(false);
        }

        public void OpenWallet()
        {
            // todo, support open aa wallet.
            ParticleAuth.Instance.OpenWallet();
        }

        public async void SwitchChain()
        {
            await ParticleAuth.Instance.SwitchChain(ChainInfo.PolygonAmoy);
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

            try
            {
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


                var signature = await ParticleAuth.Instance.EVMPersonalSign(userOpHash, AAAccountName.BICONOMY_V2());

                userOp["signature"] = signature;

                var result = await EvmService.SendUserOp(smartAccountObject, userOp);
                var txHash = JObject.Parse(result)["result"].ToString();
                Debug.Log($"txHash {txHash}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void SendTransactionWithNative()
        {
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            try
            {
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

                var signature = await ParticleAuth.Instance.EVMPersonalSign(userOpHash, AAAccountName.BICONOMY_V2());

                userOp["signature"] = signature;

                var result = await EvmService.SendUserOp(smartAccountObject, userOp);
                var txHash = JObject.Parse(result)["result"].ToString();
                Debug.Log($"txHash {txHash}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
        }

        public async void SendTransactionWithToken()
        {
            if (_evmAddress == "")
            {
                Debug.Log("don't have a evm address");
                return;
            }

            try
            {
                var transaction = new SimplifyTransaction("0xc7099b5CaE9c4c9Bc64febe8fce0321C69F15417", "0x", "0x0");

                var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), _evmAddress);

                var feeQuotes = await EvmService.GetFeeQuotes(smartAccountObject,
                    new List<SimplifyTransaction> { transaction });

                var tokenPaymaster = JObject.Parse(feeQuotes)["result"]["tokenPaymaster"];


                if (tokenPaymaster == null || tokenPaymaster.Type == JTokenType.Null)
                {
                    Debug.Log("paying token is not available");
                    return;
                }

                JArray tokenFeeQuotes = (JArray)tokenPaymaster["feeQuotes"];


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


                var signature = await ParticleAuth.Instance.EVMPersonalSign(userOpHash, AAAccountName.BICONOMY_V2());

                userOp["signature"] = signature;
                var result = await EvmService.SendUserOp(smartAccountObject, userOp);
                var txHash = JObject.Parse(result)["result"].ToString();
                Debug.Log($"txHash {txHash}");
            }
            catch (Exception e)
            {
                Debug.Log($"error {e}");
                throw;
            }
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

                var session = new JObject()
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

                var signature =
                    await ParticleAuth.Instance.EVMPersonalSign(createSessionUserOpHash, AAAccountName.BICONOMY_V2());

                Debug.Log($"signature {signature}");
                createSessionUserOp["signature"] = signature;
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

            try
            {
                var eoaAddress = _evmAddress;
                var smartAccountObject = new SmartAccountObject(AAAccountName.BICONOMY_V2(), eoaAddress);
                var validateSessionResult = await EvmService.ValidateSession(smartAccountObject, sessions, sessions[0]);
                var isPassingValidate = JObject.Parse(validateSessionResult)["result"].Value<bool>();
                Debug.Log($"isPassingValidate {isPassingValidate}");
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
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
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class Example
    {
        public void Init()
        {
            var metadata = new DAppMetaData("Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network", "");

            ChainInfo chainInfo = new AvalancheChain(AvalancheChainId.Mainnet);
            // Init and set default chain info.
            ParticleNetwork.Init(chainInfo);
            ParticleConnectInteraction.Init(chainInfo, metadata);

            // Set support chain info array. you can a chain info array.
            ParticleWalletGUI.SupportChain(new[] { chainInfo });
            // Disable buy.
            ParticleWalletGUI.EnablePay(false);
            // Disable testnet if release.
            ParticleWalletGUI.ShowTestNetwork(false);
            // Disable wallet manage page if you only support one wallet.
            ParticleWalletGUI.ShowManageWallet(false);
            // Use this method to control dark mode or light mode. you can call this method with your button.
            ParticleNetwork.SetInterfaceStyle(UserInterfaceStyle.DARK);

            // Manage Tokens and NFTs, set show only native and your tokens, NFTs, don't show other tokens and NFTs.
            ParticleWalletGUI.SetDisplayTokenAddresses(new[] { "Your token address" });
            ParticleWalletGUI.SetDisplayNFTContractAddresses(new[] { "Your nft address" });

            // Manage Tokens and NFTs, set priority tokens and NFTs. 
            ParticleWalletGUI.SetPriorityTokenAddresses(new[] { "Your token address" });
            ParticleWalletGUI.SetPriorityNFTContractAddresses(new[] { "Your nft address" });

            // Control if show add button in wallet page.
            ParticleWalletGUI.SetSupportAddToken(false);

            // Control UI pages native currency symbol
            ParticleWalletGUI.SetFiatCoin("HKD");

            // Set language
            ParticleWalletGUI.SetLanguage(Language.KO);

            // Control if show language setting button in setting page.
            ParticleWalletGUI.ShowLanguageSetting(true);

            // Control if show appearance setting button in setting page.
            ParticleWalletGUI.ShowAppearanceSetting(true);
        }

        public async void Login()
        {
            // Show login with particle auth, support apple and google.
            var nativeResultData = await ParticleAuthService.Instance.Login(LoginType.PHONE, "",
                SupportAuthType.APPLE | SupportAuthType.GOOGLE | SupportAuthType.EMAIL);
            // Get result
            Debug.Log(nativeResultData.data);
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

        public void ShowWallet()
        {
            // Before call this method, make sure you called ParticleNetwork.init and ParticleConnectInteraction.init.
            ParticleWalletGUI.NavigatorWallet();
        }

        public void SwitchChainBetweenEvm()
        {
            // for example if you both support Avalanche and Ethereum. and you init Avalanche.mainnet.
            // then you want to switch to Ethereum.mainnet.
            // Because switch from a evm chain to another evm chain, public address is the same, not changed.
            // It is a sync method. will return ture=success or false=failure immediately.
            var result = ParticleConnectInteraction.SetChainInfo(new EthereumChain(EthereumChainId.Mainnet));
        }

        public async void SwitchChainBetweenEvmAndSolana()
        {
            // for example if you both support Solana and Avalanche, and you init Avalanche.mainnet.
            // then you want to switch to Solana.mainnet.
            // Because switch from a evm chain to a solana chain, public address should change,
            // It is a async method.
            // If it is the first time your user switch from evm to solana, This method will open web page and
            // create a solana address in seconds, then turn chain info to solana.mainnet.
            // If it is not the first time, it will switch to solana.mainnet immediately.
            var nativeResultData =
                await ParticleConnect.Instance.SetChainInfoAsync(new SolanaChain(SolanaChainId.Mainnet));

            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                Debug.Log("SetChainInfoAsync:" + nativeResultData.data);
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        public async void Mint()
        {
            // Example for call mint method of a contract
            string contractAddress = "";
            string methodName = "custom_mint";
            List<object> parameters = new List<object> { "1" };

            List<object> objects = new List<object> { contractAddress, methodName, parameters };
            string data = await EvmService.AbiEncodeFunctionCall(objects);
        }

        public async void CustomMethod()
        {
            // Example for call custom method of a contract
            string contractAddress = "";
            // should add custom_ before your method name. like "custom_mint", "custom_balanceOf"
            string methodName = "custom_method";
            // List your method's parameters 
            List<object> parameters = new List<object> { "1" };
            // This is your contact abjJsonString
            string abiJson = "";

            // Combine above into a ordered list
            List<object> objects = new List<object> { contractAddress, methodName, parameters, abiJson };
            // Send 
            string dataResult = await EvmService.AbiEncodeFunctionCall(objects);
            // get data
            var data = (string)JObject.Parse(dataResult)["result"];
            // use data to configure transaction or eth_call
        }

        public async void ReadContract()
        {
            // your public address
            string from = "";
            // your contract address
            string contractAddress = "";
            // should add custom_ before your method name. like "custom_mint", "custom_balanceOf"
            string methodName = "custom_method";
            // List your method's parameters 
            List<object> parameters = new List<object> { };
            // This is your contact abjJsonString, you can get it from your contract developer.
            string abiJsonString = "";

            // read contract
            string rpcResult =
                await EvmService.ReadContract(from, contractAddress, methodName, parameters, abiJsonString);

            var result = (string)JObject.Parse(rpcResult)["result"];

            if (!String.IsNullOrEmpty(result))
            {
                Debug.Log($"result={result}");
            }
            else
            {
                var error = JObject.Parse(rpcResult)["error"];
                var code = error?["code"];
                var message = error?["message"];
                Debug.Log($"code={code}, message={message}");
            }
        }
        
        
        public async Task<string> WriteContract()
        {
            // write contract, structure a transaction
            // your public address
            string from = "";
            // your contract address
            string contractAddress = "";
            // should add custom_ before your method name. like "custom_mint", "custom_balanceOf"
            string methodName = "custom_method";
            // List your method's parameters 
            List<object> parameters = new List<object> { };
            // This is your contact abjJsonString, you can get it from your contract developer.
            string abiJsonString = "";

            var dataResult =
                await EvmService.AbiEncodeFunctionCall(contractAddress, methodName, parameters, abiJsonString);
            // get data
            var data = (string)JObject.Parse(dataResult)["result"];
            
            // the following code is to get other transaction parameters, if your contract is deployed on a chain which doesn't support EIP1559,
            // pass isSupportEip1559 false, otherwise true.
            var isSupportEip1559 = true;
            return CreateContractTransaction(from, contractAddress, data, isSupportEip1559).Result;
        }


        private async Task<string> CreateContractTransaction(string from, string contractAddress, string data, bool isSupportEip1559)
        {
            
            var gasLimitResult = await EvmService.EstimateGas(from, contractAddress, "0x0", data);
            var gasLimit = (string)JObject.Parse(gasLimitResult)["result"];
            var gasFeesResult = await EvmService.SuggestedGasFees();
            var maxFeePerGas = (double)JObject.Parse(gasFeesResult)["result"]["high"]["maxFeePerGas"];
            var maxFeePerGasHex = "0x" + ((BigInteger)(maxFeePerGas * Mathf.Pow(10, 9))).ToString("x");

            var maxPriorityFeePerGas = (double)JObject.Parse(gasFeesResult)["result"]["high"]["maxPriorityFeePerGas"];
            var maxPriorityFeePerGasHex = "0x" + ((BigInteger)(maxPriorityFeePerGas * Mathf.Pow(10, 9))).ToString("x");
            var chainId = TestAccount.EVM.ChainId;

            EthereumTransaction transaction;
            
            if (isSupportEip1559)
            {
                transaction = new EthereumTransaction(from, contractAddress, data, gasLimit, gasPrice: null,
                    value: "0x0",
                    nonce: null, 
                    type: "0x2",
                    chainId: "0x" + chainId.ToString("x"), maxPriorityFeePerGasHex, maxFeePerGasHex);
            } else
            {
                transaction = new EthereumTransaction(from, contractAddress, data, gasLimit, gasPrice: maxFeePerGasHex,
                    value: "0x0",
                    nonce: null, 
                    type: "0x0",
                    chainId: "0x" + chainId.ToString("x"), null, null);
            }
            var json = JsonConvert.SerializeObject(transaction);
            var serialized = BitConverter.ToString(Encoding.Default.GetBytes(json));
            serialized = serialized.Replace("-", "");
            return "0x" + serialized;
        }
    }
}
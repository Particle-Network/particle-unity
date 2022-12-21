using System.Collections.Generic;
using System.Reflection;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Core.Utils;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Test
{
    public class GUIDemo : MonoBehaviour
    {
        private static AndroidJavaObject activityObject;
        
        private ChainInfo _chainInfo = new EthereumChain(EthereumChainId.Goerli);
        public void SelectChain()
        {
            ChainChoice.Instance.Show((chainInfo) =>
            {
                Debug.Log($"xxhong {chainInfo.getChainName()} {chainInfo.getChainId()} {chainInfo.getChainIdName()}");
                this._chainInfo = ChainUtils.CreateChain(chainInfo.getChainName(), chainInfo.getChainId());
            });
        }
        
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
            var metadata = new DAppMetaData("Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network");

            ParticleNetwork.Init(_chainInfo);
            ParticleConnectInteraction.Init(_chainInfo, metadata);
        }

        public void ParticleWalletConnectInitialize()
        {
            var metaData = new WalletMetaData("Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network", description: "");
            ParticleWalletGUI.ParticleWalletConnectInitialize(metaData);
        }

        public void NavigatorWallet()
        {
            // If you want to navigator wallet token, set display to 0
            // If you want to navigator wallet NFT, set display to 1
            int display = 0;
            ParticleWalletGUI.NavigatorWallet(display);
        }

        public void NavigatorTokenReceive()
        {
            // If you want to test solana, your should replace under value with solana test account.
            // Navigator to token receive page
            // This is ChainLink Token in Ethereum Gnerli
            string tokenAddress = TestAccount.EVM.TokenContractAddress;
            ParticleWalletGUI.NavigatorTokenReceive(tokenAddress);
        }

        public void NavigatorTokenSend()
        {
            // If you want to test solana, your should replace under value with solana test account.
            // This is ChainLink Token in Ethereum Gnerli
            string tokenAddress = TestAccount.EVM.TokenContractAddress;
            // Another receiver address
            string toAddress = TestAccount.EVM.ReceiverAddress;
            // Send amount
            string amount = "1000000000";
            ParticleWalletGUI.NavigatorTokenSend(tokenAddress, toAddress, amount);
        }

        public void NavigatorTokenTransactionRecords()
        {
            // If you want to test solana, your should replace under value with solana test account.
            // This is ChainLink Token in Ethereum Gnerli
            string tokenAddress = TestAccount.EVM.TokenContractAddress;
            ParticleWalletGUI.NavigatorTokenTransactionRecords(tokenAddress);
        }

        public void NavigatorNFTSend()
        {
            // If you want to test solana, your should replace under value with solana test account.
            // This is a test NFT
            string mint = TestAccount.EVM.NFTContractAddress;
            string tokenId = TestAccount.EVM.NFTTokenId;
            string receiveAddress = "";
            ParticleWalletGUI.NavigatorNFTSend(mint, tokenId, receiveAddress);
        }

        public void NavigatorNFTDetails()
        {
            // If you want to test solana, your should replace under value with solana test account.
            // This is a test NFT
            string mint = TestAccount.EVM.NFTContractAddress;
            string tokenId = TestAccount.EVM.NFTTokenId;
            ParticleWalletGUI.NavigatorNFTDetails(mint, tokenId);
        }

        public void NavigatorPay()
        {
            ParticleWalletGUI.NavigatorPay();
        }

        public void NavigatorBuyBnb()
        {
            BuyCryptoConfig config = new BuyCryptoConfig(TestAccount.EVM.PublicAddress,
                OpenBuyNetwork.BinanceSmartChain, "BNB", "USD", 100);
            ParticleWalletGUI.NavigatorBuyCrypto(config);
        }

        public async void NavigatorLoginList()
        {
            var nativeResultData = await ParticleWalletGUI.Instance.NavigatorLoginList();

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

        public void NavigatorSwap()
        {
            ParticleWalletGUI.NavigatorSwap();
        }

        public async void SwitchWallet()
        {
            var walletType = WalletType.MetaMask;
            var publicAddress = "";
            var nativeResultData = await ParticleWalletGUI.Instance.SwitchWallet(walletType, publicAddress);
            
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

        public void ShowTestNetwork()
        {
            ParticleWalletGUI.ShowTestNetwork(false);
        }

        public void ShowManageWallet()
        {
            ParticleWalletGUI.ShowManageWallet(false);
        }

        public void ShowLanguageSetting()
        {
            ParticleWalletGUI.ShowLanguageSetting(true);
        }
        
        public void ShowAppearanceSetting()
        {
            ParticleWalletGUI.ShowAppearanceSetting(true);
        }

        public void SetLanguage()
        {
            ParticleWalletGUI.SetLanguage(Language.KO);
        }

        public void SetEnableBuyCryptoFeature()
        {
            ParticleWalletGUI.EnablePay(true);
        }
        
        public void SetEnableSwapFeature()
        {
            ParticleWalletGUI.EnableSwap(true);
        }

        public void SetSupportChainInfos()
        {
            ChainInfo avalanche = new AvalancheChain(AvalancheChainId.Mainnet);
            ChainInfo ethereum = new EthereumChain(EthereumChainId.Mainnet);
            ChainInfo bsc = new BSCChain(BscChainId.Mainnet);
            ParticleWalletGUI.SupportChain(new []{avalanche, bsc, ethereum});
        }

        public void GetSwapEnableState()
        {
            var result = ParticleWalletGUI.GetEnableSwap();
            Debug.Log($"Swap enable state = {result}");
        }
        
        public void GetBuyCryptoEnableState()
        {
            var result = ParticleWalletGUI.GetEnablePay();
            Debug.Log($"Buy crypto enable state = {result}");
        }

        public void SupportWalletConnect()
        {
            ParticleWalletGUI.SupportWalletConnect(true);
        }

        public void SetCustomUI()
        {
            ParticleWalletGUI.SetSupportAddToken(true);
            
            List<string> displayTokenAddresses = new List<string>();
            displayTokenAddresses.Add("0x326C977E6efc84E512bB9C30f76E30c160eD06FB");
            displayTokenAddresses.Add("0xaFF4481D10270F50f203E0763e2597776068CBc5");
            ParticleWalletGUI.SetDisplayTokenAddresses(displayTokenAddresses.ToArray());

            List<string> nftContractAddresses = new List<string>();
            nftContractAddresses.Add("0xD000F000Aa1F8accbd5815056Ea32A54777b2Fc4");
            nftContractAddresses.Add("0x225140E33a113CC616A5d5F06D01e258f5a19B7D");
            ParticleWalletGUI.SetDisplayNFTContractAddresses(nftContractAddresses.ToArray());
            
            ParticleWalletGUI.SetFiatCoin("JPY");

            // your custom ui json
            // format is here.
            // https://github.com/Particle-Network/particle-ios/blob/main/Demo/Demo/customUIConfig.json
            var txtAsset = Resources.Load<TextAsset>("customUIConfig");
            string json = txtAsset.text;
            ParticleWalletGUI.LoadCustomUIJsonString(json);
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
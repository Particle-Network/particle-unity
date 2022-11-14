using System.Reflection;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Core.Utils;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
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
            var metadata = DAppMetadata.Create("Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network");

            ParticleNetwork.Init(_chainInfo);
            ParticleConnectInteraction.Init(_chainInfo, metadata);
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
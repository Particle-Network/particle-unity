using System.Collections.Generic;
using System.Reflection;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using UnityEngine;
using CommonTip.Script;

namespace Network.Particle.Scripts.Test
{
    public class GUIDemo : MonoBehaviour
    {
        private static AndroidJavaObject activityObject;

        private ChainInfo _chainInfo = ChainInfo.Ethereum;

        public void SelectChain()
        {
            SelectChainPage.Instance.Show((chainInfo) =>
            {
                Debug.Log($"select chain{chainInfo.Name} {chainInfo.Id} {chainInfo.Network}");
                this._chainInfo =
                    chainInfo;
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
            var metadata = new DAppMetaData(TestConfig.walletConnectProjectId, "Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network", "Particle Unity Demo");

            ParticleNetwork.Init(_chainInfo);
            ParticleConnectInteraction.Init(_chainInfo, metadata);
            ParticleWalletGUI.ParticleWalletConnectInitialize(metadata);
        }

        public void NavigatorWallet()
        {
            // If you want to navigator wallet token, set display to 0
            // If you want to navigator wallet NFT, set display to 1
            int display = 0;
            ParticleWalletGUI.NavigatorWallet(display);
        }

        public void NavigatorDappBrowser()
        {
            ParticleWalletGUI.NavigatorDappBrowser("https://opensea.io");
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
            ParticleWalletGUI.NavigatorNFTSend(mint, tokenId, null, null);
        }

        public void NavigatorNFTDetails()
        {
            // If you want to test solana, your should replace under value with solana test account.
            // This is a test NFT
            string mint = TestAccount.EVM.NFTContractAddress;
            string tokenId = TestAccount.EVM.NFTTokenId;
            ParticleWalletGUI.NavigatorNFTDetails(mint, tokenId);
        }

        public void NavigatorBuyBnb()
        {
            // ParticleWalletGUI.NavigatorBuyCrypto(null);
            BuyCryptoConfig config = new BuyCryptoConfig(TestAccount.EVM.PublicAddress,
                ChainInfo.BNBChain, "BNB", "USD", 100);
            ParticleWalletGUI.NavigatorBuyCrypto(config);
        }

        public async void NavigatorLoginList()
        {
            var nativeResultData = await ParticleWalletGUI.Instance.NavigatorLoginList(
                new List<LoginListPageSupportType>
                {
                    LoginListPageSupportType.all
                });

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

        public void SetShowTestNetwork()
        {
            ParticleWalletGUI.SetShowTestNetwork(true);
        }

        public void SetShowManageWallet()
        {
            ParticleWalletGUI.SetShowManageWallet(true);
        }

        public void SetShowLanguageSetting()
        {
            ParticleWalletGUI.SetShowLanguageSetting(true);
        }

        public void SetShowSmartAccountSetting()
        {
            ParticleWalletGUI.SetShowSmartAccountSetting(true);
        }

        public void SetShowAppearanceSetting()
        {
            ParticleWalletGUI.SetShowAppearanceSetting(true);
        }

        public void SetSupportDappBrowser()
        {
            ParticleWalletGUI.SetSupportDappBrowser(true);
        }

        public void SetPayDisabled()
        {
            ParticleWalletGUI.SetPayDisabled(true);
        }

        public void SetSwapDisabled()
        {
            ParticleWalletGUI.SetSwapDisabled(true);
        }

        public void SetSupportChainInfos()
        {
            ChainInfo avalanche = ChainInfo.Avalanche;
            ChainInfo ethereum = ChainInfo.Ethereum;
            ChainInfo bnb = ChainInfo.BNBChain;
            ParticleWalletGUI.SetSupportChain(new[] { avalanche, bnb, ethereum });
        }

        public void GetSwapDisabled()
        {
            var result = ParticleWalletGUI.GetSwapDisabled();
            Debug.Log($"Is swap disabled = {result}");
        }

        public void GetPayDisabled()
        {
            var result = ParticleWalletGUI.GetPayDisabled();
            Debug.Log($"Is pay disabled = {result}");
        }

        public void SetSupportWalletConnect()
        {
            ParticleWalletGUI.SetSupportWalletConnect(true);
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

            ParticleNetwork.SetFiatCoin(FiatCoin.JPY);

            // Only works for iOS
            // your custom ui json
            var txtAsset = Resources.Load<TextAsset>("customUIConfig");
            string json = txtAsset.text;
            ParticleWalletGUI.LoadCustomUIJsonString(json);

            // Only works for iOS
            // set your wallet name and icon, only support particle auth wallet and particle auth core wallet,
            // WalletType is particle or authCore.
            ParticleWalletGUI.SetCustomWalletName("Hello Wallet",
                "https://static.particle.network/wallet-icons/Rainbow.png");

            // Only works for iOS
            // set customize localizables for iOS, you can get all ios localizable strings
            // from path yourIOSBuild/Pods/ParticleWalletGUI/XCFrameworks/ParticleWalletGUI/ParticleWalletGUI.bundle/en.lproj/Locallizable.strings.
            // 
            var enLocalizables = new Dictionary<string, string>
            {
                { "network fee", "Service Fee" },
                { "particle auth wallet", "Hello" },
            };

            var localizables = new Dictionary<Language, Dictionary<string, string>>
            {
                { Language.EN, enLocalizables }
            };
            ParticleWalletGUI.SetCustomLocalizable(localizables);
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using UnityEngine;
using UnityEngine.Rendering;

namespace Network.Particle.Scripts.Test
{
    public class ParticleNetworkSDKTest : MonoBehaviour
{
    private static AndroidJavaObject activityObject;

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
        ParticleNetwork.Init(new SolanaChain(SolanaChainId.Devnet));
    }


    public async void Login()
    {
        // login email
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var nativeResultData = await ParticleAuthService.Instance.Login(LoginType.PHONE, null, SupportAuthType.NONE);

        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"loginSuccess:{nativeResultData.data}");
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(nativeResultData.data);
            Debug.Log($"{userInfo}");
        }
        else
        {
            ShowToast($"loginFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void LoginWithEmailAccount()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var nativeResultData =
            await ParticleAuthService.Instance.Login(LoginType.EMAIL, "", SupportAuthType.NONE);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"loginSuccess:{nativeResultData.data}");
            var output = JsonConvert.DeserializeObject<UserInfo>(nativeResultData.data);
            Debug.Log(output);
        }
        else
        {
            ShowToast($"loginFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void LoginWithPhoneAccountOnlySupportApple()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var nativeResultData =
            await ParticleAuthService.Instance.Login(LoginType.PHONE, "+8613621184429", SupportAuthType.APPLE);

        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"loginSuccess:{nativeResultData.data}");
            var output = JsonConvert.DeserializeObject<UserInfo>(nativeResultData.data);
            Debug.Log(output);
        }
        else
        {
            ShowToast($"loginFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void LoginWithGoogle()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var nativeResultData =
            await ParticleAuthService.Instance.Login(LoginType.GOOGLE, null,
                SupportAuthType.GOOGLE | SupportAuthType.APPLE | SupportAuthType.FACEBOOK);

        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"loginSuccess:{nativeResultData.data}");
            var output = JsonConvert.DeserializeObject<UserInfo>(nativeResultData.data);
            Debug.Log(output);
        }
        else
        {
            ShowToast($"loginFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void Logout()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var nativeResultData = await ParticleAuthService.Instance.Logout();

        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }


    public void isLogin()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        Debug.Log(ParticleAuthServiceInteraction.IsLogin());
    }

    public async void SignMessage()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        // "Hello world !"
        string message = "0x2248656c6c6f20776f726c64202122";
        var nativeResultData = await ParticleAuthService.Instance.SignMessage(message);

        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void SignTransaction()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        string transaction = "";
        var nativeResultData =
            await ParticleAuthService.Instance.SignTransaction(transaction);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void SignAllTransactions()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        string[] transactions = { "", "" };
        var nativeResultData =
            await ParticleAuthService.Instance.SignAllTransactions(transactions);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void SignAndSendTransaction()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        string message = "";
        var nativeResultData =
            await ParticleAuthService.Instance.SignAndSendTransaction(message);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void SignTypedData()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var list = new List<JObject>
        {
            new JObject
            {
                { "type", "string" },
                { "name", "fullName" },
                { "value", "John Doe" },
            },
            new JObject
            {
                { "type", "string" },
                { "name", "fullName" },
                { "value", "John Doe" },
            }
        };

        byte[] bytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(list));
        string hexString = BitConverter.ToString(bytes);
        var message = "0x" + hexString.Replace("-", "");

        Debug.Log(message);
        var nativeResultData =
            await ParticleAuthService.Instance.SignTypedData(message, SignTypedDataVersion.V1);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public void GetAddress()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        Debug.Log(ParticleAuthServiceInteraction.GetAddress());
    }

    public void GetUserInfo()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        var userInfoJson = ParticleAuthServiceInteraction.GetUserInfo();
        var output = JsonConvert.DeserializeObject<UserInfo>(userInfoJson);
        Debug.Log(userInfoJson);
    }

    public async void SetChainInfoKovanAsync()
    {
        ChainInfo info = new EthereumChain(EthereumChainId.Goerli);
        var nativeResultData =
            await ParticleAuthService.Instance.SetChainInfoAsync(info);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public void SetChainInfoBscTestnetSync()
    {
        ChainInfo info = new BSCChain(BscChainId.Testnet);
        Debug.Log(ParticleAuthService.Instance.SetChainInfoSync(info));
    }

    public void GetChainInfo()
    {
        Debug.Log("chainName:" + ParticleNetwork.GetChainInfo().getChainName() + " chainId:" +
                  ParticleNetwork.GetChainInfo().getChainId() + " chainIdName:" +
                  ParticleNetwork.GetChainInfo().getChainIdName());
    }

    public void GetDevEnv()
    {
        Debug.Log(ParticleNetwork.GetEnv());
    }

    public void SetModalPresentStyle()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
        ParticleAuthServiceInteraction.SetModalPresentStyle("fullScreen");
    }

    public void NavigatorWalletToken()
    {
        ParticleWalletGUI.NavigatorWallet(0);
    }

    public void NavigatorWalletNFT()
    {
        ParticleWalletGUI.NavigatorWallet(1);
    }

    public void NavigatorTokenReceive()
    {
        // ethereum, kovan, chainLink 
        string tokenAddress = "0xa36085F69e2889c224210F603D836748e7dC0088";
        ParticleWalletGUI.NavigatorTokenReceive(tokenAddress);
    }

    public void NavigatorTokenSend()
    {
        // ethereum, kovan, chainLink
        string tokenAddress = "0xa36085F69e2889c224210F603D836748e7dC0088";
        string toAddress = "0xAC6d81182998EA5c196a4424EA6AB250C7eb175b";
        string amount = "1000000000";
        ParticleWalletGUI.NavigatorTokenSend(tokenAddress, toAddress, amount);
    }

    public void NavigatorTokenTransactionRecords()
    {
        // ethereum, kovan, chainLink
        string tokenAddress = "0xa36085F69e2889c224210F603D836748e7dC0088";
        ParticleWalletGUI.NavigatorTokenTransactionRecords(tokenAddress);
    }

    public void NavigatorNFTSend()
    {
        string mint = "0xf5de760f2e916647fd766b4ad9e85ff943ce3a2b";
        string receiveAddress = "";
        string tokenId = "1227488";
        ParticleWalletGUI.NavigatorNFTSend(mint, receiveAddress, tokenId);
    }

    public void NavigatorNFTDetails()
    {
        string mint = "0xf5de760f2e916647fd766b4ad9e85ff943ce3a2b";
        string tokenId = "1227488";
        ParticleWalletGUI.NavigatorNFTDetails(mint, tokenId);
    }


    public async void SolanaGetTokenList()
    {
        var nativeResultData = await ParticleWalletAPI.Instance.SolanaGetTokenList();
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void EvmGetTokenList()
    {
        var nativeResultData = await ParticleWalletAPI.Instance.EvmGetTokenList();
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void SolanaGetTokensAndNFTs()
    {
        string address = "HKerFyAkFKgTsrAZBe88MHKnbRMjeHL4NFguSPTyiT9g";
        var nativeResultData = await ParticleWalletAPI.Instance.SolanaGetTokensAndNFTs(address);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void EvmGetTokensAndNFTs()
    {
        string address = "0x16380a03F21E5a5E339c15BA8eBE581d194e0DB3";
        string[] tokenAddresses = new[] { "0xa36085F69e2889c224210F603D836748e7dC0088", "0x4F96Fe3b7A6Cf9725f59d353F723c1bDb64CA6Aa" };
        var nativeResultData = await ParticleWalletAPI.Instance.EvmGetTokensAndNFTs(address, tokenAddresses);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void SolanaGetTransactions()
    {
        string address = "HKerFyAkFKgTsrAZBe88MHKnbRMjeHL4NFguSPTyiT9g";
        var nativeResultData = await ParticleWalletAPI.Instance.SolanaGetTransactions(address, null, null, 1000);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public async void EvmGetTransactions()
    {
        string address = "0x16380a03F21E5a5E339c15BA8eBE581d194e0DB3";
        var nativeResultData = await ParticleWalletAPI.Instance.EvmGetTransactions(address);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    public void EnablePay()
    {
        ParticleWalletGUI.EnablePay(true);
    }

    public void DisablePay()
    {
        ParticleWalletGUI.EnablePay(false);
    }

    public void OpenPay()
    {
        ParticleWalletGUI.NavigatorPay();
    }

    public void OpenLoginList()
    {
        ParticleWalletGUI.Instance.NavigatorLoginList();
    }

    public void OpenSwap()
    {
        ParticleWalletGUI.NavigatorSwap();
    }

    public async void EvmRpcTest()
    {
        var list = new List<object>();
        var result = await EvmService.Rpc("particle_suggestedGasFees", list);
        Debug.Log(result);
    }

    public async void GetTokensAndNFTs()
    {
        var userInfoJson = ParticleAuthServiceInteraction.GetUserInfo();
        var userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoJson);
        if (ParticleNetwork.GetChainInfo().IsEvmChain())
        {
            var publicAddress = userInfo.GetWallet(WalletChain.Evm).PublicAddress;
            var result =  await EvmService.GetTokensAndNFTs(publicAddress);
            Debug.Log(result);
        }
        else
        {
            var publicAddress = userInfo.GetWallet(WalletChain.Solana).PublicAddress;
            var result =  await SolanaService.GetTokensAndNFTs(publicAddress);
            Debug.Log(result);
        }
    }


    public async void ParticleConnectInit()
    {
        var metadata =  DAppMetadata.Create("Particle Connect",
            "https://static.particle.network/wallet-icons/Particle.png",
            "https://particle.network");
        ParticleConnectInteraction.Init(new SolanaChain(SolanaChainId.Devnet), metadata);
    }

    public async void ParticleConnectSetChainInfo()
    {
        ChainInfo info = new EthereumChain(EthereumChainId.Goerli);
        ParticleConnectInteraction.SetChainInfo(info);
    }
    
    WalletType walletType  = WalletType.Phantom;
    public async void Connect()
    {
     
        var nativeResultData = await ParticleConnect.Instance.Connect(walletType);
    }

    public async void Disconnect()
    {
        var nativeResultData = await ParticleConnect.Instance.Connect(walletType);
    }
    
    public void IsConnect()
    {
        var publicAddress = "";
        var isConnect = ParticleConnectInteraction.IsConnected(walletType, publicAddress);
        Debug.Log($"Particle Connect is Connect = {isConnect}, publicAddress = {publicAddress}, walletType = {walletType.ToString()}");
    }
    
    public async void AdapterSignAndSend()
    {
        var publicAddress = "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN";
        var transaction = await GetSolanaTransacion();
        Debug.Log("transaction = " + transaction);
        var nativeResultData =
            await ParticleConnect.Instance.SignAndSendTransaction(walletType, publicAddress, transaction);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void AdapterSignTransaction()
    {
        var publicAddress = "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN";
        var transaction = await GetSolanaTransacion();
        Debug.Log("transaction = " + transaction);
        var nativeResultData =
            await ParticleConnect.Instance.SignTransaction(walletType, publicAddress, transaction);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void AdapterSignAllTransactions()
    {
        WalletType walletType  = WalletType.SolanaPrivateKey;
        var publicAddress = "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN";
        var transaction1 = await GetSolanaTransacion();
        var transaction2 = await GetSolanaTransacion();
        Debug.Log("transaction1 = " + transaction1);
        Debug.Log("transaction2 = " + transaction2);
        string[] transactions = new[] { transaction1, transaction2};
        var nativeResultData =
            await ParticleConnect.Instance.SignAllTransactions(walletType, publicAddress, transactions);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void AdapterSignMessage()
    {
        WalletType walletType  = WalletType.SolanaPrivateKey;
        var publicAddress = "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN";
        var message = "StV1DL6CwTryKyV";
        var nativeResultData =
            await ParticleConnect.Instance.SignMessage(walletType, publicAddress, message);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void AdapterSignTypedData()
    {
        WalletType walletType  = WalletType.SolanaPrivateKey;
        var publicAddress = "";
        var typedData = "";
        var nativeResultData =
            await ParticleConnect.Instance.SignTypedData(walletType, publicAddress, typedData);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void ImportPrivateKey()
    {
        var tranaction = await GetSolanaTransacion();
        WalletType walletType  = WalletType.SolanaPrivateKey;
        var privateKey = "3qfz3MgtnrUtLJPzmgFkAyxBsFEPGSLsQhz86TQeYhoYdXu1B47Uv9ZDN4b1mhvdjmiJYWbvCpUwX7DdhJckVVzU";

        var nativeResultData =
            await ParticleConnect.Instance.ImportWalletFromPrivateKey(walletType, privateKey);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void ImportMnemonic()
    {
        WalletType walletType  = WalletType.SolanaPrivateKey;
        var mnemonic = "chase alarm debris favorite axis catch vendor drastic federal equal inner weather";

        var nativeResultData =
            await ParticleConnect.Instance.ImportWalletFromMnemonic(walletType, mnemonic);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }
    
    public async void ExportPrivateKey()
    {
        WalletType walletType  = WalletType.SolanaPrivateKey;
        var publicAddress = "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN";

        var nativeResultData =
            await ParticleConnect.Instance.ExportWalletPrivateKey(walletType, publicAddress);
        Debug.Log(nativeResultData.data);
        if (nativeResultData.isSuccess)
        {
            ShowToast($"logoutSuccess:{nativeResultData.data}");
            // var output = JsonConvert.DeserializeObject<LoginOutput>(nativeResultData.data);
            Debug.Log(nativeResultData.data);
        }
        else
        {
            ShowToast($"logoutFailed:{nativeResultData.data}");
            var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
            Debug.Log(errorData);
        }
    }

    async static Task<string> GetSolanaTransacion()
    {
        string sender = "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN";
        string receiver = "BBBsMq9cEgRf9jeuXqd6QFueyRDhNwykYz63s1vwSCBZ";
        long amount = 10000000;
        SerializeSOLTransReq req = new SerializeSOLTransReq(sender, receiver, amount);
        var result = await SolanaService.SerializeSOLTransaction(req);
        
        var resultData = JObject.Parse(result);
        var transaction = (string) resultData["result"]["transaction"]["serialized"];
        return transaction;
    }

    public void SetSolanaGUI()
    {
        ParticleWalletGUI.ShowTestNetwork(false);
        ParticleWalletGUI.ShowManageWallet(true);
        ChainInfo[] chainNames = {new SolanaChain(SolanaChainId.Mainnet)};
        ParticleWalletGUI.SupportChain(chainNames);
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

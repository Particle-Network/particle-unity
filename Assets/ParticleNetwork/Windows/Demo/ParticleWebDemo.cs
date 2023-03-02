using System.Collections.Generic;
using System.Threading.Tasks;
using Particle.Windows.Modules.Models;
using UnityEngine;
using Vuplex.WebView;

namespace Particle.Windows.Demo
{
    public class ParticleWebDemo : MonoBehaviour
    {

        public Canvas webCanvas;
        public void Init()
        {
            var config = new ParticleConfig();
            config.ProjectId = "7fa3e77f-9d07-4417-8e45-01560fef0eab";
            config.ClientKey = "cRX26iCgKipWu6scQi6R8qWaP903EF3YsL3bxIym";
            config.AppId = "4ca94b0e-74b9-4a5b-aad3-72ee1ed84793";

            var theme = new ParticleTheme();
            theme.UiMode = "dark";
            
            string language = "en";
            string chainName = "Ethereum";
            long chainId = 5;
            
            ParticleSystem.Instance.Init(config.ToString(), theme.ToString(), language, chainName, chainId);
        }

        public async void Login()
        {
            webCanvas.sortingOrder = 2;
            var loginResult = await ParticleSystem.Instance.Login(PreferredAuthType.email, "");
            Debug.Log($"Login result {loginResult}");
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
            // you need to update it parameters.
            var transaction = ParticleSystem.Instance.MakeEvmTransaction("0x16380a03f21e5a5e339c15ba8ebe581d194e0db3", "0xA719d8C4C94C1a877289083150f8AB96AD0C6aa1", "0x",
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
            var signMessageResult = await ParticleSystem.Instance.SignTypedData(typedDataV4, SignTypedDataVersion.Default);
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
    }
}
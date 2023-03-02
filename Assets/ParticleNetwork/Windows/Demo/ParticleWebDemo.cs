using System.Collections.Generic;
using System.Threading.Tasks;
using Particle.Windows.Modules.Models;
using UnityEngine;
using Vuplex.WebView;

namespace Particle.Windows.Demo
{
    public class ParticleWebDemo : MonoBehaviour
    {

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
            var loginResult = await ParticleSystem.Instance.Login(PreferredAuthType.email, "");
            Debug.Log($"Login result {loginResult}");
        }
        
        public async void SignMessage()
        {
            var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            Debug.Log($"SignMessage result {signMessageResult}");
        }
        
        public async void SignAndSendTransaction()
        {
            var signMessageResult = await ParticleSystem.Instance.SignAndSendTransaction("hello world");
            Debug.Log($"SignAndSendTransaction result {signMessageResult}");
        }
        
        public async void SignTypedData()
        {
            // only support evm
            // pass your typedDataV4 here.
            string typedDataV4 = "";
            var signMessageResult = await ParticleSystem.Instance.SignTypedData(typedDataV4, SignTypedDataVersion.Default);
            Debug.Log($"SignTypedData result {signMessageResult}");
        }
        
        public async void SignTransaction()
        {
            // only support solana
            // pass your solana transaction here, request base58 string.
            string transaction = "";
            var signMessageResult = await ParticleSystem.Instance.SignTransaction(transaction);
            Debug.Log($"SignTransaction result {signMessageResult}");
        }
        
        public async void SignAllTransactions()
        {
            // only support solana
            // pass your solana transactions here, request base58 string list.
            List<string> transactions = new List<string> { "" };
            var signMessageResult = await ParticleSystem.Instance.SignAllTransactions(transactions);
            Debug.Log($"SignAllTransactions result {signMessageResult}");
        }
    }
}
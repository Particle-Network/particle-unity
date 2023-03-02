using System.Threading.Tasks;
using Particle.Windows.Modules.Models;
using UnityEngine;
using Vuplex.WebView;
using ParticleSystem = Windows.Modules.ParticleSystem;

namespace Particle.Windows.Demo
{
    public class ParticleWebDemo : MonoBehaviour
    {
        CanvasWebViewPrefab _canvasWebViewPrefab;

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
            var loginResult = await ParticleSystem.Instance.Login("email", "");
            Debug.Log($"login result {loginResult}");
        }
        
        public async void SignMessage()
        {
            var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            Debug.Log($"login result {signMessageResult}");
        }
        
        public async void SignAndSendTransaction()
        {
            // var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            // Debug.Log($"login result {signMessageResult}");
        }
        
        public async void SignTypedData()
        {
            // var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            // Debug.Log($"login result {signMessageResult}");
        }
        
        public async void SignTransaction()
        {
            // var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            // Debug.Log($"login result {signMessageResult}");
        }
        
        public async void SignAllTransactions()
        {
            // var signMessageResult = await ParticleSystem.Instance.SignMessage("hello world");
            // Debug.Log($"login result {signMessageResult}");
        }
    }
}
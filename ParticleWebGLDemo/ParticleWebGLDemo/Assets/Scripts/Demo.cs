using UnityEngine;

namespace DefaultNamespace
{
    public class Demo: MonoBehaviour
    {
        public async void Login()
        {
            var userInfo = await ParticleAuth.Instance.Login(null);
            Debug.Log($"userInfo {userInfo}");
            
            var userInfo2 = ParticleAuth.Instance.GetUserInfo();
            Debug.Log($"userInfo2 {userInfo2}");
            
            var address = ParticleAuth.Instance.GetWalletAddress();
            Debug.Log($"address {address}");

            ParticleAuth.Instance.SetERC4337(false);
        }
        
        public async void SignMessage()
        {
            var message = "0x48656c6c6f205061727469636c6521"; //"Hello Particle!"
            var signature = await ParticleAuth.Instance.EVMPersonalSign(message);

            Debug.Log($"signature {signature}");
        }
        
        public async void OpenWallet()
        {
            ParticleAuth.Instance.OpenWallet();
        }
        
    }
}
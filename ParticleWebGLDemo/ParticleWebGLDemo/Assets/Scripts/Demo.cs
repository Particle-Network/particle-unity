using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace DefaultNamespace
{
    public class Demo : MonoBehaviour
    {
        private void Start()
        {
        }

        public void Init()
        {
            InitConfig config = new InitConfig
            {
                projectId = "be36dab3-cefc-4345-9f80-c218f618febc",
                clientKey = "cGzYoS9EKLSmniB0kD5Z7Q6Hv9t4GPqW99w7Jw4i",
                appId = "252b2631-b2ad-48ad-b53f-c7091d1a475b",
                chainName = "xterio",
                chainId = 112358,
                securityAccount = new SecurityAccount
                {
                    promptSettingWhenSign = 0,
                    promptMasterPasswordSettingWhenLogin = 0
                },
                wallet = new Wallet
                {
                    displayWalletEntry = false,
                    defaultWalletEntryPosition = new WalletEntryPosition { x = 0.0f, y = 0.0f },
                    supportChains = new List<Chain>
                    {
                        new Chain { id = 112358, name = "xterio" },
                        new Chain { id = 2702128, name = "xterioeth" }
                    }
                }
            };

            ParticleAuth.Instance.Init(config);
        }

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

        public void OpenWallet()
        {
            ParticleAuth.Instance.OpenWallet();
        }

        public async void SwitchChain()
        {
            Chain chain = new Chain(80002, "Polygon");
            await ParticleAuth.Instance.SwitchChain(chain);
        }
    }
}
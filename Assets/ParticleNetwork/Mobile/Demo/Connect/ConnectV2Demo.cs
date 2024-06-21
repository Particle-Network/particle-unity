using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using CommonTip.Script;
using Network.Particle.Scripts.Test.Model;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Network.Particle.Scripts.Test
{
    public class ConnectV2Demo : MonoBehaviour
    {
        private ChainInfo currChainInfo = ChainInfo.EthereumSepolia;

        //Connect HomePage
        [SerializeField] private GameObject homePageGameObject;
        [SerializeField] private Button btnSelectChain;
        [SerializeField] private Button btnConnect;
        [SerializeField] private TextMeshProUGUI currChainInfoTextMeshProUGUI;
        [SerializeField] private GameObject accountItemTemple;
        [SerializeField] private GameObject accountItemParent;
        [SerializeField] private GameObject emptyAccountsTip;


      

        //Connect With Wallet
        [SerializeField] private ConnectWithWallet connectWithWalletPage;

        //Connected Wallet Oprate
        [SerializeField] private ConnectedWalletOprate connectedWalletOpratePage;

        private void Awake()
        {
            Init();
            homePageGameObject.SetActive(true);
            connectWithWalletPage.gameObject.SetActive(false);
            connectedWalletOpratePage.gameObject.SetActive(false);
            OnChainSelected(currChainInfo);
        }

        private void Start()
        {
            LoadConnectWalletAccount();
            btnSelectChain.onClick.AddListener(() => SelectChainPage.Instance.Show(OnChainSelected));
            btnConnect.onClick.AddListener(() => { connectWithWalletPage.Show(OnWalletConnectCallback); });
        }

        private void OnChainSelected(ChainInfo chainInfo)
        {
            currChainInfo = chainInfo;
            ParticleNetwork.SetChainInfo(chainInfo);
            currChainInfoTextMeshProUGUI.text = currChainInfo.Fullname + " " + currChainInfo.Id;
        }

        private void OnWalletConnectCallback(bool refresh)
        {
            if (refresh)
                LoadConnectWalletAccount();
        }

        private void Init()
        {
            var metadata = new DAppMetaData(TestConfig.walletConnectProjectId, "Particle Connect",
                "https://connect.particle.network/icons/512.png",
                "https://connect.particle.network",
                "Particle Connect Unity Demo");
            ParticleNetwork.Init(currChainInfo);
            ParticleConnectInteraction.Init(currChainInfo, metadata);
            // List<ChainInfo> chainInfos = new List<ChainInfo>
            //     { ChainInfo.Ethereum, ChainInfo.EthereumSepolia, ChainInfo.EthereumSepolia };
            // ParticleConnectInteraction.SetWalletConnectV2SupportChainInfos(chainInfos.ToArray());

            // control how to show set master password and payment password.
            // ParticleNetwork.SetSecurityAccountConfig(new SecurityAccountConfig(0, 0));
        }

        private void LoadConnectWalletAccount()
        {
            ClearAllChildren(accountItemParent.transform);
            List<AccountItem> allAccountItems = new List<AccountItem>();
            List<WalletType> walletTypes = new List<WalletType>((WalletType[])Enum.GetValues(typeof(WalletType)));
            walletTypes.ForEach(walletType =>
            {
                var accounts = ParticleConnectInteraction.GetAccounts(walletType);
                if (accounts.Count != 0)
                {
                    var item = new AccountItem(walletType, accounts);
                    allAccountItems.Add(item);
                }
            });
            if (allAccountItems.Count == 0)
            {
                emptyAccountsTip.SetActive(true);
            }
            else
            {
                emptyAccountsTip.SetActive(false);
                allAccountItems.ForEach(accountItem =>
                {
                    var accountItemGameObject = Instantiate(accountItemTemple);
                    var item = accountItemGameObject.GetComponent<ConnectedWalletAccountItem>();
                    item.InitItem(accountItem.walletType, accountItem.accounts[0]);
                    item.SetBtnDisconnectClickListner((walletType, account) =>
                    {
                        print($"item: walletType:{walletType} account:{account}");
                        connectedWalletOpratePage.Show(walletType, account, OnWalletConnectCallback);
                    });
                    accountItemGameObject.transform.SetParent(accountItemParent.transform);
                });
            }
        }

        private void ClearAllChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
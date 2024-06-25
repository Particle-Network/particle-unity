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
    public class ConnectDemo : MonoBehaviour
    {
        
        public static ChainInfo currChainInfo = ChainInfo.EthereumSepolia;

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
            LoadConnectWalletAccount();
        }

        private void OnWalletConnectCallback(bool refresh)
        {
            if (refresh)
                LoadConnectWalletAccount();
        }

        private void LoadConnectWalletAccount()
        {
            ClearAllChildren(accountItemParent.transform);
            List<AccountItem> allAccountItems = new List<AccountItem>();
            List<WalletType> walletTypes = new List<WalletType>((WalletType[])Enum.GetValues(typeof(WalletType)));
            walletTypes.ForEach(walletType =>
            {
                var accounts = ParticleConnectInteraction.GetAccounts(walletType);

                if (ParticleNetwork.GetChainInfo().IsEvmChain())
                {
                    accounts = accounts.Where(item => item.publicAddress.StartsWith("0x")).ToList();
                }
                else
                {
                    accounts = accounts.Where(item => !item.publicAddress.StartsWith("0x")).ToList();
                }

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
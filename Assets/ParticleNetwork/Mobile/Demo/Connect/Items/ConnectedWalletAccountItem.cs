using Network.Particle.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Network.Particle.Scripts.Test
{
    public class ConnectedWalletAccountItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI walletName;
        [SerializeField] private TextMeshProUGUI publicAddress;
        [SerializeField] private Image walletIcon;
        [HideInInspector] public WalletType walletType;
        [HideInInspector] public Account account;

        public void InitItem(WalletType walletType, Account account)
        {
            Debug.Log($"InitItem: {walletType} {account.publicAddress}");
            this.walletType = walletType;
            this.account = account;
            walletName.text = walletType.ToString();
            publicAddress.text = account.publicAddress;
            walletIcon.sprite = Resources.Load<Sprite>(walletType.ToString());
        }

        public void SetBtnDisconnectClickListner(UnityAction<WalletType, Account> action)
        {
            GetComponent<Button>().onClick.AddListener(() => { action(walletType, account); });
        }
    }
}
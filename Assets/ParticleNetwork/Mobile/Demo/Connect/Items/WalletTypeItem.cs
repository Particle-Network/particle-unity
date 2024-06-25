using Network.Particle.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Particle.Scripts.Test
{
    public class WalletTypeItem : MonoBehaviour
    {
        private WalletType chainInfo;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image image;

        public void InitItem(WalletType walletType)
        {
            chainInfo = walletType;
            text.text = walletType.ToString();
            image.sprite = Resources.Load<Sprite>(walletType.ToString());
        }
    }
}
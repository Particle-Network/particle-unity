using Network.Particle.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

public class WalletChoiceItem : MonoBehaviour
{
    [SerializeField] private Text walletName;

    public void InitItem(WalletType walletType)
    {
        walletName.text = walletType.ToString();
    }
}
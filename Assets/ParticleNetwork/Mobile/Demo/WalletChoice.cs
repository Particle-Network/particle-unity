
using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WalletChoice : SingletonMonoBehaviour<WalletChoice>
{
    [SerializeField] private GameObject walletItemTemplate;
    [SerializeField] private GameObject scrollContent;
    public UnityAction<WalletType> unityAction;

    void Start()
    {
        List<WalletType> walletTypes = new List<WalletType>();
        walletTypes.Add(WalletType.Particle);
        walletTypes.Add(WalletType.MetaMask);
        walletTypes.Add(WalletType.Rainbow);
        walletTypes.Add(WalletType.Trust);
        walletTypes.Add(WalletType.BitKeep);
        walletTypes.Add(WalletType.ImToken);
        walletTypes.Add(WalletType.WalletConnect);
        walletTypes.Add(WalletType.Phantom);
        walletTypes.Add(WalletType.EvmPrivateKey);
        walletTypes.Add(WalletType.SolanaPrivateKey);
        
        
        
        foreach (var walletType in walletTypes)
        {
            var walletItem = Instantiate(walletItemTemplate);
            var item = walletItem.GetComponent<WalletChoiceItem>();
            item.InitItem(walletType);
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                unityAction.Invoke(walletType);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            });
            walletItem.transform.SetParent(scrollContent.transform);
        }
    }
    
    public void Show(UnityAction<WalletType> unityAction)
    {
        this.unityAction = unityAction;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
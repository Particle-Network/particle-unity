
using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChainChoice : SingletonMonoBehaviour<ChainChoice>
{
    [SerializeField] private GameObject chainItemTemplate;
    [SerializeField] private GameObject scrollContent;
    public UnityAction<ChainInfo> unityAction;

    void Start()
    {
        List<ChainInfo> chainInfos = new List<ChainInfo>();
        chainInfos.Add(new SolanaChain(SolanaChainId.Mainnet));
        chainInfos.Add(new SolanaChain(SolanaChainId.Devnet));
        chainInfos.Add(new SolanaChain(SolanaChainId.Testnet));
        chainInfos.Add(new EthereumChain(EthereumChainId.Mainnet));
        chainInfos.Add(new EthereumChain(EthereumChainId.Ropsten));
        chainInfos.Add(new EthereumChain(EthereumChainId.Rinkeby));
        chainInfos.Add(new EthereumChain(EthereumChainId.Goerli));

        chainInfos.Add(new BSCChain(BscChainId.Mainnet));
        chainInfos.Add(new BSCChain(BscChainId.Testnet));

        chainInfos.Add(new PolygonChain(PolygonChainId.Mainnet));
        chainInfos.Add(new PolygonChain(PolygonChainId.Mumbai));

        chainInfos.Add(new AvalancheChain(AvalancheChainId.Mainnet));
        chainInfos.Add(new AvalancheChain(AvalancheChainId.Testnet));

        chainInfos.Add(new MoonbeamChain(MoonbeamChainId.Mainnet));
        chainInfos.Add(new MoonbeamChain(MoonbeamChainId.Testnet));

        chainInfos.Add(new MoonriverChain(MoonriverChainId.Mainnet));
        chainInfos.Add(new MoonriverChain(MoonriverChainId.Testnet));

        chainInfos.Add(new HecoChain(HecoChainId.Mainnet));
        chainInfos.Add(new HecoChain(HecoChainId.Testnet));

        chainInfos.Add(new FantomChain(FantomChainId.Mainnet));
        chainInfos.Add(new FantomChain(FantomChainId.Testnet));

        chainInfos.Add(new ArbitrumChain(ArbitrumChainId.Mainnet));
        chainInfos.Add(new ArbitrumChain(ArbitrumChainId.Testnet));

        chainInfos.Add(new HarmonyChain(HarmonyChainId.Mainnet));
        chainInfos.Add(new HarmonyChain(HarmonyChainId.Testnet));


        chainInfos.Add(new AuroraChain(AuroraChainId.Mainnet));
        chainInfos.Add(new AuroraChain(AuroraChainId.Testnet));

        chainInfos.Add(new KccChain(KccChainId.Mainnet));
        chainInfos.Add(new KccChain(KccChainId.Testnet));

        chainInfos.Add(new OptimismChain(OptimismChainId.Mainnet));
        chainInfos.Add(new OptimismChain(OptimismChainId.Testnet));

        chainInfos.Add(new PlatONChain(PlatONChainId.Mainnet));
        chainInfos.Add(new PlatONChain(PlatONChainId.Testnet));

        foreach (var chainInfo in chainInfos)
        {
            var chainItem = Instantiate(chainItemTemplate);
            var item = chainItem.GetComponent<ChainChoiceItem>();
            item.InitItem(chainInfo);
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                unityAction.Invoke(chainInfo);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            });
            chainItem.transform.SetParent(scrollContent.transform);
        }
    }
    
    public void Show(UnityAction<ChainInfo> unityAction)
    {
        this.unityAction = unityAction;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
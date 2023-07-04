
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

        chainInfos.Add(new FantomChain(FantomChainId.Mainnet));
        chainInfos.Add(new FantomChain(FantomChainId.Testnet));

        chainInfos.Add(new ArbitrumChain(ArbitrumChainId.One));
        chainInfos.Add(new ArbitrumChain(ArbitrumChainId.Nova));
        chainInfos.Add(new ArbitrumChain(ArbitrumChainId.Goerli));

        chainInfos.Add(new HarmonyChain(HarmonyChainId.Mainnet));
        chainInfos.Add(new HarmonyChain(HarmonyChainId.Testnet));
        
        chainInfos.Add(new AuroraChain(AuroraChainId.Mainnet));
        chainInfos.Add(new AuroraChain(AuroraChainId.Testnet));

        chainInfos.Add(new KccChain(KccChainId.Mainnet));
        chainInfos.Add(new KccChain(KccChainId.Testnet));

        chainInfos.Add(new OptimismChain(OptimismChainId.Mainnet));
        chainInfos.Add(new OptimismChain(OptimismChainId.Goerli));

        chainInfos.Add(new PlatONChain(PlatONChainId.Mainnet));
        chainInfos.Add(new PlatONChain(PlatONChainId.Testnet));
        
        chainInfos.Add(new TronChain(TronChainId.Mainnet));
        chainInfos.Add(new TronChain(TronChainId.Shasta));
        chainInfos.Add(new TronChain(TronChainId.Nile));
        
        chainInfos.Add(new OKCChain(OKCChainId.Mainnet));
        chainInfos.Add(new OKCChain(OKCChainId.Testnet));
        
        chainInfos.Add(new ThunderCoreChain(ThunderCoreChainId.Mainnet));
        chainInfos.Add(new ThunderCoreChain(ThunderCoreChainId.Testnet));
        
        chainInfos.Add(new CronosChain(CronosChainId.Mainnet));
        chainInfos.Add(new CronosChain(CronosChainId.Testnet));
        
        chainInfos.Add(new OasisEmeraldChain(OasisEmeraldChainId.Mainnet));
        chainInfos.Add(new OasisEmeraldChain(OasisEmeraldChainId.Testnet));
        
        chainInfos.Add(new GnosisChain(GnosisChainId.Mainnet));
        chainInfos.Add(new GnosisChain(GnosisChainId.Testnet));
        
        chainInfos.Add(new CeloChain(CeloChainId.Mainnet));
        chainInfos.Add(new CeloChain(CeloChainId.Testnet));
        
        chainInfos.Add(new KlaytnChain(KlaytnChainId.Mainnet));
        chainInfos.Add(new KlaytnChain(KlaytnChainId.Testnet));
        
        chainInfos.Add(new ScrollChain(ScrollChainId.Testnet));

        chainInfos.Add(new ZkSyncChain(ZkSyncChainId.Mainnet));
        chainInfos.Add(new ZkSyncChain(ZkSyncChainId.Testnet));
        
        chainInfos.Add(new MetisChain(MetisChainId.Mainnet));
        chainInfos.Add(new MetisChain(MetisChainId.Testnet));
        
        chainInfos.Add(new ConfluxESpaceChain(ConfluxESpaceChainId.Mainnet));
        chainInfos.Add(new ConfluxESpaceChain(ConfluxESpaceChainId.Testnet));
        
        chainInfos.Add(new MapoChain(MapoChainId.Mainnet));
        chainInfos.Add(new MapoChain(MapoChainId.Testnet));
        
        chainInfos.Add(new PolygonZkEVMChain(PolygonZkEVMChainId.Mainnet));
        chainInfos.Add(new PolygonZkEVMChain(PolygonZkEVMChainId.Testnet));
        
        chainInfos.Add(new BaseChain(BaseChainId.Testnet));
        chainInfos.Add(new LineaChain(LineaChainId.Testnet));
        chainInfos.Add(new ComboChain(ComboChainId.Testnet));
        chainInfos.Add(new MantleChain(MantleChainId.Testnet));
        chainInfos.Add(new ZkMetaChain(ZkMetaChainId.Testnet));
        chainInfos.Add(new OpBNBChain(OpBNBChainId.Testnet));
        chainInfos.Add(new OKBCChain(OKBCChainId.Testnet));
        chainInfos.Add(new TaikoChain(TaikoChainId.Testnet));
        
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
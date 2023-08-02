using System;
using System.Collections.Generic;
using System.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Button = UnityEngine.UI.Button;

public class ChainChoice : SingletonMonoBehaviour<ChainChoice>
{
    [SerializeField] private GameObject chainItemTemplate;
    [SerializeField] private GameObject scrollContent;
    public UnityAction<ChainInfo> unityAction;

    void Start()
    {
        List<ChainInfo> chainInfos = new List<ChainInfo>();
        chainInfos.AddRange(CreateInstancesFromEnum<SolanaChainId, SolanaChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<EthereumChainId, EthereumChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<AvalancheChainId, AvalancheChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<PolygonChainId, PolygonChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<MoonbeamChainId, MoonbeamChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<MoonriverChainId, MoonriverChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<HecoChainId, HecoChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<BSCChainId, BSCChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<FantomChainId, FantomChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ArbitrumChainId, ArbitrumChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<HarmonyChainId, HarmonyChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<AuroraChainId, AuroraChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<KccChainId, KccChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<OptimismChainId, OptimismChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<PlatONChainId, PlatONChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<TronChainId, TronChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<OKCChainId, OKCChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ThunderCoreChainId, ThunderCoreChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<CronosChainId, CronosChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<OasisEmeraldChainId, OasisEmeraldChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<GnosisChainId, GnosisChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<CeloChainId, CeloChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<KlaytnChainId, KlaytnChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ScrollChainId, ScrollChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ZkSyncChainId, ZkSyncChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<MetisChainId, MetisChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ConfluxESpaceChainId, ConfluxESpaceChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<MapoChainId, MapoChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<PolygonZkEVMChainId, PolygonZkEVMChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<BSCChainId, BSCChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<LineaChainId, LineaChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ComboChainId, ComboChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<MantaChainId, MantaChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ZkMetaChainId, ZkMetaChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<OpBNBChainId, OpBNBChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<OKBCChainId, OKBCChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<TaikoChainId, TaikoChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ReadOnChainId, ReadOnChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<ZoraChainId, ZoraChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<PGNChainId, PGNChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<MantaChainId, MantaChain>());
        chainInfos.AddRange(CreateInstancesFromEnum<NebulaChainId, NebulaChain>());
        
        
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
    
    public static IEnumerable<TClass> CreateInstancesFromEnum<TEnum, TClass>() 
        where TEnum : Enum
        where TClass : BaseChainInfo
    {
        var constructorInfo = typeof(TClass).GetConstructor(new[] { typeof(TEnum) });

        if (constructorInfo == null)
        {
            throw new ArgumentException($"Type {typeof(TClass)} does not have a constructor that takes a {typeof(TEnum)} parameter.");
        }

        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(value => (TClass)constructorInfo.Invoke(new object[] { value }));
    }

    public void Show(UnityAction<ChainInfo> unityAction)
    {
        this.unityAction = unityAction;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
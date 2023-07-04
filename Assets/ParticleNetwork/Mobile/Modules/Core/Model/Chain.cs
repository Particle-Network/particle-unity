using System;


namespace Network.Particle.Scripts.Model
{
    public static class ExtMethod
    {
        public static bool IsEvmChain(this ChainInfo chainInfo)
        {
            return chainInfo is EvmBaseChain;
        }
    }

    public enum Env
    {
        DEV = 0,
        STAGING = 1,
        PRODUCTION = 2
    }

    public interface ChainInfo
    {
        public string getChainName();
        public long getChainId();
        public string getChainIdName();

        public bool IsMainnet();
    }

    public abstract class BaseChainInfo : ChainInfo
    {
        protected string chainName;
        protected long chainId;
        protected string chainIdName;

        public string getChainName()
        {
            return chainName;
        }

        public long getChainId()
        {
            return chainId;
        }

        public string getChainIdName()
        {
            return chainIdName;
        }

        public abstract bool IsMainnet();
    }

    public abstract class SolanaBaseChain : BaseChainInfo
    {
    }

    public abstract class EvmBaseChain : BaseChainInfo
    {
    }


    class SolanaChain : SolanaBaseChain
    {
        public SolanaChain(SolanaChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Solana.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)SolanaChainId.Mainnet;
        }
    }

    class EthereumChain : EvmBaseChain
    {
        public EthereumChain(EthereumChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Ethereum.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)EthereumChainId.Mainnet;
        }
    }


    class BSCChain : EvmBaseChain
    {
        public BSCChain(BscChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.BSC.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)BscChainId.Mainnet;
        }
    }

    class PolygonChain : EvmBaseChain
    {
        public PolygonChain(PolygonChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Polygon.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)PolygonChainId.Mainnet;
        }
    }

    class AvalancheChain : EvmBaseChain
    {
        public AvalancheChain(AvalancheChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Avalanche.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)AvalancheChainId.Mainnet;
        }
    }

    class MoonbeamChain : EvmBaseChain
    {
        public MoonbeamChain(MoonbeamChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Moonbeam.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)MoonbeamChainId.Mainnet;
        }
    }

    class MoonriverChain : EvmBaseChain
    {
        public MoonriverChain(MoonriverChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Moonriver.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)MoonriverChainId.Mainnet;
        }
    }

    class HecoChain : EvmBaseChain
    {
        public HecoChain(HecoChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Heco.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)HecoChainId.Mainnet;
        }
    }

    class FantomChain : EvmBaseChain
    {
        public FantomChain(FantomChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Fantom.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)FantomChainId.Mainnet;
        }
    }

    class ArbitrumChain : EvmBaseChain
    {
        public ArbitrumChain(ArbitrumChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Arbitrum.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ArbitrumChainId.One || chainId == (long)ArbitrumChainId.Nova;
        }
    }

    class HarmonyChain : EvmBaseChain
    {
        public HarmonyChain(HarmonyChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Harmony.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)HarmonyChainId.Mainnet;
        }
    }

    class AuroraChain : EvmBaseChain
    {
        public AuroraChain(AuroraChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Aurora.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)AuroraChainId.Mainnet;
        }
    }

    class KccChain : EvmBaseChain
    {
        public KccChain(KccChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.KCC.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)KccChainId.Mainnet;
        }
    }

    class OptimismChain : EvmBaseChain
    {
        public OptimismChain(OptimismChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Optimism.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)OptimismChainId.Mainnet;
        }
    }
    
    class PlatONChain : EvmBaseChain
    {
        public PlatONChain(PlatONChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.PlatON.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)PlatONChainId.Mainnet;
        }
    }
    
    class TronChain : EvmBaseChain
    {
        public TronChain(TronChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Tron.ToString();

        }

        public override bool IsMainnet()
        {
            return chainId == (long)TronChainId.Mainnet;
        }
    }
    
    class OKCChain : EvmBaseChain
    {
        public OKCChain(OKCChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.OKC.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)OKCChainId.Mainnet;
        }
    }
    
    class ThunderCoreChain : EvmBaseChain
    {
        public ThunderCoreChain(ThunderCoreChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.ThunderCore.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ThunderCoreChainId.Mainnet;
        }
    }
    
    class CronosChain : EvmBaseChain
    {
        public CronosChain(CronosChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Cronos.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)CronosChainId.Mainnet;
        }
    }
    
    class OasisEmeraldChain : EvmBaseChain
    {
        public OasisEmeraldChain(OasisEmeraldChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.OasisEmerald.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)OasisEmeraldChainId.Mainnet;
        }
    }
    
    class GnosisChain : EvmBaseChain
    {
        public GnosisChain(GnosisChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Gnosis.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)GnosisChainId.Mainnet;
        }
    }
    
    class CeloChain : EvmBaseChain
    {
        public CeloChain(CeloChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Celo.ToString();
        }
        public override bool IsMainnet()
        {
            return chainId == (long)CeloChainId.Mainnet;
        }
    }
    
    class KlaytnChain : EvmBaseChain
    {
        public KlaytnChain(KlaytnChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Klaytn.ToString();
        }
        public override bool IsMainnet()
        {
            return chainId == (long)KlaytnChainId.Mainnet;
        }
    }
    
    class ScrollChain : EvmBaseChain
    {
        public ScrollChain(ScrollChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Scroll.ToString();
        }

        public override bool IsMainnet()
        {
            // scroll doesn't has a mainnet.
            return false;
        }
    }
    
    class ZkSyncChain : EvmBaseChain
    {
        public ZkSyncChain(ZkSyncChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.ZkSync.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ZkSyncChainId.Mainnet;
        }
    }
    
    class MetisChain : EvmBaseChain
    {
        public MetisChain(MetisChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Metis.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)MetisChainId.Mainnet;
        }
    }
    
    class ConfluxESpaceChain : EvmBaseChain
    {
        public ConfluxESpaceChain(ConfluxESpaceChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.ConfluxESpace.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ConfluxESpaceChainId.Mainnet;
        }
    }
    class MapoChain : EvmBaseChain
    {
        public MapoChain(MapoChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Mapo.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)MapoChainId.Mainnet;
        }
    }
    
    class PolygonZkEVMChain : EvmBaseChain
    {
        public PolygonZkEVMChain(PolygonZkEVMChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.PolygonZkEVM.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)PolygonZkEVMChainId.Mainnet;
        }
    }
    
    class BaseChain : EvmBaseChain
    {
        public BaseChain(BaseChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Base.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class LineaChain : EvmBaseChain
    {
        public LineaChain(LineaChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Linea.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class ComboChain : EvmBaseChain
    {
        public ComboChain(ComboChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Combo.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class MantleChain : EvmBaseChain
    {
        public MantleChain(MantleChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Mantle.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class ZkMetaChain : EvmBaseChain
    {
        public ZkMetaChain(ZkMetaChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.ZkMeta.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class OpBNBChain : EvmBaseChain
    {
        public OpBNBChain(OpBNBChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.OpBNB.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class OKBCChain : EvmBaseChain
    {
        public OKBCChain(OKBCChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.OKBC.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    class TaikoChain : EvmBaseChain
    {
        public TaikoChain(TaikoChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Taiko.ToString();
        }

        public override bool IsMainnet()
        {
            return false;
            // return chainId == (long)BaseChainId.Mainnet;
        }
    }
    
    


    public enum ChainName
    {
        Solana,
        Ethereum, 
        Avalanche, 
        Polygon,
        Moonbeam, 
        Moonriver, 
        Heco, 
        BSC,
        Fantom,
        Arbitrum,
        Harmony,
        Aurora,
        KCC,
        Optimism,
        PlatON,
        Tron,
        OKC,
        ThunderCore,
        Cronos,
        OasisEmerald,
        Gnosis,
        Celo,
        Klaytn,
        Scroll,
        ZkSync,
        Metis,
        ConfluxESpace,
        Mapo,
        PolygonZkEVM,
        Base,
        Linea, 
        Combo, 
        Mantle, 
        ZkMeta, 
        OpBNB, 
        OKBC,
        Taiko
    }
}
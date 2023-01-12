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

        public SolanaChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(SolanaChainId), chainId + "").ToString();
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

        public EthereumChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(EthereumChainId), chainId + "").ToString();
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

        public BSCChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(BscChainId), chainId + "").ToString();
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

        public PolygonChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(PolygonChainId), chainId + "").ToString();
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

        public AvalancheChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(AvalancheChainId), chainId + "").ToString();
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

        public MoonbeamChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(AvalancheChainId), chainId + "").ToString();
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

        public MoonriverChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(MoonriverChainId), chainId + "").ToString();
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

        public HecoChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(HecoChain), chainId + "").ToString();
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

        public FantomChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(FantomChainId), chainId + "").ToString();
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

        public ArbitrumChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(ArbitrumChainId), chainId + "").ToString();
            chainName = ChainName.Arbitrum.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ArbitrumChainId.Mainnet;
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

        public HarmonyChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(HarmonyChainId), chainId + "").ToString();
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

        public AuroraChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(AuroraChainId), chainId + "").ToString();
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

        public KccChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(KccChainId), chainId + "").ToString();
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

        public OptimismChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(OptimismChainId), chainId + "").ToString();
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

        public PlatONChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(PlatONChainId), chainId + "").ToString();
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

        public TronChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(TronChainId), chainId + "").ToString();
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

        public OKCChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(OKCChainId), chainId + "").ToString();
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

        public ThunderCoreChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(ThunderCoreChainId), chainId + "").ToString();
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

        public CronosChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(CronosChainId), chainId + "").ToString();
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

        public OasisEmeraldChain(long chainId)
        {
            this.chainId = chainId;
            chainIdName = Enum.Parse(typeof(OasisEmeraldChainId), chainId + "").ToString();
            chainName = ChainName.OasisEmerald.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)OasisEmeraldChainId.Mainnet;
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
        OasisEmerald
    }
}
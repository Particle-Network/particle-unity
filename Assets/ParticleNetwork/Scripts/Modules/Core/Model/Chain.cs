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
        public int getChainId();
        public string getChainIdName();

        public bool IsMainnet();
    }

    public abstract class BaseChainInfo : ChainInfo
    {
        protected string chainName;
        protected int chainId;
        protected string chainIdName;

        public string getChainName()
        {
            return chainName;
        }

        public int getChainId()
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
        public SolanaChain(SolanaChainId solanaChainId)
        {
            chainId = (int)solanaChainId;
            chainIdName = solanaChainId.ToString();
            chainName = ChainName.Solana.ToString();
        }

        public SolanaChain(int chainIdInt)
        {
            var solanaChainId = Enum.Parse(typeof(SolanaChainId), chainIdInt + "");
            chainId = (int)solanaChainId;
            chainIdName = solanaChainId.ToString();
            chainName = ChainName.Solana.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)SolanaChainId.Mainnet;
        }
    }

    class EthereumChain : EvmBaseChain
    {
        public EthereumChain(EthereumChainId ethereumChainId)
        {
            chainId = (int)ethereumChainId;
            chainIdName = ethereumChainId.ToString();
            chainName = ChainName.Ethereum.ToString();
        }

        public EthereumChain(int chainIdInt)
        {
            var ethereumChainId = Enum.Parse(typeof(EthereumChainId), chainIdInt + "");
            chainId = (int)ethereumChainId;
            chainIdName = ethereumChainId.ToString();
            chainName = ChainName.Ethereum.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)EthereumChainId.Mainnet;
        }
    }


    class BSCChain : EvmBaseChain
    {
        public BSCChain(BscChainId bscChainId)
        {
            chainId = (int)bscChainId;
            chainIdName = bscChainId.ToString();
            chainName = ChainName.BSC.ToString();
        }

        public BSCChain(int chainIdInt)
        {
            var bscChainId = Enum.Parse(typeof(BscChainId), chainIdInt + "");
            chainId = (int)bscChainId;
            chainIdName = bscChainId.ToString();
            chainName = ChainName.BSC.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)BscChainId.Mainnet;
        }
    }

    class PolygonChain : EvmBaseChain
    {
        public PolygonChain(PolygonChainId polygonChainId)
        {
            chainId = (int)polygonChainId;
            chainIdName = polygonChainId.ToString();
            chainName = ChainName.Polygon.ToString();
        }

        public PolygonChain(int chainIdInt)
        {
            var polygonChainId = Enum.Parse(typeof(PolygonChainId), chainIdInt + "");
            chainId = (int)polygonChainId;
            chainIdName = polygonChainId.ToString();
            chainName = ChainName.Polygon.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)PolygonChainId.Mainnet;
        }
    }

    class AvalancheChain : EvmBaseChain
    {
        public AvalancheChain(AvalancheChainId avalancheChainId)
        {
            chainId = (int)avalancheChainId;
            chainIdName = avalancheChainId.ToString();
            chainName = ChainName.Avalanche.ToString();
        }

        public AvalancheChain(int chainIdInt)
        {
            var avalancheChainId = Enum.Parse(typeof(AvalancheChainId), chainIdInt + "");
            chainId = (int)avalancheChainId;
            chainIdName = avalancheChainId.ToString();
            chainName = ChainName.Avalanche.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)AvalancheChainId.Mainnet;
        }
    }

    class MoonbeamChain : EvmBaseChain
    {
        public MoonbeamChain(MoonbeamChainId moonbeamChainId)
        {
            chainId = (int)moonbeamChainId;
            chainIdName = moonbeamChainId.ToString();
            chainName = ChainName.Moonbeam.ToString();
        }

        public MoonbeamChain(int chainIdInt)
        {
            var moonbeamChainId = Enum.Parse(typeof(AvalancheChainId), chainIdInt + "");
            chainId = (int)moonbeamChainId;
            chainIdName = moonbeamChainId.ToString();
            chainName = ChainName.Moonbeam.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)MoonbeamChainId.Mainnet;
        }
    }

    class MoonriverChain : EvmBaseChain
    {
        public MoonriverChain(MoonriverChainId moonriverChainId)
        {
            chainId = (int)moonriverChainId;
            chainIdName = moonriverChainId.ToString();
            chainName = ChainName.Moonriver.ToString();
        }

        public MoonriverChain(int chainIdInt)
        {
            var moonriverChainId = Enum.Parse(typeof(MoonriverChainId), chainIdInt + "");
            chainId = (int)moonriverChainId;
            chainIdName = moonriverChainId.ToString();
            chainName = ChainName.Moonriver.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)MoonriverChainId.Mainnet;
        }
    }

    class HecoChain : EvmBaseChain
    {
        public HecoChain(HecoChainId hecoChainId)
        {
            chainId = (int)hecoChainId;
            chainIdName = hecoChainId.ToString();
            chainName = ChainName.Heco.ToString();
        }

        public HecoChain(int chainIdInt)
        {
            var hecoChainId = Enum.Parse(typeof(HecoChain), chainIdInt + "");
            chainId = (int)hecoChainId;
            chainIdName = hecoChainId.ToString();
            chainName = ChainName.Heco.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)HecoChainId.Mainnet;
        }
    }

    class FantomChain : EvmBaseChain
    {
        public FantomChain(FantomChainId fantomChainId)
        {
            chainId = (int)fantomChainId;
            chainIdName = fantomChainId.ToString();
            chainName = ChainName.Fantom.ToString();
        }

        public FantomChain(int chainIdInt)
        {
            var fantomChainId = Enum.Parse(typeof(FantomChainId), chainIdInt + "");
            chainId = (int)fantomChainId;
            chainIdName = fantomChainId.ToString();
            chainName = ChainName.Fantom.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)FantomChainId.Mainnet;
        }
    }

    class ArbitrumChain : EvmBaseChain
    {
        public ArbitrumChain(ArbitrumChainId arbitrumChainId)
        {
            chainId = (int)arbitrumChainId;
            chainIdName = arbitrumChainId.ToString();
            chainName = ChainName.Arbitrum.ToString();
        }

        public ArbitrumChain(int chainIdInt)
        {
            var arbitrumChainId = Enum.Parse(typeof(ArbitrumChainId), chainIdInt + "");
            chainId = (int)arbitrumChainId;
            chainIdName = arbitrumChainId.ToString();
            chainName = ChainName.Arbitrum.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)ArbitrumChainId.Mainnet;
        }
    }

    class HarmonyChain : EvmBaseChain
    {
        public HarmonyChain(HarmonyChainId harmonyChainId)
        {
            chainId = (int)harmonyChainId;
            chainIdName = harmonyChainId.ToString();
            chainName = ChainName.Harmony.ToString();
        }

        public HarmonyChain(int chainIdInt)
        {
            var harmonyChainId = Enum.Parse(typeof(HarmonyChainId), chainIdInt + "");
            chainId = (int)harmonyChainId;
            chainIdName = harmonyChainId.ToString();
            chainName = ChainName.Harmony.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)HarmonyChainId.Mainnet;
        }
    }

    class AuroraChain : EvmBaseChain
    {
        public AuroraChain(AuroraChainId auroraChainId)
        {
            chainId = (int)auroraChainId;
            chainIdName = auroraChainId.ToString();
            chainName = ChainName.Aurora.ToString();
        }

        public AuroraChain(int chainIdInt)
        {
            var auroraChainId = Enum.Parse(typeof(AuroraChainId), chainIdInt + "");
            chainId = (int)auroraChainId;
            chainIdName = auroraChainId.ToString();
            chainName = ChainName.Aurora.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)AuroraChainId.Mainnet;
        }
    }

    class KccChain : EvmBaseChain
    {
        public KccChain(KccChainId kccChainId)
        {
            chainId = (int)kccChainId;
            chainIdName = kccChainId.ToString();
            chainName = ChainName.KCC.ToString();
        }

        public KccChain(int chainIdInt)
        {
            var kccChainId = Enum.Parse(typeof(KccChainId), chainIdInt + "");
            chainId = (int)kccChainId;
            chainIdName = kccChainId.ToString();
            chainName = ChainName.KCC.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)KccChainId.Mainnet;
        }
    }

    class OptimismChain : EvmBaseChain
    {
        public OptimismChain(OptimismChainId optimismChainId)
        {
            chainId = (int)optimismChainId;
            chainIdName = optimismChainId.ToString();
            chainName = ChainName.Optimism.ToString();
        }

        public OptimismChain(int chainIdInt)
        {
            var optimismChainId = Enum.Parse(typeof(OptimismChainId), chainIdInt + "");
            chainId = (int)optimismChainId;
            chainIdName = optimismChainId.ToString();
            chainName = ChainName.Optimism.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)OptimismChainId.Mainnet;
        }
    }
    
    class PlatONChain : EvmBaseChain
    {
        public PlatONChain(PlatONChainId platOnChainId)
        {
            chainId = (int)platOnChainId;
            chainIdName = platOnChainId.ToString();
            chainName = ChainName.PlatON.ToString();
        }

        public PlatONChain(int chainIdInt)
        {
            var platOnChainId = Enum.Parse(typeof(PlatONChainId), chainIdInt + "");
            chainId = (int)platOnChainId;
            chainIdName = platOnChainId.ToString();
            chainName = ChainName.PlatON.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (int)PlatONChainId.Mainnet;
        }
    }


    public enum ChainName
    {
        Solana,
        Ethereum, //1.Ethereum
        Avalanche, //2.Avalanche
        Polygon, //3.Polygon
        Moonbeam, //4.Moonbeam
        Moonriver, //5.Moonriver
        Heco, //6.Heco
        BSC,
        Fantom,
        Arbitrum,
        Harmony,
        Aurora,
        KCC,
        Optimism,
        PlatON
    }
}
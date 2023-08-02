using System;
using System.Reflection;

namespace Network.Particle.Scripts.Model
{
    public static class ExtMethod
    {
        public static bool IsEvmChain(this ChainInfo chainInfo)
        {
            return chainInfo is EvmBaseChain;
        }
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
        public BSCChain(BSCChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.BSC.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)BSCChainId.Mainnet;
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
            return chainId == (long)ScrollChainId.Mainnet;
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
            return chainId == (long)BaseChainId.Mainnet;
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
            return chainId == (long)LineaChainId.Mainnet;
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
            return chainId == (long)ComboChainId.Mainnet;
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
            return chainId == (long)MantleChainId.Mainnet;
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
            return chainId == (long)ZkMetaChainId.Mainnet;
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
            return chainId == (long)OpBNBChainId.Mainnet;
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
            return chainId == (long)OKBCChainId.Mainnet;
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
            return chainId == (long)TaikoChainId.Mainnet;
        }
    }

    class ReadOnChain : EvmBaseChain
    {
        public ReadOnChain(ReadOnChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.ReadOn.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ReadOnChainId.Mainnet;
        }
    }

    class ZoraChain : EvmBaseChain
    {
        public ZoraChain(ZoraChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Zora.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)ZoraChainId.Mainnet;
        }
    }

    class PGNChain : EvmBaseChain
    {
        public PGNChain(PGNChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.PGN.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)PGNChainId.Mainnet;
        }
    }

    class MantaChain : EvmBaseChain
    {
        public MantaChain(MantaChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Manta.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)MantaChainId.Mainnet;
        }
    }

    class NebulaChain : EvmBaseChain
    {
        public NebulaChain(NebulaChainId chainId)
        {
            this.chainId = (long)chainId;
            chainIdName = chainId.ToString();
            chainName = ChainName.Nebula.ToString();
        }

        public override bool IsMainnet()
        {
            return chainId == (long)NebulaChainId.Mainnet;
        }
    }


    public class ChainNameValueAttribute : Attribute
    {
        public string ChainNameValue { get; protected set; }

        public ChainNameValueAttribute(string value)
        {
            this.ChainNameValue = value;
        }
    }


    public enum ChainName
    {
        [ChainNameValue("Solana")] Solana,

        [ChainNameValue("Ethereum")] Ethereum,

        [ChainNameValue("Avalanche")] Avalanche,

        [ChainNameValue("Polygon")] Polygon,

        [ChainNameValue("Moonbeam")] Moonbeam,

        [ChainNameValue("Moonriver")] Moonriver,

        [ChainNameValue("Heco")] Heco,

        [ChainNameValue("BSC")] BSC,

        [ChainNameValue("Fantom")] Fantom,

        [ChainNameValue("Arbitrum")] Arbitrum,

        [ChainNameValue("Harmony")] Harmony,

        [ChainNameValue("Aurora")] Aurora,

        [ChainNameValue("KCC")] KCC,

        [ChainNameValue("Optimism")] Optimism,

        [ChainNameValue("PlatON")] PlatON,

        [ChainNameValue("Tron")] Tron,

        [ChainNameValue("OKC")] OKC,

        [ChainNameValue("ThunderCore")] ThunderCore,

        [ChainNameValue("Cronos")] Cronos,

        [ChainNameValue("OasisEmerald")] OasisEmerald,

        [ChainNameValue("Gnosis")] Gnosis,

        [ChainNameValue("Celo")] Celo,

        [ChainNameValue("Klaytn")] Klaytn,

        [ChainNameValue("Scroll")] Scroll,

        [ChainNameValue("ZkSync")] ZkSync,

        [ChainNameValue("Metis")] Metis,

        [ChainNameValue("ConfluxESpace")] ConfluxESpace,

        [ChainNameValue("Mapo")] Mapo,

        [ChainNameValue("PolygonZkEVM")] PolygonZkEVM,

        [ChainNameValue("Base")] Base,

        [ChainNameValue("Linea")] Linea,

        [ChainNameValue("Combo")] Combo,

        [ChainNameValue("Mantle")] Mantle,

        [ChainNameValue("ZkMeta")] ZkMeta,

        [ChainNameValue("OpBNB")] OpBNB,

        [ChainNameValue("OKBC")] OKBC,

        [ChainNameValue("Taiko")] Taiko,

        [ChainNameValue("ReadOn")] ReadOn,

        [ChainNameValue("Zora")] Zora,

        [ChainNameValue("PGN")] PGN,

        [ChainNameValue("Manta")] Manta,

        [ChainNameValue("Nebula")] Nebula
    }

    public static class ChainNameExtensions
    {
        public static string GetStringValue(this ChainName chain)
        {
            FieldInfo field = chain.GetType().GetField(chain.ToString());

            ChainNameValueAttribute[] attrs =
                field.GetCustomAttributes(typeof(ChainNameValueAttribute), false) as ChainNameValueAttribute[];

            return attrs.Length > 0 ? attrs[0].ChainNameValue : null;
        }

        public static ChainName GetChainName(this string stringValue)
        {
            foreach (ChainName chainName in Enum.GetValues(typeof(ChainName)))
            {
                if (chainName.GetStringValue().Equals(stringValue, StringComparison.OrdinalIgnoreCase))
                {
                    return chainName;
                }
            }

            throw new ArgumentException("Invalid string value for conversion.");
        }
    }
}
using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Core.Utils
{
    public class ChainUtils
    {
        public static ChainInfo FindChain(string chainNameString, long chainId)
        {
            var chainName = chainNameString.GetChainName();

            ChainInfo chainInfo = new EthereumChain(EthereumChainId.Mainnet);
            if (chainName == ChainName.Solana)
            {
                chainInfo = new SolanaChain((SolanaChainId)chainId);
            }
            else if (chainName == ChainName.Ethereum)
            {
                chainInfo = new EthereumChain((EthereumChainId)chainId);
            }
            else if (chainName == ChainName.BSC)
            {
                chainInfo = new BSCChain((BSCChainId)chainId);
            }
            else if (chainName == ChainName.Polygon)
            {
                chainInfo = new PolygonChain((PolygonChainId)chainId);
            }
            else if (chainName == ChainName.Avalanche)
            {
                chainInfo = new AvalancheChain((AvalancheChainId)chainId);
            }
            else if (chainName == ChainName.Fantom)
            {
                chainInfo = new FantomChain((FantomChainId)chainId);
            }
            else if (chainName == ChainName.Arbitrum)
            {
                chainInfo = new ArbitrumChain((ArbitrumChainId)chainId);
            }

            else if (chainName == ChainName.Moonbeam)
            {
                chainInfo = new MoonbeamChain((MoonbeamChainId)chainId);
            }
            else if (chainName == ChainName.Moonriver)
            {
                chainInfo = new MoonriverChain((MoonriverChainId)chainId);
            }
            else if (chainName == ChainName.Heco)
            {
                chainInfo = new HecoChain((HecoChainId)chainId);
            }
            else if (chainName == ChainName.Aurora)
            {
                chainInfo = new AuroraChain((AuroraChainId)chainId);
            }
            else if (chainName == ChainName.Harmony)
            {
                chainInfo = new HarmonyChain((HarmonyChainId)chainId);
            }
            else if (chainName == ChainName.KCC)
            {
                chainInfo = new KccChain((KccChainId)chainId);
            }
            else if (chainName == ChainName.Optimism)
            {
                chainInfo = new OptimismChain((OptimismChainId)chainId);
            }
            else if (chainName == ChainName.PlatON)
            {
                chainInfo = new PlatONChain((PlatONChainId)chainId);
            }
            else if (chainName == ChainName.Tron)
            {
                chainInfo = new TronChain((TronChainId)chainId);
            }
            else if (chainName == ChainName.OKC)
            {
                chainInfo = new OKCChain((OKCChainId)chainId);
            }
            else if (chainName == ChainName.ThunderCore)
            {
                chainInfo = new ThunderCoreChain((ThunderCoreChainId)chainId);
            }
            else if (chainName == ChainName.Cronos)
            {
                chainInfo = new CronosChain((CronosChainId)chainId);
            }
            else if (chainName == ChainName.OasisEmerald)
            {
                chainInfo = new OasisEmeraldChain((OasisEmeraldChainId)chainId);
            }
            else if (chainName == ChainName.Gnosis)
            {
                chainInfo = new GnosisChain((GnosisChainId)chainId);
            }
            else if (chainName == ChainName.Celo)
            {
                chainInfo = new CeloChain((CeloChainId)chainId);
            }
            else if (chainName == ChainName.Klaytn)
            {
                chainInfo = new KlaytnChain((KlaytnChainId)chainId);
            }
            else if (chainName == ChainName.Scroll)
            {
                chainInfo = new ScrollChain((ScrollChainId)chainId);
            }
            else if (chainName == ChainName.ZkSync)
            {
                chainInfo = new ZkSyncChain((ZkSyncChainId)chainId);
            }
            else if (chainName == ChainName.Metis)
            {
                chainInfo = new MetisChain((MetisChainId)chainId);
            }
            else if (chainName == ChainName.ConfluxESpace)
            {
                chainInfo = new ConfluxESpaceChain((ConfluxESpaceChainId)chainId);
            }
            else if (chainName == ChainName.Mapo)
            {
                chainInfo = new MapoChain((MapoChainId)chainId);
            }
            else if (chainName == ChainName.PolygonZkEVM)
            {
                chainInfo = new PolygonZkEVMChain((PolygonZkEVMChainId)chainId);
            }
            else if (chainName == ChainName.Base)
            {
                chainInfo = new BaseChain((BaseChainId)chainId);
            }
            else if (chainName == ChainName.Linea)
            {
                chainInfo = new LineaChain((LineaChainId)chainId);
            }
            else if (chainName == ChainName.Combo)
            {
                chainInfo = new ComboChain((ComboChainId)chainId);
            }
            else if (chainName == ChainName.Mantle)
            {
                chainInfo = new MantleChain((MantleChainId)chainId);
            }
            else if (chainName == ChainName.ZkMeta)
            {
                chainInfo = new ZkMetaChain((ZkMetaChainId)chainId);
            }
            else if (chainName == ChainName.OpBNB)
            {
                chainInfo = new OpBNBChain((OpBNBChainId)chainId);
            }
            else if (chainName == ChainName.OKBC)
            {
                chainInfo = new OKBCChain((OKBCChainId)chainId);
            }
            else if (chainName == ChainName.Taiko)
            {
                chainInfo = new TaikoChain((TaikoChainId)chainId);
            }
            else if (chainName == ChainName.ReadOn)
            {
                chainInfo = new ReadOnChain((ReadOnChainId)chainId);
            }
            else if (chainName == ChainName.Zora)
            {
                chainInfo = new ZoraChain((ZoraChainId)chainId);
            }
            else if (chainName == ChainName.PGN)
            {
                chainInfo = new PGNChain((PGNChainId)chainId);
            }
            else if (chainName == ChainName.Manta)
            {
                chainInfo = new MantaChain((MantaChainId)chainId);
            }
            else if (chainName == ChainName.Nebula)
            {
                chainInfo = new NebulaChain((NebulaChainId)chainId);
            }


            return chainInfo;
        }
    }
}
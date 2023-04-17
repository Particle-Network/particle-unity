using System;
using Network.Particle.Scripts.Model;

namespace Network.Particle.Scripts.Core.Utils
{
    public class ChainUtils
    {
        public static ChainInfo FindChain(string chainName, long chainId)
        {
            string name = chainName.ToLower();
            ChainInfo chainInfo = new EthereumChain(EthereumChainId.Mainnet);
            if (name == "solana")
            {
                if (chainId == 101)
                {
                    chainInfo = new SolanaChain(SolanaChainId.Mainnet);
                }
                else if (chainId == 102)
                {
                    chainInfo = new SolanaChain(SolanaChainId.Testnet);
                }
                else if (chainId == 103)
                {
                    chainInfo = new SolanaChain(SolanaChainId.Devnet);
                }
            }
            else if (name == "ethereum")
            {
                if (chainId == 1)
                {
                    chainInfo = new EthereumChain(EthereumChainId.Mainnet);
                }
                else if (chainId == 5)
                {
                    chainInfo = new EthereumChain(EthereumChainId.Goerli);
                }
            }
            else if (name == "bsc")
            {
                if (chainId == 56)
                {
                    chainInfo = new BSCChain(BscChainId.Mainnet);
                }
                else if (chainId == 97)
                {
                    chainInfo = new BSCChain(BscChainId.Testnet);
                }
            }
            else if (name == "polygon")
            {
                if (chainId == 137)
                {
                    chainInfo = new PolygonChain(PolygonChainId.Mainnet);
                }
                else if (chainId == 80001)
                {
                    chainInfo = new PolygonChain(PolygonChainId.Mumbai);
                }
            }
            else if (name == "avalanche")
            {
                if (chainId == 43114)
                {
                    chainInfo = new AvalancheChain(AvalancheChainId.Mainnet);
                }
                else if (chainId == 43113)
                {
                    chainInfo = new AvalancheChain(AvalancheChainId.Testnet);
                }
            }
            else if (name == "fantom")
            {
                if (chainId == 250)
                {
                    chainInfo = new FantomChain(FantomChainId.Mainnet);
                }
                else if (chainId == 4002)
                {
                    chainInfo = new FantomChain(FantomChainId.Testnet);
                }
            }
            else if (name == "arbitrum")
            {
                if (chainId == 42161)
                {
                    chainInfo = new ArbitrumChain(ArbitrumChainId.One);
                }
                else if (chainId == 42170)
                {
                    chainInfo = new ArbitrumChain(ArbitrumChainId.Nova);
                }
                else if (chainId == 421613)
                {
                    chainInfo = new ArbitrumChain(ArbitrumChainId.Goerli);
                }
            }
            else if (name == "moonbeam")
            {
                if (chainId == 1284)
                {
                    chainInfo = new MoonbeamChain(MoonbeamChainId.Mainnet);
                }
                else if (chainId == 1287)
                {
                    chainInfo = new MoonbeamChain(MoonbeamChainId.Testnet);
                }
            }
            else if (name == "moonriver")
            {
                if (chainId == 1285)
                {
                    chainInfo = new MoonriverChain(MoonriverChainId.Mainnet);
                }
                else if (chainId == 1287)
                {
                    chainInfo = new MoonriverChain(MoonriverChainId.Testnet);
                }
            }
            else if (name == "heco")
            {
                if (chainId == 128)
                {
                    chainInfo = new HecoChain(HecoChainId.Mainnet);
                }
                else if (chainId == 256)
                {
                    chainInfo = new HecoChain(HecoChainId.Testnet);
                }
            }
            else if (name == "aurora")
            {
                if (chainId == 1313161554)
                {
                    chainInfo = new AuroraChain(AuroraChainId.Mainnet);
                }
                else if (chainId == 1313161555)
                {
                    chainInfo = new AuroraChain(AuroraChainId.Testnet);
                }
            }
            else if (name == "harmony")
            {
                if (chainId == 1666600000)
                {
                    chainInfo = new HarmonyChain(HarmonyChainId.Mainnet);
                }
                else if (chainId == 1666700000)
                {
                    chainInfo = new HarmonyChain(HarmonyChainId.Testnet);
                }
            }
            else if (name == "kcc")
            {
                if (chainId == 321)
                {
                    chainInfo = new KccChain(KccChainId.Mainnet);
                }
                else if (chainId == 322)
                {
                    chainInfo = new KccChain(KccChainId.Testnet);
                }
            }
            else if (name == "optimism")
            {
                if (chainId == 10)
                {
                    chainInfo = new OptimismChain(OptimismChainId.Mainnet);
                }
                else if (chainId == 420)
                {
                    chainInfo = new OptimismChain(OptimismChainId.Goerli);
                }
            }
            else if (name == "platon")
            {
                if (chainId == 210425)
                {
                    chainInfo = new PlatONChain(PlatONChainId.Mainnet);
                }
                else if (chainId == 2203181)
                {
                    chainInfo = new PlatONChain(PlatONChainId.Testnet);
                }
            }
            else if (name == "tron")
            {
                if (chainId == 728126428)
                {
                    chainInfo = new TronChain(TronChainId.Mainnet);
                }
                else if (chainId == 2494104990)
                {
                    chainInfo = new TronChain(TronChainId.Shasta);
                }
                else if (chainId == 3448148188)
                {
                    chainInfo = new TronChain(TronChainId.Nile);
                }
            }
            else if (name == "okc")
            {
                if (chainId == 66)
                {
                    chainInfo = new OKCChain(OKCChainId.Mainnet);
                }
                else if (chainId == 65)
                {
                    chainInfo = new OKCChain(OKCChainId.Mainnet);
                }
            }
            else if (name == "thundercore")
            {
                if (chainId == 108)
                {
                    chainInfo = new ThunderCoreChain(ThunderCoreChainId.Mainnet);
                }
                else if (chainId == 18)
                {
                    chainInfo = new ThunderCoreChain(ThunderCoreChainId.Testnet);
                }
            }
            else if (name == "cronos")
            {
                if (chainId == 25)
                {
                    chainInfo = new CronosChain(CronosChainId.Mainnet);
                }
                else if (chainId == 338)
                {
                    chainInfo = new CronosChain(CronosChainId.Testnet);
                }
            }
            else if (name == "oasisemerald")
            {
                if (chainId == 42262)
                {
                    chainInfo = new OasisEmeraldChain(OasisEmeraldChainId.Mainnet);
                }
                else if (chainId == 42261)
                {
                    chainInfo = new OasisEmeraldChain(OasisEmeraldChainId.Testnet);
                }
            }
            else if (name == "gnosis")
            {
                if (chainId == 100)
                {
                    chainInfo = new GnosisChain(GnosisChainId.Mainnet);
                }
                else if (chainId == 10200)
                {
                    chainInfo = new GnosisChain(GnosisChainId.Testnet);
                }
            }
            else if (name == "celo")
            {
                if (chainId == 42220)
                {
                    chainInfo = new CeloChain(CeloChainId.Mainnet);
                }
                else if (chainId == 44787)
                {
                    chainInfo = new CeloChain(CeloChainId.Testnet);
                }
            }
            else if (name == "klaytn")
            {
                if (chainId == 8217)
                {
                    chainInfo = new KlaytnChain(KlaytnChainId.Mainnet);
                }
                else if (chainId == 1001)
                {
                    chainInfo = new KlaytnChain(KlaytnChainId.Testnet);
                }
            }
            else if (name == "scroll")
            {
                if (chainId == 534353)
                {
                    chainInfo = new ScrollChain(ScrollChainId.Testnet);
                }
            }
            else if (name == "zksync")
            {
                if (chainId == 324)
                {
                    chainInfo = new ZkSyncChain(ZkSyncChainId.Mainnet);
                }
                else if (chainId == 280)
                {
                    chainInfo = new ZkSyncChain(ZkSyncChainId.Mainnet);
                }
            }
            else if (name == "metis")
            {
                if (chainId == 1088)
                {
                    chainInfo = new MetisChain(MetisChainId.Mainnet);
                }
                else if (chainId == 599)
                {
                    chainInfo = new MetisChain(MetisChainId.Testnet);
                }
            }
            else if (name == "confluxespace")
            {
                if (chainId == 1030)
                {
                    chainInfo = new ConfluxESpaceChain(ConfluxESpaceChainId.Mainnet);
                }
                else if (chainId == 71)
                {
                    chainInfo = new ConfluxESpaceChain(ConfluxESpaceChainId.Testnet);
                }
            }
            else if (name == "mapo")
            {
                if (chainId == 22776)
                {
                    chainInfo = new MapoChain(MapoChainId.Mainnet);
                }
                else if (chainId == 212)
                {
                    chainInfo = new MapoChain(MapoChainId.Testnet);
                }
            } 
            else if (name == "polygonzkevm")
            {
                if (chainId == 1101)
                {
                    chainInfo = new PolygonZkEVMChain(PolygonZkEVMChainId.Mainnet);
                }
                else if (chainId == 1442)
                {
                    chainInfo = new PolygonZkEVMChain(PolygonZkEVMChainId.Testnet);
                }
            }
            else if (name == "base")
            {
                if (chainId == 84531)
                {
                    chainInfo = new BaseChain(BaseChainId.Testnet);
                }
            }
            

            return chainInfo;
        }
    }
}
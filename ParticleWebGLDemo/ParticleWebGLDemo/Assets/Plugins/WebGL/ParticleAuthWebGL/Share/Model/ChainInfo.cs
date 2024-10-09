using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    [Serializable]
    public class ChainInfo
    {
        [JsonProperty("id")] private long id;

        [JsonProperty("name")] private string name;

        [JsonProperty("chainType")] private string chainType;

        [JsonProperty("icon")] private string icon;

        [JsonProperty("fullname")] private string fullname;

        [JsonProperty("network")] private string network;

        [JsonProperty("website")] private string website;

        [JsonProperty("nativeCurrency")] private NativeCurrency nativeCurrency;

        [JsonProperty("rpcUrl")] private string rpcUrl;

        [JsonProperty("blockExplorerUrl")] private string blockExplorerUrl;

        [JsonProperty("features")] private List<Feature> features;

        [JsonProperty("faucetUrl")] [CanBeNull]
        private string faucetUrl;

        [JsonIgnore] public long Id => id;
        [JsonIgnore] public string Name => name;
        [JsonIgnore] public string ChainType => chainType;
        [JsonIgnore] public string Icon => icon;
        [JsonIgnore] public string Fullname => fullname;
        [JsonIgnore] public string Network => network;
        [JsonIgnore] public string Website => website;
        [JsonIgnore] public NativeCurrency NativeCurrency => nativeCurrency;
        [JsonIgnore] public string RpcUrl => rpcUrl;
        [JsonIgnore] public string BlockExplorerUrl => blockExplorerUrl;
        [JsonIgnore] public List<Feature> Features => features;
        [JsonIgnore] [CanBeNull] public string FaucetUrl => faucetUrl;

        public ChainInfo(long id, string name, string chainType, string icon, string fullname, string network,
            string website, NativeCurrency nativeCurrency, string rpcUrl, string blockExplorerUrl,
            List<Feature> features,
            [CanBeNull] string faucetUrl)
        {
            this.id = id;
            this.name = name;
            this.chainType = chainType;
            this.icon = icon;
            this.fullname = fullname;
            this.network = network;
            this.website = website;
            this.nativeCurrency = nativeCurrency;
            this.rpcUrl = rpcUrl;
            this.blockExplorerUrl = blockExplorerUrl;
            this.features = features;
            this.faucetUrl = faucetUrl;
        }

        // template code start
        public static ChainInfo Ethereum => _ethereum;

        private static ChainInfo _ethereum = new ChainInfo(
            1,
            "Ethereum",
            "evm",
            "https://static.particle.network/token-list/ethereum/native.png",
            "Ethereum Mainnet",
            "Mainnet",
            "https://ethereum.org",
            new NativeCurrency("Ether", "ETH", 18),
            "https://ethereum.publicnode.com",
            "https://etherscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Optimism => _optimism;

        private static ChainInfo _optimism = new ChainInfo(
            10,
            "Optimism",
            "evm",
            "https://static.particle.network/token-list/optimism/native.png",
            "Optimism Mainnet",
            "Mainnet",
            "https://optimism.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://mainnet.optimism.io",
            "https://optimistic.etherscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ThunderCoreTestnet => _thundercoretestnet;

        private static ChainInfo _thundercoretestnet = new ChainInfo(
            18,
            "ThunderCore",
            "evm",
            "https://static.particle.network/token-list/thundercore/native.png",
            "ThunderCore Testnet",
            "Testnet",
            "https://thundercore.com",
            new NativeCurrency("ThunderCore Token", "TT", 18),
            "https://testnet-rpc.thundercore.com",
            "https://explorer-testnet.thundercore.com",
            null,
            "https://faucet-testnet.thundercore.com"
        );


        public static ChainInfo Elastos => _elastos;

        private static ChainInfo _elastos = new ChainInfo(
            20,
            "Elastos",
            "evm",
            "https://static.particle.network/token-list/elastos/native.png",
            "Elastos Mainnet",
            "Mainnet",
            "https://elastos.org",
            new NativeCurrency("ELA", "ELA", 18),
            "https://api.elastos.io/esc",
            "https://esc.elastos.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Cronos => _cronos;

        private static ChainInfo _cronos = new ChainInfo(
            25,
            "Cronos",
            "evm",
            "https://static.particle.network/token-list/cronos/native.png",
            "Cronos Mainnet",
            "Mainnet",
            "https://cronos.org",
            new NativeCurrency("Cronos", "CRO", 18),
            "https://evm.cronos.org",
            "https://cronoscan.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BNBChain => _bnbchain;

        private static ChainInfo _bnbchain = new ChainInfo(
            56,
            "BSC",
            "evm",
            "https://static.particle.network/token-list/bsc/native.png",
            "BNB Chain",
            "Mainnet",
            "https://www.bnbchain.org/en",
            new NativeCurrency("BNB", "BNB", 18),
            "https://bsc-dataseed1.binance.org",
            "https://bscscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo OKTCTestnet => _oktctestnet;

        private static ChainInfo _oktctestnet = new ChainInfo(
            65,
            "OKC",
            "evm",
            "https://static.particle.network/token-list/okc/native.png",
            "OKTC Testnet",
            "Testnet",
            "https://www.okex.com/okexchain",
            new NativeCurrency("OKT", "OKT", 18),
            "https://exchaintestrpc.okex.org",
            "https://www.oklink.com/okc-test",
            null,
            "https://docs.oxdex.com/v/en/help/gitter"
        );


        public static ChainInfo OKTC => _oktc;

        private static ChainInfo _oktc = new ChainInfo(
            66,
            "OKC",
            "evm",
            "https://static.particle.network/token-list/okc/native.png",
            "OKTC Mainnet",
            "Mainnet",
            "https://www.okex.com/okc",
            new NativeCurrency("OKT", "OKT", 18),
            "https://exchainrpc.okex.org",
            "https://www.oklink.com/okc",
            new List<Feature>() { new Feature("SWAP") },
            null
        );


        public static ChainInfo ConfluxeSpaceTestnet => _confluxespacetestnet;

        private static ChainInfo _confluxespacetestnet = new ChainInfo(
            71,
            "ConfluxESpace",
            "evm",
            "https://static.particle.network/token-list/confluxespace/native.png",
            "Conflux eSpace Testnet",
            "Testnet",
            "https://confluxnetwork.org",
            new NativeCurrency("CFX", "CFX", 18),
            "https://evmtestnet.confluxrpc.com",
            "https://evmtestnet.confluxscan.net",
            null,
            "https://efaucet.confluxnetwork.org"
        );


        public static ChainInfo Viction => _viction;

        private static ChainInfo _viction = new ChainInfo(
            88,
            "Viction",
            "evm",
            "https://static.particle.network/token-list/viction/native.png",
            "Viction Mainnet",
            "Mainnet",
            "https://tomochain.com",
            new NativeCurrency("Viction", "VIC", 18),
            "https://rpc.viction.xyz",
            "https://vicscan.xyz",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo VictionTestnet => _victiontestnet;

        private static ChainInfo _victiontestnet = new ChainInfo(
            89,
            "Viction",
            "evm",
            "https://static.particle.network/token-list/viction/native.png",
            "Viction Testnet",
            "Testnet",
            "https://tomochain.com",
            new NativeCurrency("Viction", "VIC", 18),
            "https://rpc-testnet.viction.xyz",
            "https://testnet.vicscan.xyz",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo BNBChainTestnet => _bnbchaintestnet;

        private static ChainInfo _bnbchaintestnet = new ChainInfo(
            97,
            "BSC",
            "evm",
            "https://static.particle.network/token-list/bsc/native.png",
            "BNB Chain Testnet",
            "Testnet",
            "https://www.bnbchain.org/en",
            new NativeCurrency("BNB", "BNB", 18),
            "https://data-seed-prebsc-1-s1.binance.org:8545",
            "https://testnet.bscscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            "https://testnet.bnbchain.org/faucet-smart"
        );


        public static ChainInfo Gnosis => _gnosis;

        private static ChainInfo _gnosis = new ChainInfo(
            100,
            "Gnosis",
            "evm",
            "https://static.particle.network/token-list/gnosis/native.png",
            "Gnosis Mainnet",
            "Mainnet",
            "https://docs.gnosischain.com",
            new NativeCurrency("Gnosis", "XDAI", 18),
            "https://rpc.ankr.com/gnosis",
            "https://gnosisscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Solana => _solana;

        private static ChainInfo _solana = new ChainInfo(
            101,
            "Solana",
            "solana",
            "https://static.particle.network/token-list/solana/native.png",
            "Solana Mainnet",
            "Mainnet",
            "https://solana.com",
            new NativeCurrency("SOL", "SOL", 9),
            "https://api.mainnet-beta.solana.com",
            "https://solscan.io",
            new List<Feature>() { new Feature("SWAP") },
            null
        );


        public static ChainInfo SolanaTestnet => _solanatestnet;

        private static ChainInfo _solanatestnet = new ChainInfo(
            102,
            "Solana",
            "solana",
            "https://static.particle.network/token-list/solana/native.png",
            "Solana Testnet",
            "Testnet",
            "https://solana.com",
            new NativeCurrency("SOL", "SOL", 9),
            "https://api.testnet.solana.com",
            "https://solscan.io",
            null,
            "https://solfaucet.com"
        );


        public static ChainInfo SolanaDevnet => _solanadevnet;

        private static ChainInfo _solanadevnet = new ChainInfo(
            103,
            "Solana",
            "solana",
            "https://static.particle.network/token-list/solana/native.png",
            "Solana Devnet",
            "Devnet",
            "https://solana.com",
            new NativeCurrency("SOL", "SOL", 9),
            "https://api.devnet.solana.com",
            "https://solscan.io",
            null,
            "https://solfaucet.com"
        );


        public static ChainInfo ThunderCore => _thundercore;

        private static ChainInfo _thundercore = new ChainInfo(
            108,
            "ThunderCore",
            "evm",
            "https://static.particle.network/token-list/thundercore/native.png",
            "ThunderCore Mainnet",
            "Mainnet",
            "https://thundercore.com",
            new NativeCurrency("ThunderCore Token", "TT", 18),
            "https://mainnet-rpc.thundercore.com",
            "https://viewblock.io/thundercore",
            null,
            null
        );


        public static ChainInfo BOBTestnet => _bobtestnet;

        private static ChainInfo _bobtestnet = new ChainInfo(
            111,
            "BOB",
            "evm",
            "https://static.particle.network/token-list/bob/native.png",
            "BOB Testnet",
            "Testnet",
            "https://www.gobob.xyz",
            new NativeCurrency("ETH", "ETH", 18),
            "https://testnet.rpc.gobob.xyz",
            "https://testnet-explorer.gobob.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Fuse => _fuse;

        private static ChainInfo _fuse = new ChainInfo(
            122,
            "fuse",
            "evm",
            "https://static.particle.network/token-list/fuse/native.png",
            "Fuse Mainnet",
            "Mainnet",
            "https://www.fuse.io",
            new NativeCurrency("FUSE", "FUSE", 18),
            "https://rpc.fuse.io",
            "https://explorer.fuse.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo FuseTestnet => _fusetestnet;

        private static ChainInfo _fusetestnet = new ChainInfo(
            123,
            "fuse",
            "evm",
            "https://static.particle.network/token-list/fuse/native.png",
            "Fuse Testnet",
            "Testnet",
            "https://www.fuse.io",
            new NativeCurrency("FUSE", "FUSE", 18),
            "https://rpc.fusespark.io",
            "https://explorer.fusespark.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Polygon => _polygon;

        private static ChainInfo _polygon = new ChainInfo(
            137,
            "Polygon",
            "evm",
            "https://static.particle.network/token-list/polygon/native.png",
            "Polygon Mainnet",
            "Mainnet",
            "https://polygon.technology",
            new NativeCurrency("MATIC", "MATIC", 18),
            "https://polygon-rpc.com",
            "https://polygonscan.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Manta => _manta;

        private static ChainInfo _manta = new ChainInfo(
            169,
            "Manta",
            "evm",
            "https://static.particle.network/token-list/manta/native.png",
            "Manta Mainnet",
            "Mainnet",
            "https://manta.network",
            new NativeCurrency("ETH", "ETH", 18),
            "https://pacific-rpc.manta.network/http",
            "https://pacific-explorer.manta.network",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo XLayerTestnet => _xlayertestnet;

        private static ChainInfo _xlayertestnet = new ChainInfo(
            195,
            "OKBC",
            "evm",
            "https://static.particle.network/token-list/okc/native.png",
            "X Layer Testnet",
            "Testnet",
            "https://www.okx.com",
            new NativeCurrency("OKB", "OKB", 18),
            "https://testrpc.xlayer.tech",
            "https://www.okx.com/explorer/xlayer-test",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo XLayer => _xlayer;

        private static ChainInfo _xlayer = new ChainInfo(
            196,
            "OKBC",
            "evm",
            "https://static.particle.network/token-list/okc/native.png",
            "X Layer Mainnet",
            "Mainnet",
            "https://www.okx.com",
            new NativeCurrency("OKB", "OKB", 18),
            "https://rpc.xlayer.tech",
            "https://www.okx.com/zh-hans/explorer/xlayer",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo opBNB => _opbnb;

        private static ChainInfo _opbnb = new ChainInfo(
            204,
            "opBNB",
            "evm",
            "https://static.particle.network/token-list/opbnb/native.png",
            "opBNB Mainnet",
            "Mainnet",
            "https://opbnb.bnbchain.org",
            new NativeCurrency("BNB", "BNB", 18),
            "https://opbnb-mainnet-rpc.bnbchain.org",
            "https://mainnet.opbnbscan.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo MAPProtocolTestnet => _mapprotocoltestnet;

        private static ChainInfo _mapprotocoltestnet = new ChainInfo(
            212,
            "MAPProtocol",
            "evm",
            "https://static.particle.network/token-list/mapprotocol/native.png",
            "MAP Protocol Testnet",
            "Testnet",
            "https://maplabs.io",
            new NativeCurrency("MAPO", "MAPO", 18),
            "https://testnet-rpc.maplabs.io",
            "https://testnet.maposcan.io",
            new List<Feature>() { new Feature("EIP1559") },
            "https://faucet.mapprotocol.io"
        );


        public static ChainInfo BSquared => _bsquared;

        private static ChainInfo _bsquared = new ChainInfo(
            223,
            "BSquared",
            "evm",
            "https://static.particle.network/token-list/bsquared/native.png",
            "B² Network Mainnet",
            "Mainnet",
            "https://www.bsquared.network",
            new NativeCurrency("BTC", "BTC", 18),
            "https://rpc.bsquared.network",
            "https://explorer.bsquared.network",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Fantom => _fantom;

        private static ChainInfo _fantom = new ChainInfo(
            250,
            "Fantom",
            "evm",
            "https://static.particle.network/token-list/fantom/native.png",
            "Fantom Mainnet",
            "Mainnet",
            "https://fantom.foundation",
            new NativeCurrency("FTM", "FTM", 18),
            "https://rpc.ftm.tools",
            "https://ftmscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo zkSyncEraSepolia => _zksyncerasepolia;

        private static ChainInfo _zksyncerasepolia = new ChainInfo(
            300,
            "zkSync",
            "evm",
            "https://static.particle.network/token-list/zksync/native.png",
            "zkSync Era Sepolia",
            "Sepolia",
            "https://era.zksync.io",
            new NativeCurrency("zkSync", "ETH", 18),
            "https://sepolia.era.zksync.dev",
            "https://sepolia.explorer.zksync.io",
            new List<Feature>() { new Feature("EIP1559") },
            "https://portal.zksync.io/faucet"
        );


        public static ChainInfo KCC => _kcc;

        private static ChainInfo _kcc = new ChainInfo(
            321,
            "KCC",
            "evm",
            "https://static.particle.network/token-list/kcc/native.png",
            "KCC Mainnet",
            "Mainnet",
            "https://kcc.io",
            new NativeCurrency("KCS", "KCS", 18),
            "https://rpc-mainnet.kcc.network",
            "https://explorer.kcc.io/en",
            null,
            null
        );


        public static ChainInfo KCCTestnet => _kcctestnet;

        private static ChainInfo _kcctestnet = new ChainInfo(
            322,
            "KCC",
            "evm",
            "https://static.particle.network/token-list/kcc/native.png",
            "KCC Testnet",
            "Testnet",
            "https://scan-testnet.kcc.network",
            new NativeCurrency("KCS", "KCS", 18),
            "https://rpc-testnet.kcc.network",
            "https://scan-testnet.kcc.network",
            null,
            "https://faucet-testnet.kcc.network"
        );


        public static ChainInfo zkSyncEra => _zksyncera;

        private static ChainInfo _zksyncera = new ChainInfo(
            324,
            "zkSync",
            "evm",
            "https://static.particle.network/token-list/zksync/native.png",
            "zkSync Era",
            "Mainnet",
            "https://zksync.io",
            new NativeCurrency("zkSync", "ETH", 18),
            "https://zksync2-mainnet.zksync.io",
            "https://explorer.zksync.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo CronosTestnet => _cronostestnet;

        private static ChainInfo _cronostestnet = new ChainInfo(
            338,
            "Cronos",
            "evm",
            "https://static.particle.network/token-list/cronos/native.png",
            "Cronos Testnet",
            "Testnet",
            "https://cronos.org",
            new NativeCurrency("Cronos", "CRO", 18),
            "https://evm-t3.cronos.org",
            "https://testnet.cronoscan.com",
            new List<Feature>() { new Feature("EIP1559") },
            "https://cronos.org/faucet"
        );


        public static ChainInfo ModeTestnet => _modetestnet;

        private static ChainInfo _modetestnet = new ChainInfo(
            919,
            "Mode",
            "evm",
            "https://static.particle.network/token-list/mode/native.png",
            "Mode Testnet",
            "Testnet",
            "https://www.mode.network",
            new NativeCurrency("ETH", "ETH", 18),
            "https://sepolia.mode.network",
            "https://sepolia.explorer.mode.network",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo fiveire => _fiveire;

        private static ChainInfo _fiveire = new ChainInfo(
            995,
            "fiveire",
            "evm",
            "https://static.particle.network/token-list/fiveire/native.png",
            "5ire Mainnet",
            "Mainnet",
            "https://www.5ire.org",
            new NativeCurrency("5IRE", "5IRE", 18),
            "https://rpc.5ire.network",
            "https://5irescan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo fiveireTestnet => _fiveiretestnet;

        private static ChainInfo _fiveiretestnet = new ChainInfo(
            997,
            "fiveire",
            "evm",
            "https://static.particle.network/token-list/fiveire/native.png",
            "5ire Testnet",
            "Testnet",
            "https://www.5ire.org",
            new NativeCurrency("5IRE", "5IRE", 18),
            "https://rpc.qa.5ire.network",
            "https://scan.qa.5ire.network",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo KlaytnTestnet => _klaytntestnet;

        private static ChainInfo _klaytntestnet = new ChainInfo(
            1001,
            "Klaytn",
            "evm",
            "https://static.particle.network/token-list/klaytn/native.png",
            "Klaytn Testnet",
            "Testnet",
            "https://www.klaytn.com",
            new NativeCurrency("Klaytn", "KLAY", 18),
            "https://api.baobab.klaytn.net:8651",
            "https://baobab.scope.klaytn.com",
            null,
            "https://baobab.wallet.klaytn.foundation/faucet"
        );


        public static ChainInfo ConfluxeSpace => _confluxespace;

        private static ChainInfo _confluxespace = new ChainInfo(
            1030,
            "ConfluxESpace",
            "evm",
            "https://static.particle.network/token-list/confluxespace/native.png",
            "Conflux eSpace",
            "Mainnet",
            "https://confluxnetwork.org",
            new NativeCurrency("CFX", "CFX", 18),
            "https://evm.confluxrpc.com",
            "https://evm.confluxscan.net",
            null,
            null
        );


        public static ChainInfo Metis => _metis;

        private static ChainInfo _metis = new ChainInfo(
            1088,
            "Metis",
            "evm",
            "https://static.particle.network/token-list/metis/native.png",
            "Metis Mainnet",
            "Mainnet",
            "https://www.metis.io",
            new NativeCurrency("Metis", "METIS", 18),
            "https://andromeda.metis.io/?owner=1088",
            "https://andromeda-explorer.metis.io",
            null,
            null
        );


        public static ChainInfo PolygonzkEVM => _polygonzkevm;

        private static ChainInfo _polygonzkevm = new ChainInfo(
            1101,
            "PolygonZkEVM",
            "evm",
            "https://static.particle.network/token-list/polygonzkevm/native.png",
            "Polygon zkEVM",
            "Mainnet",
            "https://polygon.technology/polygon-zkevm",
            new NativeCurrency("ETH", "ETH", 18),
            "https://zkevm-rpc.com",
            "https://zkevm.polygonscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo CoreTestnet => _coretestnet;

        private static ChainInfo _coretestnet = new ChainInfo(
            1115,
            "Core",
            "evm",
            "https://static.particle.network/token-list/core/native.png",
            "Core Testnet",
            "Testnet",
            "https://coredao.org",
            new NativeCurrency("CORE", "CORE", 18),
            "https://rpc.test.btcs.network",
            "https://scan.test.btcs.network",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Core => _core;

        private static ChainInfo _core = new ChainInfo(
            1116,
            "Core",
            "evm",
            "https://static.particle.network/token-list/core/native.png",
            "Core Mainnet",
            "Mainnet",
            "https://coredao.org",
            new NativeCurrency("CORE", "CORE", 18),
            "https://rpc.coredao.org",
            "https://scan.coredao.org",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo BSquaredTestnet => _bsquaredtestnet;

        private static ChainInfo _bsquaredtestnet = new ChainInfo(
            1123,
            "BSquared",
            "evm",
            "https://static.particle.network/token-list/bsquared/native.png",
            "B² Network Testnet",
            "Testnet",
            "https://www.bsquared.network",
            new NativeCurrency("BTC", "BTC", 18),
            "https://b2-testnet.alt.technology",
            "https://testnet-explorer.bsquared.network",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo HybridTestnet => _hybridtestnet;

        private static ChainInfo _hybridtestnet = new ChainInfo(
            1225,
            "Hybrid",
            "evm",
            "https://static.particle.network/token-list/hybrid/native.png",
            "Hybrid Testnet",
            "Testnet",
            "https://buildonhybrid.com",
            new NativeCurrency("HYB", "HYB", 18),
            "https://hybrid-testnet.rpc.caldera.xyz/http",
            "https://explorer.buildonhybrid.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Moonbeam => _moonbeam;

        private static ChainInfo _moonbeam = new ChainInfo(
            1284,
            "Moonbeam",
            "evm",
            "https://static.particle.network/token-list/moonbeam/native.png",
            "Moonbeam Mainnet",
            "Mainnet",
            "https://moonbeam.network/networks/moonbeam",
            new NativeCurrency("GLMR", "GLMR", 18),
            "https://rpc.api.moonbeam.network",
            "https://moonbeam.moonscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Moonriver => _moonriver;

        private static ChainInfo _moonriver = new ChainInfo(
            1285,
            "Moonriver",
            "evm",
            "https://static.particle.network/token-list/moonriver/native.png",
            "Moonriver Mainnet",
            "Mainnet",
            "https://moonbeam.network/networks/moonriver",
            new NativeCurrency("MOVR", "MOVR", 18),
            "https://rpc.api.moonriver.moonbeam.network",
            "https://moonriver.moonscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo MoonbeamTestnet => _moonbeamtestnet;

        private static ChainInfo _moonbeamtestnet = new ChainInfo(
            1287,
            "Moonbeam",
            "evm",
            "https://static.particle.network/token-list/moonbeam/native.png",
            "Moonbeam Testnet",
            "Testnet",
            "https://docs.moonbeam.network/networks/testnet",
            new NativeCurrency("Dev", "DEV", 18),
            "https://rpc.api.moonbase.moonbeam.network",
            "https://moonbase.moonscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            "https://apps.moonbeam.network/moonbase-alpha/faucet"
        );


        public static ChainInfo SeiTestnet => _seitestnet;

        private static ChainInfo _seitestnet = new ChainInfo(
            1328,
            "Sei",
            "evm",
            "https://static.particle.network/token-list/sei/native.png",
            "Sei Testnet",
            "Testnet",
            "https://www.sei.io",
            new NativeCurrency("SEI", "SEI", 18),
            "https://evm-rpc-testnet.sei-apis.com",
            "https://testnet.seistream.app",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Sei => _sei;

        private static ChainInfo _sei = new ChainInfo(
            1329,
            "Sei",
            "evm",
            "https://static.particle.network/token-list/sei/native.png",
            "Sei Mainnet",
            "Mainnet",
            "https://www.sei.io",
            new NativeCurrency("SEI", "SEI", 18),
            "https://evm-rpc.sei-apis.com",
            "https://seistream.app",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BEVMCanary => _bevmcanary;

        private static ChainInfo _bevmcanary = new ChainInfo(
            1501,
            "BEVM",
            "evm",
            "https://static.particle.network/token-list/bevm/native.png",
            "BEVM Canary Mainnet",
            "Mainnet",
            "https://www.bevm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://rpc-canary-1.bevm.io",
            "https://scan-canary.bevm.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo BEVMCanaryTestnet => _bevmcanarytestnet;

        private static ChainInfo _bevmcanarytestnet = new ChainInfo(
            1502,
            "BEVM",
            "evm",
            "https://static.particle.network/token-list/bevm/native.png",
            "BEVM Canary Testnet",
            "Testnet",
            "https://www.bevm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://canary-testnet.bevm.io",
            "https://scan-canary-testnet.bevm.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo StoryNetworkTestnet => _storynetworktestnet;

        private static ChainInfo _storynetworktestnet = new ChainInfo(
            1513,
            "StoryNetwork",
            "evm",
            "https://static.particle.network/token-list/storynetwork/native.png",
            "Story Network Testnet",
            "Testnet",
            "https://explorer.testnet.storyprotocol.net",
            new NativeCurrency("IP", "IP", 18),
            "https://rpc.partner.testnet.storyprotocol.net",
            "https://explorer.testnet.storyprotocol.net",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo GravityAlpha => _gravityalpha;

        private static ChainInfo _gravityalpha = new ChainInfo(
            1625,
            "Gravity",
            "evm",
            "https://static.particle.network/token-list/gravity/native.png",
            "Gravity Alpha Mainnet",
            "Mainnet",
            "https://gravity.xyz",
            new NativeCurrency("G", "G", 18),
            "https://rpc.gravity.xyz",
            "https://gscan.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ComboTestnet => _combotestnet;

        private static ChainInfo _combotestnet = new ChainInfo(
            1715,
            "Combo",
            "evm",
            "https://static.particle.network/token-list/combo/native.png",
            "Combo Testnet",
            "Testnet",
            "https://docs.combonetwork.io",
            new NativeCurrency("BNB", "BNB", 18),
            "https://test-rpc.combonetwork.io",
            "https://combotrace-testnet.nodereal.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo SoneiumMinatoTestnet => _soneiumminatotestnet;

        private static ChainInfo _soneiumminatotestnet = new ChainInfo(
            1946,
            "Soneium",
            "evm",
            "https://static.particle.network/token-list/soneium/native.png",
            "Soneium Minato Testnet",
            "Testnet",
            "https://soneium.org",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.minato.soneium.org",
            "https://explorer-testnet.soneium.org",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo KavaTestnet => _kavatestnet;

        private static ChainInfo _kavatestnet = new ChainInfo(
            2221,
            "Kava",
            "evm",
            "https://static.particle.network/token-list/kava/native.png",
            "Kava Testnet",
            "Testnet",
            "https://www.kava.io",
            new NativeCurrency("KAVA", "KAVA", 18),
            "https://evm.testnet.kava.io",
            "https://testnet.kavascan.com",
            null,
            "https://faucet.kava.io"
        );


        public static ChainInfo Kava => _kava;

        private static ChainInfo _kava = new ChainInfo(
            2222,
            "Kava",
            "evm",
            "https://static.particle.network/token-list/kava/native.png",
            "Kava Mainnet",
            "Mainnet",
            "https://www.kava.io",
            new NativeCurrency("KAVA", "KAVA", 18),
            "https://evm.kava.io",
            "https://kavascan.com",
            null,
            null
        );


        public static ChainInfo PeaqKrest => _peaqkrest;

        private static ChainInfo _peaqkrest = new ChainInfo(
            2241,
            "peaq",
            "evm",
            "https://static.particle.network/token-list/peaq/native.png",
            "Peaq Krest Mainnet",
            "Mainnet",
            "https://www.peaq.network",
            new NativeCurrency("KRST", "KRST", 18),
            "https://erpc-krest.peaq.network",
            "https://krest.subscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo PolygonzkEVMCardona => _polygonzkevmcardona;

        private static ChainInfo _polygonzkevmcardona = new ChainInfo(
            2442,
            "PolygonZkEVM",
            "evm",
            "https://static.particle.network/token-list/polygonzkevm/native.png",
            "Polygon zkEVM Cardona",
            "Cardona",
            "https://polygon.technology",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.cardona.zkevm-rpc.com",
            "https://cardona-zkevm.polygonscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo AILayerTestnet => _ailayertestnet;

        private static ChainInfo _ailayertestnet = new ChainInfo(
            2648,
            "ainn",
            "evm",
            "https://static.particle.network/token-list/ainn/native.png",
            "AILayer Testnet",
            "Testnet",
            "https://anvm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://rpc.anvm.io",
            "https://explorer.anvm.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo AILayer => _ailayer;

        private static ChainInfo _ailayer = new ChainInfo(
            2649,
            "ainn",
            "evm",
            "https://static.particle.network/token-list/ainn/native.png",
            "AILayer Mainnet",
            "Mainnet",
            "https://anvm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://mainnet-rpc.ailayer.xyz",
            "https://mainnet-explorer.ailayer.xyz",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo GMNetwork => _gmnetwork;

        private static ChainInfo _gmnetwork = new ChainInfo(
            2777,
            "GMNetwork",
            "evm",
            "https://static.particle.network/token-list/gmnetwork/native.png",
            "GM Network Mainnet",
            "Mainnet",
            "https://gmnetwork.ai",
            new NativeCurrency("Ether", "ETH", 18),
            "https://rpc.gmnetwork.ai",
            "https://scan.gmnetwork.ai",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo SatoshiVMAlpha => _satoshivmalpha;

        private static ChainInfo _satoshivmalpha = new ChainInfo(
            3109,
            "satoshivm",
            "evm",
            "https://static.particle.network/token-list/satoshivm/native.png",
            "SatoshiVM Alpha",
            "Mainnet",
            "https://www.satoshivm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://alpha-rpc-node-http.svmscan.io",
            "https://svmscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo SatoshiVMTestnet => _satoshivmtestnet;

        private static ChainInfo _satoshivmtestnet = new ChainInfo(
            3110,
            "SatoshiVM",
            "evm",
            "https://static.particle.network/token-list/satoshivm/native.png",
            "SatoshiVM Testnet",
            "Testnet",
            "https://www.satoshivm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://test-rpc-node-http.svmscan.io",
            "https://testnet.svmscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BotanixTestnet => _botanixtestnet;

        private static ChainInfo _botanixtestnet = new ChainInfo(
            3636,
            "Botanix",
            "evm",
            "https://static.particle.network/token-list/botanix/native.png",
            "Botanix Testnet",
            "Testnet",
            "https://botanixlabs.xyz",
            new NativeCurrency("BTC", "BTC", 18),
            "https://node.botanixlabs.dev",
            "https://blockscout.botanixlabs.dev",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo AstarzkEVMMainet => _astarzkevmmainet;

        private static ChainInfo _astarzkevmmainet = new ChainInfo(
            3776,
            "AstarZkEVM",
            "evm",
            "https://static.particle.network/token-list/astarzkevm/native.png",
            "Astar zkEVM Mainet",
            "Mainnet",
            "https://astar.network",
            new NativeCurrency("Sepolia Ether", "ETH", 18),
            "https://rpc.startale.com/astar-zkevm",
            "https://astar-zkevm.explorer.startale.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo FantomTestnet => _fantomtestnet;

        private static ChainInfo _fantomtestnet = new ChainInfo(
            4002,
            "Fantom",
            "evm",
            "https://static.particle.network/token-list/fantom/native.png",
            "Fantom Testnet",
            "Testnet",
            "https://docs.fantom.foundation/quick-start/short-guide#fantom-testnet",
            new NativeCurrency("FTM", "FTM", 18),
            "https://rpc.testnet.fantom.network",
            "https://testnet.ftmscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            "https://faucet.fantom.network"
        );


        public static ChainInfo Merlin => _merlin;

        private static ChainInfo _merlin = new ChainInfo(
            4200,
            "Merlin",
            "evm",
            "https://static.particle.network/token-list/merlin/native.png",
            "Merlin Mainnet",
            "Mainnet",
            "https://merlinprotocol.org",
            new NativeCurrency("BTC", "BTC", 18),
            "https://rpc.merlinchain.io",
            "https://scan.merlinchain.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo IoTeX => _iotex;

        private static ChainInfo _iotex = new ChainInfo(
            4689,
            "iotex",
            "evm",
            "https://static.particle.network/token-list/iotex/native.png",
            "IoTeX Mainnet",
            "Mainnet",
            "https://iotex.io",
            new NativeCurrency("IOTX", "IOTX", 18),
            "https://babel-api.mainnet.iotex.io",
            "https://iotexscan.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo IoTeXTestnet => _iotextestnet;

        private static ChainInfo _iotextestnet = new ChainInfo(
            4690,
            "iotex",
            "evm",
            "https://static.particle.network/token-list/iotex/native.png",
            "IoTeX Testnet",
            "Testnet",
            "https://iotex.io",
            new NativeCurrency("IOTX", "IOTX", 18),
            "https://babel-api.testnet.iotex.io",
            "https://testnet.iotexscan.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Mantle => _mantle;

        private static ChainInfo _mantle = new ChainInfo(
            5000,
            "Mantle",
            "evm",
            "https://static.particle.network/token-list/mantle/native.png",
            "Mantle Mainnet",
            "Mainnet",
            "https://mantle.xyz",
            new NativeCurrency("MNT", "MNT", 18),
            "https://rpc.mantle.xyz",
            "https://explorer.mantle.xyz",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo MantleSepoliaTestnet => _mantlesepoliatestnet;

        private static ChainInfo _mantlesepoliatestnet = new ChainInfo(
            5003,
            "Mantle",
            "evm",
            "https://static.particle.network/token-list/mantle/native.png",
            "Mantle Sepolia Testnet",
            "Testnet",
            "https://mantle.xyz",
            new NativeCurrency("MNT", "MNT", 18),
            "https://rpc.sepolia.mantle.xyz",
            "https://explorer.sepolia.mantle.xyz",
            new List<Feature>() { new Feature("ERC4337") },
            "https://faucet.sepolia.mantle.xyz"
        );


        public static ChainInfo opBNBTestnet => _opbnbtestnet;

        private static ChainInfo _opbnbtestnet = new ChainInfo(
            5611,
            "opBNB",
            "evm",
            "https://static.particle.network/token-list/opbnb/native.png",
            "opBNB Testnet",
            "Testnet",
            "https://opbnb.bnbchain.org",
            new NativeCurrency("BNB", "BNB", 18),
            "https://opbnb-testnet-rpc.bnbchain.org",
            "https://opbnb-testnet.bscscan.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo AuraTestnet => _auratestnet;

        private static ChainInfo _auratestnet = new ChainInfo(
            6321,
            "aura",
            "evm",
            "https://static.particle.network/token-list/aura/native.png",
            "Aura Testnet",
            "Testnet",
            "https://aura.network",
            new NativeCurrency("AURA", "AURA", 18),
            "https://jsonrpc.euphoria.aura.network",
            "https://euphoria.aurascan.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Aura => _aura;

        private static ChainInfo _aura = new ChainInfo(
            6322,
            "aura",
            "evm",
            "https://static.particle.network/token-list/aura/native.png",
            "Aura Mainnet",
            "Mainnet",
            "https://aura.network",
            new NativeCurrency("AURA", "AURA", 18),
            "https://jsonrpc.aura.network",
            "https://aurascan.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo ZetaChain => _zetachain;

        private static ChainInfo _zetachain = new ChainInfo(
            7000,
            "ZetaChain",
            "evm",
            "https://static.particle.network/token-list/zetachain/native.png",
            "ZetaChain Mainnet",
            "Mainnet",
            "https://zetachain.com",
            new NativeCurrency("ZETA", "ZETA", 18),
            "https://zetachain-evm.blockpi.network/v1/rpc/public",
            "https://zetachain.blockscout.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ZetaChainTestnet => _zetachaintestnet;

        private static ChainInfo _zetachaintestnet = new ChainInfo(
            7001,
            "ZetaChain",
            "evm",
            "https://static.particle.network/token-list/zetachain/native.png",
            "ZetaChain Testnet",
            "Testnet",
            "https://zetachain.com",
            new NativeCurrency("ZETA", "ZETA", 18),
            "https://zetachain-athens-evm.blockpi.network/v1/rpc/public",
            "https://zetachain-athens-3.blockscout.com",
            new List<Feature>() { new Feature("EIP1559") },
            "https://labs.zetachain.com/get-zeta"
        );


        public static ChainInfo Cyber => _cyber;

        private static ChainInfo _cyber = new ChainInfo(
            7560,
            "Cyber",
            "evm",
            "https://static.particle.network/token-list/cyber/native.png",
            "Cyber Mainnet",
            "Mainnet",
            "https://cyber-explorer.alt.technology",
            new NativeCurrency("ETH", "ETH", 18),
            "https://cyber.alt.technology",
            "https://cyberscan.co",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Klaytn => _klaytn;

        private static ChainInfo _klaytn = new ChainInfo(
            8217,
            "Klaytn",
            "evm",
            "https://static.particle.network/token-list/klaytn/native.png",
            "Klaytn Mainnet",
            "Mainnet",
            "https://www.klaytn.com",
            new NativeCurrency("Klaytn", "KLAY", 18),
            "https://cypress.fandom.finance/archive",
            "https://scope.klaytn.com",
            new List<Feature>() { new Feature("SWAP") },
            null
        );


        public static ChainInfo Lorenzo => _lorenzo;

        private static ChainInfo _lorenzo = new ChainInfo(
            8329,
            "lorenzo",
            "evm",
            "https://static.particle.network/token-list/lorenzo/native.png",
            "Lorenzo Mainnet",
            "Mainnet",
            "https://lorenzo-protocol.xyz",
            new NativeCurrency("stBTC", "stBTC", 18),
            "https://rpc.lorenzo-protocol.xyz",
            "https://scan.lorenzo-protocol.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Base => _base;

        private static ChainInfo _base = new ChainInfo(
            8453,
            "Base",
            "evm",
            "https://static.particle.network/token-list/base/native.png",
            "Base Mainnet",
            "Mainnet",
            "https://base.org",
            new NativeCurrency("ETH", "ETH", 18),
            "https://developer-access-mainnet.base.org",
            "https://basescan.org",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Combo => _combo;

        private static ChainInfo _combo = new ChainInfo(
            9980,
            "Combo",
            "evm",
            "https://static.particle.network/token-list/combo/native.png",
            "Combo Mainnet",
            "Mainnet",
            "https://docs.combonetwork.io",
            new NativeCurrency("BNB", "BNB", 18),
            "https://rpc.combonetwork.io",
            "https://combotrace.nodereal.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo PeaqAgungTestnet => _peaqagungtestnet;

        private static ChainInfo _peaqagungtestnet = new ChainInfo(
            9990,
            "peaq",
            "evm",
            "https://static.particle.network/token-list/peaq/native.png",
            "Peaq Agung Testnet",
            "Testnet",
            "https://www.peaq.network",
            new NativeCurrency("AGUNG", "AGUNG", 18),
            "https://rpcpc1-qa.agung.peaq.network",
            "https://agung-testnet.subscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo GnosisTestnet => _gnosistestnet;

        private static ChainInfo _gnosistestnet = new ChainInfo(
            10200,
            "Gnosis",
            "evm",
            "https://static.particle.network/token-list/gnosis/native.png",
            "Gnosis Testnet",
            "Testnet",
            "https://docs.gnosischain.com",
            new NativeCurrency("Gnosis", "XDAI", 18),
            "https://optimism.gnosischain.com",
            "https://blockscout.com/gnosis/chiado",
            new List<Feature>() { new Feature("EIP1559") },
            "https://gnosisfaucet.com"
        );


        public static ChainInfo BEVM => _bevm;

        private static ChainInfo _bevm = new ChainInfo(
            11501,
            "BEVM",
            "evm",
            "https://static.particle.network/token-list/bevm/native.png",
            "BEVM Mainnet",
            "Mainnet",
            "https://www.bevm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://rpc-mainnet-1.bevm.io",
            "https://scan-mainnet.bevm.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo BEVMTestnet => _bevmtestnet;

        private static ChainInfo _bevmtestnet = new ChainInfo(
            11503,
            "BEVM",
            "evm",
            "https://static.particle.network/token-list/bevm/native.png",
            "BEVM Testnet",
            "Testnet",
            "https://www.bevm.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://testnet.bevm.io",
            "https://scan-testnet.bevm.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo ReadONTestnet => _readontestnet;

        private static ChainInfo _readontestnet = new ChainInfo(
            12015,
            "ReadON",
            "evm",
            "https://static.particle.network/token-list/readon/native.png",
            "ReadON Testnet",
            "Testnet",
            "https://opside.network",
            new NativeCurrency("READ", "READ", 18),
            "https://pre-alpha-zkrollup-rpc.opside.network/readon-content-test-chain",
            "https://readon-content-test-chain.zkevm.opside.info",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo ImmutablezkEVMTestnet => _immutablezkevmtestnet;

        private static ChainInfo _immutablezkevmtestnet = new ChainInfo(
            13473,
            "Immutable",
            "evm",
            "https://static.particle.network/token-list/immutable/native.png",
            "Immutable zkEVM Testnet",
            "Testnet",
            "https://www.immutable.com",
            new NativeCurrency("IMX", "IMX", 18),
            "https://rpc.testnet.immutable.com",
            "https://explorer.testnet.immutable.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo GravityAlphaTestnet => _gravityalphatestnet;

        private static ChainInfo _gravityalphatestnet = new ChainInfo(
            13505,
            "Gravity",
            "evm",
            "https://static.particle.network/token-list/gravity/native.png",
            "Gravity Alpha Testnet",
            "Testnet",
            "https://gravity.xyz",
            new NativeCurrency("G", "G", 18),
            "https://rpc-sepolia.gravity.xyz",
            " https://explorer-sepolia.gravity.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo EOSEVMTestnet => _eosevmtestnet;

        private static ChainInfo _eosevmtestnet = new ChainInfo(
            15557,
            "Eosevm",
            "evm",
            "https://static.particle.network/token-list/eosevm/native.png",
            "EOS EVM Testnet",
            "Testnet",
            "https://eosnetwork.com",
            new NativeCurrency("EOS", "EOS", 18),
            "https://api.testnet.evm.eosnetwork.com",
            "https://explorer.testnet.evm.eosnetwork.com",
            null,
            "https://bridge.testnet.evm.eosnetwork.com"
        );


        public static ChainInfo EthereumHolesky => _ethereumholesky;

        private static ChainInfo _ethereumholesky = new ChainInfo(
            17000,
            "Ethereum",
            "evm",
            "https://static.particle.network/token-list/ethereum/native.png",
            "Ethereum Holesky",
            "Holesky",
            "https://holesky.ethpandaops.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://ethereum-holesky.blockpi.network/v1/rpc/public",
            "https://holesky.etherscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            "https://faucet.quicknode.com/drip"
        );


        public static ChainInfo EOSEVM => _eosevm;

        private static ChainInfo _eosevm = new ChainInfo(
            17777,
            "Eosevm",
            "evm",
            "https://static.particle.network/token-list/eosevm/native.png",
            "EOS EVM",
            "Mainnet",
            "https://eosnetwork.com",
            new NativeCurrency("EOS", "EOS", 18),
            "https://api.evm.eosnetwork.com",
            "https://explorer.evm.eosnetwork.com",
            null,
            null
        );


        public static ChainInfo MAPProtocol => _mapprotocol;

        private static ChainInfo _mapprotocol = new ChainInfo(
            22776,
            "MAPProtocol",
            "evm",
            "https://static.particle.network/token-list/mapprotocol/native.png",
            "MAP Protocol",
            "Mainnet",
            "https://maplabs.io",
            new NativeCurrency("MAPO", "MAPO", 18),
            "https://rpc.maplabs.io",
            "https://mapscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Zeroone => _zeroone;

        private static ChainInfo _zeroone = new ChainInfo(
            27827,
            "Zeroone",
            "evm",
            "https://static.particle.network/token-list/zeroone/native.png",
            "Zeroone Mainnet",
            "Mainnet",
            "https://zeroone.art",
            new NativeCurrency("ZERO", "ZERO", 18),
            "https://subnets.avax.network/zeroonemai/mainnet/rpc",
            "https://subnets.avax.network/zeroonemai",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Mode => _mode;

        private static ChainInfo _mode = new ChainInfo(
            34443,
            "Mode",
            "evm",
            "https://static.particle.network/token-list/mode/native.png",
            "Mode Mainnet",
            "Mainnet",
            "https://www.mode.network",
            new NativeCurrency("ETH", "ETH", 18),
            "https://mainnet.mode.network",
            "https://explorer.mode.network",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ArbitrumOne => _arbitrumone;

        private static ChainInfo _arbitrumone = new ChainInfo(
            42161,
            "Arbitrum",
            "evm",
            "https://static.particle.network/token-list/arbitrum/native.png",
            "Arbitrum One",
            "Mainnet",
            "https://arbitrum.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://arb1.arbitrum.io/rpc",
            "https://arbiscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ArbitrumNova => _arbitrumnova;

        private static ChainInfo _arbitrumnova = new ChainInfo(
            42170,
            "Arbitrum",
            "evm",
            "https://static.particle.network/token-list/arbitrum/native.png",
            "Arbitrum Nova",
            "Mainnet",
            "https://arbitrum.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://nova.arbitrum.io/rpc",
            "https://nova.arbiscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Celo => _celo;

        private static ChainInfo _celo = new ChainInfo(
            42220,
            "Celo",
            "evm",
            "https://static.particle.network/token-list/celo/native.png",
            "Celo Mainnet",
            "Mainnet",
            "https://docs.celo.org",
            new NativeCurrency("Celo", "CELO", 18),
            "https://rpc.ankr.com/celo",
            "https://explorer.celo.org/mainnet",
            new List<Feature>() { new Feature("SWAP") },
            null
        );


        public static ChainInfo OasisEmeraldTestnet => _oasisemeraldtestnet;

        private static ChainInfo _oasisemeraldtestnet = new ChainInfo(
            42261,
            "OasisEmerald",
            "evm",
            "https://static.particle.network/token-list/oasisemerald/native.png",
            "OasisEmerald Testnet",
            "Testnet",
            "https://docs.oasis.io/dapp/emerald",
            new NativeCurrency("OasisEmerald", "ROSE", 18),
            "https://testnet.emerald.oasis.dev",
            "https://testnet.explorer.emerald.oasis.dev",
            null,
            "https://faucet.testnet.oasis.dev"
        );


        public static ChainInfo OasisEmerald => _oasisemerald;

        private static ChainInfo _oasisemerald = new ChainInfo(
            42262,
            "OasisEmerald",
            "evm",
            "https://static.particle.network/token-list/oasisemerald/native.png",
            "OasisEmerald Mainnet",
            "Mainnet",
            "https://docs.oasis.io/dapp/emerald",
            new NativeCurrency("OasisEmerald", "ROSE", 18),
            "https://emerald.oasis.dev",
            "https://explorer.emerald.oasis.dev",
            null,
            null
        );


        public static ChainInfo ZKFair => _zkfair;

        private static ChainInfo _zkfair = new ChainInfo(
            42766,
            "ZKFair",
            "evm",
            "https://static.particle.network/token-list/zkfair/native.png",
            "ZKFair Mainnet",
            "Mainnet",
            "https://zkfair.io",
            new NativeCurrency("ZKF", "USDC", 18),
            "https://rpc.zkfair.io",
            "https://scan.zkfair.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo AvalancheTestnet => _avalanchetestnet;

        private static ChainInfo _avalanchetestnet = new ChainInfo(
            43113,
            "Avalanche",
            "evm",
            "https://static.particle.network/token-list/avalanche/native.png",
            "Avalanche Testnet",
            "Testnet",
            "https://cchain.explorer.avax-test.network",
            new NativeCurrency("AVAX", "AVAX", 18),
            "https://api.avax-test.network/ext/bc/C/rpc",
            "https://testnet.snowtrace.io",
            new List<Feature>() { new Feature("EIP1559") },
            "https://faucet.avax.network"
        );


        public static ChainInfo Avalanche => _avalanche;

        private static ChainInfo _avalanche = new ChainInfo(
            43114,
            "Avalanche",
            "evm",
            "https://static.particle.network/token-list/avalanche/native.png",
            "Avalanche Mainnet",
            "Mainnet",
            "https://www.avax.network",
            new NativeCurrency("AVAX", "AVAX", 18),
            "https://api.avax.network/ext/bc/C/rpc",
            "https://snowtrace.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ZKFairTestnet => _zkfairtestnet;

        private static ChainInfo _zkfairtestnet = new ChainInfo(
            43851,
            "ZKFair",
            "evm",
            "https://static.particle.network/token-list/zkfair/native.png",
            "ZKFair Testnet",
            "Testnet",
            "https://zkfair.io",
            new NativeCurrency("ZKF", "USDC", 18),
            "https://testnet-rpc.zkfair.io",
            "https://testnet-scan.zkfair.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo CeloTestnet => _celotestnet;

        private static ChainInfo _celotestnet = new ChainInfo(
            44787,
            "Celo",
            "evm",
            "https://static.particle.network/token-list/celo/native.png",
            "Celo Testnet",
            "Testnet",
            "https://docs.celo.org",
            new NativeCurrency("Celo", "CELO", 18),
            "https://alfajores-forno.celo-testnet.org",
            "https://explorer.celo.org/alfajores",
            null,
            "https://celo.org/developers/faucet"
        );


        public static ChainInfo ZircuitTestnet => _zircuittestnet;

        private static ChainInfo _zircuittestnet = new ChainInfo(
            48899,
            "Zircuit",
            "evm",
            "https://static.particle.network/token-list/zircuit/native.png",
            "Zircuit Testnet",
            "Testnet",
            "https://www.zircuit.com",
            new NativeCurrency("Ether", "ETH", 18),
            "https://zircuit1.p2pify.com",
            "https://explorer.testnet.zircuit.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo DODOChainTestnet => _dodochaintestnet;

        private static ChainInfo _dodochaintestnet = new ChainInfo(
            53457,
            "DODOChain",
            "evm",
            "https://static.particle.network/token-list/dodochain/native.png",
            "DODOChain Testnet",
            "Testnet",
            "https://www.dodochain.com",
            new NativeCurrency("DODO", "DODO", 18),
            "https://dodochain-testnet.alt.technology",
            "https://testnet-scan.dodochain.com",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ZerooneTestnet => _zeroonetestnet;

        private static ChainInfo _zeroonetestnet = new ChainInfo(
            56400,
            "Zeroone",
            "evm",
            "https://static.particle.network/token-list/zeroone/native.png",
            "Zeroone Testnet",
            "Testnet",
            "https://zeroone.art",
            new NativeCurrency("ZERO", "ZERO", 18),
            "https://subnets.avax.network/testnetzer/testnet/rpc",
            "https://subnets-test.avax.network/testnetzer",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo LineaSepolia => _lineasepolia;

        private static ChainInfo _lineasepolia = new ChainInfo(
            59141,
            "Linea",
            "evm",
            "https://static.particle.network/token-list/linea/native.png",
            "Linea Sepolia",
            "Sepolia",
            "https://linea.build",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.sepolia.linea.build",
            "https://sepolia.lineascan.build",
            new List<Feature>() { new Feature("EIP1559") },
            "https://faucet.goerli.linea.build"
        );


        public static ChainInfo Linea => _linea;

        private static ChainInfo _linea = new ChainInfo(
            59144,
            "Linea",
            "evm",
            "https://static.particle.network/token-list/linea/native.png",
            "Linea Mainnet",
            "Mainnet",
            "https://linea.build",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.linea.build",
            "https://lineascan.build",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BOB => _bob;

        private static ChainInfo _bob = new ChainInfo(
            60808,
            "BOB",
            "evm",
            "https://static.particle.network/token-list/bob/native.png",
            "BOB Mainnet",
            "Mainnet",
            "https://www.gobob.xyz",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.gobob.xyz",
            "https://explorer.gobob.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo PolygonAmoy => _polygonamoy;

        private static ChainInfo _polygonamoy = new ChainInfo(
            80002,
            "Polygon",
            "evm",
            "https://static.particle.network/token-list/polygon/native.png",
            "Polygon Amoy",
            "Amoy",
            "https://polygon.technology",
            new NativeCurrency("MATIC", "MATIC", 18),
            "https://rpc-amoy.polygon.technology",
            "https://www.oklink.com/amoy",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BerachainbArtio => _berachainbartio;

        private static ChainInfo _berachainbartio = new ChainInfo(
            80084,
            "Berachain",
            "evm",
            "https://static.particle.network/token-list/berachain/native.png",
            "Berachain bArtio",
            "bArtio",
            "https://www.berachain.com",
            new NativeCurrency("BERA", "BERA", 18),
            "https://bartio.rpc.berachain.com",
            "https://bartio.beratrail.io",
            new List<Feature>() { new Feature("ERC4337") },
            "https://bartio.faucet.berachain.com"
        );


        public static ChainInfo Blast => _blast;

        private static ChainInfo _blast = new ChainInfo(
            81457,
            "Blast",
            "evm",
            "https://static.particle.network/token-list/blast/native.png",
            "Blast Mainnet",
            "Mainnet",
            "https://blastblockchain.com",
            new NativeCurrency("Blast Ether", "ETH", 18),
            "https://rpc.blast.io",
            "https://blastscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo LorenzoTestnet => _lorenzotestnet;

        private static ChainInfo _lorenzotestnet = new ChainInfo(
            83291,
            "lorenzo",
            "evm",
            "https://static.particle.network/token-list/lorenzo/native.png",
            "Lorenzo Testnet",
            "Testnet",
            "https://lorenzo-protocol.xyz",
            new NativeCurrency("stBTC", "stBTC", 18),
            "https://rpc-testnet.lorenzo-protocol.xyz",
            "https://scan-testnet.lorenzo-protocol.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BaseSepolia => _basesepolia;

        private static ChainInfo _basesepolia = new ChainInfo(
            84532,
            "Base",
            "evm",
            "https://static.particle.network/token-list/base/native.png",
            "Base Sepolia",
            "Sepolia",
            "https://base.org",
            new NativeCurrency("ETH", "ETH", 18),
            "https://sepolia.base.org",
            "https://sepolia.basescan.org",
            new List<Feature>() { new Feature("EIP1559") },
            "https://bridge.base.org/deposit"
        );


        public static ChainInfo TUNATestnet => _tunatestnet;

        private static ChainInfo _tunatestnet = new ChainInfo(
            89682,
            "TUNA",
            "evm",
            "https://static.particle.network/token-list/tuna/native.png",
            "TUNA Testnet",
            "Testnet",
            "https://tunachain.io",
            new NativeCurrency("BTC", "BTC", 18),
            "https://babytuna.rpc.tunachain.io",
            "https://babytuna.explorer.tunachain.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo XterioBNB => _xteriobnb;

        private static ChainInfo _xteriobnb = new ChainInfo(
            112358,
            "xterio",
            "evm",
            "https://static.particle.network/token-list/xterio/native.png",
            "Xterio(BNB) Mainnet",
            "Mainnet",
            "https://xter.io",
            new NativeCurrency("BNB", "BNB", 18),
            "https://xterio.alt.technology",
            "https://xterscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Taiko => _taiko;

        private static ChainInfo _taiko = new ChainInfo(
            167000,
            "Taiko",
            "evm",
            "https://static.particle.network/token-list/taiko/native.png",
            "Taiko Mainnet",
            "Mainnet",
            "https://taiko.xyz",
            new NativeCurrency("Ether", "ETH", 18),
            "https://rpc.mainnet.taiko.xyz",
            "https://taikoscan.network",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo TaikoHekla => _taikohekla;

        private static ChainInfo _taikohekla = new ChainInfo(
            167009,
            "Taiko",
            "evm",
            "https://static.particle.network/token-list/taiko/native.png",
            "Taiko Hekla",
            "Hekla",
            "https://taiko.xyz",
            new NativeCurrency("Ether", "ETH", 18),
            "https://rpc.hekla.taiko.xyz",
            "https://explorer.hekla.taiko.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BitlayerTestnet => _bitlayertestnet;

        private static ChainInfo _bitlayertestnet = new ChainInfo(
            200810,
            "Bitlayer",
            "evm",
            "https://static.particle.network/token-list/bitlayer/native.png",
            "Bitlayer Testnet",
            "Testnet",
            "https://www.bitlayer.org",
            new NativeCurrency("BTC", "BTC", 18),
            "https://testnet-rpc.bitlayer.org",
            "https://testnet-scan.bitlayer.org",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Bitlayer => _bitlayer;

        private static ChainInfo _bitlayer = new ChainInfo(
            200901,
            "Bitlayer",
            "evm",
            "https://static.particle.network/token-list/bitlayer/native.png",
            "Bitlayer Mainnet",
            "Mainnet",
            "https://www.bitlayer.org",
            new NativeCurrency("BTC", "BTC", 18),
            "https://rpc.bitlayer.org",
            "https://www.btrscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo DuckchainTestnet => _duckchaintestnet;

        private static ChainInfo _duckchaintestnet = new ChainInfo(
            202105,
            "Duckchain",
            "evm",
            "https://static.particle.network/token-list/duckchain/native.png",
            "Duckchain Testnet",
            "Testnet",
            "https://testnet-scan.duckchain.io",
            new NativeCurrency("TON", "TON", 18),
            "https://testnet-rpc.duckchain.io",
            "https://testnet-scan.duckchain.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo PlatON => _platon;

        private static ChainInfo _platon = new ChainInfo(
            210425,
            "PlatON",
            "evm",
            "https://static.particle.network/token-list/platon/native.png",
            "PlatON Mainnet",
            "Mainnet",
            "https://www.platon.network",
            new NativeCurrency("LAT", "LAT", 18),
            "https://openapi2.platon.network/rpc",
            "https://scan.platon.network",
            null,
            null
        );


        public static ChainInfo ArbitrumSepolia => _arbitrumsepolia;

        private static ChainInfo _arbitrumsepolia = new ChainInfo(
            421614,
            "Arbitrum",
            "evm",
            "https://static.particle.network/token-list/arbitrum/native.png",
            "Arbitrum Sepolia",
            "Sepolia",
            "https://arbitrum.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://sepolia-rollup.arbitrum.io/rpc",
            "https://sepolia.arbiscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo ScrollSepolia => _scrollsepolia;

        private static ChainInfo _scrollsepolia = new ChainInfo(
            534351,
            "Scroll",
            "evm",
            "https://static.particle.network/token-list/scroll/native.png",
            "Scroll Sepolia",
            "Sepolia",
            "https://scroll.io",
            new NativeCurrency("Scroll", "ETH", 18),
            "https://sepolia-rpc.scroll.io",
            "https://sepolia.scrollscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Scroll => _scroll;

        private static ChainInfo _scroll = new ChainInfo(
            534352,
            "Scroll",
            "evm",
            "https://static.particle.network/token-list/scroll/native.png",
            "Scroll Mainnet",
            "Mainnet",
            "https://scroll.io",
            new NativeCurrency("Scroll", "ETH", 18),
            "https://rpc.scroll.io",
            "https://scrollscan.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo MerlinTestnet => _merlintestnet;

        private static ChainInfo _merlintestnet = new ChainInfo(
            686868,
            "Merlin",
            "evm",
            "https://static.particle.network/token-list/merlin/native.png",
            "Merlin Testnet",
            "Testnet",
            "https://merlinprotocol.org",
            new NativeCurrency("BTC", "BTC", 18),
            "https://testnet-rpc.merlinchain.io",
            "https://testnet-scan.merlinchain.io",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo SeiDevnet => _seidevnet;

        private static ChainInfo _seidevnet = new ChainInfo(
            713715,
            "Sei",
            "evm",
            "https://static.particle.network/token-list/sei/native.png",
            "Sei Devnet",
            "Devnet",
            "https://www.sei.io",
            new NativeCurrency("SEI", "SEI", 18),
            "https://evm-rpc-arctic-1.sei-apis.com",
            "https://devnet.seistream.app",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo zkLinkNova => _zklinknova;

        private static ChainInfo _zklinknova = new ChainInfo(
            810180,
            "zkLink",
            "evm",
            "https://static.particle.network/token-list/zklink/native.png",
            "zkLink Nova Mainnet",
            "Mainnet",
            "https://zklink.io",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.zklink.io",
            "https://explorer.zklink.io",
            null,
            null
        );


        public static ChainInfo XterioBNBTestnet => _xteriobnbtestnet;

        private static ChainInfo _xteriobnbtestnet = new ChainInfo(
            1637450,
            "xterio",
            "evm",
            "https://static.particle.network/token-list/xterio/native.png",
            "Xterio(BNB) Testnet",
            "Testnet",
            "https://xter.io",
            new NativeCurrency("BNB", "BNB", 18),
            "https://xterio-testnet.alt.technology",
            "https://testnet.xterscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo PlatONTestnet => _platontestnet;

        private static ChainInfo _platontestnet = new ChainInfo(
            2206132,
            "PlatON",
            "evm",
            "https://static.particle.network/token-list/platon/native.png",
            "PlatON Testnet",
            "Testnet",
            "https://www.platon.network",
            new NativeCurrency("LAT", "LAT", 18),
            "https://devnetopenapi2.platon.network/rpc",
            "https://devnet2scan.platon.network",
            null,
            "https://devnet2faucet.platon.network/faucet"
        );


        public static ChainInfo XterioETH => _xterioeth;

        private static ChainInfo _xterioeth = new ChainInfo(
            2702128,
            "xterioeth",
            "evm",
            "https://static.particle.network/token-list/xterioeth/native.png",
            "Xterio(ETH) Mainnet",
            "Mainnet",
            "https://xterscan.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://xterio-eth.alt.technology",
            "https://eth.xterscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo MantaSepolia => _mantasepolia;

        private static ChainInfo _mantasepolia = new ChainInfo(
            3441006,
            "Manta",
            "evm",
            "https://static.particle.network/token-list/manta/native.png",
            "Manta Sepolia",
            "Sepolia",
            "https://manta.network",
            new NativeCurrency("ETH", "ETH", 18),
            "https://pacific-rpc.sepolia-testnet.manta.network/http",
            "https://pacific-explorer.sepolia-testnet.manta.network",
            new List<Feature>() { new Feature("EIP1559") },
            "https://pacific-info.manta.network"
        );


        public static ChainInfo AstarzkEVMTestnet => _astarzkevmtestnet;

        private static ChainInfo _astarzkevmtestnet = new ChainInfo(
            6038361,
            "AstarZkEVM",
            "evm",
            "https://static.particle.network/token-list/astarzkevm/native.png",
            "Astar zkEVM Testnet",
            "Testnet",
            "https://astar.network",
            new NativeCurrency("Sepolia Ether", "ETH", 18),
            "https://rpc.startale.com/zkyoto",
            "https://zkyoto.explorer.startale.com",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo Zora => _zora;

        private static ChainInfo _zora = new ChainInfo(
            7777777,
            "Zora",
            "evm",
            "https://static.particle.network/token-list/zora/native.png",
            "Zora Mainnet",
            "Mainnet",
            "https://zora.energy",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.zora.energy",
            "https://explorer.zora.energy",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo EthereumSepolia => _ethereumsepolia;

        private static ChainInfo _ethereumsepolia = new ChainInfo(
            11155111,
            "Ethereum",
            "evm",
            "https://static.particle.network/token-list/ethereum/native.png",
            "Ethereum Sepolia",
            "Sepolia",
            "https://sepolia.otterscan.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://rpc.sepolia.org",
            "https://sepolia.etherscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            "https://faucet.quicknode.com/drip"
        );


        public static ChainInfo OptimismSepolia => _optimismsepolia;

        private static ChainInfo _optimismsepolia = new ChainInfo(
            11155420,
            "Optimism",
            "evm",
            "https://static.particle.network/token-list/optimism/native.png",
            "Optimism Sepolia",
            "Sepolia",
            "https://optimism.io",
            new NativeCurrency("Ether", "ETH", 18),
            "https://sepolia.optimism.io",
            "https://sepolia-optimism.etherscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Ancient8Testnet => _ancient8testnet;

        private static ChainInfo _ancient8testnet = new ChainInfo(
            28122024,
            "ancient8",
            "evm",
            "https://static.particle.network/token-list/ancient8/native.png",
            "Ancient8 Testnet",
            "Testnet",
            "https://ancient8.gg",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpcv2-testnet.ancient8.gg",
            "https://scanv2-testnet.ancient8.gg",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo CyberTestnet => _cybertestnet;

        private static ChainInfo _cybertestnet = new ChainInfo(
            111557560,
            "Cyber",
            "evm",
            "https://static.particle.network/token-list/cyber/native.png",
            "Cyber Testnet",
            "Testnet",
            "https://testnet.cyberscan.co",
            new NativeCurrency("ETH", "ETH", 18),
            "https://cyber-testnet.alt.technology",
            "https://testnet.cyberscan.co",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo PlumeTestnet => _plumetestnet;

        private static ChainInfo _plumetestnet = new ChainInfo(
            161221135,
            "Plume",
            "evm",
            "https://static.particle.network/token-list/plume/native.png",
            "Plume Testnet",
            "Testnet",
            "https://testnet-explorer.plumenetwork.xyz",
            new NativeCurrency("ETH", "ETH", 18),
            "https://testnet-rpc.plumenetwork.xyz/http",
            "https://testnet-explorer.plumenetwork.xyz",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo BlastSepolia => _blastsepolia;

        private static ChainInfo _blastsepolia = new ChainInfo(
            168587773,
            "Blast",
            "evm",
            "https://static.particle.network/token-list/blast/native.png",
            "Blast Sepolia",
            "Sepolia",
            "https://blastblockchain.com",
            new NativeCurrency("Blast Ether", "ETH", 18),
            "https://sepolia.blast.io",
            "https://testnet.blastscan.io",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Tron => _tron;

        private static ChainInfo _tron = new ChainInfo(
            728126428,
            "Tron",
            "evm",
            "https://static.particle.network/token-list/tron/native.png",
            "Tron Mainnet",
            "Mainnet",
            "https://tron.network",
            new NativeCurrency("TRX", "TRX", 6),
            "https://api.trongrid.io",
            "https://tronscan.io",
            new List<Feature>() { new Feature("ON-RAMP") },
            null
        );


        public static ChainInfo Ancient8 => _ancient8;

        private static ChainInfo _ancient8 = new ChainInfo(
            888888888,
            "ancient8",
            "evm",
            "https://static.particle.network/token-list/ancient8/native.png",
            "Ancient8 Mainnet",
            "Mainnet",
            "https://ancient8.gg",
            new NativeCurrency("ETH", "ETH", 18),
            "https://rpc.ancient8.gg",
            "https://scan.ancient8.gg",
            new List<Feature>() { new Feature("EIP1559") },
            null
        );


        public static ChainInfo Aurora => _aurora;

        private static ChainInfo _aurora = new ChainInfo(
            1313161554,
            "Aurora",
            "evm",
            "https://static.particle.network/token-list/aurora/native.png",
            "Aurora Mainnet",
            "Mainnet",
            "https://aurora.dev",
            new NativeCurrency("Ether", "ETH", 18),
            "https://mainnet.aurora.dev",
            "https://explorer.aurora.dev",
            new List<Feature>() { new Feature("SWAP") },
            null
        );


        public static ChainInfo AuroraTestnet => _auroratestnet;

        private static ChainInfo _auroratestnet = new ChainInfo(
            1313161555,
            "Aurora",
            "evm",
            "https://static.particle.network/token-list/aurora/native.png",
            "Aurora Testnet",
            "Testnet",
            "https://aurora.dev",
            new NativeCurrency("Ether", "ETH", 18),
            "https://testnet.aurora.dev",
            "https://explorer.testnet.aurora.dev",
            null,
            "https://aurora.dev/faucet"
        );


        public static ChainInfo SKALENebula => _skalenebula;

        private static ChainInfo _skalenebula = new ChainInfo(
            1482601649,
            "Nebula",
            "evm",
            "https://static.particle.network/token-list/nebula/native.png",
            "SKALE Nebula",
            "Mainnet",
            "https://mainnet.skalenodes.com",
            new NativeCurrency("sFUEL", "sFUEL", 18),
            "https://mainnet.skalenodes.com/v1/green-giddy-denebola",
            "https://green-giddy-denebola.explorer.mainnet.skalenodes.com",
            null,
            null
        );


        public static ChainInfo Harmony => _harmony;

        private static ChainInfo _harmony = new ChainInfo(
            1666600000,
            "Harmony",
            "evm",
            "https://static.particle.network/token-list/harmony/native.png",
            "Harmony Mainnet",
            "Mainnet",
            "https://www.harmony.one",
            new NativeCurrency("ONE", "ONE", 18),
            "https://api.harmony.one",
            "https://explorer.harmony.one",
            null,
            null
        );


        public static ChainInfo HarmonyTestnet => _harmonytestnet;

        private static ChainInfo _harmonytestnet = new ChainInfo(
            1666700000,
            "Harmony",
            "evm",
            "https://static.particle.network/token-list/harmony/native.png",
            "Harmony Testnet",
            "Testnet",
            "https://www.harmony.one",
            new NativeCurrency("ONE", "ONE", 18),
            "https://api.s0.b.hmny.io",
            "https://explorer.pops.one",
            null,
            "https://faucet.pops.one"
        );


        public static ChainInfo KakarotSepolia => _kakarotsepolia;

        private static ChainInfo _kakarotsepolia = new ChainInfo(
            1802203764,
            "Kakarot",
            "evm",
            "https://static.particle.network/token-list/kakarot/native.png",
            "Kakarot Sepolia",
            "Sepolia",
            "https://www.kakarot.org",
            new NativeCurrency("Ether", "ETH", 18),
            "https://sepolia-rpc.kakarot.org",
            "https://sepolia.kakarotscan.org",
            new List<Feature>() { new Feature("ERC4337") },
            null
        );


        public static ChainInfo TronShasta => _tronshasta;

        private static ChainInfo _tronshasta = new ChainInfo(
            2494104990,
            "Tron",
            "evm",
            "https://static.particle.network/token-list/tron/native.png",
            "Tron Shasta",
            "Shasta",
            "https://www.trongrid.io/shasta",
            new NativeCurrency("TRX", "TRX", 6),
            "https://api.shasta.trongrid.io",
            "https://shasta.tronscan.org",
            null,
            null
        );


        public static ChainInfo TronNile => _tronnile;

        private static ChainInfo _tronnile = new ChainInfo(
            3448148188,
            "Tron",
            "evm",
            "https://static.particle.network/token-list/tron/native.png",
            "Tron Nile",
            "Nile",
            "https://nileex.io",
            new NativeCurrency("TRX", "TRX", 6),
            "https://nile.trongrid.io",
            "https://nile.tronscan.org",
            null,
            "https://nileex.io/join/getJoinPage"
        );


        private static Dictionary<string, ChainInfo> ParticleChains = new Dictionary<string, ChainInfo>()
        {
            { "ethereum-1", Ethereum },

            { "optimism-10", Optimism },

            { "thundercore-18", ThunderCoreTestnet },

            { "elastos-20", Elastos },

            { "cronos-25", Cronos },

            { "bsc-56", BNBChain },

            { "okc-65", OKTCTestnet },

            { "okc-66", OKTC },

            { "confluxespace-71", ConfluxeSpaceTestnet },

            { "viction-88", Viction },

            { "viction-89", VictionTestnet },

            { "bsc-97", BNBChainTestnet },

            { "gnosis-100", Gnosis },

            { "solana-101", Solana },

            { "solana-102", SolanaTestnet },

            { "solana-103", SolanaDevnet },

            { "thundercore-108", ThunderCore },

            { "bob-111", BOBTestnet },

            { "fuse-122", Fuse },

            { "fuse-123", FuseTestnet },

            { "polygon-137", Polygon },

            { "manta-169", Manta },

            { "okbc-195", XLayerTestnet },

            { "okbc-196", XLayer },

            { "opbnb-204", opBNB },

            { "mapprotocol-212", MAPProtocolTestnet },

            { "bsquared-223", BSquared },

            { "fantom-250", Fantom },

            { "zksync-300", zkSyncEraSepolia },

            { "kcc-321", KCC },

            { "kcc-322", KCCTestnet },

            { "zksync-324", zkSyncEra },

            { "cronos-338", CronosTestnet },

            { "mode-919", ModeTestnet },

            { "fiveire-995", fiveire },

            { "fiveire-997", fiveireTestnet },

            { "klaytn-1001", KlaytnTestnet },

            { "confluxespace-1030", ConfluxeSpace },

            { "metis-1088", Metis },

            { "polygonzkevm-1101", PolygonzkEVM },

            { "core-1115", CoreTestnet },

            { "core-1116", Core },

            { "bsquared-1123", BSquaredTestnet },

            { "hybrid-1225", HybridTestnet },

            { "moonbeam-1284", Moonbeam },

            { "moonriver-1285", Moonriver },

            { "moonbeam-1287", MoonbeamTestnet },

            { "sei-1328", SeiTestnet },

            { "sei-1329", Sei },

            { "bevm-1501", BEVMCanary },

            { "bevm-1502", BEVMCanaryTestnet },

            { "storynetwork-1513", StoryNetworkTestnet },

            { "gravity-1625", GravityAlpha },

            { "combo-1715", ComboTestnet },

            { "soneium-1946", SoneiumMinatoTestnet },

            { "kava-2221", KavaTestnet },

            { "kava-2222", Kava },

            { "peaq-2241", PeaqKrest },

            { "polygonzkevm-2442", PolygonzkEVMCardona },

            { "ainn-2648", AILayerTestnet },

            { "ainn-2649", AILayer },

            { "gmnetwork-2777", GMNetwork },

            { "satoshivm-3109", SatoshiVMAlpha },

            { "satoshivm-3110", SatoshiVMTestnet },

            { "botanix-3636", BotanixTestnet },

            { "astarzkevm-3776", AstarzkEVMMainet },

            { "fantom-4002", FantomTestnet },

            { "merlin-4200", Merlin },

            { "iotex-4689", IoTeX },

            { "iotex-4690", IoTeXTestnet },

            { "mantle-5000", Mantle },

            { "mantle-5003", MantleSepoliaTestnet },

            { "opbnb-5611", opBNBTestnet },

            { "aura-6321", AuraTestnet },

            { "aura-6322", Aura },

            { "zetachain-7000", ZetaChain },

            { "zetachain-7001", ZetaChainTestnet },

            { "cyber-7560", Cyber },

            { "klaytn-8217", Klaytn },

            { "lorenzo-8329", Lorenzo },

            { "base-8453", Base },

            { "combo-9980", Combo },

            { "peaq-9990", PeaqAgungTestnet },

            { "gnosis-10200", GnosisTestnet },

            { "bevm-11501", BEVM },

            { "bevm-11503", BEVMTestnet },

            { "readon-12015", ReadONTestnet },

            { "immutable-13473", ImmutablezkEVMTestnet },

            { "gravity-13505", GravityAlphaTestnet },

            { "eosevm-15557", EOSEVMTestnet },

            { "ethereum-17000", EthereumHolesky },

            { "eosevm-17777", EOSEVM },

            { "mapprotocol-22776", MAPProtocol },

            { "zeroone-27827", Zeroone },

            { "mode-34443", Mode },

            { "arbitrum-42161", ArbitrumOne },

            { "arbitrum-42170", ArbitrumNova },

            { "celo-42220", Celo },

            { "oasisemerald-42261", OasisEmeraldTestnet },

            { "oasisemerald-42262", OasisEmerald },

            { "zkfair-42766", ZKFair },

            { "avalanche-43113", AvalancheTestnet },

            { "avalanche-43114", Avalanche },

            { "zkfair-43851", ZKFairTestnet },

            { "celo-44787", CeloTestnet },

            { "zircuit-48899", ZircuitTestnet },

            { "dodochain-53457", DODOChainTestnet },

            { "zeroone-56400", ZerooneTestnet },

            { "linea-59141", LineaSepolia },

            { "linea-59144", Linea },

            { "bob-60808", BOB },

            { "polygon-80002", PolygonAmoy },

            { "berachain-80084", BerachainbArtio },

            { "blast-81457", Blast },

            { "lorenzo-83291", LorenzoTestnet },

            { "base-84532", BaseSepolia },

            { "tuna-89682", TUNATestnet },

            { "xterio-112358", XterioBNB },

            { "taiko-167000", Taiko },

            { "taiko-167009", TaikoHekla },

            { "bitlayer-200810", BitlayerTestnet },

            { "bitlayer-200901", Bitlayer },

            { "duckchain-202105", DuckchainTestnet },

            { "platon-210425", PlatON },

            { "arbitrum-421614", ArbitrumSepolia },

            { "scroll-534351", ScrollSepolia },

            { "scroll-534352", Scroll },

            { "merlin-686868", MerlinTestnet },

            { "sei-713715", SeiDevnet },

            { "zklink-810180", zkLinkNova },

            { "xterio-1637450", XterioBNBTestnet },

            { "platon-2206132", PlatONTestnet },

            { "xterioeth-2702128", XterioETH },

            { "manta-3441006", MantaSepolia },

            { "astarzkevm-6038361", AstarzkEVMTestnet },

            { "zora-7777777", Zora },

            { "ethereum-11155111", EthereumSepolia },

            { "optimism-11155420", OptimismSepolia },

            { "ancient8-28122024", Ancient8Testnet },

            { "cyber-111557560", CyberTestnet },

            { "plume-161221135", PlumeTestnet },

            { "blast-168587773", BlastSepolia },

            { "tron-728126428", Tron },

            { "ancient8-888888888", Ancient8 },

            { "aurora-1313161554", Aurora },

            { "aurora-1313161555", AuroraTestnet },

            { "nebula-1482601649", SKALENebula },

            { "harmony-1666600000", Harmony },

            { "harmony-1666700000", HarmonyTestnet },

            { "kakarot-1802203764", KakarotSepolia },

            { "tron-2494104990", TronShasta },

            { "tron-3448148188", TronNile },
        };

        // template code end        [CanBeNull]
        public static ChainInfo GetChain(long chainId, string chainName)
        {
            var key = $"{chainName.ToLower()}-{chainId}";
            if (ParticleChains.ContainsKey(key))
            {
                return ParticleChains[key];
            }

            return null;
        }

        [CanBeNull]
        public static ChainInfo GetEvmChain(long chainId)
        {
            var chain = ParticleChains.Values.FirstOrDefault(it => it.ChainType == "evm" && it.Id == chainId);
            return chain;
        }

        [CanBeNull]
        public static ChainInfo GetSolanaChain(long chainId)
        {
            var chain = ParticleChains.Values.FirstOrDefault(it => it.ChainType == "solana" && it.Id == chainId);
            return chain;
        }

        private static readonly List<string> sortKeys = new List<string>
        {
            "Solana",
            "Ethereum",
            "BSC",
            "opBNB",
            "Polygon",
            "Avalanche",
            "Moonbeam",
            "Moonriver",
            "Heco",
            "Fantom",
            "Arbitrum",
            "Harmony",
            "Aurora",
            "Optimism",
            "KCC",
            "PlatON",
            "Tron",
        };

        public static List<ChainInfo> GetAllChainInfos(Func<ChainInfo, ChainInfo, int> compareFn = null)
        {
            var chains = ParticleChains.Values.ToList();
            if (compareFn != null)
            {
                chains.Sort(new Comparison<ChainInfo>(compareFn));
                return chains;
            }

            chains.Sort((a, b) =>
            {
                if (sortKeys.Contains(a.name) && sortKeys.Contains(b.name))
                {
                    if (a.name == b.name)
                    {
                        if (a.network == "Mainnet")
                        {
                            return -1;
                        }
                        else if (b.network == "Mainnet")
                        {
                            return 1;
                        }

                        return 0;
                    }
                    else if (sortKeys.IndexOf(a.name) > sortKeys.IndexOf(b.name))
                    {
                        return 1;
                    }

                    return -1;
                }
                else if (sortKeys.Contains(a.name))
                {
                    return -1;
                }
                else if (sortKeys.Contains(b.name))
                {
                    return 1;
                }
                else if (a.name == b.name)
                {
                    if (a.network == "Mainnet")
                    {
                        return -1;
                    }
                    else if (b.network == "Mainnet")
                    {
                        return 1;
                    }

                    return string.Compare(a.fullname, b.fullname, StringComparison.CurrentCulture);
                }
                else
                {
                    return string.Compare(a.name, b.name, StringComparison.CurrentCulture);
                }
            });

            return chains;
        }

        public bool IsEvmChain()
        {
            return chainType == "evm";
        }

        public bool IsSolanaChain()
        {
            return chainType == "solana";
        }

        public bool IsMainnet()
        {
            return network == "Mainnet";
        }

        public bool IsEIP1559Supported()
        {
            return features?.Any(feature => feature.Name == "EIP1559") ?? false;
        }

        public string GetParticleNode(string projectId, string projectKey)
        {
            return
                $"https://rpc.particle.network/evm-chain?chainId=${id}&projectUuid=${projectId}&projectKey=${projectKey}";
        }

        public bool IsSupportWalletConnect()
        {
            return chainType == "evm" && name != "Tron";
        }
    }

    public class NativeCurrency
    {
        public string Name;
        public string Symbol;
        public int Decimals;

        public NativeCurrency(string name, string symbol, int decimals)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Decimals = decimals;
        }
    }

    public class Feature
    {
        public string Name;

        public Feature(string name)
        {
            this.Name = name;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Network.Particle.Scripts.Model
{
    public class ChainInfo
    {
        private long id;
        private string name;
        private string chainType;
        private string icon;
        private string fullname;
        private string network;
        private string website;
        private NativeCurrency nativeCurrency;
        private string rpcUrl;
        private string blockExplorerUrl;
        private List<Feature> features;
        private string? faucetUrl;

        public long Id => id;
        public string Name => name;
        public string ChainType => chainType;
        public string Icon => icon;
        public string Fullname => fullname;
        public string Network => network;
        public string Website => website;
        public NativeCurrency NativeCurrency => nativeCurrency;
        public string RpcUrl => rpcUrl;
        public string BlockExplorerUrl => blockExplorerUrl;
        public List<Feature> Features => features;
        public string? FaucetUrl => faucetUrl;

        public ChainInfo(long id, string name, string chainType, string icon, string fullname, string network,
            string website, NativeCurrency nativeCurrency, string rpcUrl, string blockExplorerUrl,
            List<Feature> features,
            string? faucetUrl)
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
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo EthereumGoerli => _ethereumgoerli;

    private static ChainInfo _ethereumgoerli = new ChainInfo(
        5,
        "Ethereum",
        "evm",
        "https://static.particle.network/token-list/ethereum/native.png",
        "Ethereum Goerli",
        "Goerli",
        "https://goerli.net/#about",
        new NativeCurrency("Ether", "ETH", 18),
        "https://ethereum-goerli.publicnode.com",
        "https://goerli.etherscan.io",
        new List<Feature>() { new Feature("EIP1559")},
        "https://goerlifaucet.com"
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
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
        null,
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
        null,
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
        null,
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
        null,
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
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
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
            

    public static ChainInfo Heco => _heco;

    private static ChainInfo _heco = new ChainInfo(
        128,
        "Heco",
        "evm",
        "https://static.particle.network/token-list/heco/native.png",
        "Heco Mainnet",
        "Mainnet",
        "https://www.hecochain.com",
        new NativeCurrency("HT", "HT", 18),
        "https://http-mainnet.hecochain.com",
        "https://hecoinfo.com",
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        " https://pacific-explorer.manta.network",
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo X1Testnet => _x1testnet;

    private static ChainInfo _x1testnet = new ChainInfo(
        195,
        "OKBC",
        "evm",
        "https://static.particle.network/token-list/okc/native.png",
        "X1 Testnet",
        "Testnet",
        "https://www.okx.com/okbc/docs/dev/quick-start/introduction/introduction-to-okbchain",
        new NativeCurrency("OKB", "OKB", 18),
        "https://testrpc.x1.tech",
        "https://www.oklink.com/x1-test",
        null,
        "https://www.okx.com/cn/okbc/faucet"
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
        new List<Feature>() { new Feature("EIP1559")},
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
        "https://testnet.mapscan.io",
        new List<Feature>() { new Feature("EIP1559")},
        "https://faucet.mapprotocol.io"
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
        null,
        null
    );
            

    public static ChainInfo zkSyncEraTestnet => _zksynceratestnet;

    private static ChainInfo _zksynceratestnet = new ChainInfo(
        280,
        "zkSync",
        "evm",
        "https://static.particle.network/token-list/zksync/native.png",
        "zkSync Era Testnet",
        "Testnet",
        "https://era.zksync.io/docs",
        new NativeCurrency("zkSync", "ETH", 18),
        "https://zksync2-testnet.zksync.dev",
        "https://goerli.explorer.zksync.io",
        new List<Feature>() { new Feature("EIP1559")},
        "https://portal.zksync.io/faucet"
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
        "https://cronos.org/faucet"
    );
            

    public static ChainInfo OptimismGoerli => _optimismgoerli;

    private static ChainInfo _optimismgoerli = new ChainInfo(
        420,
        "Optimism",
        "evm",
        "https://static.particle.network/token-list/optimism/native.png",
        "Optimism Goerli",
        "Testnet",
        "https://optimism.io",
        new NativeCurrency("Ether", "ETH", 18),
        "https://goerli.optimism.io",
        "https://goerli-optimism.etherscan.io",
        new List<Feature>() { new Feature("EIP1559")},
        "https://faucet.triangleplatform.com/optimism/goerli"
    );
            

    public static ChainInfo PGN => _pgn;

    private static ChainInfo _pgn = new ChainInfo(
        424,
        "PGN",
        "evm",
        "https://static.particle.network/token-list/pgn/native.png",
        "PGN Mainnet",
        "Mainnet",
        "https://publicgoods.network",
        new NativeCurrency("ETH", "ETH", 18),
        "https://sepolia.publicgoods.network",
        "https://explorer.publicgoods.network",
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo MetisGoerli => _metisgoerli;

    private static ChainInfo _metisgoerli = new ChainInfo(
        599,
        "Metis",
        "evm",
        "https://static.particle.network/token-list/metis/native.png",
        "Metis Goerli",
        "Goerli",
        "https://www.metis.io",
        new NativeCurrency("Metis", "METIS", 18),
        "https://goerli.gateway.metisdevops.link",
        "https://goerli.explorer.metisdevops.link",
        null,
        "https://goerli.faucet.metisdevops.link"
    );
            

    public static ChainInfo ZoraGoerli => _zoragoerli;

    private static ChainInfo _zoragoerli = new ChainInfo(
        999,
        "Zora",
        "evm",
        "https://static.particle.network/token-list/zora/native.png",
        "Zora Goerli",
        "Goerli",
        "https://testnet.wanscan.org",
        new NativeCurrency("ETH", "ETH", 18),
        "https://testnet.rpc.zora.energy",
        "https://testnet.explorer.zora.energy",
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
        null
    );
            

    public static ChainInfo BSquaredTestnet => _bsquaredtestnet;

    private static ChainInfo _bsquaredtestnet = new ChainInfo(
        1102,
        "BSquared",
        "evm",
        "https://static.particle.network/token-list/bsquared/native.png",
        "B² Network Testnet",
        "Testnet",
        "https://www.bsquared.network",
        new NativeCurrency("BTC", "BTC", 18),
        "https://haven-rpc.bsquared.network",
        "https://haven-explorer.bsquared.network",
        null,
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
        "https://apps.moonbeam.network/moonbase-alpha/faucet"
    );
            

    public static ChainInfo PolygonzkEVMTestnet => _polygonzkevmtestnet;

    private static ChainInfo _polygonzkevmtestnet = new ChainInfo(
        1442,
        "PolygonZkEVM",
        "evm",
        "https://static.particle.network/token-list/polygonzkevm/native.png",
        "Polygon zkEVM Testnet",
        "Testnet",
        "https://polygon.technology/solutions/polygon-zkevm",
        new NativeCurrency("ETH", "ETH", 18),
        "https://rpc.public.zkevm-test.net",
        "https://testnet-zkevm.polygonscan.com",
        null,
        "https://public.zkevm-test.net"
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
        null,
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
        null,
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
        "http://testnet.kavascan.com",
        new List<Feature>() { new Feature("undefined")},
        null
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
        null,
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
        null,
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
        null,
        null
    );
            

    public static ChainInfo MantleTestnet => _mantletestnet;

    private static ChainInfo _mantletestnet = new ChainInfo(
        5001,
        "Mantle",
        "evm",
        "https://static.particle.network/token-list/mantle/native.png",
        "Mantle Testnet",
        "Testnet",
        "https://mantle.xyz",
        new NativeCurrency("MNT", "MNT", 18),
        "https://rpc.testnet.mantle.xyz",
        "https://explorer.testnet.mantle.xyz",
        null,
        "https://faucet.testnet.mantle.xyz"
    );
            

    public static ChainInfo opBNBTestnet => _opbnbtestnet;

    private static ChainInfo _opbnbtestnet = new ChainInfo(
        5611,
        "opBNB",
        "evm",
        "https://static.particle.network/token-list/bsc/native.png",
        "opBNB Testnet",
        "Testnet",
        "https://opbnb.bnbchain.org",
        new NativeCurrency("BNB", "BNB", 18),
        "https://opbnb-testnet-rpc.bnbchain.org",
        "https://opbnb-testnet.bscscan.com",
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
        "https://labs.zetachain.com/get-zeta"
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
        null,
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
        "https://gnosisfaucet.com"
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
        null,
        null
    );
            

    public static ChainInfo LumozzkEVMTestnet => _lumozzkevmtestnet;

    private static ChainInfo _lumozzkevmtestnet = new ChainInfo(
        12008,
        "Lumoz",
        "evm",
        "https://static.particle.network/token-list/opside/native.png",
        "Lumoz zkEVM Testnet",
        "Testnet",
        "https://lumoz.org",
        new NativeCurrency("Lumoz", "MOZ", 18),
        "https://alpha-zkrollup-rpc.lumoz.org/public",
        "https://public.zkevm.lumoz.info",
        null,
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
        null,
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
        new List<Feature>() { new Feature("undefined")},
        null
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo LumiBitTestnet => _lumibittestnet;

    private static ChainInfo _lumibittestnet = new ChainInfo(
        28206,
        "lumibit",
        "evm",
        "https://static.particle.network/token-list/lumibit/native.png",
        "LumiBit Testnet",
        "Testnet",
        null,
        new NativeCurrency("BTC", "BTC", 18),
        "https://test-rpc.lumibit.org",
        "https://test-scan.lumibit.org",
        null,
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
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
        null,
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
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
        " https://celo.org/developers/faucet"
    );
            

    public static ChainInfo PGNSepolia => _pgnsepolia;

    private static ChainInfo _pgnsepolia = new ChainInfo(
        58008,
        "PGN",
        "evm",
        "https://static.particle.network/token-list/pgn/native.png",
        "PGN Sepolia",
        "Sepolia",
        "https://publicgoods.network",
        new NativeCurrency("ETH", "ETH", 18),
        "https://sepolia.publicgoods.network",
        "https://explorer.sepolia.publicgoods.network",
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo LineaGoerli => _lineagoerli;

    private static ChainInfo _lineagoerli = new ChainInfo(
        59140,
        "Linea",
        "evm",
        "https://static.particle.network/token-list/linea/native.png",
        "Linea Goerli",
        "Goerli",
        "https://linea.build",
        new NativeCurrency("ETH", "ETH", 18),
        "https://rpc.goerli.linea.build",
        "https://goerli.lineascan.build",
        new List<Feature>() { new Feature("EIP1559")},
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
        "https://linea-mainnet.infura.io/v3/9aa3d95b3bc440fa88ea12eaa4456161",
        "https://lineascan.build",
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo PolygonMumbai => _polygonmumbai;

    private static ChainInfo _polygonmumbai = new ChainInfo(
        80001,
        "Polygon",
        "evm",
        "https://static.particle.network/token-list/polygon/native.png",
        "Polygon Mumbai",
        "Mumbai",
        "https://polygon.technology",
        new NativeCurrency("MATIC", "MATIC", 18),
        "https://polygon-mumbai.gateway.tenderly.co",
        "https://mumbai.polygonscan.com",
        new List<Feature>() { new Feature("EIP1559")},
        "https://faucet.polygon.technology"
    );
            

    public static ChainInfo BerachainArtio => _berachainartio;

    private static ChainInfo _berachainartio = new ChainInfo(
        80085,
        "Berachain",
        "evm",
        "https://static.particle.network/token-list/berachain/native.png",
        "Berachain Artio",
        "Artio",
        "https://www.berachain.com",
        new NativeCurrency("BERA", "BERA", 18),
        "https://artio.rpc.berachain.com",
        "https://artio.beratrail.io",
        null,
        "https://artio.faucet.berachain.com"
    );
            

    public static ChainInfo BaseGoerli => _basegoerli;

    private static ChainInfo _basegoerli = new ChainInfo(
        84531,
        "Base",
        "evm",
        "https://static.particle.network/token-list/base/native.png",
        "Base Goerli",
        "Goerli",
        "https://base.org",
        new NativeCurrency("ETH", "ETH", 18),
        "https://base-goerli.public.blastapi.io",
        "https://goerli.basescan.org",
        new List<Feature>() { new Feature("EIP1559")},
        "https://bridge.base.org/deposit"
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
        new List<Feature>() { new Feature("EIP1559")},
        "https://bridge.base.org/deposit"
    );
            

    public static ChainInfo ComboTestnet => _combotestnet;

    private static ChainInfo _combotestnet = new ChainInfo(
        91715,
        "Combo",
        "evm",
        "https://static.particle.network/token-list/combo/native.png",
        "Combo Testnet",
        "Testnet",
        "https://docs.combonetwork.io",
        new NativeCurrency("BNB", "BNB", 18),
        "https://test-rpc.combonetwork.io",
        "https://combotrace-testnet.nodereal.io",
        new List<Feature>() { new Feature("EIP1559")},
        null
    );
            

    public static ChainInfo TaikoKatla => _taikokatla;

    private static ChainInfo _taikokatla = new ChainInfo(
        167008,
        "Taiko",
        "evm",
        "https://static.particle.network/token-list/taiko/native.png",
        "Taiko Katla",
        "Katla",
        "https://taiko.xyz",
        new NativeCurrency("Ether", "ETH", 18),
        "https://rpc.katla.taiko.xyz",
        "https://explorer.katla.taiko.xyz",
        new List<Feature>() { new Feature("EIP1559")},
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
            

    public static ChainInfo ArbitrumGoerli => _arbitrumgoerli;

    private static ChainInfo _arbitrumgoerli = new ChainInfo(
        421613,
        "Arbitrum",
        "evm",
        "https://static.particle.network/token-list/arbitrum/native.png",
        "Arbitrum Goerli",
        "Goerli",
        "https://arbitrum.io",
        new NativeCurrency("Arbitrum Gorli Ether", "AGOR", 18),
        "https://goerli-rollup.arbitrum.io/rpc",
        "https://goerli.arbiscan.io",
        new List<Feature>() { new Feature("EIP1559")},
        "https://faucet.triangleplatform.com/arbitrum/goerli"
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
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
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
        null,
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
        "https://testnet-scan.merlinchain.io/",
        null,
        null
    );
            

    public static ChainInfo AstarzkEVMTestnet => _astarzkevmtestnet;

    private static ChainInfo _astarzkevmtestnet = new ChainInfo(
        1261120,
        "AstarZkEVM",
        "evm",
        "https://static.particle.network/token-list/astarzkevm/native.png",
        "Astar zkEVM Testnet",
        "Testnet",
        "https://astar.network",
        new NativeCurrency("Sepolia Ether", "ETH", 18),
        "https://rpc.zkatana.gelato.digital",
        "https://zkatana.blockscout.com",
        null,
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
            

    public static ChainInfo MantaTestnet => _mantatestnet;

    private static ChainInfo _mantatestnet = new ChainInfo(
        3441005,
        "Manta",
        "evm",
        "https://static.particle.network/token-list/manta/native.png",
        "Manta Testnet",
        "Testnet",
        "https://manta.network",
        new NativeCurrency("ETH", "ETH", 18),
        "https://pacific-rpc.testnet.manta.network/http",
        "https://pacific-explorer.testnet.manta.network",
        new List<Feature>() { new Feature("EIP1559")},
        "https://pacific-info.manta.network"
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
        new List<Feature>() { new Feature("EIP1559")},
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
        "https://eth-sepolia.g.alchemy.com/v2/demo",
        "https://sepolia.etherscan.io",
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        new List<Feature>() { new Feature("EIP1559")},
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
        null,
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
        null,
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
        null,
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
            

    private static Dictionary < string, ChainInfo > ParticleChains = new Dictionary<string, ChainInfo > ()
    {
                
        {"ethereum-1" , Ethereum },
            
        {"ethereum-5" , EthereumGoerli },
            
        {"optimism-10" , Optimism },
            
        {"thundercore-18" , ThunderCoreTestnet },
            
        {"cronos-25" , Cronos },
            
        {"bsc-56" , BNBChain },
            
        {"okc-65" , OKTCTestnet },
            
        {"okc-66" , OKTC },
            
        {"confluxespace-71" , ConfluxeSpaceTestnet },
            
        {"viction-88" , Viction },
            
        {"viction-89" , VictionTestnet },
            
        {"bsc-97" , BNBChainTestnet },
            
        {"gnosis-100" , Gnosis },
            
        {"solana-101" , Solana },
            
        {"solana-102" , SolanaTestnet },
            
        {"solana-103" , SolanaDevnet },
            
        {"thundercore-108" , ThunderCore },
            
        {"heco-128" , Heco },
            
        {"polygon-137" , Polygon },
            
        {"manta-169" , Manta },
            
        {"okbc-195" , X1Testnet },
            
        {"opbnb-204" , opBNB },
            
        {"mapprotocol-212" , MAPProtocolTestnet },
            
        {"fantom-250" , Fantom },
            
        {"zksync-280" , zkSyncEraTestnet },
            
        {"zksync-300" , zkSyncEraSepolia },
            
        {"kcc-321" , KCC },
            
        {"kcc-322" , KCCTestnet },
            
        {"zksync-324" , zkSyncEra },
            
        {"cronos-338" , CronosTestnet },
            
        {"optimism-420" , OptimismGoerli },
            
        {"pgn-424" , PGN },
            
        {"metis-599" , MetisGoerli },
            
        {"zora-999" , ZoraGoerli },
            
        {"klaytn-1001" , KlaytnTestnet },
            
        {"confluxespace-1030" , ConfluxeSpace },
            
        {"metis-1088" , Metis },
            
        {"polygonzkevm-1101" , PolygonzkEVM },
            
        {"bsquared-1102" , BSquaredTestnet },
            
        {"moonbeam-1284" , Moonbeam },
            
        {"moonriver-1285" , Moonriver },
            
        {"moonbeam-1287" , MoonbeamTestnet },
            
        {"polygonzkevm-1442" , PolygonzkEVMTestnet },
            
        {"bevm-1501" , BEVMCanary },
            
        {"bevm-1502" , BEVMCanaryTestnet },
            
        {"kava-2221" , KavaTestnet },
            
        {"kava-2222" , Kava },
            
        {"fantom-4002" , FantomTestnet },
            
        {"merlin-4200" , Merlin },
            
        {"mantle-5000" , Mantle },
            
        {"mantle-5001" , MantleTestnet },
            
        {"opbnb-5611" , opBNBTestnet },
            
        {"zetachain-7000" , ZetaChain },
            
        {"zetachain-7001" , ZetaChainTestnet },
            
        {"klaytn-8217" , Klaytn },
            
        {"base-8453" , Base },
            
        {"combo-9980" , Combo },
            
        {"gnosis-10200" , GnosisTestnet },
            
        {"bevm-11503" , BEVMTestnet },
            
        {"lumoz-12008" , LumozzkEVMTestnet },
            
        {"readon-12015" , ReadONTestnet },
            
        {"eosevm-15557" , EOSEVMTestnet },
            
        {"ethereum-17000" , EthereumHolesky },
            
        {"eosevm-17777" , EOSEVM },
            
        {"mapprotocol-22776" , MAPProtocol },
            
        {"lumibit-28206" , LumiBitTestnet },
            
        {"arbitrum-42161" , ArbitrumOne },
            
        {"arbitrum-42170" , ArbitrumNova },
            
        {"celo-42220" , Celo },
            
        {"oasisemerald-42261" , OasisEmeraldTestnet },
            
        {"oasisemerald-42262" , OasisEmerald },
            
        {"zkfair-42766" , ZKFair },
            
        {"avalanche-43113" , AvalancheTestnet },
            
        {"avalanche-43114" , Avalanche },
            
        {"zkfair-43851" , ZKFairTestnet },
            
        {"celo-44787" , CeloTestnet },
            
        {"pgn-58008" , PGNSepolia },
            
        {"linea-59140" , LineaGoerli },
            
        {"linea-59144" , Linea },
            
        {"polygon-80001" , PolygonMumbai },
            
        {"berachain-80085" , BerachainArtio },
            
        {"base-84531" , BaseGoerli },
            
        {"base-84532" , BaseSepolia },
            
        {"combo-91715" , ComboTestnet },
            
        {"taiko-167008" , TaikoKatla },
            
        {"platon-210425" , PlatON },
            
        {"arbitrum-421613" , ArbitrumGoerli },
            
        {"arbitrum-421614" , ArbitrumSepolia },
            
        {"scroll-534351" , ScrollSepolia },
            
        {"scroll-534352" , Scroll },
            
        {"merlin-686868" , MerlinTestnet },
            
        {"astarzkevm-1261120" , AstarzkEVMTestnet },
            
        {"platon-2206132" , PlatONTestnet },
            
        {"manta-3441005" , MantaTestnet },
            
        {"zora-7777777" , Zora },
            
        {"ethereum-11155111" , EthereumSepolia },
            
        {"optimism-11155420" , OptimismSepolia },
            
        {"ancient8-28122024" , Ancient8Testnet },
            
        {"blast-168587773" , BlastSepolia },
            
        {"tron-728126428" , Tron },
            
        {"aurora-1313161554" , Aurora },
            
        {"aurora-1313161555" , AuroraTestnet },
            
        {"nebula-1482601649" , SKALENebula },
            
        {"harmony-1666600000" , Harmony },
            
        {"harmony-1666700000" , HarmonyTestnet },
            
        {"tron-2494104990" , TronShasta },
            
        {"tron-3448148188" , TronNile },
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

        public static List<ChainInfo> GetAllChains()
        {
            return ParticleChains.Values.ToList();
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
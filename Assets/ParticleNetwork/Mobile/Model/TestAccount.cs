using System.Numerics;
using UnityEngine;

namespace Network.Particle.Scripts.Model
{
    public class TestAccount
    {
        public string PublicAddress;

        public string PrivateKey;

        public string Mnemonic;

        public string TokenContractAddress;

        public BigInteger Amount;

        public string NFTContractAddress;

        public string NFTTokenId;

        public string ReceiverAddress;

        public int ChainId;

        public TestAccount(string publicAddress, string privateKey, string mnemonic, string tokenContractAddress,
            BigInteger amount, string nftContractAddress, string nftTokenId, string receiverAddress, int ChainId)
        {
            this.PublicAddress = publicAddress;
            this.PrivateKey = privateKey;
            this.Mnemonic = mnemonic;
            this.TokenContractAddress = tokenContractAddress;
            this.Amount = amount;
            this.NFTContractAddress = nftContractAddress;
            this.NFTTokenId = nftTokenId;
            this.ReceiverAddress = receiverAddress;
            this.ChainId = ChainId;
        }

        /// <summary>
        /// This is a evm test account with some test token, you can use this account do all test cases.
        /// </summary>
        public static TestAccount EVM = new TestAccount(
            "0x2648cfE97e33345300Db8154670347b08643570b",
            "eacd18277e3cfca6446801b7587c9d787d5ee5d93f6a38752f7d94eddadc469e",
            "hood result social fetch pet code check yard school jealous trick lazy",
            "0x326C977E6efc84E512bB9C30f76E30c160eD06FB",
            BigInteger.Parse("10000000000000000"),
            "0xD000F000Aa1F8accbd5815056Ea32A54777b2Fc4",
            "1412",
            "0xAC6d81182998EA5c196a4424EA6AB250C7eb175b",
            5);
        
        /// <summary>
        /// This is a solana test account with some test token, you can use this account do all test cases.
        /// </summary>
        public static TestAccount Solana = new TestAccount(
            "Cnbvi3bjBkYbWPVHG4dp6GAZD3qUp8nj9YsVt2PEgH77",
            "5fBYPZdP5nqH5DSAjgjMi4aSf113m5PuavakojZ7C9svt1i8vyq26pXpEf1Suivg91TUAp7TX1pqK49rgXQfAAjT",
            "vacant focus country eye wine where lady doll boat sort ticket grab",
            "GobzzzFQsFAHPvmwT42rLockfUCeV3iutEkK218BxT8K",
            BigInteger.Parse("100000000"),
            "HLyQCnxBo5SGmYBv3aRCH9tPqT9TvexHY2JaGnqvfWuw",
            "",
            "9LR6zGAFB3UJcLg9tWBQJxEJCbZh2UTnSU14RBxsK1ZN",
            103);
    }
}
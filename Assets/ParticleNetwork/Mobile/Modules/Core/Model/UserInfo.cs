using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Model
{
    public enum WalletChain
    {
        Solana,
        Evm
    }

    public static class WalletChainExt
    {
        public static string GetString(this WalletChain walletChain)
        {
            switch (walletChain)
            {
                case WalletChain.Solana:
                    return "solana";
                case WalletChain.Evm:
                    return "evm_chain";
                default:
                    throw new ArgumentOutOfRangeException(nameof(walletChain), walletChain, null);
            }
        }
    }

    [JsonObject]
    public class UserInfo
    {
        [JsonProperty(PropertyName = "token")] public string Token;

        [JsonProperty(PropertyName = "uuid")] public string Uuid;

        [JsonProperty(PropertyName = "wallets")]
        public List<Wallet> Wallets;

        [JsonProperty(PropertyName = "phone")] [CanBeNull]
        public string Phone;

        [JsonProperty(PropertyName = "email")] [CanBeNull]
        public string Email;

        [JsonProperty(PropertyName = "name")] [CanBeNull]
        public string Name;

        [JsonProperty(PropertyName = "avatar")] [CanBeNull]
        public string Avater;

        [JsonProperty(PropertyName = "facebook_id")] [CanBeNull]
        public string FacebookId;

        [JsonProperty(PropertyName = "facebook_email")] [CanBeNull]
        public string FacebookEmail;

        [JsonProperty(PropertyName = "apple_id")] [CanBeNull]
        public string AppleId;

        [JsonProperty(PropertyName = "apple_email")] [CanBeNull]
        public string AppleEmail;

        [JsonProperty(PropertyName = "google_id")] [CanBeNull]
        public string GoogleId;

        [JsonProperty(PropertyName = "google_email")] [CanBeNull]
        public string GoogleEmail;

        [JsonProperty(PropertyName = "twitter_id")] [CanBeNull]
        public string TwitterId;

        [JsonProperty(PropertyName = "twitter_email")] [CanBeNull]
        public string TwitterEmail;

        [JsonProperty(PropertyName = "discord_id")] [CanBeNull]
        public string DiscordId;

        [JsonProperty(PropertyName = "discord_email")] [CanBeNull]
        public string DiscordEmail;

        [JsonProperty(PropertyName = "github_id")] [CanBeNull]
        public string GithubId;

        [JsonProperty(PropertyName = "github_email")] [CanBeNull]
        public string GithubEmail;

        [JsonProperty(PropertyName = "twitch_id")] [CanBeNull]
        public string TwitchId;

        [JsonProperty(PropertyName = "twitch_email")] [CanBeNull]
        public string TwitchEmail;

        [JsonProperty(PropertyName = "microsoft_id")] [CanBeNull]
        public string MicrosoftId;

        [JsonProperty(PropertyName = "microsoft_email")] [CanBeNull]
        public string MicrosoftEmail;

        [JsonProperty(PropertyName = "linkedin_id")] [CanBeNull]
        public string LinkedinId;

        [JsonProperty(PropertyName = "linkedin_email")] [CanBeNull]
        public string LinkedinEmail;

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt;

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt;

        public UserInfo()
        {
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        //getWallet
        public Wallet GetWallet(WalletChain walletChain)
        {
            var wallet = Wallets.FirstOrDefault(x => x.ChainName == walletChain.GetString());
            return wallet;
        }
    }

    [JsonObject]
    public class Wallet
    {
        [JsonProperty(PropertyName = "uuid")] public string Uuid;

        [JsonProperty(PropertyName = "publicAddress")]
        public string PublicAddress;

        [JsonProperty(PropertyName = "chainName")]
        public string ChainName;

        [JsonProperty(PropertyName = "public_address")]
        private string PublicAddress2
        {
            set => PublicAddress = value;
        }

        [JsonProperty(PropertyName = "chain_name")]
        private string ChainName2
        {
            set => ChainName = value;
        }


        [JsonProperty(PropertyName = "chain")]
        private string ChainName3
        {
            set => ChainName = value;
        }

        [JsonProperty(PropertyName = "private_key")]
        public string PrivateKey;
    }
}
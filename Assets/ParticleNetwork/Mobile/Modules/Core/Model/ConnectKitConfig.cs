using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Network.Particle.Scripts.Model
{
    public enum ConnectOption
    {
        Email,
        Phone,
        Social,
        Wallet
    }

    public enum EnableSocialProvider
    {
        Google,
        Facebook,
        Apple,
        Twitter,
        Discord,
        Github,
        Twitch,
        Microsoft,
        Linkedin,
    }

    public enum EnableWallet
    {
        MetaMask,
        Rainbow,
        Trust,
        ImToken,
        Bitget,
        OKX,
        Phantom,
        WalletConnect
    }

    public enum EnableWalletLabel
    {
        Recommended,
        Popular,
        None
    }

    public class AdditionalLayoutOptions
    {
        public bool IsCollapseWalletList;
        public bool IsSplitEmailAndSocial;
        public bool IsSplitEmailAndPhone;
        public bool IsHideContinueButton;

        public AdditionalLayoutOptions(bool isCollapseWalletList, bool isSplitEmailAndSocial, bool isSplitEmailAndPhone,
            bool isHideContinueButton)
        {
            this.IsCollapseWalletList = isCollapseWalletList;
            this.IsSplitEmailAndSocial = isSplitEmailAndSocial;
            this.IsSplitEmailAndPhone = isSplitEmailAndPhone;
            this.IsHideContinueButton = isHideContinueButton;
        }
    }

    public class EnableWalletProvider
    {
        public EnableWallet EnableWallet;
        public EnableWalletLabel Label;

        public EnableWalletProvider(EnableWallet enableWallet, EnableWalletLabel label)
        {
            this.EnableWallet = enableWallet;
            this.Label = label;
        }
    }


    public class ConnectKitConfig
    {
        /// Connect options, support `Email`, `Phone`, `Socoal` and `Wallet`, the sort order is used for connect kit login UI. 
        public List<ConnectOption> ConnectOptions;

        /// Social providers, support `Google`, `Apple` and other social options, the sort order is used for connect kit login UI.
        [CanBeNull] public List<EnableSocialProvider> SocialProviders;

        /// Wallet providers, support `Metamask`, `Trust` and other wallet options, the sort order is used for connect kit login UI.
        [CanBeNull] public List<EnableWalletProvider> WalletProviders;

        /// Layout options.
        public AdditionalLayoutOptions AdditionalLayoutOptions;

        /// Project icon, supports base64 string and url.
        [CanBeNull] public String Logo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectOptions"></param>
        /// <param name="socialProviders"></param>
        /// <param name="walletProviders"></param>
        /// <param name="additionalLayoutOptions"></param>
        /// <param name="logo"></param>
        public ConnectKitConfig(List<ConnectOption> connectOptions, List<EnableSocialProvider> socialProviders,
            List<EnableWalletProvider> walletProviders, AdditionalLayoutOptions additionalLayoutOptions, String logo)
        {
            this.ConnectOptions = connectOptions;
            this.SocialProviders = socialProviders;
            this.WalletProviders = walletProviders;
            this.AdditionalLayoutOptions = additionalLayoutOptions;
            this.Logo = logo;
        }
    }
}
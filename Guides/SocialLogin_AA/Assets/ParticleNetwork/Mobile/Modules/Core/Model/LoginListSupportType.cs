namespace Network.Particle.Scripts.Model
{
    public enum LoginListPageSupportType
    {
        all,
        /// Login with email
        email,
        /// Login with phone
        phone,
        /// Login with google
        google,
        /// Login with facebook
        facebook,
        /// Login with apple
        apple,
        /// Login with discord
        discord,
        /// Login with github
        github,
        /// Login with twitch
        twitch,
        /// Login with microsoft
        microsoft,
        /// Login with linkedin
        linkedin,
        /// Login with private key or mnemonic
        privateKey,
        /// Login with metamask
        metamask,
        /// Login with rainbow
        rainbow,
        /// Login with trust
        trust,
        /// Login with imtoken
        imtoken,
        /// Login with bitkeep
        bitkeep,
        /// Login with wallet connect qrcode
        walletConnect,
        /// Login with phantom
        phantom,
        /// Login with gnosis safe
        gnosis,
        /// Login with twittwer
        twitter,
    }
}
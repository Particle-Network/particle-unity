namespace Network.Particle.Scripts.Model
{
    public enum LoginType
    {
        EMAIL,
        PHONE,
        GOOGLE,
        FACEBOOK,
        APPLE,
        TWITTER,
        DISCORD,
        GITHUB,
        TWITCH,
        MICROSOFT,
        LINKEDIN,
        JWT,
    }

    public enum SupportLoginType
    {
        PHONE,
        EMAIL,
        GOOGLE,
        APPLE,
        FACEBOOK,
        DISCORD,
        GITHUB,
        TWITCH,
        MICROSOFT,
        LINKEDIN,
        TWITTER,
    }

    public enum SocialLoginPrompt
    {
        None,
        Consent,
        SelectAccount,
    }
}
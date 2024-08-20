namespace Network.Particle.Scripts.Model
{
    public enum LoginType
    {
        Email,
        Phone,
        Google,
        Facebook,
        Apple,
        Twitter,
        Discord,
        Github,
        Twitch,
        Microsoft,
        Linkedin,
        Jwt,
    }

    public enum SupportLoginType
    {
        Email,
        Phone,
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

    public enum SocialLoginPrompt
    {
        None,
        Consent,
        SelectAccount,
    }
}
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
    }

    public enum SupportAuthType
    {
        NONE = 1 << 0, // 1
        APPLE = 1 << 1, // 2
        GOOGLE = 1 << 2, // 4
        FACEBOOK = 1 << 3, // 8
        DISCORD = 1 << 4, // 16
        GITHUB = 1 << 5, // 32
        TWITCH = 1 << 6, // 64
        MICROSOFT = 1 << 7, // 128
        LINKEDIN = 1 << 8, // 256
        ALL = (1 << 9) - 1, // 511
    }
}
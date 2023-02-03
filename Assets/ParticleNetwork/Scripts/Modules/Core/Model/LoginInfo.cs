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
        PHONE = 1 << 9, // 512
        EMAIL = 1 << 10, // 1024
        ALL = (1 << 11) - 1, // 2048 - 1 
    }
}
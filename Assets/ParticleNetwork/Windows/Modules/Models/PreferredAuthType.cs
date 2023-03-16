#if !UNITY_ANDROID && !UNITY_IOS
namespace Particle.Windows.Modules.Models
{
    public enum PreferredAuthType
    {
        email,
        phone,
        jwt,
    }
}
#endif
#if !UNITY_ANDROID && !UNITY_IOS
namespace Particle.Windows.Modules.Models
{
    public enum SignTypedDataVersion
    {
        Default,
        v1,
        v3,
        v4,
        v4Unique
    }
}
#endif
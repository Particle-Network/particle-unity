#if !UNITY_ANDROID && !UNITY_IOS
namespace Particle.Windows.Modules.Models
{
    public enum SignMethod
    {
        // Evm
        eth_sendTransaction,
        eth_signTypedData,
        eth_signTypedData_v1,
        eth_signTypedData_v3,
        eth_signTypedData_v4,
        personal_sign,

        // Solana
        signTransaction,
        signAndSendTransaction,
        signMessage,
        signAllTransactions,
    }
}
#endif
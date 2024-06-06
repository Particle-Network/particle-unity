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
        eth_signTypedData_v4_uniq,
        personal_sign,
        personal_sign_uniq,

        // Solana
        signTransaction,
        signAndSendTransaction,
        signMessage,
        signAllTransactions,
    }
}
#endif
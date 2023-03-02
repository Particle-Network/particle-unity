namespace Particle.Windows.Modules.Models
{
    public enum SignMethod
    {
        // Evm
        eth_SendTransaction,
        eth_SignTypedData,
        eth_SignTypedData_v1,
        eth_SignTypedData_v3,
        eth_SignTypedData_v4,
        personal_Sign,

        // Solana
        signTransaction,
        signAndSendTransaction,
        signMessage,
        signAllTransactions,
    }
}
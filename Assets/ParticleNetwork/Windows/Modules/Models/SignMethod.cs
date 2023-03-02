namespace Particle.Windows.Modules.Models
{
    public enum SignMethod
    {
        // Evm
        eth_SendTransaction,
        eth_SignTypedData,
        eth_SignTypedData1,
        eth_SignTypedData3,
        eth_SignTypedData4,
        personal_Sign,

        // Solana
        signTransaction,
        signAndSendTransaction,
        signMessage,
        signAllTransactions,
    }
}
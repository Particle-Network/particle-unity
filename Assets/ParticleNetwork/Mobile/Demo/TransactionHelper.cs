using System.Numerics;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json.Linq;

namespace Network.Particle.Scripts.Test
{
    public class TransactionHelper
    {
        public static async Task<string> GetSolanaTransacion()
        {
            string sender = ""; // GetAddress();
            string receiver = "BBBsMq9cEgRf9jeuXqd6QFueyRDhNwykYz63s1vwSCBZ";
            long amount = 10000000;
            SerializeSOLTransReq req = new SerializeSOLTransReq(sender, receiver, amount);
            var result = await SolanaService.SerializeSOLTransaction(req);

            var resultData = JObject.Parse(result);
            var transaction = (string)resultData["result"]["transaction"]["serialized"];
            return transaction;
        }

        public static async Task<string> GetEVMTransacion()
        {
            // mock send some chain link token from send to receiver.
            string from = ""; //GetAddress();
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
        }
        
        public static async Task<string> GetEVMTransactionWithConnect()
        {
            string from = "0x498c9b8379E2e16953a7b1FF94ea11893d09A3Ed";
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
        }
    }
}
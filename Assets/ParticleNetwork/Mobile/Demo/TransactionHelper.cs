using System.Numerics;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json.Linq;

namespace Network.Particle.Scripts.Test
{
    public class TransactionHelper
    {
        public static async Task<string> GetSolanaTransacion(string sender)
        {
            string receiver = "BBBsMq9cEgRf9jeuXqd6QFueyRDhNwykYz63s1vwSCBZ";
            long amount = 10000000;
            SerializeSOLTransReq req = new SerializeSOLTransReq(sender, receiver, amount);
            var result = await SolanaService.SerializeSOLTransaction(req);

            var resultData = JObject.Parse(result);
            var transaction = (string)resultData["result"]["transaction"]["serialized"];
            return transaction;
        }

        public static async Task<string> GetEVMTransacion(string sender)
        {
            string from = sender;
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
        }
        
        public static async Task<string> GetEVMTransactionWithConnect(string sender)
        {
            string from = sender;
            string receiver = TestAccount.EVM.ReceiverAddress;
            string contractAddress = TestAccount.EVM.TokenContractAddress;
            BigInteger amount = TestAccount.EVM.Amount;
            var dataResult = await EvmService.Erc20Transfer(contractAddress, receiver, amount);
            var data = (string)JObject.Parse(dataResult)["result"];
            return await EvmService.CreateTransaction(from, data, amount, receiver, true);
        }
    }
}
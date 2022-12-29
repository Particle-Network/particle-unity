using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using Newtonsoft.Json;

namespace Network.Particle.Scripts.Core.UnityEditorTestMode
{
      /// <summary>
    /// only used for debug in unity editor
    /// in the editor,with login api, you can get a test private key
    /// you can call sign functions to get a signature
    /// </summary>
    public class DevModeService
    {
        public async static void Login()
        {
#if UNITY_EDITOR
            string path = "testmode/login";
            var result = await Request(path, "", new object[] { });
            ParticleAuthService.Instance.LoginCallBack(result);
#endif
        }

        public static void Logout()
        {
#if UNITY_EDITOR
            PersistTools.Clear();
#endif
        }

        public async static void SolanaSignTransactions(string[] transactions)
        {
#if UNITY_EDITOR
            string path = "testmode/solana/sign_transactions";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["private_key"] = ParticleNetwork.GetPrivateKey();
            dict["transactions"] = transactions;
            var json = JsonConvert.SerializeObject(dict);
            var result = await Request(path, json);
            ParticleAuthService.Instance.SignTransactionCallBack(result);
#endif
        }

        public async static void SolanaSignMessages(string[] messages)
        {
#if UNITY_EDITOR
            string path = "testmode/solana/sign_messages";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["private_key"] = ParticleNetwork.GetPrivateKey();
            dict["messages"] = messages;
            var json = JsonConvert.SerializeObject(dict);
            var result = await Request(path, json);
            ParticleAuthService.Instance.SignMessageCallBack(result);
#endif
        }

        public async static void EvmSignTransactions(string[] transactions)
        {
#if UNITY_EDITOR
            string path = "testmode/evm-chain/sign_transactions";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["private_key"] = ParticleNetwork.GetPrivateKey();
            dict["transactions"] = transactions;
            var json = JsonConvert.SerializeObject(dict);
            var result = await Request(path, json);
            ParticleAuthService.Instance.SignTransactionCallBack(result);
#endif
        }

        public async static void EvmSignMessages(string[] messages)
        {
#if UNITY_EDITOR
            string path = "testmode/evm-chain/sign_messages";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["private_key"] = ParticleNetwork.GetPrivateKey();
            dict["messages"] = messages;
            var json = JsonConvert.SerializeObject(dict);
            var result = await Request(path, json);
            ParticleAuthService.Instance.SignMessageCallBack(result);
#endif
        }

        public async static Task<string> Request(string path, [CanBeNull] string method, object[] parameters)
        {
            ParticleRpcRequest<object> request = new ParticleRpcRequest<object>(method, parameters);
            return await APIService.Request(path, request);
        }

        public async static Task<string> Request(string path, string json)
        {
#if UNITY_EDITOR
            return await APIService.Request(path, json);
#endif
            return string.Empty;
        }
    }

}
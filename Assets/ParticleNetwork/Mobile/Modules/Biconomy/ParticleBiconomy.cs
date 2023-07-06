using System.Collections.Generic;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network.Particle.Scripts.Core
{
    public class ParticleBiconomy : SingletonMonoBehaviour<ParticleBiconomy>
    {
        private TaskCompletionSource<NativeResultData> rpcGetFeeQuotesTask;
        private TaskCompletionSource<NativeResultData> isDeployTask;

        /// <summary>
        /// Rpc get fee quotes, used with custom FeeMode 
        /// </summary>
        /// <param name="eoaAddress"></param>
        /// <param name="transactions"></param>
        /// <returns></returns>
        public Task<NativeResultData> RpcGetFeeQuotes(string eoaAddress, List<string> transactions)
        {
            rpcGetFeeQuotesTask = new TaskCompletionSource<NativeResultData>();
            ParticleBiconomyInteraction.RpcGetFeeQuotes(eoaAddress, transactions);
            return rpcGetFeeQuotesTask.Task;
        }

        public void RpcGetFeeQuotesTaskCallBack(string json)
        {
            Debug.Log($"RpcGetFeeQuotesTaskCallBack:{json}");

            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            rpcGetFeeQuotesTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }

        public Task<NativeResultData> IsDepoly(string eoaAddress)
        {
            isDeployTask = new TaskCompletionSource<NativeResultData>();
            ParticleBiconomyInteraction.IsDeploy(eoaAddress);

            return isDeployTask.Task;
        }
        
        public void IsDeployCallBack(string json)
        {
            Debug.Log($"IsDeploy:{json}");

            var resultData = JObject.Parse(json);
            var status = (int)resultData["status"];
            rpcGetFeeQuotesTask?.TrySetResult(new NativeResultData(status == 1, resultData["data"].ToString()));
        }
        
    }
}
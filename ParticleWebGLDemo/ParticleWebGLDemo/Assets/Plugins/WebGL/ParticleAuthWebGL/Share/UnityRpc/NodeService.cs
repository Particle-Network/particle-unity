using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Network.Particle.Scripts.Model;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

namespace Network.Particle.Scripts.Core
{
    public static class ExtensionMethods
    {
        public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
        {
            var tcs = new TaskCompletionSource<object>();
            asyncOp.completed += obj => { tcs.SetResult(null); };
            return ((Task)tcs.Task).GetAwaiter();
        }
    }

    public class NodeService
    {
        private static string url = ParticleUnityRpc.rpcUrl;

        private static string authenticate(string username, string password)
        {
            string auth = username + ":" + password;
            auth = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(auth));
            auth = "Basic " + auth;
            return auth;
        }

        public async static Task<string> Request(string path, ParticleRpcRequest<object> requestParams)
        {
            var postData = JsonConvert.SerializeObject(requestParams);
            return await Request(path, postData);
        }

        public async static Task<string> Request(string path, string postData)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(ParticleUnityRpc.projectId), "Project ID is not set");
            Assert.IsTrue(!string.IsNullOrEmpty(ParticleUnityRpc.clientKey),
                "Project Client Key is not set");

            string authorization = authenticate(ParticleUnityRpc.projectId,
                ParticleUnityRpc.clientKey);
            Debug.Log($"Request params ${postData}");
            byte[] postDataJson = System.Text.Encoding.UTF8.GetBytes(postData);
            using (var www = UnityWebRequest.PostWwwForm(url + path, UnityWebRequest.kHttpVerbPOST))
            {
                www.chunkedTransfer = false;
                www.uploadHandler = new UploadHandlerRaw(postDataJson);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("AUTHORIZATION", authorization);
                try
                {
                    await www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        var text = www.downloadHandler.text;
                        Debug.Log($"Response: {text}");
                        return text;
                    }
                    else
                    {
                        Debug.LogError($"Response Error: {www.error}");
                        return $"Error: {www.error}";
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception: {ex.Message}");
                    return $"Exception: {ex.Message}";
                }
            }

            return "";
        }
    }
}
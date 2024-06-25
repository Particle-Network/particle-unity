using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Network.Particle.Scripts.Test
{
    public class ConnectWithWallet : MonoBehaviour
    {
        [SerializeField] private GameObject walletTypeAuthCoreTemplate;
        [SerializeField] private GameObject walletTypeOtherTemplate;
        [SerializeField] private GameObject scrollContent;
        [SerializeField] private Button btnBack;

        public UnityAction<bool> unityAction; //refresh wallet list

        void Start()
        {
            List<WalletType> walletTypes = new List<WalletType>((WalletType[])Enum.GetValues(typeof(WalletType)));
            walletTypes = walletTypes.Where(e => e != WalletType.EvmPrivateKey && e != WalletType.SolanaPrivateKey).ToList();

            foreach (var walletType in walletTypes)
            {
                if (walletType == WalletType.AuthCore)
                {
                    var chainItem = Instantiate(walletTypeAuthCoreTemplate);
                    var item = chainItem.GetComponent<WalletTypeParticleAuthCoreItem>();
                    item.InitItem(walletType);
                    item.SetBtnConnectClickListner(() =>
                    {
                        ConnectConfig config = item.GetConnectConfig();
                        ConnectAuthCore(config);
                    });
                    chainItem.transform.SetParent(scrollContent.transform);
                }
                else
                {
                    var chainItem = Instantiate(walletTypeOtherTemplate);
                    var item = chainItem.GetComponent<WalletTypeItem>();
                    item.InitItem(walletType);
                    item.GetComponent<Button>().onClick.AddListener(() => { ConnectWallet(walletType); });
                    chainItem.transform.SetParent(scrollContent.transform);
                }
            }

            btnBack.onClick.AddListener(Hidden);
        }


        public void Show(UnityAction<bool> unityAction)
        {
            this.unityAction = unityAction;
            gameObject.SetActive(true);
        }

        private void Hidden()
        {
            gameObject.SetActive(false);
        }

        private async Task ConnectWallet(WalletType walletType)
        {
            var nativeResultData = await ParticleConnect.Instance.Connect(walletType);
            Debug.Log(nativeResultData.data);

            if (nativeResultData.isSuccess)
            {
                var publicAddress = JObject.Parse(nativeResultData.data)["publicAddress"].ToString();
                Tips.Instance.Show(
                    $"{MethodBase.GetCurrentMethod()?.Name} publicAddress:{publicAddress}  Success:{nativeResultData.data}");
                Debug.Log(nativeResultData.data);
                unityAction.Invoke(true);
                Hidden();
            }
            else
            {
                Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }

        private async Task ConnectAuthCore(ConnectConfig connectConfig)
        {
            try
            {
                var nativeResultData = await ParticleConnect.Instance.Connect(WalletType.AuthCore, connectConfig);
                Debug.Log(nativeResultData.data);

                if (nativeResultData.isSuccess)
                {
                    var publicAddress = JObject.Parse(nativeResultData.data)["publicAddress"].ToString();
                    Tips.Instance.Show(
                        $"{MethodBase.GetCurrentMethod()?.Name} publicAddress:{publicAddress}  Success:{nativeResultData.data}");
                    Debug.Log(nativeResultData.data);
                    unityAction.Invoke(true);
                    Hidden();
                }
                else
                {
                    Tips.Instance.Show($"{MethodBase.GetCurrentMethod()?.Name} Failed:{nativeResultData.data}");
                    var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                    Debug.Log(errorData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred: {e.Message}");
            }
        }
    }
}
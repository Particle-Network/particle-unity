using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network.Particle.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SupportLoginType = Network.Particle.Scripts.Model.SupportLoginType;

namespace Network.Particle.Scripts.Test
{
    public class WalletTypeParticleAuthCoreItem : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private Button btnConnect;
        [SerializeField] private GameObject gridSupportLoginTypePannel;
        [SerializeField] private GameObject supportLoginTypeItemTemplate;

        public void SetBtnConnectClickListner(UnityAction action)
        {
            btnConnect.onClick.AddListener(action);
        }

        public void InitItem(WalletType walletType)
        {
            InitDropDown();
            InitSupportLoginTypes();
        }

        private void InitDropDown()
        {
            dropdown.ClearOptions();
            List<LoginType> loginTypes = new List<LoginType>((LoginType[])Enum.GetValues(typeof(LoginType)));
            dropdown.AddOptions(loginTypes.ConvertAll(x => x.ToString()));
        }

        List<SupportLoginTypeItem> supportLoginTypeItems = new List<SupportLoginTypeItem>();

        private void InitSupportLoginTypes()
        {
            List<SupportLoginType> supportLoginTypes = Enum.GetValues(typeof(SupportLoginType))
                .Cast<SupportLoginType>()
                .ToList();
            foreach (var supportLoginType in supportLoginTypes)
            {
                var chainItem = Instantiate(supportLoginTypeItemTemplate);
                var item = chainItem.GetComponent<SupportLoginTypeItem>();
                item.InitItem(supportLoginType);
                supportLoginTypeItems.Add(item);
                chainItem.transform.SetParent(gridSupportLoginTypePannel.transform);
            }
        }

        public ConnectConfig GetConnectConfig()
        {
            LoginPageConfig loginPageConfig = new LoginPageConfig("Particle Unity Example",
                "An example description", "https://connect.particle.network/icons/512.png");
            List<SupportLoginType> supportLoginTypes = supportLoginTypeItems.FindAll(x => x.GetSelected())
                .ConvertAll(x => x.GetSupportLoginType());

            var account = inputField.text;
            LoginType loginType = (LoginType)Enum.Parse(typeof(LoginType), dropdown.options[dropdown.value].text);

            ConnectConfig configConfig = new ConnectConfig(loginType, account, null, supportLoginTypes, null,
                loginPageConfig);
            StringBuilder sb = new StringBuilder();
            supportLoginTypes.ForEach(x => sb.Append(x.ToString() + " "));
            print(
                $"xxhong account:{account} loginType:{loginType} supportLoginTypes:{sb.ToString()}loginPageConfig:{loginPageConfig.ToString()}");
            return configConfig;
        }
    }
}
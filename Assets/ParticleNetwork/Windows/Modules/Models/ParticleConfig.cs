#if !UNITY_ANDROID && !UNITY_IOS
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Particle.Windows.Modules.Models
{
    [JsonObject]
    public class ParticleConfig
    {
        [JsonProperty(PropertyName = "projectId")]
        public string ProjectId;

        [JsonProperty(PropertyName = "clientKey")]
        public string ClientKey;

        [JsonProperty(PropertyName = "appId")] public string AppId;

        [JsonProperty(PropertyName = "securityAccount")]
        public ParticleConfigSecurityAccount SecurityAccount;

        [JsonProperty(PropertyName = "wallet")]
        public ParticleConfigWallet Wallet;

        public ParticleConfig([CanBeNull] ParticleConfigSecurityAccount SecurityAccount,
            [CanBeNull] ParticleConfigWallet Wallet)
        {
            this.ProjectId = ParticleUnityRpc.Instance.projectId;
            this.ClientKey = ParticleUnityRpc.Instance.clientKey;
            this.AppId = ParticleUnityRpc.Instance.appId;
            this.SecurityAccount = SecurityAccount;
            this.Wallet = Wallet;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class SupportChain
    {
        [JsonProperty(PropertyName = "name")] public string Name;

        [JsonProperty(PropertyName = "id")] public long Id;

        public SupportChain(string name, long id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

    [JsonObject]
    public class ParticleConfigWallet
    {
        [JsonProperty(PropertyName = "displayWalletEntry")]
        public bool DisplayWalletEntry;

        [JsonProperty(PropertyName = "supportChains")]
        public List<SupportChain> SupportChains;

        [JsonProperty(PropertyName = "customStyle")] [CanBeNull]
        public string CustomStyle;

        public ParticleConfigWallet(bool displayWalletEntry,
            List<SupportChain> supportChains, [CanBeNull] string customStyle)
        {
            this.DisplayWalletEntry = displayWalletEntry;
            this.SupportChains = supportChains;
            this.CustomStyle = customStyle;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class ParticleConfigSecurityAccount
    {
        [JsonProperty(PropertyName = "promptSettingWhenSign")]
        public int PromptSettingWhenSign;

        [JsonProperty(PropertyName = "promptMasterPasswordSettingWhenLogin")]
        public int PromptMasterPasswordSettingWhenLogin;

        public ParticleConfigSecurityAccount(int promptSettingWhenSign, int promptMasterPasswordSettingWhenLogin)
        {
            this.PromptSettingWhenSign = promptSettingWhenSign;
            this.PromptMasterPasswordSettingWhenLogin = promptMasterPasswordSettingWhenLogin;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    [JsonObject]
    public class ParticleTheme
    {
        [JsonProperty(PropertyName = "uiMode")]
        public string UiMode;

        [JsonProperty(PropertyName = "displayCloseButton")]
        public bool DisplayCloseButton;

        [JsonProperty(PropertyName = "displayWallet")]
        public bool DisplayWallet;

        [JsonProperty(PropertyName = "modalBorderRadius")]
        public int ModalBorderRadius;


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
#endif
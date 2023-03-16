#if !UNITY_ANDROID && !UNITY_IOS
using System.Collections.Generic;
using System.Numerics;
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
            
            [JsonProperty(PropertyName = "appId")] 
            public string AppId;
        
            [JsonProperty(PropertyName = "securityAccount")] 
            public ParticleConfigSecurityAccount SecurityAccount;
            
            [JsonProperty(PropertyName = "wallet")] 
            public ParticleConfigWallet Wallet;
            
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        [JsonObject]
        public class ParticleConfigWallet
        {
            
            [JsonProperty(PropertyName = "displayWalletEntry")] 
            public bool DisplayWalletEntry;
            
            [JsonProperty(PropertyName = "defaultWalletEntryPosition")] 
            public Vector2 DefaultWalletEntryPosition;
            
            [JsonProperty(PropertyName = "supportChains")] 
            public List<string> SupportChains;
            
            [JsonProperty(PropertyName = "customStyle")] 
            public string CustomStyle;
        }

        [JsonObject]
        public class ParticleConfigSecurityAccount
        {
            [JsonProperty(PropertyName = "promptSettingWhenSign")] 
            public int PromptSettingWhenSign;
            
            [JsonProperty(PropertyName = "promptMasterPasswordSettingWhenLogin")] 
            public int PromptMasterPasswordSettingWhenLogin;
            
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
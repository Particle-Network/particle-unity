using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using Newtonsoft.Json.Converters;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public static class ParticleAuthCoreInteraction
    {
        public static void Init()
        {
#if UNITY_ANDROID&& !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("init");
#elif UNITY_IOS&& !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreInitialize();
#else

#endif
        }

        /// <summary>
        /// Set blind enable, default value is false.
        /// This switch will work if the following conditions are met:
        /// 1. your account is connected with JWT
        /// 2. your account does not set payment password
        /// 3. SecurityAccountConfig.promptSettingWhenSign is 0, you can call ParticleNetwork.SetSecurityAccountConfig to update its value.
        /// </summary>
        /// <param name="enable">Enable</param>
        public static void SetBlindEnable(bool enable)
        {
#if UNITY_ANDROID&& !UNITY_EDITOR
// todo
            ParticleNetwork.CallAuthCoreNative("init");
#elif UNITY_IOS&& !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSetBlindEnable(enable);
#else

#endif
        }

        internal static void Connect(LoginType loginType, [CanBeNull] string account, [CanBeNull] string code,
            SocialLoginPrompt? socialLoginPrompt)
        {
            var obj = new JObject
            {
                { "loginType", loginType.ToString() },
                { "account", account },
                { "code", code },
                { "socialLoginPrompt", socialLoginPrompt.ToString() },
            };

            var json = JsonConvert.SerializeObject(obj);

            Debug.Log(json);

#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallAuthCoreNative("connect",jwt);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreConnect(json);
#else

#endif
        }

        internal static void PresentLoginPage(LoginType loginType, [CanBeNull] string account,
            SupportAuthType supportAuthTypes,
            SocialLoginPrompt? socialLoginPrompt, [CanBeNull] LoginPageConfig loginPageConfig
        )
        {
            var authTypeList = ParticleTools.GetSupportAuthTypeValues(supportAuthTypes);
            string accountNative = "";
            if (string.IsNullOrEmpty(account))
                accountNative = "";
            else
                accountNative = account;


            var obj = new JObject
            {
                { "loginType", loginType.ToString() },
                { "account", accountNative },
                { "supportAuthTypeValues", JToken.FromObject(authTypeList) },
                { "socialLoginPrompt", socialLoginPrompt.ToString() },
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };
            
            if (loginPageConfig != null) obj["loginPageConfig"] = JToken.FromObject(loginPageConfig, JsonSerializer.Create(settings));


            var json = JsonConvert.SerializeObject(obj);

            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCorePresentLoginPage(json);
#else

#endif
        }

        internal static void SendPhoneCode(string phone)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallAuthCoreNative("connect",jwt);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSendPhoneCode(phone);
#else

#endif
        }

        /// <summary>
        /// Send a verification code to your email 
        /// </summary>
        /// <param name="email">Email, for example `user@example.com`</param>
        internal static void SendEmailCode(string email)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallAuthCoreNative("connect",jwt);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSendEmailCode(email);
#else

#endif
        }

        internal static void ConnectJWT(string jwt)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallAuthCoreNative("connect",jwt);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreConnectJWT(jwt);
#else

#endif
        }

        internal static void Disconnect()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("disconnect");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreDisconnect();
#else

#endif
        }

        internal static void IsConnected()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("isConnected");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreIsConnected();
#else

#endif
        }


        public static void ChangeMasterPassword()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
             ParticleNetwork.CallAuthCoreNative("changeMasterPassword");
#elif UNITY_IOS && !UNITY_EDITOR
             ParticleNetworkIOSBridge.authCoreChangeMasterPassword();
#else

#endif
        }

        /// <summary>
        /// Get user info json string
        /// </summary>
        /// <returns></returns>
        public static string GetUserInfo()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetAuthCoreBridgeClass().CallStatic<string>("getUserInfo");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.authCoreGetUserInfo();
#else
            return "";
#endif
        }

        /// <summary>
        /// Has master password
        /// </summary>
        /// <returns></returns>
        internal static bool HasMasterPassword()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return  ParticleNetwork.GetAuthCoreBridgeClass().CallStatic<bool>("hasMasterPassword");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.authCoreHasMasterPassword();
#else
            return false;
#endif
        }

        /// <summary>
        /// Has payment password
        /// </summary>
        /// <returns></returns>
        public static bool HasPaymentPassword()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return  ParticleNetwork.GetAuthCoreBridgeClass().CallStatic<bool>("hasPaymentPassword");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.authCoreHasPaymentPassword();
#else
            return false;
#endif
        }

        internal static void SwitchChain(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.Name },
                { "chain_id", chainInfo.Id },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("switchChain",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSwitchChain(json);
#else
            Debug.Log($"SwitchChain json: {json}");
#endif
        }


        internal static void OpenAccountAndSecurity()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        ParticleNetwork.CallAuthCoreNative("openAccountAndSecurity");
#elif UNITY_IOS &&!UNITY_EDITOR
        ParticleNetworkIOSBridge.authCoreOpenAccountAndSecurity();
#else

#endif
        }

        public static string EvmGetAddress()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        return ParticleNetwork.GetAuthCoreBridgeClass().CallStatic<string>("evmGetAddress");
#elif UNITY_IOS &&!UNITY_EDITOR
        return ParticleNetworkIOSBridge.authCoreEvmGetAddress();
#else
            return "";
#endif
        }

        public static void EvmPersonalSign(string message)
        {
            string serializedMessage = "";
            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("evmPersonalSign",serializedMessage);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreEvmPersonalSign(serializedMessage);
#else
            return;
#endif
        }

        public static void EvmPersonalSignUnique(string message)
        {
            string serializedMessage = "";
            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("evmPersonalSignUnique",serializedMessage);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreEvmPersonalSignUnique(serializedMessage);
#else
            return;
#endif
        }

        public static void EvmSignTypedData(string message)
        {
            string serializedMessage = "";
            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("evmSignTypedData",serializedMessage);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreEvmSignTypedData(serializedMessage);
#else
            return;
#endif
        }

        public static void EvmSignTypedDataUnique(string message)
        {
            string serializedMessage = "";
            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("evmSignTypedDataUnique",serializedMessage);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreEvmSignTypedDataUnique(serializedMessage);
#else
            return;
#endif
        }

        public static void EvmSendTransaction(string transaction, [CanBeNull] AAFeeMode feeMode = null)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "transaction", transaction },
                { "fee_mode", feeMode == null ? null : JToken.FromObject(feeMode) },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
ParticleNetwork.CallAuthCoreNative("evmSendTransaction",json);
#elif UNITY_IOS &&!UNITY_EDITOR
         ParticleNetworkIOSBridge.authCoreEvmSendTransaction(json);
#else
            return;
#endif
        }

        internal static void BatchSendTransactions(List<string> transactions,
            [CanBeNull] AAFeeMode feeMode = null)

        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "transactions", JToken.FromObject(transactions) },
                { "fee_mode", feeMode == null ? null : JToken.FromObject(feeMode) },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            ParticleNetwork.CallNative("batchSendTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreEvmBatchSendTransactions(json);
#else

#endif
        }

        public static string SolanaGetAddress()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetAuthCoreBridgeClass().CallStatic<string>("solanaGetAddress");
#elif UNITY_IOS &&!UNITY_EDITOR
            return ParticleNetworkIOSBridge.authCoreSolanaGetAddress();
#else
            return "";
#endif
        }

        public static void SolanaSignMessage(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("solanaSignMessage",message);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSolanaSignMessage(message);
#else
            return;
#endif
        }

        public static void SolanaSignTransaction(string transaction)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("solanaSignTransaction",transaction);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSolanaSignTransaction(transaction);
#else
            return;
#endif
        }

        public static void SolanaSignAllTransactions(string[] transactions)
        {
            var json = JsonConvert.SerializeObject(transactions);

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("solanaSignAllTransactions",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSolanaSignAllTransactions(json);
#else
            return;
#endif
        }

        public static void SolanaSendAndSendTransaction(string transaction)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallAuthCoreNative("solanaSignAndSendTransaction",transaction);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSolanaSignAndSendTransaction(transaction);
#else
            return;
#endif
        }

        public static void SetCustomUI(string json)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
// todo
            // ParticleNetwork.CallAuthCoreNative("solanaSignAndSendTransaction",transaction);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.authCoreSetCustomUI(json);
#else
            return;
#endif
        }
    }
}
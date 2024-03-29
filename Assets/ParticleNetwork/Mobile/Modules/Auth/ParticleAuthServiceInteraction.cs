﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core.UnityEditorTestMode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Utils;
using UnityEngine;


namespace Network.Particle.Scripts.Core
{
    public static class ParticleAuthServiceInteraction
    {
        internal static void Login(LoginType loginType, [CanBeNull] string account, SupportAuthType supportAuthTypes,
            SocialLoginPrompt? socialLoginPrompt, [CanBeNull] LoginAuthorization authorization
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

            if (authorization != null) obj["authorization"] = JToken.FromObject(authorization);

            var json = JsonConvert.SerializeObject(obj);

            Debug.Log(json);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("login",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.login(json);
#else

#endif
        }

        internal static void IsLoginAsync()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("isLoginAsync");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.isLoginAsync();
#else

#endif
        }

        internal static void Logout()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("logout");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.logout();
#else

#endif
        }

        internal static void FastLogout()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("fastLogout");
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.fastLogout();
#else

#endif
        }

        public static bool IsLogin()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<bool>("isLogin");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.isLogin();
#else
            return false;
#endif
        }

        internal static void SignMessage(string message)
        {
            string serializedMessage;
            if (ParticleNetwork.GetChainInfo().IsEvmChain())
            {
                if (HexUtils.IsHexadecimal(message))
                {
                    serializedMessage = message;
                }
                else
                {
                    serializedMessage = HexUtils.ConvertHex(message);
                }
            }
            else
            {
                serializedMessage = message;
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signMessage",serializedMessage);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signMessage(serializedMessage);
#else
            DevModeService.SolanaSignMessages(new[] { message });
#endif
        }

        internal static void SignMessageUnique(string message)
        {
            string serializedMessage;

            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signMessageUnique",serializedMessage);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signMessageUnique(serializedMessage);
#else
            DevModeService.EvmSignMessages(new[]
            {
                message
            });
#endif
        }

        internal static void SignTransaction(string transaction)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signTransaction",transaction);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signTransaction(transaction);
#else

#endif
        }

        internal static void SignAllTransactions(string[] transactions)
        {
            var json = JsonConvert.SerializeObject(transactions);
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signAllTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signAllTransactions(json);
#else

#endif
        }

        internal static void SignAndSendTransaction(string transaction, [CanBeNull] AAFeeMode feeMode = null)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "transaction", transaction },
                { "fee_mode", feeMode == null ? null : JToken.FromObject(feeMode) },
            });

#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signAndSendTransaction",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signAndSendTransaction(json);
#else

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
            ParticleNetwork.CallNative("batchSendTransactions",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.batchSendTransactions(json);
#else

#endif
        }

        internal static void SignTypedData(string message, SignTypedDataVersion signTypedDataVersion)
        {
            string serializedMessage;

            if (HexUtils.IsHexadecimal(message))
            {
                serializedMessage = message;
            }
            else
            {
                serializedMessage = HexUtils.ConvertHex(message);
            }

            var json = JsonConvert.SerializeObject(new JObject
            {
                { "message", serializedMessage },
                { "version", signTypedDataVersion.ToString() },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("signTypedData",json);
#elif UNITY_IOS && !UNITY_EDITOR
            ParticleNetworkIOSBridge.signTypedData(json);
#else

#endif
        }

        public static string GetAddress()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<string>("getAddress");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getAddress();
#else
            return "";
#endif
        }

        /**
        {
            "uuid": "",
            "token": "",
            "wallets": [{
                "chain_name": "",
                "public_address": "",
                "uuid": ""
            }]
        }
     */
        public static string GetUserInfo()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ParticleNetwork.GetUnityBridgeClass().CallStatic<string>("getUserInfo");
#elif UNITY_IOS && !UNITY_EDITOR
            return ParticleNetworkIOSBridge.getUserInfo();
#else
            return PersistTools.GetUserInfoJson();
#endif
        }

        public static bool HasMasterPassword()
        {
            var userInfo = GetUserInfo();
            var hasMasterPassword = (bool)JObject.Parse(userInfo)["security_account"]?["has_set_master_password"];
            return hasMasterPassword;
        }

        public static bool HasPaymentPassword()
        {
            var userInfo = GetUserInfo();
            var result = (bool)JObject.Parse(userInfo)["security_account"]?["has_set_payment_password"];
            return result;
        }

        public static bool HasSecurityAccount()
        {
            var userInfo = GetUserInfo();
            var email = (string)JObject.Parse(userInfo)["security_account"]?["email"];
            var phone = (string)JObject.Parse(userInfo)["security_account"]?["phone"];
            return !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(phone);
        }

        public static void GetSecurityAccount()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
             ParticleNetwork.GetUnityBridgeClass().CallStatic("getSecurityAccount");
#elif UNITY_IOS && !UNITY_EDITOR
             ParticleNetworkIOSBridge.getSecurityAccount();
#else

#endif
        }

        public static void SetChainInfoAsync(ChainInfo chainInfo)
        {
            var json = JsonConvert.SerializeObject(new JObject
            {
                { "chain_name", chainInfo.Name },
                { "chain_id", chainInfo.Id },
                { "chain_id_name", chainInfo.Network },
            });
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("setChainInfoAsync",json);
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setChainInfoAsync(json);
#else
            Debug.Log($"SetChainInfoAsync json: {json}");
#endif
        }


        /// <summary>
        /// Set iOS modal present style
        /// </summary>
        /// <param name="style">Set fullScreen or formSheet, default value is formSheet</param>
        public static void SetiOSModalPresentStyle(iOSModalPresentStyle style)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setModalPresentStyle(style.ToString());
#else
#endif
        }

        /// <summary>
        /// Set medium screen, this method works from iOS 15.
        /// If you want to show half screen in embedded safari, set this method to true.
        /// and make sure you didn't call SetModalPresentStyle with FullScreen.
        /// </summary>
        /// <param name="isMedium">Default value is false</param>
        public static void SetiOSMediumScreen(bool isMedium)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.setMediumScreen(isMedium);
#else
#endif
        }


        /// <summary>
        /// only Android support
        /// set auth browser height percent, you must use android auth sdk tiramisu
        /// if you didn't call this method, the default value is 0.9f
        /// </summary>
        /// <param name="percent"> percent>0 percent<=1 </param>
        public static void SetBrowserHeightPercent(float percent)
        {
            if (percent <= 0 || percent > 1) throw new Exception("require 0<percent<=1");
            Debug.Log($"SetBrowserHeightPercent percent: {percent}");
#if UNITY_ANDROID && !UNITY_EDITOR
            var authTiramisu = new AndroidJavaClass("com.particle.browser.ParticleNetworkAuthTiramisu");
            authTiramisu.CallStatic("setBrowserHeightPercent", percent);
#elif UNITY_IOS &&!UNITY_EDITOR
#else

#endif
        }

        public static void OpenWebWallet(string jsonString)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ParticleNetwork.CallNative("openWebWallet");
#elif UNITY_IOS &&!UNITY_EDITOR
            ParticleNetworkIOSBridge.openWebWallet(jsonString);
#else

#endif
        }

        internal static void OpenAccountAndSecurity()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        ParticleNetwork.CallNative("openAccountAndSecurity");
#elif UNITY_IOS &&!UNITY_EDITOR
        ParticleNetworkIOSBridge.openAccountAndSecurity();
#else

#endif
        }
    }
}
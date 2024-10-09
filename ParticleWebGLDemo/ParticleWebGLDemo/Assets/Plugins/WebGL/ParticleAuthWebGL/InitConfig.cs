using System;
using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;

[Serializable]
public struct WalletEntryPosition
{
    public float x;
    public float y;

    public WalletEntryPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}

[Serializable]
public struct Wallet
{
    public bool displayWalletEntry;
    public WalletEntryPosition defaultWalletEntryPosition;
    public List<ChainInfo> supportChains;

    public Wallet(bool displayWalletEntry, WalletEntryPosition defaultWalletEntryPosition,
        List<ChainInfo> supportChains)
    {
        this.displayWalletEntry = displayWalletEntry;
        this.defaultWalletEntryPosition = defaultWalletEntryPosition;
        this.supportChains = supportChains;
    }
}

[Serializable]
public struct SecurityAccount
{
    public int promptSettingWhenSign;
    public int promptMasterPasswordSettingWhenLogin;

    public SecurityAccount(int promptSettingWhenSign, int promptMasterPasswordSettingWhenLogin)
    {
        this.promptSettingWhenSign = promptSettingWhenSign;
        this.promptMasterPasswordSettingWhenLogin = promptMasterPasswordSettingWhenLogin;
    }
}

[Serializable]
public struct InitConfig
{
    public string projectId;
    public string clientKey;
    public string appId;
    public string chainName;
    public long chainId;
    public SecurityAccount securityAccount;
    public Wallet wallet;


    [JsonIgnore] 
    public ChainInfo chainInfo;

    public InitConfig(string projectId, string clientKey, string appId, ChainInfo chainInfo,
        SecurityAccount securityAccount, Wallet wallet)
    {
        this.projectId = projectId;
        this.clientKey = clientKey;
        this.appId = appId;
        this.chainName = chainInfo.Name;
        this.chainId = chainInfo.Id;
        this.securityAccount = securityAccount;
        this.wallet = wallet;

        this.chainInfo = chainInfo;
    }
}

[Serializable]
public struct UISettings
{
    public string uiMode;
    public bool displayCloseButton;
    public bool displayWallet;
    public int modalBorderRadius;

    public UISettings(string uiMode, bool displayCloseButton, bool displayWallet, int modalBorderRadius)
    {
        this.uiMode = uiMode;
        this.displayCloseButton = displayCloseButton;
        this.displayWallet = displayWallet;
        this.modalBorderRadius = modalBorderRadius;
    }
}

[Serializable]
public struct Authorization
{
    /// <summary>
    /// hex sign message.
    /// </summary>
    public string message;

    /// <summary>
    /// optional, default false.
    /// </summary>
    public bool uniq;

    public Authorization(string message, bool uniq)
    {
        this.message = message;
        this.uniq = uniq;
    }
}

[Serializable]
public struct LoginConfig
{
    /// <summary>
    /// support email | phone | jwt | google | apple | facebook | discord | twitter | twitch | microsoft | linkedin
    /// </summary>
    public string preferredAuthType;

    /// <summary>
    /// when set email/phone account and preferredAuthType is email or phone, 
    /// Particle Auth will enter directly input verification code page.
    /// when set JWT value and preferredAuthType is jwt, Particle Auth will auto login.
    /// </summary>
    public string account;

    /// <summary>
    /// hide particle loading when use jwt authorization.
    /// </summary>
    public bool? hideLoading;

    /// <summary>
    /// social login prompt.  none | consent | select_account
    /// </summary>
    public string socialLoginPrompt;

    /// <summary>
    /// optional, login with authorize
    /// </summary>
    public Authorization? authorization;

    public LoginConfig(string preferredAuthType, string account, bool? hideLoading, string socialLoginPrompt,
        Authorization? authorization)
    {
        this.preferredAuthType = preferredAuthType;
        this.account = account;
        this.hideLoading = hideLoading;
        this.socialLoginPrompt = socialLoginPrompt;
        this.authorization = authorization;
    }
}

public class ErrorException : Exception
{
    public int Code { get; }

    public ErrorException(int code, string message) : base(message)
    {
        Code = code;
    }

    public ErrorException(int code, string message, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }
}
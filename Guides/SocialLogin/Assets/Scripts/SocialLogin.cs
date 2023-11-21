using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SocialLogin : MonoBehaviour
{
    [CanBeNull] private string _publicAddress = null;
    private WalletType? _walletType = null;

    // Start is called before the first frame update
    void Start()
    {
        ParticleNetwork.Init(ChainInfo.PolygonMumbai);
        var walletConnectProjectId = "f431aaea6e4dea6a669c0496f9c009c1";
        ParticleConnectInteraction.Init(ChainInfo.PolygonMumbai,
            new DAppMetaData(walletConnectProjectId, "Guide Wallet", "Guide Icon", "Guide Url", "Guide description"));
        ParticleWalletGUI.ParticleWalletConnectInitialize(new WalletMetaData("Guide Wallet", "Guide Icon", "Guide Url", "Guide description",
            walletConnectProjectId));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public async void ConnectGoogle()
    {
        try
        {
            var config = new ConnectConfig(LoginType.GOOGLE, "", SupportAuthType.ALL, socialLoginPrompt:SocialLoginPrompt.SelectAccount);
            // var config = new ConnectConfig(LoginType.JWT, "Your JWT", SupportAuthType.ALL);
            var nativeResultData =
                await ParticleConnect.Instance.Connect(WalletType.Particle, config);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var accountJson = JObject.Parse(nativeResultData.data);

                this._publicAddress = accountJson["publicAddress"].ToString();
                this._walletType = WalletType.Particle;
                Debug.Log($"publicAddress: {_publicAddress}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }
    
    public async void ConnectMetamask()
    {
        try
        {
            var nativeResultData =
                await ParticleConnect.Instance.Connect(WalletType.MetaMask);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var accountJson = JObject.Parse(nativeResultData.data);

                this._publicAddress = accountJson["publicAddress"].ToString();
                this._walletType = WalletType.MetaMask;
                Debug.Log($"publicAddress: {_publicAddress}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void Disconnect()
    {
        if (this._publicAddress == null || this._walletType == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        this._publicAddress = null;
        this._walletType = null;
        
        try
        {
            var nativeResultData =
                await ParticleConnect.Instance.Disconnect(WalletType.Particle, this._publicAddress);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void SendTransaction()
    {
        if (this._publicAddress == null || this._walletType == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        try
        {
            var address = this._publicAddress;
            const string to = "0x0000000000000000000000000000000000000000";
            var transaction =
                await EvmService.CreateTransaction(address, "0x", BigInteger.Parse("10000000000000000"), to);
            var nativeResultData =
                await ParticleConnect.Instance.SignAndSendTransaction((WalletType)this._walletType, _publicAddress,
                    transaction);
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var result = nativeResultData.data;
                Debug.Log($"result: {result}");
            }
            else
            {
                var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
                Debug.Log(errorData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred: {e.Message}");
        }
    }

    public async void SignMessage()
    {
        if (this._publicAddress == null || this._walletType == null)
        {
            Debug.Log("did not connect any account");
            return;
        }

        try
        {
            var nativeResultData =
                await ParticleConnect.Instance.SignMessage((WalletType)this._walletType, _publicAddress,
                    "Hello Particle");
            if (nativeResultData.isSuccess)
            {
                Debug.Log(nativeResultData.data);
                var result = nativeResultData.data;
                Debug.Log($"result: {result}");
            }
            else
            {
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
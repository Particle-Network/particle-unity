using System;
using System.Collections;
using System.Collections.Generic;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Test;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ParticleNetworkSDKDemo : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        var currChainInfo = ChainInfo.EthereumSepolia;
        var metadata = new DAppMetaData(TestConfig.walletConnectProjectId, "Particle Connect",
            "https://connect.particle.network/icons/512.png",
            "https://connect.particle.network",
            "Particle Connect Unity Demo");
        ParticleNetwork.Init(currChainInfo);
        ParticleConnectInteraction.Init(currChainInfo, metadata);
        // List<ChainInfo> chainInfos = new List<ChainInfo>
        //     { ChainInfo.Ethereum, ChainInfo.EthereumSepolia, ChainInfo.EthereumSepolia };
        // ParticleConnectInteraction.SetWalletConnectV2SupportChainInfos(chainInfos.ToArray());

        // control how to show set master password and payment password.
        // ParticleNetwork.SetSecurityAccountConfig(new SecurityAccountConfig(0, 0));
    }
}
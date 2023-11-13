using System.Collections;
using System.Collections.Generic;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Assertions;

public class ParticleUnityRpc : SingletonMonoBehaviour<ParticleUnityRpc>
{
    public string rpcUrl = "https://rpc.particle.network/";
    public string projectId;
    public string appId;
    public string clientKey;

    protected override void Awake()
    {
        base.Awake();
        Assert.IsTrue(!string.IsNullOrEmpty(rpcUrl), "Please set the rpcUrl");
        Assert.IsTrue(!string.IsNullOrEmpty(projectId), "Please set the projectId");
        Assert.IsTrue(!string.IsNullOrEmpty(appId), "Please set the appId");
        Assert.IsTrue(!string.IsNullOrEmpty(clientKey), "projectClientKey == null");
    }

    public void showTips()
    {
        
    }
}

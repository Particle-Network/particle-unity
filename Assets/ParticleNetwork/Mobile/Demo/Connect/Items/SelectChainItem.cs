using System;
using System.Collections.Generic;
using System.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Button = UnityEngine.UI.Button;


public class SelectChainItem : MonoBehaviour
{
    private ChainInfo chainInfo;
    [SerializeField] private TextMeshProUGUI text;

    public void InitItem(ChainInfo chainInfo)
    {
        text.text = chainInfo.Name + " " + chainInfo.Network + " " + chainInfo.Id.ToString();
    }
}
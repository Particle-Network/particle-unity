
using Network.Particle.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

public class ChainChoiceItem : MonoBehaviour
{
    private ChainInfo chainInfo;
    [SerializeField] private Text chainName;

    public void InitItem(ChainInfo chainInfo)
    {
        chainName.text = chainInfo.Name + " " + chainInfo.Network + " " + chainInfo.Id.ToString();
    }
 
}
using System;
using System.Collections.Generic;
using System.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;
using Button = UnityEngine.UI.Button;

public class ChainChoice : SingletonMonoBehaviour<ChainChoice>
{
    [SerializeField] private GameObject chainItemTemplate;
    [SerializeField] private GameObject scrollContent;
    public UnityAction<ChainInfo> unityAction;

    void Start()
    {
        List<ChainInfo> chainInfos = ChainInfo.GetAllChains();
        chainInfos = chainInfos
            .OrderByDescending(chainInfo => chainInfo.Id == 80001)
            .ThenBy(chainInfo => chainInfo.Id)
            .ToList();


        foreach (var chainInfo in chainInfos)
        {
            var chainItem = Instantiate(chainItemTemplate);
            var item = chainItem.GetComponent<ChainChoiceItem>();
            item.InitItem(chainInfo);
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                unityAction.Invoke(chainInfo);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            });
            chainItem.transform.SetParent(scrollContent.transform);
        }
    }


    public void Show(UnityAction<ChainInfo> unityAction)
    {
        this.unityAction = unityAction;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
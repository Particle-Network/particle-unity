using System;
using System.Collections.Generic;
using System.Linq;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;
using Button = UnityEngine.UI.Button;

public class SelectChainPage : SingletonMonoBehaviour<SelectChainPage>
{
    [SerializeField] private GameObject chainItemTemplate;
    [SerializeField] private GameObject scrollContent;
    public UnityAction<ChainInfo> unityAction;
    [SerializeField] private Button btnBack;

    void Start()
    {
        List<ChainInfo> chainInfos = ChainInfo.GetAllChainInfos();

        foreach (var chainInfo in chainInfos)
        {
            var chainItem = Instantiate(chainItemTemplate);
            var item = chainItem.GetComponent<SelectChainItem>();
            item.InitItem(chainInfo);
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                unityAction.Invoke(chainInfo);
                Hidden();
            });
            chainItem.transform.SetParent(scrollContent.transform);
        }

        btnBack.onClick.AddListener(Hidden);
    }

    private void Hidden()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    public void Show(UnityAction<ChainInfo> unityAction)
    {
        this.unityAction = unityAction;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
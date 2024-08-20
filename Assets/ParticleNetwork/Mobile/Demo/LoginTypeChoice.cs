using System.Collections.Generic;
using Network.Particle.Scripts.Model;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginTypeChoice : SingletonMonoBehaviour<LoginTypeChoice>
{
    [SerializeField] private GameObject loginTypeTemplate;
    [SerializeField] private GameObject scrollContent;
    public UnityAction<LoginType> unityAction;

    void Start()
    {
        List<LoginType> loginTypes = new List<LoginType>
        {
            LoginType.Phone, LoginType.Email, LoginType.Apple, LoginType.Google,
            LoginType.Facebook, LoginType.Github, LoginType.Twitch, LoginType.Twitter, LoginType.Discord,
            LoginType.Linkedin, LoginType.Microsoft
        };
        
        foreach (var loginType in loginTypes)
        {
            var loginTypeItem = Instantiate(loginTypeTemplate);
            var item = loginTypeItem.GetComponent<LoginTypeItem>();
            item.InitItem(loginType);
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                unityAction.Invoke(loginType);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            });
            loginTypeItem.transform.SetParent(scrollContent.transform);
        }
    }

    public void Show(UnityAction<LoginType> unityAction)
    {
        this.unityAction = unityAction;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
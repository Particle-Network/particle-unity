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
        List<LoginType> loginTypes = new List<LoginType>();
        loginTypes.Add(LoginType.PHONE);
        loginTypes.Add(LoginType.EMAIL);
        loginTypes.Add(LoginType.APPLE);
        loginTypes.Add(LoginType.GOOGLE);
        loginTypes.Add(LoginType.FACEBOOK);
        loginTypes.Add(LoginType.GITHUB);
        loginTypes.Add(LoginType.TWITCH);
        loginTypes.Add(LoginType.TWITTER);
        loginTypes.Add(LoginType.DISCORD);
        loginTypes.Add(LoginType.LINKEDIN);
        loginTypes.Add(LoginType.MICROSOFT);

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
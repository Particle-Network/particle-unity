using Network.Particle.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

public class LoginTypeItem : MonoBehaviour
{
    [SerializeField] private Text loginTypeText;

    public void InitItem(LoginType loginType)
    {
        loginTypeText.text = loginType.ToString();
    }
}
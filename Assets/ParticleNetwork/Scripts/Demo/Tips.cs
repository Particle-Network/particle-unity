using System;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class Tips : SingletonMonoBehaviour<Tips>
{
    // Start is called before the first frame update
    private string strMsg = "Please check the source code for details.";

    [SerializeField] private Text message;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        gameObject.SetActive(true);
        if (String.IsNullOrEmpty(message)) message = strMsg;
        this.message.text = message;
        // Invoke("HideTips", 2f);
    }

    private void HideTips()
    {
        gameObject.SetActive(false);
    }
}
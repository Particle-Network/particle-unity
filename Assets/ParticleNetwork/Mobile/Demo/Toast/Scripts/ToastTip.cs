
using System;
using Network.Particle.Scripts.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace CommonTip.Script
{
    public class ToastTip : SingletonMonoBehaviour<ToastTip>
    {
        public Text tipText;
        public float  durTime=1f;
        private float timer = 0;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnShow(string tip)
        {
            tipText.text = tip;
            timer = 0;
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(tipText.transform.parent.GetComponent<RectTransform>());
        }
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer>=durTime)
            {
                gameObject.SetActive(false);
                timer -= durTime;
            }
        }
    }
}


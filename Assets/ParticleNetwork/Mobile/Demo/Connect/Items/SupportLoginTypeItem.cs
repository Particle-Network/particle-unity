using Network.Particle.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.Particle.Scripts.Test
{
    public class SupportLoginTypeItem : MonoBehaviour
    {
        private bool isSelected = true;
        [SerializeField] private Image image;
        [SerializeField] private Image unSelectedImage;
        private SupportLoginType supportLoginType;

        public void InitItem(SupportLoginType supportLoginType)
        {
            this.supportLoginType = supportLoginType;
            image.sprite = Resources.Load<Sprite>(supportLoginType.ToString());
            gameObject.GetComponent<Button>().onClick.AddListener(() => { SetSelected(!isSelected); });
        }

        public void SetSelected(bool isSelected)
        {
            this.isSelected = isSelected;
            if (isSelected)
            {
                unSelectedImage.gameObject.SetActive(false);
            }
            else
            {
                unSelectedImage.gameObject.SetActive(true);
            }
        }

        public SupportLoginType GetSupportLoginType()
        {
            return supportLoginType;
        }

        public bool GetSelected()
        {
            return isSelected;
        }
    }
}
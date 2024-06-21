using TMPro;
using UnityEngine;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class SelectButtonSingleText : BasicButton
    {
        public GameObject ActiveImage, InactiveImage;
        public TextMeshProUGUI ButtonText;

        public void Activate()
        {
            InactiveImage.gameObject.SetActive(true);
            ActiveImage.gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            InactiveImage.gameObject.SetActive(false);
            ActiveImage.gameObject.SetActive(true);
        }
    }
}
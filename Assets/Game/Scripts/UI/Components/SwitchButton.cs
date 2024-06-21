using UnityEngine;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class SwitchButton : BasicButton
    {
        [SerializeField] private GameObject InactiveImage, ActiveImage;

        public void Switch(bool isEnabled)
        {
            if (isEnabled)
            {
                InactiveImage.SetActive(false);
                ActiveImage.SetActive(true);
            }
            else
            {
                InactiveImage.SetActive(true);
                ActiveImage.SetActive(false);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class BasicButton : MonoBehaviour
    {
        public Button Button;

        private void OnValidate()
        {
            Button = GetComponent<Button>();
        }

        public virtual void OnEnable()
        {
            Button.onClick.AddListener(Click);
        }

        public virtual void OnDisable()
        {
            Button.onClick.RemoveListener(Click);
        }

        private void Click()
        {
            // AudioSystem.Instance.PlayClick();
        }
    }
}
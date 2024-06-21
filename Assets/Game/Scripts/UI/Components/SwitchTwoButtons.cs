using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class SwitchTwoButtons : MonoBehaviour
    {
        [SerializeField] private Toggle _on, _off;

        public Action OnSwitch;

        public void OnEnable()
        {
            _on.onValueChanged.AddListener(Click);
        }
        public void OnDisable() =>
            _on.onValueChanged.RemoveAllListeners();

        private void Click(bool isOn) => OnSwitch?.Invoke();

        public void Switch(bool isEnabled)
        {
            if (isEnabled)
            {
                _on.SetIsOnWithoutNotify(true);
                _off.SetIsOnWithoutNotify(false);
            }
            else
            {
                _on.SetIsOnWithoutNotify(false);
                _off.SetIsOnWithoutNotify(true);
            }
        }
    }
}
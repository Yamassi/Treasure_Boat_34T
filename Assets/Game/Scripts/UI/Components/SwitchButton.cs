using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class SwitchButton : BasicButton
    {
        [SerializeField] private Toggle Toggle;
        [SerializeField] TextMeshProUGUI _toggleText;

        public Action OnSwitch;

        public override void OnEnable()
        {
            Toggle.onValueChanged.AddListener(Click);
        }

        public override void OnDisable()
        {
            Toggle.onValueChanged.RemoveAllListeners();
        }

        private void Click(bool isOn)
        {
            OnSwitch?.Invoke();
            _toggleText.text = isOn ? "On" : "Off";
        }

        public void Switch(bool isEnabled)
        {
            if (isEnabled)
            {
                Toggle.SetIsOnWithoutNotify(true);
            }
            else
            {
                Toggle.SetIsOnWithoutNotify(false);
            }
        }
    }
}
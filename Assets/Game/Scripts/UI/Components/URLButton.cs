using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Components
{
    [RequireComponent(typeof(Button))]
    public class URLButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private String URL;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(GoToLink);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(GoToLink);
        }

        private void GoToLink()
        {
            Application.OpenURL(URL);
        }
    }
}
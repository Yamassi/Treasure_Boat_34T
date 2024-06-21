using Tretimi.Game.Scripts.System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class InAppButton : MonoBehaviour
    {
        private IInAppService _inAppService;
        [SerializeField] private Button _button;

        [Inject]
        public void Construct(IInAppService inAppService)
        {
            _inAppService = inAppService;
        }

        private void OnValidate()
        {
            _button = GetComponent<Button>();
            GetComponent<CodelessIAPButton>().button = _button;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenInAppLoadingPage);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenInAppLoadingPage);
        }

        private void OpenInAppLoadingPage()
        {
            _inAppService.OpenInAppLoadingPage("Loading");
        }
    }
}
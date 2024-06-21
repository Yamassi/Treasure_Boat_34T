using Tretimi.Game.Scripts.System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        private void Click()
        {
            _audioService.PlayClick();
        }
    }
}
using Tretimi.Game.Scripts.System;
using UnityEngine;
using Zenject;

namespace Tretimi.Game.Scripts.Core
{
    public class Bootloader : MonoBehaviour
    {
        private IAudioService _audioService;
        private IDataService _dataService;
        private IGame _game;

        [Inject]
        public void Construct(IAudioService audioService, IGame game, IDataService dataService)
        {
            _audioService = audioService;
            _game = game;
            _dataService = dataService;
        }

        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            _dataService.LoadData();

            _game.Init();

            _audioService.LoadSettingsValues();
        }

        private void OnApplicationFocus(bool isFocus)
        {
            Debug.Log($"Application Pause {isFocus}");
            if (!isFocus)
                _dataService.SaveData();
        }

        private void OnApplicationQuit()
        {
            _dataService.SaveData();
            _game.DeInit();
        }
    }
}
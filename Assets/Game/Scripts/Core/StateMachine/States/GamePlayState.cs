using System.Threading;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.GamePlay;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages.GamePlayUI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class GamePlayState : State
    {
        private GamePlayUI _gamePlayUI;
        private GamePlayController _gamePlayController;
        private CompositeDisposable _disposable = new();
        private CancellationTokenSource _cts = new();
        private int _fishCount, _maxFish, _coins;
        private float _time;
        private bool _isPause;


        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService,
            GamePlayUI gamePlayUI,
            GamePlayController gamePlayController
        )
        {
            _audioService = audioService;
            _stateSwitcher = stateSwitcher;
            _dataService = dataService;
            _uiService = uiService;
            _gamePlayUI = gamePlayUI;
            _gamePlayController = gamePlayController;
        }

        public override void Enter()
        {
            ComponentsToggle(true);
            Subsribe();
            Init();
        }

        public override void Exit()
        {
            ComponentsToggle(false);
            Unsubsribe();
            DeInit();
        }

        public override void Subsribe()
        {
            _gamePlayUI.PauseBtn.onClick.AddListener(Pause);
            _gamePlayUI.Pause.BackToMenu.onClick.AddListener(MainMenu);
            _gamePlayUI.Pause.Repeat.onClick.AddListener(Repeat);
            _gamePlayUI.Pause.Close.onClick.AddListener(Resume);
            _gamePlayUI.Pause.Continue.onClick.AddListener(Resume);
            _gamePlayUI.Pause.Music.OnSwitch += SwitchMusic;
            _gamePlayUI.Pause.Sound.OnSwitch += SwitchSound;

            _gamePlayUI.Win.Repeat.onClick.AddListener(Repeat);
            _gamePlayUI.Win.BackToMenu.onClick.AddListener(MainMenu);
            _gamePlayUI.Win.NextLevel.onClick.AddListener(NextLevel);

            _gamePlayUI.Lose.Repeat.onClick.AddListener(Repeat);
            _gamePlayUI.Lose.BackToMenu.onClick.AddListener(MainMenu);

            _cts = new();
        }

        public override void Unsubsribe()
        {
            _gamePlayUI.PauseBtn.onClick.RemoveAllListeners();
            _gamePlayUI.Pause.BackToMenu.onClick.RemoveAllListeners();
            _gamePlayUI.Pause.Repeat.onClick.RemoveAllListeners();
            _gamePlayUI.Pause.Close.onClick.RemoveAllListeners();
            _gamePlayUI.Pause.Continue.onClick.RemoveAllListeners();
            _gamePlayUI.Pause.Music.OnSwitch -= SwitchMusic;
            _gamePlayUI.Pause.Sound.OnSwitch -= SwitchSound;

            _gamePlayUI.Win.Repeat.onClick.RemoveAllListeners();
            _gamePlayUI.Win.BackToMenu.onClick.RemoveAllListeners();
            _gamePlayUI.Win.NextLevel.onClick.RemoveAllListeners();

            _gamePlayUI.Lose.Repeat.onClick.RemoveAllListeners();
            _gamePlayUI.Lose.BackToMenu.onClick.RemoveAllListeners();

            _cts.Cancel();
            _cts.Dispose();
            _disposable.Clear();
        }

        public override void ComponentsToggle(bool value)
        {
            _gamePlayUI.gameObject.SetActive(value);
            _gamePlayController.gameObject.SetActive(value);
        }

        private void Init()
        {
            Reset();
            UpdateUI();

            _audioService.PlayGamePlayMusic();

            _gamePlayController.InitGame();
        }


        private void UpdateUI()
        {
            _gamePlayUI.Fish.text = $"{_fishCount}/{_maxFish}";
            _gamePlayUI.Coins.text = _coins.ToString();
        }

        private void Reset()
        {
            _isPause = false;
            _fishCount = 0;
            
            _gamePlayUI.Pause.gameObject.SetActive(false);
            _gamePlayUI.Win.gameObject.SetActive(false);
            _gamePlayUI.Lose.gameObject.SetActive(false);
        }

        private void NextLevel()
        {
            int currentLevel = PlayerPrefs.GetInt(Const.CURRENT_LEVEL);

            if (currentLevel < _dataService.Levels.Count)
            {
                currentLevel++;
                PlayerPrefs.SetInt(Const.CURRENT_LEVEL, currentLevel);
            }

            if (currentLevel == _dataService.Levels.Count)
            {
                currentLevel = 0;
                PlayerPrefs.SetInt(Const.CURRENT_LEVEL, currentLevel);
            }

            _stateSwitcher.SwitchState<GamePlayState>();
        }

        private void Pause()
        {
            Time.timeScale = 0;
            _gamePlayUI.Pause.gameObject.SetActive(true);
        }

        private void Resume()
        {
            Time.timeScale = 1;
            _gamePlayUI.Pause.gameObject.SetActive(false);
        }

        private void Repeat() => _stateSwitcher.SwitchState<GamePlayState>();
        private void MainMenu() => _stateSwitcher.SwitchState<MainMenuState>();

        private void DeInit()
        {
            _gamePlayController.ClearGamePlay();
            Time.timeScale = 1;
        }

        private void SwitchMusic() => _gamePlayUI.Pause.Music.Switch(_audioService.SwitchMusic());

        private void SwitchSound() => _gamePlayUI.Pause.Sound.Switch(_audioService.SwitchSound());
    }
}
using System.Threading;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.GamePlay;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages.GamePlayUI;
using UniRx;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class GamePlayState : State
    {
        private GamePlayUI _gamePlayUI;
        private GamePlayController _gamePlayController;
        private CompositeDisposable _disposable = new();
        private CancellationTokenSource _cts = new();
        private int _fishCount, _maxFishes, _coins, _lifes;
        private bool _isPause;
        private int _currentLevel;


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

            _gamePlayUI.Left.onClick.AddListener(() => _gamePlayController.MoveLeft());
            _gamePlayUI.Straight.onClick.AddListener(() => _gamePlayController.MoveStraight());
            _gamePlayUI.Right.onClick.AddListener(() => _gamePlayController.MoveRight());

            _gamePlayController.OnGetCoin += GetCoin;
            _gamePlayController.OnGetFish += GetFish;
            _gamePlayController.OnHitObstacle += HitObstacle;

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

            _gamePlayUI.Left.onClick.RemoveAllListeners();
            _gamePlayUI.Straight.onClick.RemoveAllListeners();
            _gamePlayUI.Right.onClick.RemoveAllListeners();

            _gamePlayController.OnGetCoin -= GetCoin;
            _gamePlayController.OnGetFish -= GetFish;
            _gamePlayController.OnHitObstacle -= HitObstacle;

            _cts.Cancel();
            _cts.Dispose();
            _disposable.Clear();
        }

        public override void ComponentsToggle(bool value)
        {
            _gamePlayUI.gameObject.SetActive(value);
            _gamePlayController.gameObject.SetActive(value);
        }

        private async void Init()
        {
            Reset();
            UpdateUI();

            _audioService.PlayGamePlayMusic();

            int currentBoat = PlayerPrefs.GetInt(Const.CURRENT_BOAT);
            _currentLevel = PlayerPrefs.GetInt(Const.CURRENT_LEVEL);
            _maxFishes += _currentLevel;
            _lifes = PlayerPrefs.GetInt(Const.CURRENT_HEART) + 3;
            UpdateUI();

            await _gamePlayController.Init(currentBoat, _currentLevel);
        }

        private void HitObstacle()
        {
            _lifes--;
            if (_lifes <= 0)
            {
                _lifes = 0;
                Lose();
            }

            UpdateUI();
        }

        private void GetFish()
        {
            _fishCount++;

            if (_fishCount >= _maxFishes)
            {
                Win();
            }

            UpdateUI();
        }

        private void GetCoin()
        {
            _coins++;
            UpdateUI();
        }

        private void Lose()
        {
            int reward = _coins;
            _gamePlayUI.Win.Score.text = reward.ToString();
            _dataService.AddCoins(reward);

            _gamePlayUI.Lose.gameObject.SetActive(true);
            _gamePlayController.ClearGamePlay();

            _audioService.Lose();
        }

        private void Win()
        {
            int reward = 0;
            if (_lifes >= 3)
            {
                foreach (var star in _gamePlayUI.Win.Stars)
                {
                    star.gameObject.SetActive(true);
                }

                reward = 100;
                _dataService.SetLevel(_currentLevel, LevelState.ThreeStar);
            }
            else if (_lifes == 2)
            {
                _gamePlayUI.Win.Stars[0].gameObject.SetActive(true);
                _gamePlayUI.Win.Stars[1].gameObject.SetActive(true);
                _gamePlayUI.Win.Stars[2].gameObject.SetActive(false);
                _dataService.SetLevel(_currentLevel, LevelState.TwoStar);
                reward = 80;
            }
            else if (_lifes == 1)
            {
                _gamePlayUI.Win.Stars[0].gameObject.SetActive(true);
                _gamePlayUI.Win.Stars[1].gameObject.SetActive(false);
                _gamePlayUI.Win.Stars[2].gameObject.SetActive(false);
                _dataService.SetLevel(_currentLevel, LevelState.OneStar);
                reward = 50;
            }

            if (_currentLevel < _dataService.Levels.Count)
            {
                _dataService.SetLevel(_currentLevel + 1, LevelState.Open);
            }

            _gamePlayUI.Win.Score.text = reward.ToString();
            _dataService.AddCoins(reward);

            _gamePlayUI.Win.gameObject.SetActive(true);
            _gamePlayController.ClearGamePlay();

            _audioService.Win();
        }

        private void UpdateUI()
        {
            _gamePlayUI.Fish.text = $"{_fishCount}/{_maxFishes}";
            _gamePlayUI.Coins.text = _coins.ToString();
            _gamePlayUI.Health.text = _lifes.ToString();
        }

        private void Reset()
        {
            _isPause = false;
            _fishCount = 0;
            _maxFishes = 5;

            _gamePlayUI.Pause.gameObject.SetActive(false);
            _gamePlayUI.Win.gameObject.SetActive(false);
            _gamePlayUI.Lose.gameObject.SetActive(false);
        }

        private void NextLevel()
        {
            if (_currentLevel < _dataService.Levels.Count)
            {
                _currentLevel++;
                PlayerPrefs.SetInt(Const.CURRENT_LEVEL, _currentLevel);
            }

            if (_currentLevel == _dataService.Levels.Count)
            {
                _currentLevel = 0;
                PlayerPrefs.SetInt(Const.CURRENT_LEVEL, _currentLevel);
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
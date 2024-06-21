using System.Threading;
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
        private int _score;
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
            _gamePlayUI.Result.Repeat.onClick.AddListener(() => _stateSwitcher.SwitchState<GamePlayState>());
            _gamePlayUI.Result.BackToMenu.onClick.AddListener(() => _stateSwitcher.SwitchState<MainMenuState>());

            _cts = new();
        }

        public override void Unsubsribe()
        {
            _gamePlayUI.Result.Repeat.onClick.RemoveAllListeners();
            _gamePlayUI.Result.BackToMenu.onClick.RemoveAllListeners();

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

            _gamePlayController.InitGame();
        }


        private void UpdateUI()
        {
            _gamePlayUI.Score.text = $"Score:{_score}";
        }

        private void Reset()
        {
            _isPause = false;
            _score = 0;

            _gamePlayUI.Result.gameObject.SetActive(false);
        }

        private void Repeat()
        {
            _stateSwitcher.SwitchState<GamePlayState>();
        }

        private void DeInit()
        {
            _gamePlayUI.Result.gameObject.SetActive(false);

            _gamePlayController.ClearGamePlay();
            Time.timeScale = 1;
        }
    }
}
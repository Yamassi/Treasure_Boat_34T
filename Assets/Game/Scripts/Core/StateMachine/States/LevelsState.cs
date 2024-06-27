using Tretimi.Game.Scripts.Core.StateMachine;
using Tretimi.Game.Scripts.Core.StateMachine.States;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Levels;
using Tretimi.Game.Scripts.UI.Pages.Top;
using UnityEngine;
using Zenject;

namespace Tretimi.Core.SM
{
    public class LevelsState : State
    {
        private IAudioService _audioService;
        private Levels _levels;
        private MainMenu _mainMenu;
        private IDataService _dataService;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            Levels levels,
            IAudioService audioService,
            IDataService dataService,
            Top top,
            MainMenu mainMenu)
        {
            _dataService = dataService;
            _mainMenu = mainMenu;
            _levels = levels;
            _stateSwitcher = stateSwitcher;
            _audioService = audioService;
        }

        public override void Enter()
        {
            ComponentsToggle(true);
            Subsribe();
            SetLevels();
        }

        public override void Exit()
        {
            ComponentsToggle(false);
            Unsubsribe();
        }

        public override void Subsribe()
        {
            foreach (var level in _levels.LevelsItems)
            {
                level.OnSelect += Select;
            }

            _levels.Close.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());
            _levels.Play.onClick.AddListener(
                () =>
                {
                    PlayerPrefs.SetInt(Const.CURRENT_LEVEL, PlayerPrefs.GetInt(Const.SELECTED_LEVEL));
                    _stateSwitcher.SwitchState<GamePlayState>();
                });
        }

        public override void Unsubsribe()
        {
            foreach (var level in _levels.LevelsItems)
            {
                level.OnSelect -= Select;
            }

            _levels.Close.onClick.RemoveAllListeners();
            _levels.Play.onClick.RemoveAllListeners();
        }

        public override void ComponentsToggle(bool value)
        {
            _levels.gameObject.SetActive(value);
            _mainMenu.gameObject.SetActive(value);
        }

        private void Select(int id)
        {
            _dataService.SelectLevel(id);
            SetLevels();
        }

        private void SetLevels()
        {
            var levels = _dataService.Levels;
            Debug.Log($"SetLevels {levels.Count}");
            for (int i = 0; i < levels.Count; i++)
            {
                switch (levels[i].State)
                {
                    case LevelState.Lock:
                        _levels.LevelsItems[i].SetLock();
                        break;
                    case LevelState.Open:
                        _levels.LevelsItems[i].SetOpen();
                        break;
                    case LevelState.OneStar:
                        _levels.LevelsItems[i].SetOpen();
                        _levels.LevelsItems[i].SetStars(LevelsItem.StarsCount.OneStar);
                        break;
                    case LevelState.TwoStar:
                        _levels.LevelsItems[i].SetOpen();
                        _levels.LevelsItems[i].SetStars(LevelsItem.StarsCount.TwoStars);
                        break;
                    case LevelState.ThreeStar:
                        _levels.LevelsItems[i].SetOpen();
                        _levels.LevelsItems[i].SetStars(LevelsItem.StarsCount.ThreeStars);
                        break;
                }
            }

            int selectedLevel = PlayerPrefs.GetInt(Const.SELECTED_LEVEL);
            _levels.LevelsItems[selectedLevel].SetSelected();
        }
    }
}
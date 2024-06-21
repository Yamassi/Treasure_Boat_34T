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
                level.OnPlay += Play;
            }
        }

        public override void Unsubsribe()
        {
            foreach (var level in _levels.LevelsItems)
            {
                level.OnPlay -= Play;
            }
        }

        public override void ComponentsToggle(bool value)
        {
            _levels.gameObject.SetActive(value);
            _mainMenu.gameObject.SetActive(value);
        }

        private void Play(int id)
        {
            PlayerPrefs.SetInt("CurrentLevel", id);
            _stateSwitcher.SwitchState<GamePlayState>();
        }

        private void SetLevels()
        {
            var levels = _dataService.Levels;

            for (int i = 0; i < levels.Count; i++)
            {
                switch (levels[i])
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
        }
    }
}
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using UnityEngine;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class AchievementState : State
    {
        private Achievement _achievement;
        private MainMenu _mainMenu;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService,
            Achievement achievement,
            MainMenu mainMenu)
        {
            _audioService = audioService;
            _stateSwitcher = stateSwitcher;
            _dataService = dataService;
            _uiService = uiService;
            _achievement = achievement;
            _mainMenu = mainMenu;
        }

        public override void Enter()
        {
            ComponentsToggle(true);
            Subsribe();
        }

        public override void Exit()
        {
            ComponentsToggle(false);
            Unsubsribe();
        }

        public override void ComponentsToggle(bool value)
        {
            _achievement.gameObject.SetActive(value);
            _mainMenu.gameObject.SetActive(value);
        }

        public override void Subsribe()
        {

        }

        public override void Unsubsribe()
        {

        }
    }
}

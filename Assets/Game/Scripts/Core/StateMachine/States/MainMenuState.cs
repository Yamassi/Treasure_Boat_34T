using Tretimi.Core.SM;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Bottom;
using Tretimi.Game.Scripts.UI.Pages.Top;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class MainMenuState : State
    {
        private MainMenu _mainMenu;
        private int _currentMode;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService, 
            MainMenu mainMenu,
            Top top,
            Bottom bottom)
        {
            _bottom = bottom;
            _audioService = audioService;
            _stateSwitcher = stateSwitcher;
            _dataService = dataService;
            _uiService = uiService;
            _mainMenu = mainMenu;
            _top = top;
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
        }

        public override void Subsribe()
        {
            base.Subsribe();
            _mainMenu.Play.onClick.AddListener(Play);
        }

        public override void Unsubsribe()
        {
            base.Unsubsribe();
            _mainMenu.Play.onClick.RemoveAllListeners();
        }

        public override void ComponentsToggle(bool value)
        {
            _mainMenu.gameObject.SetActive(value);
            _top.gameObject.SetActive(value);
            _bottom.gameObject.SetActive(value);
        }

        private void Play()
        {
            _stateSwitcher.SwitchState<LevelsState>();
        }

        private void Init()
        {
            _audioService.PlayMenuMusic();

            _uiService.UpdateUI();
        }
    }
}
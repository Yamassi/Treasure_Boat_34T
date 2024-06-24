using Tretimi.Core.SM;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Top;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class MainMenuState : State
    {
        private MainMenu _mainMenu;
        private Top _top;
        private int _currentMode;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService, MainMenu mainMenu,
            Top top)
        {
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
            _mainMenu.Settings.onClick.AddListener(
                () => _stateSwitcher.SwitchState<SettingsState>());
            _mainMenu.Shop.onClick.AddListener(
                () => _stateSwitcher.SwitchState<ShopState>());
            _mainMenu.Home.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());
            _mainMenu.Levels.onClick.AddListener(
                () => _stateSwitcher.SwitchState<LevelsState>());
            _mainMenu.Achievement.onClick.AddListener(
                () => _stateSwitcher.SwitchState<AchievementState>());
            _mainMenu.Play.onClick.AddListener(Play);
        }

        public override void Unsubsribe()
        {
            _mainMenu.Settings.onClick.RemoveAllListeners();
            _mainMenu.Shop.onClick.RemoveAllListeners();
            _mainMenu.Home.onClick.RemoveAllListeners();
            _mainMenu.Levels.onClick.RemoveAllListeners();
            _mainMenu.Achievement.onClick.RemoveAllListeners();
            _mainMenu.Play.onClick.RemoveAllListeners();
        }

        public override void ComponentsToggle(bool value)
        {
            _mainMenu.gameObject.SetActive(value);
        }

        private void Play()
        {
            _stateSwitcher.SwitchState<GamePlayState>();
        }

        private void Init()
        {
            _audioService.PlayMenuMusic();
            _top.gameObject.SetActive(true);

            _uiService.UpdateUI();
        }
    }
}
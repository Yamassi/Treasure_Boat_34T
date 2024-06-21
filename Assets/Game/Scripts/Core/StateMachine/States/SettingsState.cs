using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class SettingsState : State
    {
        private Settings _settings;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService,
            Settings settings)
        {
            _audioService = audioService;
            _stateSwitcher = stateSwitcher;
            _dataService = dataService;
            _uiService = uiService;
            _settings = settings;
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
            _settings.Back.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());
            _settings.Music.Button.onClick.AddListener(SwitchMusic);
            _settings.Sound.Button.onClick.AddListener(SwitchSound);
        }

        public override void Unsubsribe()
        {
            _settings.Back.onClick.RemoveAllListeners();
            _settings.Music.Button.onClick.RemoveAllListeners();
            _settings.Sound.Button.onClick.RemoveAllListeners();
        }

        public override void ComponentsToggle(bool value)
        {
            _settings.gameObject.SetActive(value);
        }

        private void Init()
        {
            _settings.Music.Switch(_audioService.isMusicEnabled);
            _settings.Sound.Switch(_audioService.isSoundEnabled);
        }

        private void SwitchMusic()
        {
            _settings.Music.Switch(_audioService.SwitchMusic());
        }

        private void SwitchSound()
        {
            _settings.Sound.Switch(_audioService.SwitchSound());
        }
    }
}
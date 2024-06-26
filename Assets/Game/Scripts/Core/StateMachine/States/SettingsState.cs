using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Bottom;
using Tretimi.Game.Scripts.UI.Pages.Top;
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
            Settings settings,
            Top top,
            Bottom bottom)
        {
            _bottom = bottom;
            _top = top;
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
            base.Subsribe();
            _settings.Back.onClick.AddListener(() => _stateSwitcher.SwitchState<MainMenuState>());
            _settings.Music.OnSwitch += SwitchMusic;
            _settings.Sound.OnSwitch += SwitchSound;
        }

        public override void Unsubsribe()
        {
            base.Unsubsribe();
            _settings.Back.onClick.RemoveAllListeners();
            _settings.Music.OnSwitch -= SwitchMusic;
            _settings.Sound.OnSwitch -= SwitchSound;
        }

        public override void ComponentsToggle(bool value)
        {
            _settings.gameObject.SetActive(value);
            _top.gameObject.SetActive(value);
            _top.Settings.gameObject.SetActive(!value);
            _bottom.gameObject.SetActive(value);
        }

        private void Init()
        {
            _settings.Music.Switch(_audioService.isMusicEnabled);
            _settings.Sound.Switch(_audioService.isSoundEnabled);
        }

        private void SwitchMusic() => _settings.Music.Switch(_audioService.SwitchMusic());

        private void SwitchSound() => _settings.Sound.Switch(_audioService.SwitchSound());
    }
}
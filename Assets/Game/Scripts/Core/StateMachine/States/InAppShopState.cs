using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages.InAppShop;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class InAppShopState : State
    {
        private InAppShop _inAppShop;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService,
            InAppShop inAppShop)
        {
            _inAppShop = inAppShop;
            _audioService = audioService;
            _stateSwitcher = stateSwitcher;
            _dataService = dataService;
            _uiService = uiService;
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
            _inAppShop.Close.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());
        }

        public override void Unsubsribe()
        {
            _inAppShop.Close.onClick.RemoveAllListeners();
        }

        public override void ComponentsToggle(bool value)
        {
            _inAppShop.gameObject.SetActive(value);
        }

        private void Init()
        {
        }
    }
}
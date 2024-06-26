using Tretimi.Game.Scripts.Core.StateMachine.States;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages.Bottom;
using Tretimi.Game.Scripts.UI.Pages.Top;

namespace Tretimi.Game.Scripts.Core.StateMachine
{
    public abstract class State
    {
        protected IStateSwitcher _stateSwitcher;
        protected IDataService _dataService;
        protected IAudioService _audioService;
        protected IUIService _uiService;
        protected Top _top;
        protected Bottom _bottom;
        public abstract void Enter();
        public abstract void Exit();

        public virtual void Subsribe()
        {
            _top.Settings.onClick.AddListener(()=>_stateSwitcher.SwitchState<SettingsState>());
            _bottom.Shop.onClick.AddListener(()=>_stateSwitcher.SwitchState<ShopState>());
            _bottom.Missions.onClick.AddListener(()=>_stateSwitcher.SwitchState<MissionsState>());
            _bottom.Home.onClick.AddListener(()=>_stateSwitcher.SwitchState<MainMenuState>());
        }

        public virtual void Unsubsribe()
        {
            _top.Settings.onClick.RemoveAllListeners();
            _bottom.Shop.onClick.RemoveAllListeners();
            _bottom.Missions.onClick.RemoveAllListeners();
            _bottom.Home.onClick.RemoveAllListeners();
        }

        public abstract void ComponentsToggle(bool value);
    }
}
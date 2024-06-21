using Tretimi.Game.Scripts.System;

namespace Tretimi.Game.Scripts.Core.StateMachine
{
    public abstract class State
    {
        protected IStateSwitcher _stateSwitcher;
        protected IDataService _dataService;
        protected IAudioService _audioService;
        protected IUIService _uiService;
        public abstract void Enter();
        public abstract void Exit();

        public virtual void Subsribe()
        {
        }

        public virtual void Unsubsribe()
        {
        }

        public abstract void ComponentsToggle(bool value);
    }
}
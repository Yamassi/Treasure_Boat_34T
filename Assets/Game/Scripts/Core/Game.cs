using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Tretimi.Game.Scripts.Core.StateMachine;
using Tretimi.Game.Scripts.Core.StateMachine.States;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI;
using UnityEngine;
using Zenject;
using Tretimi.Core.SM;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Tretimi.Game.Scripts.Core
{
    public class Game : IGame, IStateSwitcher
    {
        private IDataService _dataService;
        private StateMachine.StateMachine _stateMachine;
        public List<State> AllStates { get; private set; }
        private UIHolder _uIHolder;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(IDataService dataService, UIHolder uiHolder, DiContainer diContainer)
        {
            Debug.Log("Inject Game");
            _dataService = dataService;
            _uIHolder = uiHolder;
            _diContainer = diContainer;
        }

        public async void Init()
        {
            _stateMachine = new();

            AllStates = new()
            {
                _diContainer.Resolve<LoadingState>(),
                _diContainer.Resolve<MainMenuState>(),
                _diContainer.Resolve<SettingsState>(),
                _diContainer.Resolve<MissionsState>(),
                _diContainer.Resolve<LevelsState>(),
                _diContainer.Resolve<ShopState>(),
                _diContainer.Resolve<GamePlayState>(),
            };

            await UniTask.Delay(100);

            _stateMachine.Init(AllStates[0]);

            await UniTask.Delay(9000);
            RequestToRate();
        }

        public void DeInit()
        {
            _stateMachine.CurrentState.Exit();
        }

        private void RequestToRate()
        {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
        }

        public void SwitchState<T>() where T : State
        {
            var state = AllStates.FirstOrDefault(s => s is T);
            _stateMachine.ChangeState(state);
        }

        public State GetCurrentState => _stateMachine.CurrentState;
    }
}
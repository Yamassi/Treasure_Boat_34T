using Tretimi.Game.Scripts.Core.Services;
using Tretimi.Game.Scripts.Core;
using Tretimi.Game.Scripts.Core.StateMachine.States;
using Tretimi.Game.Scripts.GamePlay;
using Tretimi.Game.Scripts.System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Tretimi.Core.SM;

namespace Tretimi.Game.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioService;
        [SerializeField] private InAppService inAppService;
        [SerializeField] private GamePlayController _gamePlay;

        public override void InstallBindings()
        {
            BindServices();
            BindCore();
            BindStates();
            BindGamePlay();
        }

        private void BindGamePlay()
        {
            Container.Bind<GamePlayController>().FromInstance(_gamePlay).AsSingle();
        }

        private void BindCore()
        {
            Container.BindInterfacesTo<Core.Game>().FromNew().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<LoadingState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<SettingsState>().AsSingle();
            Container.Bind<MissionsState>().AsSingle();
            Container.Bind<LevelsState>().AsSingle();
            Container.Bind<GamePlayState>().AsSingle();
            Container.Bind<ShopState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IAudioService>().FromInstance(_audioService).AsSingle();
            Container.Bind<IDataService>().To<DataService>().FromNew().AsSingle();
            Container.Bind<IUIService>().To<UIService>().FromNew().AsSingle();
            Container.Bind<IInAppService>().FromInstance(inAppService).AsSingle();
        }
    }
}
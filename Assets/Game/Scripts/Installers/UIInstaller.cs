using Tretimi.Game.Scripts.UI;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Bottom;
using Tretimi.Game.Scripts.UI.Pages.DailyRewards;
using Tretimi.Game.Scripts.UI.Pages.GamePlayUI;
using Tretimi.Game.Scripts.UI.Pages.InAppShop;
using Tretimi.Game.Scripts.UI.Pages.Levels;
using Tretimi.Game.Scripts.UI.Pages.Missions;
using Tretimi.Game.Scripts.UI.Pages.Shop;
using Tretimi.Game.Scripts.UI.Pages.Top;
using UnityEngine;
using Zenject;

namespace Tretimi.Game.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIHolder _uIHolder;

        public override void InstallBindings()
        {
            Container.Bind<UIHolder>().FromInstance(_uIHolder).AsSingle();
            Container.Bind<Loading>().FromInstance(_uIHolder.Loading).AsSingle();
            Container.Bind<MainMenu>().FromInstance(_uIHolder.MainMenu).AsSingle();
            Container.Bind<DailyRewards>().FromInstance(_uIHolder.DailyRewards).AsSingle();
            Container.Bind<Missions>().FromInstance(_uIHolder.Missions).AsSingle();
            Container.Bind<Levels>().FromInstance(_uIHolder.Levels).AsSingle();
            Container.Bind<Achievement>().FromInstance(_uIHolder.Achievement).AsSingle();
            Container.Bind<InAppShop>().FromInstance(_uIHolder.InAppShop).AsSingle();
            Container.Bind<Settings>().FromInstance(_uIHolder.Settings).AsSingle();
            Container.Bind<Shop>().FromInstance(_uIHolder.Shop).AsSingle();
            Container.Bind<Top>().FromInstance(_uIHolder.Top).AsSingle();
            Container.Bind<Bottom>().FromInstance(_uIHolder.Bottom).AsSingle();
            Container.Bind<GamePlayUI>().FromInstance(_uIHolder.GamePlayUI).AsSingle();
        }
    }
}
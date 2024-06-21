using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages.Shop;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class ShopState : State
    {
        private Shop _shop;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService, Shop shop)
        {
            _audioService = audioService;
            _stateSwitcher = stateSwitcher;
            _dataService = dataService;
            _uiService = uiService;
            _shop = shop;
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
            _shop.Close.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());
            _shop.OnTryToBuy += TryToBuy;
        }

        public override void Unsubsribe()
        {
            _shop.Close.onClick.RemoveAllListeners();
            _shop.OnTryToBuy -= TryToBuy;
        }

        public override void ComponentsToggle(bool value)
        {
            _shop.gameObject.SetActive(value);
        }

        private void Init()
        {
        }


        private void TryToBuy(int coins, int tickets)
        {
            if (_dataService.Coins >= coins)
            {
                _dataService.RemoveCoins(coins);

                _uiService.UpdateUI();
            }
        }
    }
}
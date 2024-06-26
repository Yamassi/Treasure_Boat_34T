using System;
using System.Collections.Generic;
using System.Linq;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Bottom;
using Tretimi.Game.Scripts.UI.Pages.Shop;
using Tretimi.Game.Scripts.UI.Pages.Top;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class ShopState : State
    {
        private Shop _shop;


        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            IDataService dataService,
            IAudioService audioService,
            IUIService uiService, 
            Shop shop,
            Top top,
            Bottom bottom)
        {
            _bottom = bottom;
            _top = top;
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
            base.Subsribe();

            _shop.OnTryToBuyHeart += TryToBuyHeart;
            _shop.OnSelectHeart += SelectHeart;
            
            _shop.OnTryToBuyBoat += TryToBuyBoat;
            _shop.OnSelectBoat += SelectBoat;
            
            _shop.OnTryToBuyBackground += TryToBuyBackground;
            _shop.OnSelectBackground += SelectBackground;
        }

        public override void Unsubsribe()
        {
            base.Unsubsribe();

            _shop.OnTryToBuyHeart -= TryToBuyHeart;
            _shop.OnSelectHeart -= SelectHeart;
            
            _shop.OnTryToBuyBoat -= TryToBuyBoat;
            _shop.OnSelectBoat -= SelectBoat;
            
            _shop.OnTryToBuyBackground -= TryToBuyBackground;
            _shop.OnSelectBackground -= SelectBackground;
        }

        public override void ComponentsToggle(bool value)
        {
            _shop.gameObject.SetActive(value);
            _top.gameObject.SetActive(value);
            _bottom.gameObject.SetActive(value);
        }

        private void Init()
        {
            SetHearts();
            SetBoats();
            SetBackgrounds();
        }


        private void TryToBuyHeart(int id, int coins)
        {
            Debug.Log("Buy Heart");
            if (_dataService.Coins >= coins)
            {
                _dataService.RemoveCoins(coins);
                _uiService.UpdateUI();
                
                _dataService.AddHeart(id);
                SetHearts();
            }
        }

        private void SelectHeart(int id)
        {
            _dataService.SelectHeart(id);
            SetHearts();
        }

        private void SetHearts()
        {
            var items = _dataService.Hearts.ToList();
            SetItems(items,_shop.Hearts);
        }

        private void TryToBuyBoat(int id, int coins)
        {
            if (_dataService.Coins >= coins)
            {
                _dataService.RemoveCoins(coins);
                _uiService.UpdateUI();
                
                _dataService.AddBoat(id);
                SetBoats();
            }
        }
        private void SelectBoat(int id)
        {
            _dataService.SelectBoat(id);
            SetBoats();
        }

        private void SetBoats()
        {
            var items = _dataService.Boats.ToList();
            SetItems(items,_shop.Boats);
        }

        private void TryToBuyBackground(int id, int coins)
        {
            if (_dataService.Coins >= coins)
            {
                _dataService.RemoveCoins(coins);
                _uiService.UpdateUI();
                
                _dataService.AddBackground(id);
                SetBackgrounds();
            }
        }
        private void SelectBackground(int id)
        {
            _dataService.SelectBackground(id);
            SetBackgrounds();
        }

        public void SetBackgrounds()
        {
            var items = _dataService.Backgrounds.ToList();
            SetItems(items,_shop.Backgrounds);
        }

        private void SetItems<T>(List<T> items, List<ShopItem> shopItems)
        {
            for (int i = 0; i < items.Count; i++)
            {
                switch (items[i])
                {
                    case ShopItemData.OnSale:
                        shopItems[i].SetOnSale();
                        break;
                    case ShopItemData.Available:
                        shopItems[i].SetAvailable();
                        break;
                    case ShopItemData.Selected:
                        shopItems[i].SetSelected();
                        break;
                }
            }
        }
    }
}
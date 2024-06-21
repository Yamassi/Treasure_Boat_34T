using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.Shop
{
    public class Shop : MonoBehaviour
    {
        public List<ShopItem> ShopItems;
        public Button Close;
        public Action<int, int> OnTryToBuy;
        public Action<int> OnSelectBall;

        private void OnEnable()
        {
            for (int i = 0; i < ShopItems.Count; i++)
            {
                ShopItems[i].OnTryToBuy += TryToBuyBall;
                ShopItems[i].OnSelect += Select;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < ShopItems.Count; i++)
            {
                ShopItems[i].OnTryToBuy -= TryToBuyBall;
                ShopItems[i].OnSelect -= Select;
            }
        }

        private void Select(int id) =>
            OnSelectBall?.Invoke(id);

        private void TryToBuyBall(int id, int price) =>
            OnTryToBuy?.Invoke(id, price);
    }
}
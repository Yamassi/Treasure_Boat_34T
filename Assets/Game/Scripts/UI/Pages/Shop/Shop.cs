using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.Shop
{
    public class Shop : MonoBehaviour
    {
        public List<ShopItem> Hearts;
        public List<ShopItem> Boats;
        public List<ShopItem> Backgrounds;

        public Action<int, int> OnTryToBuyHeart;
        public Action<int> OnSelectHeart;

        public Action<int, int> OnTryToBuyBoat;
        public Action<int> OnSelectBoat;

        public Action<int, int> OnTryToBuyBackground;
        public Action<int> OnSelectBackground;

        private void OnEnable()
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].OnTryToBuy += TryToBuyHeart;
                Hearts[i].OnSelect += SelectHeart;
            }

            for (int i = 0; i < Boats.Count; i++)
            {
                Boats[i].OnTryToBuy += TryToBuyBoat;
                Boats[i].OnSelect += SelectBoat;
            }

            for (int i = 0; i < Backgrounds.Count; i++)
            {
                Backgrounds[i].OnTryToBuy += TryToBuyBackground;
                Backgrounds[i].OnSelect += SelectBackground;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].OnTryToBuy -= TryToBuyHeart;
                Hearts[i].OnSelect -= SelectHeart;
            }

            for (int i = 0; i < Boats.Count; i++)
            {
                Boats[i].OnTryToBuy -= TryToBuyBoat;
                Boats[i].OnSelect -= SelectBoat;
            }

            for (int i = 0; i < Backgrounds.Count; i++)
            {
                Backgrounds[i].OnTryToBuy -= TryToBuyBackground;
                Backgrounds[i].OnSelect -= SelectBackground;
            }
        }

        private void SelectHeart(int id) =>
            OnSelectHeart?.Invoke(id);

        private void TryToBuyHeart(int id, int price) =>
            OnTryToBuyHeart?.Invoke(id, price);

        private void SelectBoat(int id) =>
            OnSelectBoat?.Invoke(id);

        private void TryToBuyBoat(int id, int price) =>
            OnTryToBuyBoat?.Invoke(id, price);

        private void SelectBackground(int id) =>
            OnSelectBackground?.Invoke(id);

        private void TryToBuyBackground(int id, int price) =>
            OnTryToBuyBackground?.Invoke(id, price);
    }
}
using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private int _price;
        [SerializeField] private Button _buy, _select;
        [SerializeField] private Image _selected;
        [SerializeField] private TextMeshProUGUI _priceTxt;
        [SerializeField] private bool _isInApp;

        public Action<int, int> OnTryToBuy;
        public Action<int> OnSelect;

        private void OnValidate()
        {
            _id = transform.GetSiblingIndex();

            if (!_isInApp && _priceTxt!=null)
                _priceTxt.text = _price.ToString();
        }

        private void OnDisable()
        {
            if (!_isInApp)
                _buy.onClick.RemoveAllListeners();

            _select.onClick.RemoveAllListeners();
        }

        [Button]
        public void SetOnSale()
        {
            if (!_isInApp)
            {
                _buy.onClick.RemoveAllListeners();
                _buy.onClick.AddListener(() => OnTryToBuy?.Invoke(_id, _price));
            }

            _buy.gameObject.SetActive(true);
            _select.gameObject.SetActive(false);
            _selected.gameObject.SetActive(false);
        }

        [Button]
        public void SetAvailable()
        {
            _select.onClick.RemoveAllListeners();
            _select.onClick.AddListener(() => OnSelect?.Invoke(_id));
            _buy.gameObject.SetActive(false);
            _select.gameObject.SetActive(true);
            _selected.gameObject.SetActive(false);
        }

        [Button]
        public void SetSelected()
        {
            _buy.gameObject.SetActive(false);
            _select.gameObject.SetActive(false);
            _selected.gameObject.SetActive(true);
        }
    }
}
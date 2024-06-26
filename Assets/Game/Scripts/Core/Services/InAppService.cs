using Cysharp.Threading.Tasks;
using TMPro;
using Tretimi.Game.Scripts.Core.StateMachine.States;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Zenject;

namespace Tretimi.Game.Scripts.Core.Services
{
    public class InAppService : MonoBehaviour, IInAppService
    {
        private IDataService _dataService;
        private IUIService _uiService;
        [SerializeField] private GameObject _inAppLoadingPage;
        [SerializeField] private TextMeshProUGUI _messageText;
        private ShopState _shopState;

        [Inject]
        public void Construct(
            IDataService dataService,
            IUIService uiService,
            ShopState shopState)
        {
            _shopState = shopState;
            _dataService = dataService;
            _uiService = uiService;
        }

        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void PurschaseComplete(Product product)
        {
            Debug.Log($"PurschaseComplete {product}");
            PurchaseCompletedSuccesfull(product);
            CloseInAppLoadingPage("Succesfull!");
        }

        public void PurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
        {
            Debug.Log($"PurchaseFailed {product}");
            CloseInAppLoadingPage("Failed!");
        }

        public void PurchaseDetailedFailed(Product product, PurchaseFailureDescription purchaseFailureDescription)
        {
            Debug.Log($"PurchaseDetailedFailed {product}");
            CloseInAppLoadingPage("Failed!");
        }

        private void PurchaseCompletedSuccesfull(Product product)
        {
            if (product.definition.id == Const.IAP_1)
            {
                _dataService.AddCoins(3000);
                _uiService.UpdateUI();
            }
            if (product.definition.id == Const.IAP_2)
            {
                _dataService.AddCoins(5000);
                _uiService.UpdateUI();
            }
            if (product.definition.id == Const.IAP_3)
            {
                _dataService.AddCoins(8000);
                _uiService.UpdateUI();
            }
            if (product.definition.id == Const.IAP_4)
            {
                _dataService.AddBackground(5);
                _shopState.SetBackgrounds();
            }
            if (product.definition.id == Const.IAP_5)
            {
                _dataService.AddBackground(6);
                _shopState.SetBackgrounds();
            }
            if (product.definition.id == Const.IAP_6)
            {
                _dataService.AddBackground(7);
                _shopState.SetBackgrounds();
            }
        }

        public void OpenInAppLoadingPage(string message)
        {
            Debug.Log($"Message {message}");
            _inAppLoadingPage.SetActive(true);
            _messageText.text = message;
        }

        public async void CloseInAppLoadingPage(string message)
        {
            Debug.Log($"Message {message}");
            _messageText.text = message;

            await UniTask.Delay(2000);
            _inAppLoadingPage.SetActive(false);
        }
    }
}
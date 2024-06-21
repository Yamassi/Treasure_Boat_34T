using Cysharp.Threading.Tasks;
using TMPro;
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

        [Inject]
        public void Construct(
            IDataService dataService,
            IUIService uiService)
        {
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
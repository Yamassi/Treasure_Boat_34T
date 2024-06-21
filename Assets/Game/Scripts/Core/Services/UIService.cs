using System.Threading.Tasks;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Tretimi.Game.Scripts.Core.Services
{
    public class UIService : IUIService
    {
        private UIHolder _uiHolder;
        private IDataService _dataService;

        [Inject]
        public void Construct(IDataService dataService, UIHolder uiHolder)
        {
            _uiHolder = uiHolder;
            _dataService = dataService;
        }

        public void UpdateUI()
        {
            _uiHolder.Top.Coins.text = _dataService.Coins.ToString();
        }

        public async Task SetMenuBackground(int index)
        {
            _uiHolder.Background.sprite = await Assets.GetAsset<Sprite>($"Background{index}");
        }
    }
}
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
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI
{
    public class UIHolder : MonoBehaviour
    {
        public Loading Loading;
        public MainMenu MainMenu;
        public DailyRewards DailyRewards;
        public Missions Missions;
        public InAppShop InAppShop;
        public Levels Levels;
        public Settings Settings;
        public Achievement Achievement;
        public Shop Shop;
        public Top Top;
        public Bottom Bottom;
        public GamePlayUI GamePlayUI;
        public Image Background;
    }
}
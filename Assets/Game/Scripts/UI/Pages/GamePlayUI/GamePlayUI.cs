using System.Threading.Tasks;
using TMPro;
using Tretimi.Game.Scripts.System;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.GamePlayUI
{
    public class GamePlayUI : MonoBehaviour
    {
        public Result Result;
        public TextMeshProUGUI Score;
        public Image Background;

        public async Task SetBackground(int id)
        {
            Background.sprite = await Assets.GetAsset<Sprite>($"GameBackground{id}");
        }
    }
}
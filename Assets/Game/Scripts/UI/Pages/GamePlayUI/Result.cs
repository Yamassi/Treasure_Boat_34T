using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages
{
    public class Result : MonoBehaviour
    {
        public TextMeshProUGUI Header, Score, Earn;
        public Button Repeat;
        [FormerlySerializedAs("StartGame")] public Button NextLevel;
        public Button BackToMenu;
        public Image Background;
        public Image[] Stars;
    }
}
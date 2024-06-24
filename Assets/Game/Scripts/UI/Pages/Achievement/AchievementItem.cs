using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tretimi.Game.Scripts.UI.Pages
{
    public class AchievementItem : MonoBehaviour
    {
        [SerializeField] GameObject _done;
        [SerializeField] GameObject _undone;


        public void SetDone()
        {
            _done.SetActive(true);
            _undone.SetActive(false);
        }

        public void SetUndone()
        {
            _undone.SetActive(true);
            _done.SetActive(false);
        }
    }
}

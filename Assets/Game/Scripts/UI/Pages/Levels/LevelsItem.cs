using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.Levels
{
    public class LevelsItem : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private Button _play;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _opened;
        [SerializeField] private bool _withStars;

        public Action<int> OnPlay;

        private void OnDisable() =>
            _play.onClick.RemoveAllListeners();
        [Button]

        public void SetOpen()
        {
            _lock.gameObject.SetActive(false);

            if (_withStars)
            {
                _stars[0].gameObject.SetActive(false);
                _stars[1].gameObject.SetActive(false);
                _stars[2].gameObject.SetActive(false);
                _opened.gameObject.SetActive(true);
            }

            _play.onClick.RemoveAllListeners();
            _play.onClick.AddListener(() => OnPlay?.Invoke(_id));
        }

        [Button]
        public void SetLock()
        {
            _lock.gameObject.SetActive(true);

            if (_withStars)
            {
                _stars[0].gameObject.SetActive(false);
                _stars[1].gameObject.SetActive(false);
                _stars[2].gameObject.SetActive(false);
            }
        }
        public void SetStars(StarsCount stars)
        {
            switch (stars)
            {
                case StarsCount.OneStar:
                    _stars[0].gameObject.SetActive(true);
                    _stars[1].gameObject.SetActive(false);
                    _stars[2].gameObject.SetActive(false);
                    break;
                case StarsCount.TwoStars:
                    _stars[0].gameObject.SetActive(true);
                    _stars[1].gameObject.SetActive(true);
                    _stars[2].gameObject.SetActive(false);
                    break;
                case StarsCount.ThreeStars:
                    _stars[0].gameObject.SetActive(true);
                    _stars[1].gameObject.SetActive(true);
                    _stars[2].gameObject.SetActive(true);
                    break;
            }
            _opened.gameObject.SetActive(false);
        }

        public enum StarsCount
        {
            OneStar,
            TwoStars,
            ThreeStars
        }
    }
}
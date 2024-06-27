using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.Levels
{
    public class LevelsItem : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private Button _open;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _selected;
        [SerializeField] private bool _withStars;
        [SerializeField] private TextMeshProUGUI _level;

        public Action<int> OnSelect;

        private void OnValidate()
        {
            _level = GetComponentInChildren<TextMeshProUGUI>();
            _level.text = $"{_id + 1}";
        }

        private void OnEnable()
        {
            _open.onClick.RemoveAllListeners();
            _open.onClick.AddListener(() => OnSelect?.Invoke(_id));
        }

        private void OnDisable() =>
            _open.onClick.RemoveAllListeners();

        [Button]
        public void SetSelected()
        {
            Debug.Log("Set Selected");
            _lock.gameObject.SetActive(false);
            _selected.gameObject.SetActive(true);
            _open.gameObject.SetActive(false);
        }

        [Button]
        public void SetOpen()
        {
            Debug.Log("Set Open");
            _lock.gameObject.SetActive(false);
            _selected.gameObject.SetActive(false);
            _open.gameObject.SetActive(true);
        }

        [Button]
        public void SetLock()
        {
            Debug.Log("Set Lock");
            _lock.gameObject.SetActive(true);
            _selected.gameObject.SetActive(false);
            _open.gameObject.SetActive(true);

            if (_withStars)
            {
                _stars[0].gameObject.SetActive(false);
                _stars[1].gameObject.SetActive(false);
                _stars[2].gameObject.SetActive(false);
            }
        }

        public void SetStars(StarsCount stars)
        {
            Debug.Log($"Set Stars {stars}");
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

            _selected.gameObject.SetActive(false);
        }

        public enum StarsCount
        {
            OneStar,
            TwoStars,
            ThreeStars
        }
    }
}
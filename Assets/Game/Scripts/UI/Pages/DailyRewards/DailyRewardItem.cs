using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.DailyRewards
{
    public class DailyRewardItem : MonoBehaviour
    {
        [SerializeField] private int _id, _reward;
        [SerializeField] private RewardType _rewardType;
        [SerializeField] private Image _coinImage, _energyImage;
        [SerializeField] private Image _undone, _taked;
        [SerializeField] private Button _get;
        [SerializeField] private TextMeshProUGUI _rewardTxt;

        public Action<int, int, RewardType> OnGetReward;

        private void OnValidate()
        {
            _id = transform.GetSiblingIndex();
            _rewardTxt.text = _reward.ToString();

            if (_rewardType == RewardType.Coins)
            {
                _coinImage.gameObject.SetActive(true);
                _energyImage.gameObject.SetActive(false);
            }

            if (_rewardType == RewardType.Energy)
            {
                _coinImage.gameObject.SetActive(false);
                _energyImage.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            _get.onClick.RemoveAllListeners();
        }

        [Button]
        public void SetUndone()
        {
            _undone.gameObject.SetActive(true);
            _taked.gameObject.SetActive(false);
            _get.gameObject.SetActive(false);
        }

        [Button]
        public void SetDone()
        {
            _undone.gameObject.SetActive(false);
            _taked.gameObject.SetActive(false);
            _get.gameObject.SetActive(true);

            _get.onClick.RemoveAllListeners();
            _get.onClick.AddListener(() => OnGetReward?.Invoke(_id, _reward, _rewardType));
        }

        [Button]
        public void SetTaked()
        {
            _undone.gameObject.SetActive(false);
            _taked.gameObject.SetActive(true);
            _get.gameObject.SetActive(false);
        }
    }

    public enum RewardType
    {
        Coins,
        Energy
    }
}
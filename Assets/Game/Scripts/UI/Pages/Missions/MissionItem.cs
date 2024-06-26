using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Pages.Missions
{
    public class MissionItem : MonoBehaviour
    {
        [SerializeField] private int _id, _rewardSumm;
        [SerializeField] private Button _complete;
        [SerializeField] private Image _uncomplete, _taked;
        // [SerializeField] private TextMeshProUGUI _reward;

        public Action<int, int> OnGetReward;

        private void OnValidate()
        {
            _id = transform.GetSiblingIndex();
            // _reward.text = _rewardSumm.ToString();
        }

        private void OnDisable() =>
            _complete.onClick.RemoveAllListeners();

        public void SetUncomplete()
        {
            _uncomplete.gameObject.SetActive(true);
            _complete.gameObject.SetActive(false);
            _taked.gameObject.SetActive(false);
        }

        public void SetComplete()
        {
            _uncomplete.gameObject.SetActive(false);
            _complete.gameObject.SetActive(true);
            _taked.gameObject.SetActive(false);

            _complete.onClick.RemoveAllListeners();
            _complete.onClick.AddListener(() => OnGetReward?.Invoke(_id, _rewardSumm));
        }

        public void SetTaked()
        {
            _uncomplete.gameObject.SetActive(false);
            _complete.gameObject.SetActive(false);
            _taked.gameObject.SetActive(true);
        }
    }
}
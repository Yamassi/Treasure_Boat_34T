using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi.Game.Scripts.UI.Components
{
    public class TabManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tabHeader;
        [SerializeField] private List<GameObject> tabs;
        [SerializeField] private Button leftArrowButton;
        [SerializeField] private Button rightArrowButton;
        [SerializeField] private bool _isBoats;
        private int _currentTabIndex = 0;

        private void Start()
        {
            leftArrowButton.onClick.AddListener(PreviousTab);
            rightArrowButton.onClick.AddListener(NextTab);

            UpdateTabVisibility();
        }

        private void OnDisable()
        {
            leftArrowButton.onClick.RemoveListener(PreviousTab);
            rightArrowButton.onClick.RemoveListener(PreviousTab);
        }

        private void PreviousTab()
        {
            _currentTabIndex--;
            if (_currentTabIndex < 0)
            {
                _currentTabIndex = tabs.Count - 1;
            }
            UpdateTabVisibility();
        }

        private void NextTab()
        {
            _currentTabIndex++;
            if (_currentTabIndex >= tabs.Count)
            {
                _currentTabIndex = 0;
            }
            UpdateTabVisibility();
        }

        private void UpdateTabVisibility()
        {
            for (int i = 0; i < tabs.Count; i++)
            {
                tabs[i].SetActive(i == _currentTabIndex);
            }
            if(_tabHeader != null)
            {
                if(!_isBoats)
                {
                    if (_currentTabIndex == 0) _tabHeader.text = "Coins";
                    if (_currentTabIndex == 1) _tabHeader.text = "Hearts";
                    if (_currentTabIndex == 2) _tabHeader.text = "Boats";
                    if (_currentTabIndex == 3) _tabHeader.text = "Backgrounds";
                }
                else
                {
                    if (_currentTabIndex == 0) _tabHeader.text = "STANDARD";
                    if (_currentTabIndex == 1) _tabHeader.text = "INFLATABLE";
                    if (_currentTabIndex == 2) _tabHeader.text = "MOTORBOAT";
                    if (_currentTabIndex == 3) _tabHeader.text = "DINGHY";
                }
            }
        }
    }
}

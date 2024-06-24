using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tretimi
{
    public class PageSwitch : MonoBehaviour
    {
        [SerializeField] Button _prevPage;
        [SerializeField] Button _nextPage;
        [SerializeField] GameObject[] _pages;

        private void OnEnable()
        {
            _prevPage.onClick.AddListener(PrevPage);
            _nextPage.onClick.AddListener(NextPage);
        }

        private void OnDisable()
        {
            _prevPage.onClick.RemoveAllListeners();
            _nextPage.onClick.RemoveAllListeners();
        }

        private void NextPage()
        {
            _pages[0].SetActive(false);
            _pages[1].SetActive(true);
        }
        private void PrevPage()
        {
            _pages[0].SetActive(true);
            _pages[1].SetActive(false);
        }
    }
}

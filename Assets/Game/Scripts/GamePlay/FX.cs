using System;
using DG.Tweening;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class FX : MonoBehaviour
    {
        private Tween _tween;

        public void Init()
        {
            transform.parent = null;
            _tween = transform
                .DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f)
                .OnComplete(() => { Destroy(gameObject); });
        }

        private void OnDisable()
        {
            _tween.Kill();
        }

        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}
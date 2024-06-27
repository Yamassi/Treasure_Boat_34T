using System;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public abstract class FallItem : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private FX _fx;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetGravity(float gravity)
        {
            _rigidbody.gravityScale = gravity;
        }

        private void OnDestroy()
        {
            _fx.gameObject.SetActive(true);
            _fx.Init();
            Debug.Log("Destroyed");
        }
    }
}
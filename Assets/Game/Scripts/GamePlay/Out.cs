using System;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class Out : MonoBehaviour
    {
        public Action OnOut;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Puck>(out Puck puck))
            {
                puck.Stop();
                Destroy(puck.gameObject);
                OnOut?.Invoke();
            }
        }
    }
}
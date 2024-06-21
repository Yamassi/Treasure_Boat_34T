using System;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class Gates : MonoBehaviour
    {
        public Action OnGoal;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Puck>(out Puck puck))
            {
                puck.Stop();
                OnGoal?.Invoke();
            }
        }
    }
}
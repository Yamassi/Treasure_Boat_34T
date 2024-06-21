using Tretimi.Game.Scripts.System;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class HoopObstacle : MonoBehaviour
    {
        [SerializeField] private GameObject _obstacle;
        private IAudioService _audioService;

        public void Init(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 gravityDirection = Physics2D.gravity.normalized;
            Vector2 collisionDirection = (other.transform.position - transform.position).normalized;
            float dotProduct = Vector2.Dot(gravityDirection, collisionDirection);

            if (dotProduct > 0)
            {
                Debug.Log("Объект ударился снизу");
                _obstacle.gameObject.SetActive(false);
            }
            else if (dotProduct < 0)
            {
                Debug.Log("Объект ударился сверху");
                _obstacle.gameObject.SetActive(true);
                _audioService.Obstacle();
            }
            else
            {
                Debug.Log("Объект ударился сбоку");
                _obstacle.gameObject.SetActive(false);
            }
        }
    }
}
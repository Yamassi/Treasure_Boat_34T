using System;
using NaughtyAttributes;
using Tretimi.Game.Scripts.System;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class Boat : MonoBehaviour
    {
        public float moveAmount = 2f;
        public float rotationAngle = 15f;


        private Vector3 leftPosition;
        private Vector3 centerPosition;
        private Vector3 rightPosition;

        private Quaternion leftRotation;
        private Quaternion centerRotation;
        private Quaternion rightRotation;
        private IAudioService _audioService;

        public Action OnHitObstacle, OnGetFish, OnGetCoin;

        public void Init(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void OnEnable()
        {
            centerPosition = transform.position;
            leftPosition = centerPosition + Vector3.left * moveAmount;
            rightPosition = centerPosition + Vector3.right * moveAmount;

            centerRotation = Quaternion.Euler(0, 0, 0);
            leftRotation = Quaternion.Euler(0, 0, rotationAngle);
            rightRotation = Quaternion.Euler(0, 0, -rotationAngle);
        }

        [Button]
        public void MoveLeft()
        {
            transform.position = leftPosition;
            transform.rotation = leftRotation;
        }

        [Button]
        public void MoveCenter()
        {
            transform.position = centerPosition;
            transform.rotation = centerRotation;
        }

        [Button]
        public void MoveRight()
        {
            transform.position = rightPosition;
            transform.rotation = rightRotation;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Obstacle obstacle))
            {
                Debug.Log("Obstacle");
                OnHitObstacle?.Invoke();
                _audioService.Obstacle();
                Destroy(obstacle.gameObject);
            }

            if (other.TryGetComponent(out Fish fish))
            {
                Debug.Log("Fish");
                OnGetFish?.Invoke();
                _audioService.Goal();
                Destroy(fish.gameObject);
            }

            if (other.TryGetComponent(out Coin coin))
            {
                Debug.Log("Coin");
                OnGetCoin?.Invoke();
                _audioService.Goal();
                Destroy(coin.gameObject);
            }
        }
    }
}
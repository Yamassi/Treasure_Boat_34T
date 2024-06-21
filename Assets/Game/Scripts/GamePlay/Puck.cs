using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tretimi.Game.Scripts.System;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class Puck : MonoBehaviour
    {
        [SerializeField] private int _liveTimeInMiliSec;
        [SerializeField] private float _force;
        [SerializeField] private Rigidbody2D _rigidbody;
        private CancellationTokenSource _cts;
        private Tween _tween;
        private IAudioService _audioService;

        private void OnEnable()
        {
            _cts = new();
        }

        private void OnDisable()
        {
            _tween.Kill();
            _cts.Cancel();
            _cts.Dispose();
        }

        public void Init(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void SetForce(float force)
        {
            _force = force;
        }

        public async void Throw(Vector3 target)
        {
            Vector3 direction = target - transform.position;
            const float speed = 20;
            Debug.Log(_force);
            _rigidbody.AddForce(direction.normalized * _force * speed, ForceMode2D.Impulse);
            _audioService.Obstacle();
            await UniTask.Delay(_liveTimeInMiliSec, cancellationToken: _cts.Token);
            Stop();
            Destroy(this.gameObject);
        }

        public void Stop() => _rigidbody.velocity = Vector3.zero;
    }
}
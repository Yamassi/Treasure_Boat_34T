using System;
using Tretimi.Game.Scripts.System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class Hoop : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
        private bool _isBallFlew = false;
        private float _cooldowntime;
        private const float Cooldown = 0.2f;
        private CompositeDisposable _disposable = new();
        private bool _isBlocked = false;
        public Action OnGoal;

        public void Init(IAudioService audioService)
        {
            HoopObstacle[] obstacles = GetComponentsInChildren<HoopObstacle>();

            foreach (var obstacle in obstacles)
            {
                obstacle.Init(audioService);
            }
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }

        private void OnEnable()
        {
            _collider.OnTriggerEnter2DAsObservable().Subscribe(other =>
            {
                if (!_isBallFlew && !_isBlocked)
                {
                    _isBallFlew = true;
                    Vector2 gravityDirection = Physics2D.gravity.normalized;
                    Vector2 collisionDirection = (other.transform.position - transform.position).normalized;
                    float dotProduct = Vector2.Dot(gravityDirection, collisionDirection);

                    if (dotProduct > 0)
                    {
                        _spriteRenderer.sortingOrder = 2;
                    }
                    else if (dotProduct < 0)
                    {
                        _spriteRenderer.sortingOrder = 4;
                        OnGoal?.Invoke();
                    }
                    else
                    {
                        _spriteRenderer.sortingOrder = 2;
                    }
                }
            }).AddTo(_disposable);

            _cooldowntime = 0;

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_isBallFlew)
                {
                    _cooldowntime += Time.deltaTime;

                    if (_cooldowntime >= Cooldown)
                    {
                        _isBallFlew = false;
                        _cooldowntime = 0;
                    }
                }
            }).AddTo(_disposable);
        }
    }
}
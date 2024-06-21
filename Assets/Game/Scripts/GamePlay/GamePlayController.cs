using NaughtyAttributes;
using Tretimi.Game.Scripts.System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class GamePlayController : MonoBehaviour
    {
        [SerializeField] private Transform _ballPoint;
        [SerializeField] private GameObject _ballPrefab;
        private Puck _currentBall;

        private CompositeDisposable _disposable = new();
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            ClearGamePlay();
        }

        [Button]
        public void ClearGamePlay()
        {
            _disposable.Clear();
        }

        public void InitGame()
        {
        }

        public void Spawn()
        {
            _currentBall = Instantiate(_ballPrefab,
                    _ballPoint.position,
                    Quaternion.identity,
                    _ballPoint)
                .GetComponent<Puck>();
            Debug.Log(_currentBall);
            Debug.Log(_audioService);
            _currentBall.Init(_audioService);
        }
    }
}
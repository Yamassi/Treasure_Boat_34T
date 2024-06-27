using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaughtyAttributes;
using Tretimi.Game.Scripts.System;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class GamePlayController : MonoBehaviour
    {
        [SerializeField] private Transform _boatPoint;
        [SerializeField] private Transform _fallItemsPoint;
        [SerializeField] private GameObject _boatPrefab, _fishPrefab, _coinPrefab;
        [SerializeField] private List<GameObject> _obstaclePrefabs = new();
        [SerializeField] private float _gravityDecrease;
        [SerializeField] private float _spawnTimeDecrease;
        [SerializeField] private float _spawnTime;
        private float _gravity = 0.05f;
        private Boat _currentBoat;

        private CompositeDisposable _disposable = new();
        private IAudioService _audioService;

        public Action OnHitObstacle, OnGetFish, OnGetCoin;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public async Task Init(int boat, int level)
        {
            _gravity = Mathf.Max(0, _gravity + level * _gravityDecrease);
            Debug.Log($"Gravity {_gravity}");
            _spawnTime = Mathf.Max(0, _spawnTime - level * _spawnTimeDecrease);

            for (int i = 0; i < 7; i++)
            {
                _obstaclePrefabs.Add(await Assets.GetAsset<GameObject>($"Obstacle{i}"));
            }

            _fishPrefab = await Assets.GetAsset<GameObject>($"Fish");
            _coinPrefab = await Assets.GetAsset<GameObject>($"Coin");
            _boatPrefab = await Assets.GetAsset<GameObject>($"Boat{boat}");

            SpawnBoat();
            StartSpawn();
        }

        protected virtual void OnDisable()
        {
            ClearGamePlay();
        }

        [Button]
        public void ClearGamePlay()
        {
            _disposable.Clear();

            if (_currentBoat != null)
            {
                _currentBoat.OnHitObstacle -= HitObstacle;
                _currentBoat.OnGetFish -= GetFish;
                _currentBoat.OnGetCoin -= GetCoin;
                Destroy(_currentBoat.gameObject);
            }

            var fishes = _fallItemsPoint.GetComponentsInChildren<Fish>();
            foreach (var fish in fishes)
            {
                Destroy(fish.gameObject);
            }

            var obstacles = _fallItemsPoint.GetComponentsInChildren<Obstacle>();
            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle.gameObject);
            }

            var coins = _fallItemsPoint.GetComponentsInChildren<Coin>();
            foreach (var coin in coins)
            {
                Destroy(coin.gameObject);
            }
        }

        private void StartSpawn()
        {
            float timer = 0;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                timer += Time.deltaTime;

                if (timer >= _spawnTime)
                {
                    timer = 0;
                    RandomSpawn();
                }
            }).AddTo(_disposable);
        }

        private void RandomSpawn()
        {
            int itemType = Random.Range(0, 3);

            if (itemType == 0)
                SpawnRandomObstacle();
            else if (itemType == 1)
                SpawnFish();
            else
                SpawnCoin();
        }

        public void MoveLeft() => _currentBoat?.MoveLeft();

        public void MoveStraight() => _currentBoat?.MoveCenter();
        public void MoveRight() => _currentBoat?.MoveRight();

        private void SpawnRandomObstacle()
        {
            int randomObstacle = Random.Range(0, _obstaclePrefabs.Count);

            var spawnPosition = GetRandomPosition();

            var obstacle = Instantiate(_obstaclePrefabs[randomObstacle],
                spawnPosition,
                Quaternion.identity,
                _fallItemsPoint);

            obstacle.GetComponent<Obstacle>().SetGravity(_gravity);
        }

        private void SpawnCoin()
        {
            var spawnPosition = GetRandomPosition();

            var coin = Instantiate(_coinPrefab,
                spawnPosition,
                Quaternion.identity,
                _fallItemsPoint);

            coin.GetComponent<Coin>().SetGravity(_gravity);
        }

        private void SpawnFish()
        {
            var spawnPosition = GetRandomPosition();

            var fish = Instantiate(_fishPrefab,
                spawnPosition,
                Quaternion.identity,
                _fallItemsPoint);

            fish.GetComponent<Fish>().SetGravity(_gravity);
        }

        public void SpawnBoat()
        {
            _currentBoat = Instantiate(_boatPrefab,
                    _boatPoint.position,
                    Quaternion.identity,
                    _boatPoint)
                .GetComponent<Boat>();

            _currentBoat.Init(_audioService);
            _currentBoat.OnHitObstacle += HitObstacle;
            _currentBoat.OnGetFish += GetFish;
            _currentBoat.OnGetCoin += GetCoin;
        }

        private void GetFish() => OnGetFish?.Invoke();

        private void HitObstacle() => OnHitObstacle?.Invoke();

        private void GetCoin() => OnGetCoin?.Invoke();

        private Vector3 GetRandomPosition()
        {
            int randomPosition = Random.Range(0, 3);
            float positionX = 0;

            if (randomPosition == 0)
                positionX = 1.6f;
            else if (randomPosition == 1)
                positionX = 0;
            else if (randomPosition == 2)
                positionX = -1.6f;

            return new Vector3(positionX, _fallItemsPoint.position.y, _fallItemsPoint.position.z);
        }
    }
}
using System;
using UniRx;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class TouchSlider : IDisposable
    {
        public float moveSpeed = 0.3f;
        public float leftLimit = -6.7f;
        public float rightLimit = 3f;

        private bool isDragging = false;
        private Vector3 startPosition;

        private CompositeDisposable _disposable = new();

        private Transform _target;

        public TouchSlider(Transform target)
        {
            _target = target;
        }

        public void Init()
        {
            Input();
        }

        private void Input()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (UnityEngine.Input.touchCount > 0)
                {
                    Touch touch = UnityEngine.Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                        if (hit.collider != null && hit.collider.GetComponent<Puck>())
                        {
                            isDragging = true;
                            startPosition = touch.position;
                        }
                    }
                    else if (touch.phase == TouchPhase.Moved && isDragging)
                    {
                        float deltaX = touch.position.x - startPosition.x;
                        Vector3 newPosition =
                            _target.position + new Vector3(deltaX * moveSpeed * Time.deltaTime, 0f, 0f);
                        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);
                        _target.position = newPosition;
                        startPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        isDragging = false;
                    }
                }
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Clear();
        }
    }
}
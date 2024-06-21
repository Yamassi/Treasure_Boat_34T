using Tretimi.Game.Scripts.System;
using UnityEngine;

namespace Tretimi.Game.Scripts.GamePlay
{
    public class Obstacle : MonoBehaviour
    {
        private IAudioService _audioService;

        public void Init(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _audioService.Obstacle();
        }
    }
}
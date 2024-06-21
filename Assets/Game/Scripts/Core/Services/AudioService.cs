using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Tretimi.Game.Scripts.System;
using UnityEngine;
using UnityEngine.Audio;

namespace Tretimi.Game.Scripts.Core.Services
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private bool _isEnable;

        [ShowIf("_isEnable")] [SerializeField] private AudioSource _musicPlayer, _soundPlayer;
        [ShowIf("_isEnable")] [SerializeField] protected AudioMixer _audioMixer;
        [ShowIf("_isEnable")] [SerializeField] private AudioClip[] _gamePlayMusic;
        [ShowIf("_isEnable")] [SerializeField] private AudioClip[] _menuMusic;

        [ShowIf("_isEnable")] [SerializeField] private AudioClip _click,
            _obstacle,
            _goal,
            _win,
            _lose;

        public int CurrentMusic { get; private set; }
        public bool _isMainMenu = true;
        private bool _isSoundEnabled, _isMusicEnabled;


        private void Awake()
        {
            Initialize();
        }

        private async void Initialize()
        {
            DontDestroyOnLoad(this.gameObject);

            await UniTask.Delay(100);
            if (_isEnable)
                PlayRandomMusic();

            _isSoundEnabled = PlayerPrefs.GetFloat("SoundVolume") == 1;
            _isMusicEnabled = PlayerPrefs.GetFloat("MusicVolume") == 1;
        }

        public async void LoadSettingsValues()
        {
            await UniTask.Delay(100);
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            float soundVolume = PlayerPrefs.GetFloat("SoundVolume");

            Debug.Log($"Load Settings musis is {musicVolume}, sound is {soundVolume}");
            _audioMixer.SetFloat("Sound", Mathf.Log10(Mathf.Clamp(soundVolume, 0.0001f, 1f)) * 20);
            _audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(musicVolume, 0.0001f, 1f)) * 20);
        }

        public void ChangeSoundVolume(float volume)
        {
            _audioMixer.SetFloat("Sound", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
            PlayerPrefs.SetFloat("SoundVolume", (int)volume);
        }

        public void ChangeMusicVolume(float volume)
        {
            _audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
            PlayerPrefs.SetFloat("MusicVolume", (int)volume);
        }

        public void PlayClick() => PlaySound(_click);

        public void Obstacle() => PlaySound(_obstacle);

        public void Goal() => PlaySound(_goal);

        public void Win() => PlaySound(_win);

        public void Lose() => PlaySound(_lose);


        private void PlaySound(AudioClip audio)
        {
            if (audio != null)
                _soundPlayer.PlayOneShot(audio);
        }

        public void PlayMusicByIndex(int index)
        {
            PlayMusic(_gamePlayMusic[index]);
            CurrentMusic = index;
        }

        private void PlayMusic(AudioClip audio)
        {
            if (audio != null)
            {
                _musicPlayer.Stop();
                _musicPlayer.clip = audio;
                _musicPlayer.Play();
            }
        }

        public void PlayRandomMusic()
        {
            int randomMusic = Random.Range(0, _gamePlayMusic.Length);

            if (_menuMusic == null || _menuMusic.Length == 0)
                return;

            if (_gamePlayMusic == null || _gamePlayMusic.Length == 0)
                return;

            if (_isMainMenu)
                PlayMusic(_menuMusic[0]);

            else

                PlayMusic(_gamePlayMusic[0]);


            CurrentMusic = randomMusic;
        }

        private void Update()
        {
            if (!_musicPlayer.isPlaying && _isEnable)
            {
                PlayRandomMusic();
            }
        }

        public void StopMusic()
        {
            _isEnable = false;
            _musicPlayer.Stop();
        }

        public void ResumeMusic()
        {
            _isEnable = true;
        }

        public void PlayMenuMusic()
        {
            if (_menuMusic == null || _menuMusic.Length == 0)
                return;

            if (_musicPlayer.clip != _menuMusic[0])
            {
                _isMainMenu = true;
                PlayRandomMusic();
            }
        }

        public void PlayGamePlayMusic()
        {
            if (_gamePlayMusic == null || _gamePlayMusic.Length == 0)
                return;

            if (_musicPlayer.clip != _gamePlayMusic[0])
            {
                _isMainMenu = false;
                PlayRandomMusic();
            }
        }

        public bool SwitchSound()
        {
            _isSoundEnabled = !_isSoundEnabled;
            UpdateVolume();
            return _isSoundEnabled;
        }

        public bool SwitchMusic()
        {
            _isMusicEnabled = !_isMusicEnabled;
            UpdateVolume();
            return _isMusicEnabled;
        }

        public bool isSoundEnabled => _isSoundEnabled;
        public bool isMusicEnabled => _isMusicEnabled;

        private void UpdateVolume()
        {
            int soundVolume = _isSoundEnabled ? 1 : 0;
            int musicVolume = _isMusicEnabled ? 1 : 0;

            ChangeSoundVolume(soundVolume);
            ChangeMusicVolume(musicVolume);
        }
    }
}
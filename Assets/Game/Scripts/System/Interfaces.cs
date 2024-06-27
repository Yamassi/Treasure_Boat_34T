using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tretimi.Game.Scripts.Core.StateMachine;
using Tretimi.Game.Scripts.Data;

namespace Tretimi.Game.Scripts.System
{
    public interface IAudioService
    {
        void LoadSettingsValues();
        void PlayRandomMusic();
        void StopMusic();
        void ResumeMusic();
        void PlayClick();
        void Obstacle();
        void Goal();
        void Win();
        void Lose();
        void PlayMenuMusic();
        void PlayGamePlayMusic();
        bool SwitchSound();
        bool SwitchMusic();
        bool isSoundEnabled { get; }
        bool isMusicEnabled { get; }
    }

    public interface IGame
    {
        void Init();
        void DeInit();
    }

    public interface IUIService
    {
        void UpdateUI();
        Task SetMenuBackground(int index);
    }

    public interface IDataService
    {
        void LoadData();
        void SaveData();
        int Coins { get; }
        void AddCoins(int coins);
        void RemoveCoins(int coins);
        DateTime NextRewardTime { get; }
        void SetNextRewardTime(DateTime dateTime);
        IReadOnlyList<RewardState> Rewards { get; }
        void SetReward(int id, RewardState state);
        IReadOnlyList<MissionState> Missions { get; }
        void SetMission(int id, MissionState state);
        IReadOnlyList<LevelData> Levels { get; }
        void SetLevel(int id, LevelState state);
        void SelectLevel(int id);
        IReadOnlyList<ShopItemData> Hearts { get; }
        void AddHeart(int id);
        void SelectHeart(int id);
        IReadOnlyList<ShopItemData> Boats { get; }
        void AddBoat(int id);
        void SelectBoat(int id);
        IReadOnlyList<ShopItemData> Backgrounds { get; }
        void AddBackground(int id);
        void SelectBackground(int id);
    }

    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : State;

        State GetCurrentState { get; }
    }

    public interface IInAppService
    {
        void OpenInAppLoadingPage(string message);
    }
}
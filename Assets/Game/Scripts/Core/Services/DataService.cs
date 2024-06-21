using System;
using System.Collections.Generic;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using UnityEngine;

namespace Tretimi.Game.Scripts.Core.Services
{
    public class DataService : IDataService
    {
        private SaveData _data;

        public void LoadData()
        {
            SaveData saveData = DataProvider.LoadDataJSON();

            if (saveData is null)
            {
                Debug.Log("Saved Data is null and reseted");
                _data = new()
                {
                    Coins = Const.FirstMoney,
                };

                PlayerPrefs.SetFloat("MusicVolume", 1);
                PlayerPrefs.SetFloat("SoundVolume", 1);
            }
            else
                _data = saveData;
        }

        public int Coins => _data.Coins;

        public void AddCoins(int coins)
        {
            if (coins > 0)
                _data.Coins += coins;
        }

        public void RemoveCoins(int coins)
        {
            if (coins > 0)
                _data.Coins -= coins;

            if (_data.Coins < 0)
                _data.Coins = 0;
        }

        public DateTime NextRewardTime => _data.NextRewardTime;

        public void SetNextRewardTime(DateTime dateTime)
        {
            _data.NextRewardTime = dateTime;
        }

        public IReadOnlyList<RewardState> Rewards => _data.Rewards;

        public void SetReward(int id, RewardState state)
        {
            _data.Rewards[id] = state;
        }

        public IReadOnlyList<MissionState> Missions => _data.Missions;

        public void SetMission(int id, MissionState state)
        {
            _data.Missions[id] = state;
        }

        public IReadOnlyList<LevelState> Levels => _data.Levels;

        public void SetLevel(int id, LevelState state)
        {
            _data.Levels[id] = state;
        }

        public void SaveData() => DataProvider.SaveDataJSON(_data);
    }
}
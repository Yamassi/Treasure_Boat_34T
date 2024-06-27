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
                List<LevelState> levels = new();
                for (int i = 0; i < 24; i++)
                {
                    if (i == 0)
                        levels.Add(LevelState.Selected);
                    else
                        levels.Add(LevelState.Lock);
                }

                List<MissionState> missions = new();
                for (int i = 0; i < 7; i++)
                {
                    missions.Add(MissionState.Uncomplete);
                }

                Debug.Log("Saved Data is null and reseted");
                _data = new()
                {
                    Coins = Const.FIRST_MONEY,
                    Levels = new(levels),
                    Missions = new(missions),
                    Hearts = new()
                    {
                        ShopItemData.Selected,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale
                    },
                    Boats = new()
                    {
                        ShopItemData.Selected,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale
                    },
                    Backgrounds = new()
                    {
                        ShopItemData.Selected,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale,
                        ShopItemData.OnSale
                    }
                };

                PlayerPrefs.SetFloat(Const.MUSIC_VOLUME, 1);
                PlayerPrefs.SetFloat(Const.SOUND_VOLUME, 1);

                PlayerPrefs.SetInt(Const.CURRENT_LEVEL, 0);
                PlayerPrefs.SetInt(Const.CURRENT_HEART, 0);
                PlayerPrefs.SetInt(Const.CURRENT_BOAT, 0);
                PlayerPrefs.SetInt(Const.CURRENT_BACKGROUND, 0);
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

        public void SelectLevel(int id)
        {
            int currentItem = PlayerPrefs.GetInt(Const.CURRENT_LEVEL);
            _data.Levels[currentItem] = LevelState.Open;

            _data.Levels[id] = LevelState.Selected;
            PlayerPrefs.SetInt(Const.CURRENT_LEVEL, id);
        }

        public IReadOnlyList<ShopItemData> Hearts => _data.Hearts;

        public void AddHeart(int id)
        {
            _data.Hearts[id] = ShopItemData.Available;
        }

        public void SelectHeart(int id)
        {
            int currentItem = PlayerPrefs.GetInt(Const.CURRENT_HEART);
            _data.Hearts[currentItem] = ShopItemData.Available;
            Debug.Log($"Current Item - {currentItem}");
            Debug.Log($"Current Item Data - {_data.Hearts[currentItem]}");

            _data.Hearts[id] = ShopItemData.Selected;
            PlayerPrefs.SetInt(Const.CURRENT_HEART, id);
            for (int i = 0; i < _data.Hearts.Count; i++)
            {
                Debug.Log(_data.Hearts[i]);
            }
        }

        public IReadOnlyList<ShopItemData> Boats => _data.Boats;

        public void AddBoat(int id)
        {
            _data.Boats[id] = ShopItemData.Available;
        }

        public void SelectBoat(int id)
        {
            int currentItem = PlayerPrefs.GetInt(Const.CURRENT_BOAT);
            _data.Boats[currentItem] = ShopItemData.Available;

            _data.Boats[id] = ShopItemData.Selected;
            PlayerPrefs.SetInt(Const.CURRENT_BOAT, id);
        }

        public IReadOnlyList<ShopItemData> Backgrounds => _data.Backgrounds;

        public void AddBackground(int id)
        {
            _data.Backgrounds[id] = ShopItemData.Available;
        }

        public void SelectBackground(int id)
        {
            int currentItem = PlayerPrefs.GetInt(Const.CURRENT_BACKGROUND);
            _data.Backgrounds[currentItem] = ShopItemData.Available;

            _data.Backgrounds[id] = ShopItemData.Selected;
            PlayerPrefs.SetInt(Const.CURRENT_BACKGROUND, id);
        }

        public void SaveData() => DataProvider.SaveDataJSON(_data);
    }
}
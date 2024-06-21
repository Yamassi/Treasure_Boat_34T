using System;
using System.Collections.Generic;

namespace Tretimi.Game.Scripts.Data
{
    [Serializable]
    public class SaveData
    {
        public int Coins;
        public DateTime NextRewardTime;
        public List<RewardState> Rewards;
        public List<MissionState> Missions;
        public List<LevelState> Levels;
    }

    [Serializable]
    public enum RewardState
    {
        Undone,
        Done,
        Taked,
    }

    [Serializable]
    public enum ShopItem
    {
        OnSale,
        Available,
        Selected
    }

    [Serializable]
    public enum MissionState
    {
        Uncomplete,
        Complete,
        Taked
    }

    [Serializable]
    public enum LevelState
    {
        Lock,
        Open,
        OneStar,
        TwoStar,
        ThreeStar
    }
}
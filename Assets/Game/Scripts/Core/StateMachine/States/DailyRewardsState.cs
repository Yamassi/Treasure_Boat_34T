using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages.DailyRewards;
using Tretimi.Game.Scripts.UI.Pages.Top;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class DailyRewardsState : State
    {
        private DailyRewards _dailyRewards;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            DailyRewards dailyRewards,
            IAudioService audioService,
            Top top,
            IDataService dataService,
            IUIService uiService)
        {
            _uiService = uiService;
            _dataService = dataService;
            _dailyRewards = dailyRewards;
            _stateSwitcher = stateSwitcher;
            _audioService = audioService;
        }

        public override void Enter()
        {
            ComponentsToggle(true);
            Subsribe();
            Init();
        }

        public override void Exit()
        {
            ComponentsToggle(false);
            Unsubsribe();
        }

        public override void Subsribe()
        {
            _dailyRewards.Close.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());

            foreach (var rewardItem in _dailyRewards.RewardItems)
            {
                rewardItem.OnGetReward += GetReward;
            }
        }

        public override void Unsubsribe()
        {
            _dailyRewards.Close.onClick.RemoveAllListeners();

            foreach (var rewardItem in _dailyRewards.RewardItems)
            {
                rewardItem.OnGetReward -= GetReward;
            }
        }

        public override void ComponentsToggle(bool value)
        {
            _dailyRewards.gameObject.SetActive(value);
        }

        private void Init()
        {
            SetRewards();
        }

        private void GetReward(int id, int reward, RewardType rewardType)
        {
            _dataService.SetReward(id, RewardState.Taked);
            _dataService.AddCoins(reward);
            _uiService.UpdateUI();

            SetRewards();
        }

        private void SetRewards()
        {
            for (int i = 0; i < _dataService.Rewards.Count; i++)
            {
                switch (_dataService.Rewards[i])
                {
                    case RewardState.Undone:
                        _dailyRewards.RewardItems[i].SetUndone();
                        break;
                    case RewardState.Done:
                        _dailyRewards.RewardItems[i].SetDone();
                        break;
                    case RewardState.Taked:
                        _dailyRewards.RewardItems[i].SetTaked();
                        break;
                }
            }
        }
    }
}
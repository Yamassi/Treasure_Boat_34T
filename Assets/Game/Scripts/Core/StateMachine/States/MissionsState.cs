using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Missions;
using Tretimi.Game.Scripts.UI.Pages.Top;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class MissionsState : State
    {
        private Missions _missions;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            Missions missions,
            IAudioService audioService,
            Top top,
            MainMenu mainMenu,
            IDataService dataService,
            IUIService uiService)
        {
            _uiService = uiService;
            _dataService = dataService;
            _missions = missions;
            _stateSwitcher = stateSwitcher;
            _audioService = audioService;
        }

        public override void Enter()
        {
            ComponentsToggle(true);
            Subsribe();
            SetMissions();
        }

        public override void Exit()
        {
            ComponentsToggle(false);
            Unsubsribe();
        }

        public override void Subsribe()
        {
            _missions.Close.onClick.AddListener(
                () => _stateSwitcher.SwitchState<MainMenuState>());
            for (int i = 0; i < _missions.MissionsItems.Count; i++)
            {
                _missions.MissionsItems[i].OnGetReward += GetReward;
            }
        }

        public override void Unsubsribe()
        {
            _missions.Close.onClick.RemoveAllListeners();
            for (int i = 0; i < _missions.MissionsItems.Count; i++)
            {
                _missions.MissionsItems[i].OnGetReward -= GetReward;
            }
        }

        public override void ComponentsToggle(bool value)
        {
            _missions.gameObject.SetActive(value);
        }

        private void GetReward(int id, int reward)
        {
            _dataService.AddCoins(reward);
            _dataService.SetMission(id, MissionState.Taked);
            _uiService.UpdateUI();
            SetMissions();
        }

        private void SetMissions()
        {
            var missions = _dataService.Missions;

            for (int i = 0; i < missions.Count; i++)
            {
                switch (missions[i])
                {
                    case MissionState.Uncomplete:
                        _missions.MissionsItems[i].SetUncomplete();
                        break;
                    case MissionState.Complete:
                        _missions.MissionsItems[i].SetComplete();
                        break;
                    case MissionState.Taked:
                        _missions.MissionsItems[i].SetTaked();
                        break;
                }
            }
        }
    }
}
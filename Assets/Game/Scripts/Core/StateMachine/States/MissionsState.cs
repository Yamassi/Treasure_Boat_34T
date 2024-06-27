using System.Linq;
using Tretimi.Game.Scripts.Data;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using Tretimi.Game.Scripts.UI.Pages.Bottom;
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
            Bottom bottom,
            MainMenu mainMenu,
            IDataService dataService,
            IUIService uiService)
        {
            _bottom = bottom;
            _top = top;
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
            base.Subsribe();
            for (int i = 0; i < _missions.MissionsItems.Count; i++)
            {
                _missions.MissionsItems[i].OnGetReward += GetReward;
            }
        }

        public override void Unsubsribe()
        {
            base.Unsubsribe();
            for (int i = 0; i < _missions.MissionsItems.Count; i++)
            {
                _missions.MissionsItems[i].OnGetReward -= GetReward;
            }
        }

        public override void ComponentsToggle(bool value)
        {
            _missions.gameObject.SetActive(value);
            _top.gameObject.SetActive(value);
            _bottom.gameObject.SetActive(value);
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
            if (_dataService.Coins >= 100 && _dataService.Missions[0] == MissionState.Uncomplete)
                _dataService.SetMission(0,MissionState.Complete);
            
            if (_dataService.Coins >= 1000 && _dataService.Missions[3] == MissionState.Uncomplete)
                _dataService.SetMission(3,MissionState.Complete);
            
            if (_dataService.Coins >= 5000 && _dataService.Missions[4] == MissionState.Uncomplete)
                _dataService.SetMission(4,MissionState.Complete);
            
            int threeStarsLevelsCount = _dataService.Levels.Count(s => s == LevelState.ThreeStar);
            
            if (threeStarsLevelsCount >= 5 && _dataService.Missions[1] == MissionState.Uncomplete)
                _dataService.SetMission(1,MissionState.Complete);
            
            if (threeStarsLevelsCount >= 10 && _dataService.Missions[2] == MissionState.Uncomplete)
                _dataService.SetMission(2,MissionState.Complete);
            
            if (threeStarsLevelsCount >= 20 && _dataService.Missions[5] == MissionState.Uncomplete)
                _dataService.SetMission(5,MissionState.Complete);

            if (threeStarsLevelsCount == _dataService.Levels.Count &&
                _dataService.Missions[6] == MissionState.Uncomplete)
            {
                _dataService.SetMission(6,MissionState.Complete);
                _dataService.AddBoat(3);
            }
                
            
            
            
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
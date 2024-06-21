using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tretimi.Game.Scripts.System;
using Tretimi.Game.Scripts.UI.Pages;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tretimi.Game.Scripts.Core.StateMachine.States
{
    public class LoadingState : State
    {
        private CompositeDisposable _disposable = new();
        private Loading _loading;

        [Inject]
        public void Construct(IStateSwitcher stateSwitcher,
            Loading loading,
            IUIService uiService)
        {
            _stateSwitcher = stateSwitcher;
            _loading = loading;
            _uiService = uiService;
        }

        public override void Enter()
        {
            ComponentsToggle(true);
            Subsribe();
            Loading();
        }

        public override void Exit()
        {
            ComponentsToggle(false);
            Unsubsribe();
        }

        public override void Subsribe()
        {
        }

        public override void Unsubsribe()
        {
            _disposable.Clear();
        }

        public override void ComponentsToggle(bool value)
        {
            _loading.gameObject.SetActive(value);
        }

        private async void Loading()
        {
            Slider slider = _loading.Slider;
            float time = 0;
            float loading = 0;
            slider.maxValue = 100;

            Observable.EveryUpdate().Subscribe(_ =>
            {
                time += Time.deltaTime;
                loading = _loading.SliderCurve.Evaluate(time / 3) * 100;

                if (loading >= 100)
                {
                    loading = 100;
                    _loading.Slider.value = loading;
                    _disposable.Clear();

                    _stateSwitcher.SwitchState<MainMenuState>();
                }

                slider.value = loading;
            }).AddTo(_disposable);

            await _loading.Logo.transform.DOPunchRotation(new Vector3(0, 360, 0), 1);
            await _loading.Logo.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f);
        }
    }
}
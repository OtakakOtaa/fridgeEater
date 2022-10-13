using System;
using EventsSystem;
using EventsSystem.Signals;
using GameScene.Fridge.Model;
using GameScene.Fridge.View;
using GameScene.Systems;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeRule : IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly GameTimer _timer;

        private readonly FridgeModel _fridgeModel;
        private readonly FridgeView _fridgeView;

        private readonly CompositeDisposable _disposable = new();
        
        public FridgeRule(SignalBus signalBus, FridgeModel fridgeModel, FridgeView fridgeView, GameTimer timer)
        {
            _signalBus = signalBus;
            _timer = timer;
            
            _fridgeModel = fridgeModel;
            _fridgeView = fridgeView;
            
            var signal = CreateInitializeFridgeSignal();
            InitializeView(signal);
            
            SubscribeModel();
            SubscribeView();
            
            _signalBus?.Fire(signal);
        }

        private void SubscribeModel()
        {
            _timer.Time
                .Buffer(TimeSpan.FromSeconds(_fridgeModel.FridgeSettings.HungerDamageDeltaDelay))
                .Subscribe(_ =>
                {
                    _fridgeModel.TakeAwaySatiety(_fridgeModel.FridgeSettings.HungerDamage);
                })
                .AddTo(_disposable);
        }

        private void SubscribeView()
        {
        }
        
        private void InitializeView(FridgeInitializeSignal signal)
        {
            _fridgeView.Initialize();
            _fridgeView.SubscribeToModel(signal);
        }
        
        private FridgeInitializeSignal CreateInitializeFridgeSignal()
        {
            return new FridgeInitializeSignal
            {
                Fridge = _fridgeModel.Transform,
                FridgeSatietyReadable = _fridgeModel.FridgeSatiety,
            };
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
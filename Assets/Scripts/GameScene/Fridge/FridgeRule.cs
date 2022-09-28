using EventsSystem;
using EventsSystem.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeRule
    {
        private readonly SignalBus _signalBus;
        
        private readonly FridgeModel _fridgeModel;
        private readonly FridgeView _fridgeView;

        public FridgeRule(SignalBus signalBus, FridgeModel fridgeModel, FridgeView fridgeView)
        {
            _signalBus = signalBus;
            _fridgeModel = fridgeModel;
            _fridgeView = fridgeView;
            
            SubscribeModel();
            _signalBus?.Fire(new FridgeInitializeSignal { Fridge = _fridgeModel.Transform });
        }

        private void SubscribeModel()
        {
        }
    }
}
using EventsSystem;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeEventMediator
    {
        private SignalBus _signalBus;

        private FridgeModel _fridgeModel;
        private FridgeView _fridgeView;

        public FridgeEventMediator(SignalBus signalBus, FridgeModel fridgeModel, FridgeView fridgeView)
        {
            _signalBus = signalBus;
            _fridgeModel = fridgeModel;
            _fridgeView = fridgeView;
        }
        
    }
}
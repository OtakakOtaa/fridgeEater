using EventsSystem;

namespace GameScene.Fridge
{
    public class FridgeEventMediator
    {
        private EventBus _eventBus;

        private FridgeModel _fridgeModel;
        private FridgeView _fridgeView;

        public FridgeEventMediator(EventBus eventBus, FridgeModel fridgeModel, FridgeView fridgeView)
        {
            _eventBus = eventBus;
            _fridgeModel = fridgeModel;
            _fridgeView = fridgeView;
        }
        
    }
}
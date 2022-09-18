using EventsSystem.Signals;
using GameScene.Fridge;
using Zenject;

namespace GameScene.Systems
{
    public class GameSceneGate : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly FridgeModel _fridge;
        
        public GameSceneGate(SignalBus signalBus, FridgeModel fridge)
        {
            _signalBus = signalBus;
            _fridge = fridge;
        }
        
        public void Initialize()
        {
            _signalBus.Fire(new StartGameSceneSignal(_fridge.Transform));
        }
    }
}
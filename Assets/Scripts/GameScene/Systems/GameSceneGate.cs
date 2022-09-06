using EventsSystem;
using GameScene.Fridge;
using UnityEngine;
using Zenject;

namespace GameScene
{
    public class GameSceneGate : IInitializable
    {
        private EventBus _eventBus;
        private FridgeModel _fridge;
        
        public GameSceneGate(EventBus eventBus, FridgeModel fridge)
        {
            _eventBus = eventBus;
            _fridge = fridge;
        }
        
        public void Initialize()
        {
            _eventBus.GameSceneEvents.StartGame.PlayerTransform = _fridge.Transform;
            _eventBus.Broadcast(_eventBus.GameSceneEvents.StartGame);
        }
    }
}
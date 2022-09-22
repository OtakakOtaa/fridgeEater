using EventsSystem.Signals;
using GameScene.Camera;
using GameScene.Fridge;
using GameScene.Shelter;
using GameScene.Systems.WallThroughSeeSystem.Head;
using UniRx;
using Zenject;

namespace GameScene.Runner
{
    public class GameSceneGate
    {
        private readonly SignalBus _signalBus;
        private readonly FridgeSpawner _fridgeSpawner;
        private readonly ShelterProvider _shelterProvider;
        private readonly VCsTargetSetter _vCsTargetSetter;
        private readonly WallThroughSeeRunner _wallThroughSeeRunner;

        private readonly FridgeModel.Factory _fridgeModelFactory;
        
        public GameSceneGate(SignalBus signalBus, FridgeSpawner fridgeSpawner,
            ShelterProvider shelterProvider, VCsTargetSetter vCsTargetSetter, 
            WallThroughSeeRunner wallThroughSeeRunner, FridgeModel.Factory fridgeModelFactory)
        {
            _signalBus = signalBus;
            _fridgeSpawner = fridgeSpawner;
            _shelterProvider = shelterProvider;
            _vCsTargetSetter = vCsTargetSetter;
            _wallThroughSeeRunner = wallThroughSeeRunner;
            _fridgeModelFactory = fridgeModelFactory;

            InitializeGame();
        }

        private void InitializeGame()
        {
            SubscribeWaiter();
            _fridgeModelFactory.Create();
        }

        private void SubscribeWaiter()
        {
            _signalBus.Subscribe<FridgeInitializeSignal>(ev =>
            {
                _fridgeSpawner.Spawn();
                _vCsTargetSetter.BindTarget(ev.Fridge);
                _wallThroughSeeRunner.BindToTarget(ev.Fridge);
            });
            _shelterProvider.SheltersGathered.Subscribe(ev => _signalBus.Fire<StartGameSceneSignal>());
        }
    }
}
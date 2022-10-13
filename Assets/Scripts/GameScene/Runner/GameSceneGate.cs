using System;
using EventsSystem.Signals;
using GameScene.Camera;
using GameScene.Fridge;
using GameScene.Fridge.Instatiate;
using GameScene.Fridge.Model;
using GameScene.Shelter_entities;
using GameScene.Shelter_entities.Services;
using GameScene.Systems;
using GameScene.Systems.WallThroughSeeSystem.Head;
using GameScene.Systems.WayPointProvider_;
using UniRx;
using Zenject;

namespace GameScene.Runner
{
    public class GameSceneGate : IDisposable, IInitializable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly FridgeSpawner _fridgeSpawner;
        [Inject] private readonly ShelterProvider _shelterProvider;
        [Inject] private readonly VCsTargetSetter _vCsTargetSetter;
        [Inject] private readonly WallThroughSeeRunner _wallThroughSeeRunner;
        [Inject] private readonly FridgeModel.Factory _fridgeModelFactory;
        [Inject] private readonly StartTracker _startTracker;
        [Inject] private readonly WayPointProvider _wayPointProvider;
        #region NonLazyInitialize
        
        [Inject] private readonly ShelterPool _shelterPool;

        #endregion

        private readonly CompositeDisposable _disposable = new();

        public void Initialize()
        {
            InitializeGame();
        }
        
        private void InitializeGame()
        {
            SubscribeWaiter();
            _fridgeModelFactory.Create();
        }

        private void SubscribeWaiter()
        {
            _signalBus.Subscribe<FridgeInitializeSignal>(BindFridgeToGame);
            _startTracker.GameStart
                .First()
                .Subscribe( _ => 
                {
                    _shelterProvider.GatherAllShelterToScene();
                    _wayPointProvider.GatherWayPoint();
                    _signalBus?.Fire<StartGameSceneSignal>(); 
                })
                .AddTo(_disposable);
        }
        
        private void BindFridgeToGame(FridgeInitializeSignal signal)
        {
            _fridgeSpawner.Spawn(signal.Fridge);
            _vCsTargetSetter.BindTarget(signal.Fridge);
            _wallThroughSeeRunner.BindToTarget(signal.Fridge);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
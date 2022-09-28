using GameScene.Fridge;
using GameScene.Fridge.Instatiate;
using GameScene.Shelter_entities;
using GameScene.Shelter_entities.Services;
using UnityEngine;
using Zenject;

namespace GameScene.Runner
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ShelterProvider _shelterProvider;
        [SerializeField] private FridgeSpawner _fridgeSpawner;

        [SerializeField] private StartTracker _startTracker;
        
        public override void InstallBindings()
        {
            BindGameGate();
            BindShelterSystems();
            BindFridge();
        }
        
        private void BindFridge()
        {
            Container.Bind<FridgeSpawner>().FromInstance(_fridgeSpawner);
            Container.BindFactory<FridgeModel, FridgeModel.Factory>().FromSubContainerResolve()
                .ByNewContextPrefabResource(ResourcePathHolder.Fridge);
        }

        private void BindGameGate()
        {
            Container.BindInterfacesAndSelfTo<GameSceneGate>().AsSingle().NonLazy();
            Container.Bind<StartTracker>().FromInstance(_startTracker);
        }
        private void BindShelterSystems()
        {
            Container.Bind<ShelterProvider>().FromInstance(_shelterProvider);
            Container.Bind<ShelterPool>().AsSingle();
        }
    }
}
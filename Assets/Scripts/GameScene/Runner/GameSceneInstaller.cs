using GameScene.Fridge;
using GameScene.Shelter;
using UnityEngine;
using Zenject;

namespace GameScene.Runner
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ShelterProvider _shelterProvider;
        [SerializeField] private FridgeSpawner _fridgeSpawner;

        public override void InstallBindings()
        {
            BindGameGate();
            BindShelterProvider();
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
        }
        private void BindShelterProvider()
        {
            Container.Bind<ShelterProvider>().FromInstance(_shelterProvider);
        }
    }
}
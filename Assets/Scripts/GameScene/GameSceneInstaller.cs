using GameScene.Fridge;
using Zenject;

namespace GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSceneGate>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
            BindFridge();
        }

        private void BindFridge()
        {
            Container.BindInterfacesAndSelfTo<FridgeModel>().FromSubContainerResolve()
                .ByNewContextPrefabResource(ResourcePathHolder.Fridge).AsSingle().NonLazy();
        }
    }
}
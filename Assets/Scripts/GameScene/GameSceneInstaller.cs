using GameScene.Fridge;
using GameScene.Systems;
using GameScene.Systems.WallThroughSeeSystem;
using UnityEngine;
using Zenject;

namespace GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GizmosRenderer _gizmosRenderer;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private WallThroughSee.Settings _wallThroughSee; 
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSceneGate>().AsSingle().NonLazy();
            Container.Bind<GizmosRenderer>().FromInstance(_gizmosRenderer);
            
            BindPlayerInput();
            BindFridge();
            BindWallThroughSee();
        }

        private void BindFridge()
        {
            Container.BindInterfacesAndSelfTo<FridgeModel>().FromSubContainerResolve()
                .ByNewContextPrefabResource(ResourcePathHolder.Fridge).AsSingle().NonLazy();
        }

        private void BindPlayerInput()
        {
            Container.Bind<FloatingJoystick>().FromInstance(_joystick);
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
        }

        private void BindWallThroughSee()
        {
            Container.Bind<WallThroughSee.Settings>().FromInstance(_wallThroughSee);
            Container.BindInterfacesAndSelfTo<WallThroughSee>().AsSingle().NonLazy();
        }
    }
}
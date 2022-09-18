using GameScene.Fridge;
using GameScene.Systems;
using UnityEngine;
using Zenject;

namespace GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GizmosRenderer _gizmosRenderer;
        [SerializeField] private FloatingJoystick _joystick;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSceneGate>().AsSingle().NonLazy();
            Container.Bind<GizmosRenderer>().FromInstance(_gizmosRenderer);
            BindPlayerInput();
            BindFridge();
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
    }
}
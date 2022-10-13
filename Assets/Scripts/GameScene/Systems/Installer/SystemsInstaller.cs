using GameScene.Systems.WayPointProvider_;
using UnityEngine;
using Zenject;

namespace GameScene.Systems
{
    public class SystemsInstaller : MonoInstaller
    {
        [SerializeField] private GizmosRenderer _gizmosRenderer;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private WayPointProvider _wayPointProvider;
        
        
        public override void InstallBindings()
        {
            BindGizmos();
            BindPlayerInput();
            BindGameTimer();
            BindWayPointProvider();
        }
        
        private void BindGizmos()
        {
            Container.Bind<GizmosRenderer>().FromInstance(_gizmosRenderer);
        }
        private void BindPlayerInput()
        {
            Container.Bind<FloatingJoystick>().FromInstance(_joystick);
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }
        private void BindGameTimer()
        {
            Container.BindInterfacesAndSelfTo<GameTimer>().AsSingle();
        }

        private void BindWayPointProvider()
        {
            Container.Bind<WayPointProvider>().FromInstance(_wayPointProvider);
            Container.Bind<WayPointPool>().AsSingle();
        }
    }
}
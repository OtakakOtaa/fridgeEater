using UnityEngine;
using Zenject;

namespace GameScene.Systems.WallThroughSeeSystem
{
    public class WallThroughSeeInstaller : MonoInstaller
    {
        [SerializeField] private Head.WallThroughSeeRunner.Settings _wallThroughSee; 

        public override void InstallBindings()
        {
            Container.Bind<Head.WallThroughSeeRunner.Settings>().FromInstance(_wallThroughSee);
            Container.BindInterfacesAndSelfTo<Head.WallThroughSeeAnimator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Head.WallThroughSeeRunner>().AsSingle().NonLazy();
        }
    }
}
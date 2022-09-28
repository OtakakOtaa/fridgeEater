using UnityEngine;
using Zenject;

namespace GameScene.Camera
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private VCsTargetSetter.VCs _vcs;

        public override void InstallBindings()
        {
            Container.Bind<VCsTargetSetter.VCs>().FromInstance(_vcs);
            Container.Bind<VCsTargetSetter>().AsSingle().NonLazy();
        }
    }
}
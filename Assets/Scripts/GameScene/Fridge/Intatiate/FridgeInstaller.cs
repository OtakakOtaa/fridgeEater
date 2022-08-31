using GameScene.Fridge.Systems;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeInstaller : MonoInstaller
    {
        [SerializeField] private FridgeModel.Settings _settings;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FridgeModel>().AsSingle();
            Container.Bind<FridgeView>().FromComponentOnRoot().AsSingle();
            Container.Bind<FridgeModel.Settings>().FromInstance(_settings);
            
            BindComponents();
            BindSystems();
        }

        private void BindComponents()
        {
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterController>().FromComponentOnRoot().AsSingle();
        }

        private void BindSystems()
        {
            Container.Bind<FridgeMovement>().AsSingle().NonLazy();
        }
    }
}
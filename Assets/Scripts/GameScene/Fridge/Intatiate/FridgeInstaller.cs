using GameScene.Fridge.Systems;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeInstaller : MonoInstaller
    {
        [SerializeField] private FridgeModel.Settings _settings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FridgeModel>().AsSingle();
            Container.Bind<FridgeView>().FromComponentOnRoot();
            Container.Bind<FridgeModel.Settings>().FromInstance(_settings);
            
            BindComponents();
            BindSystems();
        }

        private void BindComponents()
        {
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<CharacterController>().FromComponentOnRoot();
        }

        private void BindSystems()
        {
            Container.Bind<FridgeDirectionMoveCalculator>().AsSingle().NonLazy();
            Container.Bind<FridgeMovement>().AsSingle().NonLazy();
        }
    }
}
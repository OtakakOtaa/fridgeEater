using GameScene.Fridge.Model;
using GameScene.Fridge.Model.Systems.FridgeSatiety;
using GameScene.Fridge.Model.Systems.Movement;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge.Instatiate
{
    public class FridgeInstaller : MonoInstaller
    {
        [SerializeField] private FridgeModel.Settings _settings;
        
        public override void InstallBindings()
        {
            Container.Bind<FridgeRule>().AsSingle().NonLazy();
            Container.Bind<FridgeModel.Settings>().FromInstance(_settings);
            
            BindModel();
            BindView();
            BindComponents();
        }

        private void BindView()
        {
            Container.Bind<FridgeView>().FromComponentOnRoot();
        }

        private void BindModel()
        {
            Container.BindInterfacesAndSelfTo<FridgeModel>().AsSingle();
            BindModelSystems();
        }
        
        private void BindComponents()
        {
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<CharacterController>().FromComponentOnRoot();
        }

        private void BindModelSystems()
        {
            Container.Bind<FridgeDirectionMoveCalculator>().AsSingle().NonLazy();
            Container.Bind<FridgeMovement>().AsSingle().NonLazy();
            
            Container.Bind<FridgeSatiety>().AsSingle();
        }
    }
}
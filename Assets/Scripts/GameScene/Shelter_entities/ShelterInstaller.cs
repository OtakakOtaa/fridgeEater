using GameScene.Shelter_entities.Model;
using UnityEngine;
using Zenject;

namespace GameScene.Shelter_entities
{
    public class ShelterInstaller : MonoInstaller
    {
        [SerializeField] private ShelterView _shelterView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShelterModel>().AsSingle().NonLazy();
            Container.Bind<ShelterView>().FromInstance(_shelterView);
        }
    }
}
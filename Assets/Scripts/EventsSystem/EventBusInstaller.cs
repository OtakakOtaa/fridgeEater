using Zenject;

namespace EventsSystem
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSceneEvents>().AsSingle().NonLazy();
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}
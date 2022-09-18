using EventsSystem.Signals;
using Zenject;

namespace EventsSystem
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            AddSignals();
        }
        
        private void AddSignals()
        {
            Container.DeclareSignal<StartGameSceneSignal>();
        }
    }
}

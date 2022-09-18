using EventsSystem;
using EventsSystem.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameScene.Systems.WallThroughSeeSystem.Head
{
    public partial class WallThroughSeeRunner : ITickable
    {
        private readonly Settings _settings;
        private Transform _target;

        public BoolReactiveProperty Run { get; private set; }
        public ReactiveCommand TargetSelected { get; private set; }

        public WallThroughSeeRunner(SignalBus signalBus, Settings settings)
        {
            _settings = settings;
            Run = new BoolReactiveProperty(false);
            TargetSelected = new ReactiveCommand();
            
            signalBus.Subscribe<StartGameSceneSignal>( ev =>
            {
                _target = ev.Fridge;
                TargetSelected?.Execute();
            });
        }

        public void Tick()
        {
            if(_target != null) 
                TryDetectObstacle();
        }

        public Vector3 GetTargetAsViewPort()
        {
            var camera = UnityEngine.Camera.main;
            return camera!.WorldToViewportPoint(_target.position);
        }
        
        private void TryDetectObstacle()
        {
            var ray = BuildRayToTarget(out var length);
            var isHit = Physics.Raycast(ray, out var hit, length, _settings.RayObstacleMask);
            Run.Value = isHit;
        }

        private Ray BuildRayToTarget(out float length)
        {
            var camera = UnityEngine.Camera.main;
            var cameraPosition = camera!.transform.position;
            var directionToTarget =  _target.position - cameraPosition;
            length = directionToTarget.magnitude;
            return new Ray(cameraPosition, directionToTarget);
        }
    }
}

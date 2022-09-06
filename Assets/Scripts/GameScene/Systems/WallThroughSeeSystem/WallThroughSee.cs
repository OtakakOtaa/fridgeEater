using EventsSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Systems.WallThroughSeeSystem
{
    public partial class WallThroughSee : ITickable
    {
        private readonly Settings _settings;
        private Transform _target;
        
        public WallThroughSee(EventBus eventBus, Settings settings)
        {
            _settings = settings;
            eventBus.AddListener<StartGameEvent>( evt =>  _target = evt.PlayerTransform);
        }

        public void Tick()
        {
            TryDetectObstacle();
        }

        private void TryDetectObstacle()
        {
            var ray = BuildRayToTarget(out var length);
            var isHit = Physics.Raycast(ray, out var hit, length, _settings.RayObstacleMask);
            if (isHit) ThroughSee();
            else DisableThroughSee();
        }

        private Ray BuildRayToTarget(out float length)
        {
            var camera = UnityEngine.Camera.main;
            var cameraPosition = camera!.transform.position;
            var directionToTarget =  _target.position - cameraPosition;
            length = directionToTarget.magnitude;
            return new Ray(cameraPosition, directionToTarget);
        }

        private void ThroughSee()
        {
            var camera = UnityEngine.Camera.main;
            var viewPortPosition = camera!.WorldToViewportPoint(_target.position);
            
            _settings.ThroughSee.SetVector(ThroughSeeShader.PosID,viewPortPosition);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.SizeID, _settings.VisionRange);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.SmoothnessID, _settings.Smoothness);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.NoiseIntensityID, _settings.NoiseIntensity);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.OpacityID, _settings.Opacity);
        }
        
        private void DisableThroughSee()
        {
            _settings.ThroughSee.SetFloat(ThroughSeeShader.SizeID, 0f);
        }
        
    }
}

using System;
using Cinemachine;
using EventsSystem;
using UnityEngine;

namespace GameScene.Camera
{
    public class VCsTargetSetter
    {
        private readonly VCs _vcs;

        public VCsTargetSetter(VCs vcs, EventBus eventBus)
        {
            _vcs = vcs;
            eventBus.AddListener<StartGameEvent>(evt => BindTarget(evt.PlayerTransform));
        }

        private void BindTarget(Transform target)
        {
            _vcs.MainGameCamera.Follow = target;
        }
        
        [Serializable] public class VCs
        {
            [SerializeField] private CinemachineVirtualCamera _mainGameCamera;

            public CinemachineVirtualCamera MainGameCamera => _mainGameCamera;
        } 
    }
}
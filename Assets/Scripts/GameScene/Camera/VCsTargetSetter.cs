using System;
using Cinemachine;
using EventsSystem.Signals;
using UnityEngine;
using Zenject;

namespace GameScene.Camera
{
    public class VCsTargetSetter
    {
        private readonly VCs _vcs;

        public VCsTargetSetter(VCs vcs)
        {
            _vcs = vcs;
        }

        public void BindTarget(Transform target)
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
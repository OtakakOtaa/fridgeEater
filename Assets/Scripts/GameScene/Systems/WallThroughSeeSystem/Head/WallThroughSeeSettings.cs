using System;
using UnityEngine;

namespace GameScene.Systems.WallThroughSeeSystem.Head
{
    public partial class WallThroughSeeRunner
    {
        [Serializable] public class Settings
        {
            [SerializeField] private Material _throughSee;
            [SerializeField] private LayerMask _rayObstacleMask;
            
            [Space, Header("shader params")]
            [SerializeField, Range(0.7f, 3f)] private float _visionSize;
            [SerializeField, Range(0f, 1f)] private float _smoothness;
            [SerializeField, Range(0f, 1f)] private float _noiseIntensity;
            [SerializeField, Range(0f, 1f)] private float _opacity;
            
            [Space, Header("animations")] 
            [SerializeField] private float _animationTime;

            public Material ThroughSee => _throughSee;
            public LayerMask RayObstacleMask => _rayObstacleMask;
            
            public float VisionRange => _visionSize;
            public float NoiseIntensity => _noiseIntensity;
            public float Smoothness => _smoothness;
            public float Opacity => _opacity;
            public float AnimationTime => _animationTime;
        }
    }
}
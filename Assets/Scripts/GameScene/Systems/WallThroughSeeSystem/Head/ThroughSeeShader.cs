using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace GameScene.Systems.WallThroughSeeSystem.Head
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class ThroughSeeShader
    {
        public static readonly int PosID = Shader.PropertyToID("_Position");
        public static readonly int SizeID = Shader.PropertyToID("_Size");
        public static readonly int SmoothnessID = Shader.PropertyToID("_Smoothness");
        public static readonly int NoiseIntensityID = Shader.PropertyToID("_NoiseIntensity");
        public static readonly int OpacityID = Shader.PropertyToID("_Opacity");
    }
}
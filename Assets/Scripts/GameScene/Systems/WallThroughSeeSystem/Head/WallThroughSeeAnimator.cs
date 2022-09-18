using System;
using DG.Tweening;
using NSTools.Core;
using UniRx;
using UnityEngine;

namespace GameScene.Systems.WallThroughSeeSystem.Head
{
    public class WallThroughSeeAnimator : IDisposable
    {
        private readonly WallThroughSeeRunner.Settings _settings;
        private readonly WallThroughSeeRunner _wallThroughSeeRunner;
        private readonly CompositeDisposable _disposable;

        private Tween _animation;
        
        public WallThroughSeeAnimator(WallThroughSeeRunner wallThroughSeeRunner, WallThroughSeeRunner.Settings settings)
        {
            _settings = settings;
            _wallThroughSeeRunner = wallThroughSeeRunner;
            _disposable = new CompositeDisposable();
            _wallThroughSeeRunner.TargetSelected.Subscribe(_ => Subscribe()).AddTo(_disposable);
        }

        private void Subscribe()
        {
            _wallThroughSeeRunner.Run
                .Where(value => value)
                .Subscribe(_ =>
                {
                    _animation = DOTween.To(AnimateThroughSee, 0f, 1f, _settings.AnimationTime);
                })
                .AddTo(_disposable);
            
            _wallThroughSeeRunner.Run
                .Where(value => !value)
                .Subscribe(_ =>
                {
                    _animation.Kill();
                    DisableThroughSee();
                })
                .AddTo(_disposable);
        } 
        
        private void AnimateThroughSee(float pt)
        {
            var ezPt1 = EZ.BackOut(pt);
            var ezPt2 = EZ.CubicInOut(pt);

            var visionRange = Mathf.Lerp(0f, _settings.VisionRange, ezPt1);
            var smoothness = Mathf.Lerp(0f, _settings.Smoothness, ezPt2);
            var noiseIntensity = Mathf.Lerp(0f, _settings.NoiseIntensity, ezPt1);
            var opacity = Mathf.Lerp(0f, _settings.Opacity, ezPt2);
         
            BindShaderToTarget();
            SetShaderParams(visionRange, smoothness, noiseIntensity, opacity);
        }
        
        private void DisableThroughSee()
        {
            _settings.ThroughSee.SetFloat(ThroughSeeShader.SizeID, 0f);
            SetShaderParams(0f,0f,0f,0f);
        }

        private void BindShaderToTarget()
        {
            _settings.ThroughSee
                .SetVector(ThroughSeeShader.PosID, _wallThroughSeeRunner.GetTargetAsViewPort());
        }

        private void SetShaderParams(float visionRange, float smoothness, float noiseIntensity, float opacity)
        {
            _settings.ThroughSee.SetFloat(ThroughSeeShader.SizeID, visionRange);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.SmoothnessID, smoothness);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.NoiseIntensityID, noiseIntensity);
            _settings.ThroughSee.SetFloat(ThroughSeeShader.OpacityID, opacity);
        }

        public void Dispose()
        {
            DisableThroughSee();
            _disposable?.Dispose();
        }
        
    }
}
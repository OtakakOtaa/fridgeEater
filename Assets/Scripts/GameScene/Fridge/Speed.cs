using DG.Tweening;
using NSTools.Core;
using UnityEngine;

namespace GameScene.Fridge
{
    public class Speed
    {
        public float CurrentSpeed { get; private set; } = 0f;
        private float _maxSpeed;
        
        private Tween _acceleration;
        
        public Speed(float maxSpeed, float accelerationTime)
        {
            _maxSpeed = maxSpeed;
            _acceleration = DOTween.To(CalculateSpeed, 0f, maxSpeed, accelerationTime).Pause();
        }

        public void StartAcceleration() => _acceleration.Play();
        public void StopAcceleration()
        {
            _acceleration.Rewind();
            CurrentSpeed = 0f;
        }

        private void CalculateSpeed(float pt)
        {
            var ezPt = EZ.BackIn(pt);
            CurrentSpeed = Mathf.Lerp(0f, _maxSpeed, ezPt);
        }
        
    }
}
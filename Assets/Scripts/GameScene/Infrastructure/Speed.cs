using DG.Tweening;
using NSTools.Core;
using UnityEngine;

namespace GameScene.Systems
{
    public class Speed
    {
        public float CurrentSpeed { get; private set; } = 0;
        private readonly float _maxSpeed;
        private readonly float _accelerationTime;
        private Tween _acceleration;

        public Speed(float maxSpeed, float accelerationTime)
        {
            _maxSpeed = maxSpeed;
            _accelerationTime = accelerationTime;
        }

       
        private void CalculateSpeed(float pt)
        {
            var ezPt = EZ.BackIn(pt);
            CurrentSpeed = Mathf.Lerp(0f, _maxSpeed, ezPt);
            Debug.Log(nameof(CurrentSpeed) + nameof(CurrentSpeed) + " " + CurrentSpeed);
        }
        
        public void StartAcceleration()
        {
            _acceleration = DOTween.To(CalculateSpeed, 0f, 1f, _accelerationTime);
        }

        public void StopAcceleration()
        {
            _acceleration.Kill();
            CurrentSpeed = 0f;
        }
    }
}
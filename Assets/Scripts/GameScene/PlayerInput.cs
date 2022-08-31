using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace GameScene
{
    public class PlayerInput : ITickable
    {
        public bool IsTouch { get; private set; }
        public Vector2 DirectionDrag => _joystick.DirectionDrag;

        private readonly Joystick _joystick = new Joystick();
        
        public void Tick()
        {
            ObserveTouch();
        }
        
        private void ObserveTouch()
        {
            if (Input.touchCount != 0)
            {
                var touch = Input.GetTouch(0);   
                _joystick.UpdateJoystick(touch);
                IsTouch = true;
            }
            else
            {
                _joystick.TouchDragEnd();
                IsTouch = false;
            }
        }
        
        private class Joystick
        {
            private bool _isTouched;
            private Vector2 _originTouchPosition;
            private const float DeadZoneR = 30f;

            public Vector2 DirectionDrag { get; private set; }

            public void UpdateJoystick(Touch touch)
            {
                TrySetOriginTouch(touch);
                DirectionDrag = CalculateDirectionDrag(touch.position);
            }
            
            public void TouchDragEnd()
            {
                _isTouched = false;
            }

            private Vector2 CalculateDirectionDrag(Vector2 touch)
            {
                var direction = _originTouchPosition - touch; 
                
                return (direction.magnitude > DeadZoneR) ? direction.normalized : Vector2.zero;
            }
            private void TrySetOriginTouch(Touch touch)
            {
                if (!_isTouched)
                    _originTouchPosition = touch.position;
                _isTouched = true;
            }
        }
        
    }
    
}
using UnityEngine;
using Zenject;

namespace GameScene.Systems
{
    public class PlayerInput : ITickable
    {
        public bool IsTouch { get; private set; }
        
        public FloatingJoystick Joystick { get; private set; }
        
        public PlayerInput(FloatingJoystick joystick)
        {
            Joystick = joystick;
        }
        
        public void Tick()
        {
            ObserveTouch();
        }
        
        private void ObserveTouch()
        {
            IsTouch = Input.touchCount != 0;
        }
        
    }
}
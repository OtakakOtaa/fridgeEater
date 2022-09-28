using GameScene.Systems;
using UnityEngine;

namespace GameScene.Fridge.Systems.Movement
{
    public class FridgeDirectionMoveCalculator
    {
        private readonly PlayerInput _input;
        private readonly GizmosRenderer _gizmosRenderer;
        private readonly Transform _fridge;
        private readonly FridgeModel.Settings _settings;
        
        public FridgeDirectionMoveCalculator(
            PlayerInput input, 
            GizmosRenderer gizmosRenderer,
            FridgeModel.Settings settings,
            Transform fridge) 
        {
            _input = input;
            _gizmosRenderer = gizmosRenderer;
            _settings = settings;
            _fridge = fridge;
        }
        
        public Vector3 Calculate()
        {
            var inputVector = new Vector3(_input.Joystick.Horizontal, 0f, _input.Joystick.Vertical).normalized;
            var cameraYRotate = UnityEngine.Camera.main!.transform.rotation.eulerAngles.y;
            
            var moveDirection = Quaternion.Euler(0f, cameraYRotate ,0f) * inputVector;
            
            _gizmosRenderer.Draw(() =>
            {
                var color = Gizmos.color;
                Gizmos.color = Color.blue;
                var position = _fridge.position;
                Gizmos.DrawLine(position, position + moveDirection);
                Gizmos.DrawSphere(position, 0.2f);
                Gizmos.color = color;
            });

            moveDirection = Vector3.RotateTowards(
                _fridge.forward, 
                moveDirection, 
                _settings.RotateSpeed, 
                0f
            );

            return moveDirection;
        }
    }
}
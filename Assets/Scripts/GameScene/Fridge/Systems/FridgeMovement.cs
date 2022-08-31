using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;

namespace GameScene.Fridge.Systems
{
    public class FridgeMovement
    {
        private readonly CharacterController _character;
        private readonly PlayerInput _input;
        private readonly Transform _fridge;

        private readonly Speed _speed;
        private const float SpeedMultiplier = 0.5f;
        private const float AccelerationTime = 0.5f;

        private bool _isMove = false;
        
        public FridgeMovement(Transform fridge, PlayerInput input, CharacterController character, FridgeModel.Settings settings)
        {
            _fridge = fridge;
            _input = input;
            _character = character;

            _speed = new Speed(settings.Speed, AccelerationTime);
        }


        public void Update()
        {
            if (_input.IsTouch)
            {
                if (!_isMove)
                {
                    _speed.StartAcceleration();
                    _isMove = true;
                }
                Move();
            }
            if (_isMove)
            {
                _speed.StopAcceleration();
                _isMove = false;
            }
        }
        
        private void Move()
        {
            var direction = new Vector3(_input.DirectionDrag.x, 0f, _input.DirectionDrag.y);
            _character.Move(direction * _speed.CurrentSpeed * SpeedMultiplier);
            _fridge.rotation = Quaternion.LookRotation(direction);
        }
        
    }
}

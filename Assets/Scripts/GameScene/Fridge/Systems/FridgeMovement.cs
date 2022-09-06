using GameScene.Systems;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace GameScene.Fridge.Systems
{
    public class FridgeMovement
    {
        private readonly PlayerInput _input;
        private readonly CharacterController _character;

        private readonly FridgeDirectionMoveCalculator _directionMoveCalculator;
        private readonly Transform _fridge;
        private readonly Speed _speed;
        
        private const float SpeedMultiplier = 0.005f;
        private const float AccelerationTime = 0.5f;

        private bool _isMove;
        
        public FridgeMovement(PlayerInput input,
            FridgeModel.Settings settings,
            CharacterController characterController,
            FridgeDirectionMoveCalculator directionMoveCalculator,
            Transform fridge)
        {
            _input = input;
            _character = characterController;
            _directionMoveCalculator = directionMoveCalculator;
            _fridge = fridge;

            _speed = new Speed(settings.Speed, AccelerationTime);
        }

        public void Update()
        {
            if (_input.IsTouch)
            {
                TrySetStartInputState();
                Move();
            }
            else
            {
                TrySetEndInputState();
            }
        }
        
        private void Move()
        { 
            _character.Move(_directionMoveCalculator.Calculate() * _speed.CurrentSpeed * SpeedMultiplier);
            _fridge.rotation = Quaternion.LookRotation(_directionMoveCalculator.Calculate());
        }
        
        private void TrySetStartInputState()
        {
            if (_isMove) return;
            _speed.StartAcceleration();
            _isMove = true;
        }

        private void TrySetEndInputState()
        {
            if (!_isMove) return;
            _speed.StopAcceleration();
            _isMove = false;
        }
        
    }
}

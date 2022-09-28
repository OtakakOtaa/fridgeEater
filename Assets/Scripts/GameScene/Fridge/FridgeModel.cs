using System;
using GameScene.Fridge.Systems;
using GameScene.Fridge.Systems.Movement;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeModel : ITickable
    {
        private readonly FridgeView _fridgeView;
        private readonly FridgeMovement _fridgeMovement;
        
        public Transform Transform => _fridgeView.transform;
        
        public FridgeModel(FridgeMovement fridgeMovement, FridgeView fridgeView)
        {
            _fridgeMovement = fridgeMovement;
            _fridgeView = fridgeView;
        }

        public void Tick()
        {
            _fridgeMovement.Update();
        }

        [Serializable] public class Settings
        {
            [Header("Movement")]
            [SerializeField] private float _speed;
            [Range(0.5f, 2f), SerializeField] private float _accelerationTime;
            [Range(0, 1f)] [SerializeField] private float _rotateSpeed;
            
            public float Speed => _speed;
            public float RotateSpeed => _rotateSpeed;
            public float AccelerationTime => _accelerationTime;
        }

        public class Factory : PlaceholderFactory<FridgeModel>
        {
            private FridgeModel _fridgeModel;
            public override FridgeModel Create()
            {
                if (_fridgeModel == null)
                    return _fridgeModel = base.Create();
                else
                    return _fridgeModel;
            }
        }
    }
}

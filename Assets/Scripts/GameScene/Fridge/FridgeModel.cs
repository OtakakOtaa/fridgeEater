using System;
using GameScene.Fridge.Systems;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeModel : ITickable
    {
        private FridgeView _fridgeView;
        
        private FridgeMovement _fridgeMovement;

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
            [SerializeField] private float _speed;
            
            public float Speed => _speed;
        }
    }
}
using System;
using GameScene.Fridge.Model.Systems.FridgeSatiety;
using GameScene.Fridge.Model.Systems.Movement;
using GameScene.Systems;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameScene.Fridge.Model
{
    public partial class FridgeModel : ITickable
    {
        private readonly FridgeMovement _fridgeMovement;
        private readonly FridgeSatiety _fridgeSatiety;
        
        public Settings FridgeSettings { get; private set; }
        public Transform Transform { get; private set; }
        public IFridgeSatietyReadable FridgeSatiety => _fridgeSatiety;

        public FridgeModel(FridgeMovement fridgeMovement, Transform transform, FridgeSatiety fridgeSatiety, 
            Settings settings)
        {
            FridgeSettings = settings;
            
            Transform = transform;
            _fridgeMovement = fridgeMovement;
            _fridgeSatiety = fridgeSatiety;
        }

        public void TakeAwaySatiety(float damage)
        {
            _fridgeSatiety.DepriveSatiety((int)damage);
        }
        
        public void Tick()
        {
            _fridgeMovement.Update();
        }
    }
}

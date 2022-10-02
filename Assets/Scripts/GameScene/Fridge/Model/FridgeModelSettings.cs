using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScene.Fridge.Model
{
    public partial class FridgeModel
    {
        [Serializable] public class Settings
        {
            [Header("Movement")]
            [SerializeField] private float _speed;
            [Range(0.5f, 2f), SerializeField] private float _accelerationTime;
            [Range(0, 1f)] [SerializeField] private float _rotateSpeed;
            
            [Header("Satiety")]
            [SerializeField, Range(1, 10f)] private float _hungerDamage;
            [SerializeField, Range(0.5f, 5f)] private float _hungerDamageDeltaDelay;

            #region Movement

            public float Speed => _speed;
            public float RotateSpeed => _rotateSpeed;
            public float AccelerationTime => _accelerationTime;

            #endregion
         
            #region Satiety
            
            public float HungerDamage => _hungerDamage;
            public float HungerDamageDeltaDelay => _hungerDamageDeltaDelay;

            
            #endregion

        }
    }
}
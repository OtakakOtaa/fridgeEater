using System;
using UnityEngine;

namespace GameScene.Fridge.Instatiate
{
    public class FridgeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        public void Spawn(Transform fridge)
        {
            fridge.position = _spawnPoint.position;
            PutOnPlace();
        }

        private Vector3 PutOnPlace()
        {
            var isHit = Physics.Raycast(_spawnPoint.position, -_spawnPoint.up, out var plane);
            
            if (isHit) return Vector3.ProjectOnPlane(_spawnPoint.position, plane.normal);
            
            Debug.LogError(typeof(FridgeSpawner) + " incorrect spawn dot position!)");
            return Vector3.zero;
        }
    }
}
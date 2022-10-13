using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameScene.Fridge.Instatiate
{
    public class FridgeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        public void Spawn(Transform fridge)
        {
            fridge.position = _spawnPoint.position;
        }
    }
}
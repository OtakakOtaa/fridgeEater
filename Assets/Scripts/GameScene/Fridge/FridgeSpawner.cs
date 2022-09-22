using UnityEngine;
using Zenject;

namespace GameScene.Fridge
{
    public class FridgeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
            //[Inject] private FridgeModel _fridgeModel;

        public void Spawn()
        {
          //  _fridgeModel.Transform.position = _spawnPoint.position;
        }
    }
}
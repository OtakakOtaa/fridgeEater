using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Systems
{
    public class GizmosRenderer : MonoBehaviour
    {
        private readonly List<Action> _renderingPool 
            = new List<Action>();
        
        public void Draw(Action gizmo)
        {
            _renderingPool.Add(gizmo);
        }
        
        private void OnDrawGizmos() 
        {
            foreach (var item in _renderingPool)
                item?.Invoke();
            _renderingPool.Clear();
        }
    }
}
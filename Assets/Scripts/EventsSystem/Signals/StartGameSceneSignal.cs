using GameScene.Fridge;
using UnityEngine;

namespace EventsSystem.Signals
{
    public class StartGameSceneSignal
    {
        public Transform Fridge { get; private set; }

        public StartGameSceneSignal(Transform fridge)
        {
            Fridge = fridge;
        }
    }
}
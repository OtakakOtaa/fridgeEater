using GameScene.Fridge.Model.Systems.FridgeSatiety;
using UnityEngine;

namespace EventsSystem.Signals
{
    public class FridgeInitializeSignal
    {
        public Transform Fridge;
        public IFridgeSatietyReadable FridgeSatietyReadable;
    }
}
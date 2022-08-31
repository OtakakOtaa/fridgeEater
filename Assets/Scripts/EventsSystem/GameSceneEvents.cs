using UnityEngine;

namespace EventsSystem
{
    public class GameSceneEvents
    {
        public readonly StartGameEvent StartGame = new StartGameEvent();
    }

    public class StartGameEvent : GameEvent
    {
        public Transform PlayerTransform;
    } 
}
using System;
using System.Collections.Generic;
using Zenject;

namespace EventsSystem
{
    public class EventBus
    {
        private readonly Dictionary<Type, Action<GameEvent>> _events =
            new Dictionary<Type, Action<GameEvent>>();

        private readonly Dictionary<Delegate, Action<GameEvent>> _eventLookups =
            new Dictionary<Delegate, Action<GameEvent>>();

        [Inject] public GameSceneEvents GameSceneEvents { private set; get; }

        public void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            void NewAction(GameEvent e) => evt((T)e);
            _eventLookups[evt] = NewAction;

            if (_events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                _events[typeof(T)] = internalAction += NewAction;
            else
                _events[typeof(T)] = NewAction;
        }

        public void RemoveListener<T>(Action<T> evt) where T : GameEvent
        {
            if (_eventLookups.TryGetValue(evt, out var action))
            {
                if (_events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        _events.Remove(typeof(T));
                    else
                        _events[typeof(T)] = tempAction;
                }

                _eventLookups.Remove(evt);
            }
        }

        public void Broadcast(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out var action))
            {
                action?.Invoke(evt);
            }
        }
        
    };
}

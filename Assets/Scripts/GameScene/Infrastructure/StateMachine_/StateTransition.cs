using System;
using GameScene.Infrastructure.StateMachine_.States;

namespace GameScene.Infrastructure.StateMachine_
{
    public class StateTransition
    {
        public readonly State To;
        public readonly Func<bool> Condition;

        public StateTransition(State to, Func<bool> condition)
        {
            Condition = condition;
            To = to;
        }

        public static implicit operator bool(StateTransition stateTransition)
        {
            return stateTransition.Condition();
        }
    }
}
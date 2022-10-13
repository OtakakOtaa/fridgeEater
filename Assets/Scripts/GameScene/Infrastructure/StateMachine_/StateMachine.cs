using System;
using System.Collections.Generic;
using System.Linq;
using GameScene.Infrastructure.StateMachine_.States;

namespace GameScene.Infrastructure.StateMachine_
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        private readonly Dictionary<Type, List<StateTransition>> _transitions;
        private readonly List<StateTransition> _anyTransitions;

        public StateMachine(IEnumerable<IExitableState> states)
        {
            _states = states.ToDictionary( state => state.GetType());
            _transitions = new Dictionary<Type, List<StateTransition>>();
            _anyTransitions = new List<StateTransition>();
            _activeState = null;
        }
        
        public void Enter<TState>() where TState : State
        {
            var nextState = ChangeState<TState>();
            nextState.Enter();
            _activeState = nextState;
        }

        public void AddTransition(State from, State to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out var list))
            {
                _transitions[from.GetType()] = new List<StateTransition>();
            }
            _transitions.TryGetValue(from.GetType(), out var transitionsList);
            transitionsList?.Add(new StateTransition(to, condition));
        }

        public void AddAnyTransition(State to, Func<bool> condition)
        {
            _anyTransitions.Add(new StateTransition(to, condition));
        }
        
        public void UpdateActiveState()  
        {
            CheckTransitions(out var transition, _activeState.GetType());
            if (transition != null)
            {
                ChangeState<>();
            }
            if (_activeState is IStayableState stayableState)
                stayableState.Stay();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var nextState = GetState<TState>();
            return nextState;  
        }

        private TState GetState<TState>() where TState : class, IExitableState
            => _states[typeof(TState)] as TState;

        private void CheckTransitions(out StateTransition transition, Type state)
        {
            transition = null;
            if (!_transitions.TryGetValue(state, out var activeStateTransitions)) return;
            
            foreach (var item in activeStateTransitions.Where(item => item))
            {
                transition = item;
                return;
            }
            foreach (var item in _anyTransitions.Where(item => item))
            {
                transition = item;
                return;
            }
        }
    }
}
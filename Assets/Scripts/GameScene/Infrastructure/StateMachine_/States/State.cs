namespace GameScene.Infrastructure.StateMachine_.States
{
    public abstract class State : IExitableState
    {
        public abstract void Enter();

        public virtual void Exit() { }
    }
}
using GameScene.Infrastructure.StateMachine_.States;
using GameScene.Shelter_entities.Model;
using GameScene.Shelter_entities.Services;

namespace GameScene.AI.States
{
    public class ShelterSearchState : State, IStayableState
    {
        private readonly ShelterPool _shelterPool;

        public ShelterSearchState(ShelterPool shelterPool)
        {
            _shelterPool = shelterPool;
        }
        
        public override void Enter()
        {
            var trySuccessful = _shelterPool.TryTakeFreeShelter(out var shelter);
        }
        
        public void Stay()
        {
            
        }
    }
}
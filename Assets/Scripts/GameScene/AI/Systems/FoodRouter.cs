using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace GameScene.AI.Systems
{
    public class FoodRouter
    {
        private readonly NavMeshAgent _agent;
        
        public FoodRouter(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public IObservable<int> Rotate(Transform target)
        {
            return Observable.Create<int>(observer =>
            {
                while (true)
                {
                    _agent.SetDestination(target.position);
                    await UniTask.Yield();
                    if(_agent.pathStatus != NavMeshPathStatus.PathComplete) continue;
                    
                    observer.OnNext(1);
                    observer.OnCompleted();
                    
                    return Disposable.Empty;
                }
            });
        }
    }
}
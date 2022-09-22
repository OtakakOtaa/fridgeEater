using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
                RotateAsync(target).Wait();
                
                observer.OnNext(0);
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }

        private async Task RotateAsync(Transform target)
        {
            while (true)
            {
                _agent.SetDestination(target.position);
                await UniTask.Yield();
                if (_agent.pathStatus == NavMeshPathStatus.PathComplete) return;
            }
        }
    }
}
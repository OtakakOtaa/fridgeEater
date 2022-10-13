using System;
using Cysharp.Threading.Tasks;
using EventsSystem.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameScene.Systems
{
    public class GameTimer : IDisposable
    {
        private int _startingPoint;
        private bool _disposable;
        
        public ReactiveProperty<int> Time { get; private set; }

        public GameTimer(SignalBus signalBus)
        {
            Time = new ReactiveProperty<int>();
            signalBus.Subscribe<StartGameSceneSignal>(StartTimer);
        }

        private void StartTimer()
        {
            _startingPoint = (int)UnityEngine.Time.time;
            Time.Value = 0;
            CountingTime();
        }
        
        private async void CountingTime()
        {
            while (!_disposable)
            {
                await UniTask.WaitUntil(SecondPassedPredicate);
                Time.Value++;    
            }
        }

        private bool SecondPassedPredicate() => 
            UnityEngine.Time.time - (Time.Value + _startingPoint) >= 1f;

        public void Dispose()
        {
            _disposable = true;
        }
    }
}
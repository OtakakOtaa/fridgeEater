using UniRx;
using UnityEngine;

namespace GameScene.Runner
{
    public class StartTracker : MonoBehaviour
    {
        public ReactiveCommand GameStart { get; private set; } = new();

        private void Start()
        {
            GameStart?.Execute();
        }
    }
}
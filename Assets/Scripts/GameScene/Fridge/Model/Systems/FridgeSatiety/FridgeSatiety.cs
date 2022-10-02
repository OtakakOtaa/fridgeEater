using System;
using UniRx;

namespace GameScene.Fridge.Model.Systems.FridgeSatiety
{
    public class FridgeSatiety : IFridgeSatietyReadable
    {
        private const int Max = 100;
        private readonly ReactiveProperty<int> _current;
        
        public ReadOnlyReactiveProperty<int> Current { get; }
        public ReactiveCommand Die { get; }
        
        public int GetMaxValue() => Max; 

        
        public FridgeSatiety()
        {
            Die = new ReactiveCommand();
            _current = new ReactiveProperty<int>(Max);
            Current = new ReadOnlyReactiveProperty<int>(_current);
        }

        public void ReplenishSatiety(int value)
        {
            _current.Value += (int)Math.Clamp(value, 0f, Max);
        }

        public void DepriveSatiety(int value)
        {
            _current.Value -= (int)Math.Clamp(value, 0f, Max);
            if (_current.Value > 0) return;
            Die?.Execute();
            _current.Value = 0;
        }
    }
}
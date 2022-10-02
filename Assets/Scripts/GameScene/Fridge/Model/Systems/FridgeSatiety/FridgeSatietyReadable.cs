using UniRx;

namespace GameScene.Fridge.Model.Systems.FridgeSatiety
{
    public interface IFridgeSatietyReadable
    {
        int GetMaxValue();
        public ReadOnlyReactiveProperty<int> Current { get; }
    }
}
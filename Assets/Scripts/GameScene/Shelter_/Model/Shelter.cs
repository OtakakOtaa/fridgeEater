using UniRx;

namespace GameScene.Shelter
{
    public abstract class Shelter
    {
        public BoolReactiveProperty Contained { get; private set; }
        
        protected Shelter()
        {
            Contained = new BoolReactiveProperty(false);
        }

        public void SetContainedState(bool contained)
        {
            Contained.Value = contained;
        }
    }
}
using GameScene.AI.Systems;
using Zenject;

namespace GameScene.AI
{
    public class FoodModel : ITickable
    {
        private FoodRouter _foodRouter;

        public FoodModel(FoodRouter foodRouter)
        {
            _foodRouter = foodRouter;
        }

        public void Tick()
        {
        }
    }
}
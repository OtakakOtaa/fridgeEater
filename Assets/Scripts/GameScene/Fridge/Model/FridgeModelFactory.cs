using Zenject;

namespace GameScene.Fridge.Model
{
    public partial class FridgeModel
    {
        public class Factory : PlaceholderFactory<FridgeModel>
        {
            private FridgeModel _fridgeModel;
            public override FridgeModel Create()
            {
                if (_fridgeModel == null)
                    return _fridgeModel = base.Create();
                return _fridgeModel;
            }
        }
    }
}
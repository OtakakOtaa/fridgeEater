using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace GameScene.Systems.WayPointProvider_
{
    public class WayPointPool : IDisposable
    {
        private readonly CompositeDisposable _disposable;
        public WayPoint[] WayPoints { get; private set; }

        public WayPointPool(WayPointProvider wayPointProvider)
        {
            _disposable = new CompositeDisposable();
            
            wayPointProvider.WayPointGathered
                .First()
                .Subscribe( points => WayPoints = points.ToArray())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
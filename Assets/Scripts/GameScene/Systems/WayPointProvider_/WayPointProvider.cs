using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace GameScene.Systems.WayPointProvider_
{
    public class WayPointProvider : MonoBehaviour
    {
        public ReactiveCommand<IEnumerable<WayPoint>> WayPointGathered { get; private set; }

        public void GatherWayPoint()
        {
            var wayPoints = 
                from point in FindObjectsOfType<WayPoint>()
                select point.GetComponent<WayPoint>();
            WayPointGathered?.Execute(wayPoints);
        }
    }
}
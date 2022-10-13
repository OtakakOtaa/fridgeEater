using UnityEngine;

namespace GameScene.Systems.WayPointProvider_
{
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private Color _tagColor;
        [SerializeField, Range(0.1f, 4f)] private float _tagRadius;

        private void OnDrawGizmos()
        {
            var cashedColor = Gizmos.color;
            Gizmos.color = _tagColor;
            Gizmos.DrawSphere(transform.position, _tagRadius);
            Gizmos.color = cashedColor;
        }
    }
}
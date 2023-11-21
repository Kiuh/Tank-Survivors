using Enemies;
using UnityEngine;

namespace Tank
{
    public class EnemyFinder : MonoBehaviour
    {
        [SerializeField]
        private float detectionRadius = 10f;

        public Transform GetNearestTransformOrNull()
        {
            var objects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            if (objects.Length == 0)
                return null;

            Transform nearestEnemy = null;
            foreach (var obj in objects)
            {
                if (obj.gameObject.TryGetComponent<IEnemy>(out var enemy))
                {
                    if (
                        nearestEnemy == null
                        || SqrDistanceTo(obj.transform) < SqrDistanceTo(nearestEnemy)
                    )
                    {
                        nearestEnemy = obj.transform;
                    }
                }
            }

            return nearestEnemy;
        }

        private float SqrDistanceTo(Transform target)
        {
            return (transform.position - target.position).sqrMagnitude;
        }
    }
}

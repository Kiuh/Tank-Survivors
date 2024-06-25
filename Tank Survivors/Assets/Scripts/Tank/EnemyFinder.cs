using System.Collections.Generic;
using System.Linq;
using General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank
{
    public class EnemyFinder : MonoBehaviour
    {
        [SerializeField]
        private float detectionRadius = 10f;

        [Required]
        [SerializeField]
        private EnemyGenerator enemyGenerator;

        public IEnumerable<Transform> GetAllEnemies()
        {
            return enemyGenerator.Enemies;
        }

        private void OnDrawGizmos()
        {
            Color color = Gizmos.color;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = color;
        }

        public Transform GetNearestTransformOrNull()
        {
            IEnumerable<Transform> allEnemiesTransforms = GetAllEnemies();
            IOrderedEnumerable<(Transform transform, float dist)> ordered = allEnemiesTransforms
                .Select(x => (transform: x, dist: SqrDistanceTo(x)))
                .Where(x => x.dist <= detectionRadius)
                .OrderBy(x => x.dist);
            return ordered.Select(x => x.transform).FirstOrDefault();
        }

        private float SqrDistanceTo(Transform target)
        {
            return (transform.position - target.position).sqrMagnitude;
        }
    }
}

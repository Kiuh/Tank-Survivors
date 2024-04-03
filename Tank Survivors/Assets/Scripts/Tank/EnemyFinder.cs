using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace Tank
{
    public class EnemyFinder : MonoBehaviour
    {
        [SerializeField]
        private float detectionRadius = 10f;

        public IEnumerable<IEnemy> GetAllEnemies()
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            List<IEnemy> enemies = new List<IEnemy>();
            if (objects.Length == 0)
            {
                return enemies;
            }

            foreach (Collider2D obj in objects)
            {
                if (obj.gameObject.TryGetComponent(out IEnemy enemy))
                {
                    enemies.Add(enemy);
                }
            }
            return enemies;
        }

        public IEnumerable<Transform> GetAllEnemiesTransform()
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            List<Transform> enemies = new List<Transform>();
            if (objects.Length == 0)
            {
                return enemies;
            }

            foreach (Collider2D obj in objects)
            {
                if (obj.gameObject.TryGetComponent(out IEnemy enemy))
                {
                    enemies.Add(obj.transform);
                }
            }
            return enemies;
        }

        public Transform GetNearestTransformOrNull()
        {
            IEnumerable<Transform> enemies = GetAllEnemiesTransform();

            if (enemies.Count() == 0)
            {
                return null;
            }

            Transform nearestEnemy = null;
            foreach (Transform enemy in enemies)
            {
                if (nearestEnemy == null || SqrDistanceTo(enemy) < SqrDistanceTo(nearestEnemy))
                {
                    nearestEnemy = enemy;
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

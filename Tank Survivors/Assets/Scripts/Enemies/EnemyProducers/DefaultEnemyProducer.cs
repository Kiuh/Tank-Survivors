using Sirenix.OdinInspector;
using System;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    public class DefaultEnemyProducer : IEnemyProducer
    {
        [AssetsOnly]
        [SerializeField]
        [FoldoutGroup("DefaultEnemyProducer")]
        private GameObject enemyPrefab;

        [SerializeField]
        [FoldoutGroup("DefaultEnemyProducer")]
        private float spawnInterval;

        [SerializeField]
        [FoldoutGroup("DefaultEnemyProducer")]
        private float startCircleRadius;

        [SerializeField]
        [FoldoutGroup("DefaultEnemyProducer")]
        private float endCircleRadius;

        [SerializeField]
        [FoldoutGroup("DefaultEnemyProducer")]
        private float startTime;

        [SerializeField]
        [FoldoutGroup("DefaultEnemyProducer")]
        private float endTime;

        private float timer = 0;

        public float StartTime => startTime;

        public float EndTime => endTime;

        public void Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                UnityEngine.Object
                    .Instantiate(
                        enemyPrefab,
                        tank.transform.position + GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>()
                    .Initialize(tank);
                timer = spawnInterval;
            }
        }

        private Vector3 GetRandomPoint()
        {
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (endCircleRadius - startCircleRadius))
                + (point.normalized * startCircleRadius);
        }
    }
}

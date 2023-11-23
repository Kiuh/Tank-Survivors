using Sirenix.OdinInspector;
using System;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    public class ConstantEnemyProducer : IEnemyProducer
    {
        [AssetsOnly]
        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private float spawnInterval;

        [SerializeField]
        private float startCircleRadius;

        [SerializeField]
        private float endCircleRadius;

        private float timer = 0;

        public float StartTime => 0.0f;

        public float EndTime => float.PositiveInfinity;

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

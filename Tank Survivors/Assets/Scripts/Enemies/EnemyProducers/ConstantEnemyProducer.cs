using System;
using Configs;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    public class ConstantEnemyProducer : IEnemyProducer
    {
        [AssetsOnly]
        [SerializeField]
        [AssetList(CustomFilterMethod = "EnemiesFilter")]
        [FoldoutGroup("ConstantEnemyProducer")]
        private GameObject enemyPrefab;

        [SerializeField]
        private IEnemyConfig enemyConfig;

        private bool EnemiesFilter(GameObject obj)
        {
            return obj.TryGetComponent<IEnemy>(out _);
        }

        [SerializeField]
        [FoldoutGroup("ConstantEnemyProducer")]
        private float spawnInterval;

        [SerializeField]
        [FoldoutGroup("ConstantEnemyProducer")]
        private float startCircleRadius;

        [SerializeField]
        [FoldoutGroup("ConstantEnemyProducer")]
        private float endCircleRadius;

        private float timer = 0;

        public float StartTime => 0.0f;

        public float EndTime => float.PositiveInfinity;

        public void Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                UnityEngine
                    .Object.Instantiate(
                        enemyPrefab,
                        tank.transform.position + GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>()
                    .Initialize(tank, enemyConfig);
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

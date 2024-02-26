using System;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.EnemyProducers
{
    [Serializable]
    public class BossProducer
    {
        [AssetsOnly]
        [SerializeField]
        [AssetList(CustomFilterMethod = nameof(EnemiesFilter))]
        [FoldoutGroup("BossProducer")]
        private GameObject bossPrefab;

        private bool EnemiesFilter(GameObject obj)
        {
            return obj.TryGetComponent<IEnemy>(out _);
        }

        [SerializeField]
        [Unit(Units.Minute)]
        [FoldoutGroup("BossProducer")]
        private float startTime;

        [SerializeField]
        [FoldoutGroup("BossProducer")]
        private float startCircleRadius;

        [SerializeField]
        [FoldoutGroup("BossProducer")]
        private float endCircleRadius;

        public float StartTime => startTime * 60;
        private IEnemy boss;

        public void Produce(TankImpl tank, Transform enemyRoot)
        {
            boss = UnityEngine
                .Object.Instantiate(
                    bossPrefab,
                    tank.transform.position + GetRandomPoint(),
                    Quaternion.identity,
                    enemyRoot
                )
                .GetComponent<IEnemy>();
            boss.Initialize(tank);
        }

        public Vector3 GetRandomPoint()
        {
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (endCircleRadius - startCircleRadius))
                + (point.normalized * startCircleRadius);
        }

        public void OnBossDead(Action action)
        {
            boss.OnDeath += action;
        }
    }
}

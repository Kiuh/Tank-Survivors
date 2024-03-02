using System;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    [LabelText("DefaultEnemyProducer")]
    public class DefaultEnemyProducer : BaseProducer
    {
        [SerializeField]
        [Unit(Units.Second)]
        private float spawnInterval;

        [SerializeField]
        private float startCircleRadius;

        [SerializeField]
        private float endCircleRadius;

        [SerializeField]
        private float startTime;

        [SerializeField]
        private float endTime;

        private float timer = 0;

        public override float StartTime => startTime;

        public override float EndTime => endTime;

        public override void Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                var enemy = UnityEngine
                    .Object.Instantiate(
                        EnemyPrefab,
                        tank.transform.position + GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>();
                enemy.Initialize(tank);
                CloneModules(Modules, enemy.Modules);
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

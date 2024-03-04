using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    [LabelText("ConstantEnemyProducer")]
    public class ConstantEnemyProducer : BaseProducer
    {
        [OdinSerialize]
        [Unit(Units.Second)]
        private float spawnInterval;

        [OdinSerialize]
        private float startCircleRadius;

        [OdinSerialize]
        private float endCircleRadius;

        private float timer = 0;

        public override float StartTime => 0.0f;

        public override float EndTime => float.PositiveInfinity;

        public override void Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                IEnemy enemy = UnityEngine
                    .Object.Instantiate(
                        EnemyPrefab,
                        tank.transform.position + GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>();
                CloneModules(Modules, enemy.Modules);
                enemy.Initialize(tank);
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

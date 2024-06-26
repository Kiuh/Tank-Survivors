using System;
using Configs;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    [LabelText("ConstantEnemyProducer")]
    public class ConstantEnemyProducer : BaseProducer
    {
        [SerializeField]
        [Unit(Units.Second)]
        private float spawnInterval;

        [SerializeField]
        [EnumToggleButtons]
        private CircleRadius.Preset radiusPreset;

        [SerializeField]
        private CircleRadius radius;

        [ReadOnly]
        [SerializeField]
        private float timer = 0;

        public override float StartTime => 0.0f;

        public override float EndTime => float.PositiveInfinity;

        public override IEnemy Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            ProgressorTimer += Time.deltaTime;
            if (timer < 0)
            {
                UpgradeStats();
                IEnemy enemy = UnityEngine
                    .Object.Instantiate(
                        EnemyPrefab,
                        tank.transform.position
                            + radius.GetCircleZone(radiusPreset).GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>();
                CloneModules(Modules, enemy.Modules);
                enemy.Initialize(tank);
                timer = spawnInterval;
                return enemy;
            }
            return null;
        }
    }
}

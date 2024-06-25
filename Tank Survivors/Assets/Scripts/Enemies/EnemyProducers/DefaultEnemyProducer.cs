using System;
using Configs;
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
        [EnumToggleButtons]
        private CircleRadius.Preset radiusPreset;

        [SerializeField]
        private CircleRadius radius;

        [SerializeField]
        private float startTime;

        [SerializeField]
        private float endTime;

        private float timer = 0;

        public override float StartTime => startTime;

        public override float EndTime => endTime;

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

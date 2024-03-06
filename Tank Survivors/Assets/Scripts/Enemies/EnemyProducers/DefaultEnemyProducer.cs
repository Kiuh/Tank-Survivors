﻿using System;
using Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;
using static Configs.CircleRadius;

namespace Enemies.Producers
{
    [Serializable]
    [LabelText("DefaultEnemyProducer")]
    public class DefaultEnemyProducer : BaseProducer
    {
        [SerializeField]
        [Unit(Units.Second)]
        private float spawnInterval;

        [OdinSerialize]
        [EnumToggleButtons]
        private Preset radiusPreset;

        [OdinSerialize]
        private CircleRadius radius;

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
            CircleZone boundsRadius = radius.GetCircleZone(radiusPreset);
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (boundsRadius.Max - boundsRadius.Min))
                + (point.normalized * boundsRadius.Min);
        }
    }
}

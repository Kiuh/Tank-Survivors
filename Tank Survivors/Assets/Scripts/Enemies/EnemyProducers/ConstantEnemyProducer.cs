using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
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

        public IEnemy Enemy => enemyPrefab.GetComponent<IEnemy>();

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("ConstantEnemyProducer/Modules")]
        public List<IModule> Modules { get; set; } = new();

        public void Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                var enemy = UnityEngine
                    .Object.Instantiate(
                        enemyPrefab,
                        tank.transform.position + GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>();
                enemy.Initialize(tank);
                enemy.Modules = Modules;
                timer = spawnInterval;
            }
        }

        private Vector3 GetRandomPoint()
        {
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (endCircleRadius - startCircleRadius))
                + (point.normalized * startCircleRadius);
        }

        [Button("GetModules")]
        [FoldoutGroup("ConstantEnemyProducer/Modules")]
        private void GetModules()
        {
            Modules = Enemy.Modules;
        }
    }
}

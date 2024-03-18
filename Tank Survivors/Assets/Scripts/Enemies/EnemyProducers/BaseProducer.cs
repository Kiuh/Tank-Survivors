using System.Collections.Generic;
using Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    public abstract class BaseProducer : IEnemyProducer
    {
        [AssetsOnly]
        [SerializeField]
        [AssetList(CustomFilterMethod = "EnemiesFilter")]
        [FoldoutGroup("Enemy")]
        protected GameObject EnemyPrefab;

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Enemy/Enemy stats")]
        public List<IModule> Modules { get; set; } = new();

        public IEnemy Enemy => EnemyPrefab.GetComponent<IEnemy>();

        [Button("Clone stats from prefab")]
        [FoldoutGroup("Enemy/Enemy stats")]
        private void GetModules()
        {
            CloneModules(Enemy.Modules, Modules);
        }

        [OdinSerialize]
        [FoldoutGroup("Progressor")]
        public Progressor Progressor { get; set; }
        public float ProgressorTimer { get; set; } = 0.0f;

        private bool EnemiesFilter(GameObject obj)
        {
            return obj.TryGetComponent<IEnemy>(out _);
        }

        protected void CloneModules(List<IModule> source, List<IModule> target)
        {
            target.Clear();
            source.ForEach(module => target.Add(module.Clone()));
        }

        protected void UpgragdeStats()
        {
            int applyCount = (int)(ProgressorTimer / Progressor.Interval);
            foreach (IModuleUpgrade upgrade in Progressor.UpgradebleModules)
            {
                for (int i = 0; i < applyCount; i++)
                {
                    upgrade.ApplyUpgrade(this);
                }
            }
            ProgressorTimer -= applyCount * Progressor.Interval;
        }

        public abstract float StartTime { get; }
        public abstract float EndTime { get; }
        public abstract void Produce(TankImpl tank, Transform enemyRoot);
    }
}

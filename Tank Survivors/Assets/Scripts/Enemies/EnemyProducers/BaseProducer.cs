using System;
using System.Collections.Generic;
using Configs;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    public abstract class BaseProducer : IEnemyProducer
    {
        [AssetsOnly]
        [SerializeField]
        [AssetList(CustomFilterMethod = "EnemiesFilter")]
        [FoldoutGroup("Enemy")]
        protected GameObject EnemyPrefab;

        [SerializeReference]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Enemy/Enemy stats")]
        private List<IModule> baseModules = new();

        [SerializeReference]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Enemy/Enemy stats")]
        [LabelText("Modified Modules")]
        [ReadOnly]
        private List<IModule> modules = new();

        public List<IModule> Modules
        {
            get => modules;
            set => modules = value;
        }
        public IEnemy Enemy => EnemyPrefab.GetComponent<IEnemy>();

        [Button("Clone stats from prefab")]
        [FoldoutGroup("Enemy/Enemy stats")]
        private void GetModules()
        {
            CloneModules(Enemy.Modules, baseModules);
        }

        [SerializeField]
        [FoldoutGroup("Progressor")]
        private Progressor progressor;

        public Progressor Progressor
        {
            get => progressor;
            set => progressor = value;
        }

        [ReadOnly]
        [SerializeField]
        private float progressorTimer = 0.0f;
        public float ProgressorTimer
        {
            get => progressorTimer;
            set => progressorTimer = value;
        }

        public virtual void Initialize()
        {
            CloneModules(baseModules, Modules);
        }

        private bool EnemiesFilter(GameObject obj)
        {
            return obj.TryGetComponent<IEnemy>(out _);
        }

        protected void CloneModules(List<IModule> source, List<IModule> target)
        {
            target.Clear();
            source.ForEach(module => target.Add(module.Clone()));
        }

        protected void UpgradeStats()
        {
            int applyCount = (int)(ProgressorTimer / Progressor.Interval);
            foreach (IModuleUpgrade upgrade in Progressor.UpgradableModules)
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
        public abstract IEnemy Produce(TankImpl tank, Transform enemyRoot);
    }
}

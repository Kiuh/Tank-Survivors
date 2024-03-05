using System.Collections.Generic;
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

        private bool EnemiesFilter(GameObject obj)
        {
            return obj.TryGetComponent<IEnemy>(out _);
        }

        protected void CloneModules(List<IModule> source, List<IModule> target)
        {
            target.Clear();
            source.ForEach(module => target.Add(module.Clone()));
        }

        public abstract float StartTime { get; }
        public abstract float EndTime { get; }
        public abstract void Produce(TankImpl tank, Transform enemyRoot);
    }
}

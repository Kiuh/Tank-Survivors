using System;
using System.Collections.Generic;
using Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;
using static Configs.CircleRadius;

namespace Enemies.EnemyProducers
{
    [Serializable]
    [HideReferenceObjectPicker]
    [LabelText("Boss Producer")]
    public class BossProducer
    {
        [AssetsOnly]
        [SerializeField]
        [AssetList(CustomFilterMethod = nameof(EnemiesFilter))]
        [FoldoutGroup("Boss")]
        private GameObject bossPrefab;

        private bool EnemiesFilter(GameObject obj)
        {
            return obj.TryGetComponent<IEnemy>(out _);
        }

        [OdinSerialize]
        [FoldoutGroup("Boss/Boss stats")]
        private List<IModule> baseModules = new();

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Boss/Boss stats")]
        [LabelText("Modules")]
        [ReadOnly]
        public List<IModule> Modules { get; set; } = new();

        [SerializeField]
        [Unit(Units.Minute)]
        private float startTime;

        [OdinSerialize]
        [EnumToggleButtons]
        private Preset preset;

        [OdinSerialize]
        private CircleRadius radius;

        public float StartTime => startTime * 60;
        private IEnemy boss;

        public void Initialize()
        {
            CloneModules(baseModules, Modules);
        }

        public IEnemy Produce(TankImpl tank, Transform enemyRoot)
        {
            boss = UnityEngine
                .Object.Instantiate(
                    bossPrefab,
                    tank.transform.position + radius.GetCircleZone(preset).GetRandomPoint(),
                    Quaternion.identity,
                    enemyRoot
                )
                .GetComponent<IEnemy>();
            CloneModules(Modules, boss.Modules);
            boss.Initialize(tank);
            return boss;
        }

        public void OnBossDead(Action action)
        {
            boss.OnDeath += action;
        }

        [Button("Clone stats from prefab")]
        [FoldoutGroup("Boss/Boss stats")]
        private void GetModules()
        {
            IEnemy enemy = bossPrefab.GetComponent<IEnemy>();
            CloneModules(enemy.Modules, baseModules);
        }

        protected void CloneModules(List<IModule> source, List<IModule> target)
        {
            target.Clear();
            source.ForEach(module => target.Add(module.Clone()));
        }
    }
}

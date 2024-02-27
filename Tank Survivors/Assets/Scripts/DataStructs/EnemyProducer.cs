using System;
using Assets.Scripts.Configs.Enemies;
using Enemies.Producers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace DataStructs
{
    [Serializable]
    public struct EnemyProducer
    {
        [OdinSerialize]
        [FoldoutGroup("Producer")]
        public IEnemyProducer Producer { get; private set; }

        [OdinSerialize]
        [FoldoutGroup("Producer")]
        public ProgressorProperties ProgressorProperties { get; private set; }
    }
}

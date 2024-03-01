using System;
using Configs;
using Enemies.Producers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace DataStructs
{
    [Serializable]
    public struct EnemyProducer
    {
        [OdinSerialize]
        [FoldoutGroup("$producerName")]
        [HideLabel]
        public IEnemyProducer Producer { get; private set; }

        [OdinSerialize]
        [FoldoutGroup("$producerName")]
        public ProgressorProperties ProgressorProperties { get; private set; }

        [OdinSerialize]
        private string producerName;
    }
}

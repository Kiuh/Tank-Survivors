using System;
using Configs;
using Enemies.Producers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace DataStructs
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class EnemyProducer
    {
        [OdinSerialize]
        [FoldoutGroup("$" + nameof(producerName))]
        [HideLabel]
        public IEnemyProducer Producer { get; private set; }

        [OdinSerialize]
        [FoldoutGroup("Progressor")]
        public Properties ProgressorProperties { get; private set; }

        [OdinSerialize]
        [FoldoutGroup("$" + nameof(producerName))]
        private string producerName = "Producer";
    }
}

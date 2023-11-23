using Enemies.Producers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig", order = 1)]
    public class Enemies : SerializedScriptableObject
    {
        [OdinSerialize]
        public List<IEnemyProducer> EnemyProducers { get; private set; }
    }
}

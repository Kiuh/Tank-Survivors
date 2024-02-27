using System.Collections.Generic;
using DataStructs;
using Enemies.EnemyProducers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig", order = 1)]
    public class Enemies : SerializedScriptableObject
    {
        [OdinSerialize]
        public List<EnemyProducer> EnemyProducers { get; private set; }

        [OdinSerialize]
        public List<BossProducer> BossProducers { get; private set; }
    }
}

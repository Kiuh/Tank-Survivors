using Enemies.Producers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig", order = 1)]
    public class Enemies : ScriptableObject
    {
        [SerializeField]
        private List<SerializedEnemyProducer> enemyProducers;
        public IEnumerable<IEnemyProducer> EnemyProducers =>
            enemyProducers.Select(x => x.ToEnemyProducer());
    }
}

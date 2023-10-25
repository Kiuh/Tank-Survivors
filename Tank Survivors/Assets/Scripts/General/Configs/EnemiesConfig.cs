using General.EnemyProducers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig", order = 1)]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField]
        private List<SerializedEnemyProducer> enemyProducers;
        public IEnumerable<IEnemyProducer> EnemyProducers =>
            enemyProducers.Select(x => x.ToEnemyProducer());
    }
}

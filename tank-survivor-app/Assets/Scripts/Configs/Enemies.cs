using System.Collections.Generic;
using Enemies.EnemyProducers;
using Enemies.Producers;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig", order = 1)]
    public class Enemies : ScriptableObject
    {
        [SerializeReference]
        private List<IEnemyProducer> enemyProducers;

        public List<IEnemyProducer> EnemyProducers
        {
            get => enemyProducers;
            private set => enemyProducers = value;
        }

        [SerializeReference]
        private List<BossProducer> bossProducers;
        public List<BossProducer> BossProducers
        {
            get => bossProducers;
            private set => bossProducers = value;
        }
    }
}

using Enemies.Producers;
using System.Collections.Generic;
using System.Linq;
using Tank;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.EnemyGenerator")]
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField]
        private Transform enemyRoot;

        [SerializeField]
        private GameContext gameContext;

        [SerializeField]
        private Timer timer;

        [SerializeField]
        private TankImpl tank;

        private List<IEnemyProducer> enemyProducers;
        private List<IEnemyProducer> toRemove = new();

        private void Awake()
        {
            enemyProducers = gameContext.GameConfig.EnemiesConfig.EnemyProducers.ToList();
        }

        private void Update()
        {
            foreach (IEnemyProducer producer in enemyProducers)
            {
                if (
                    timer.CurrentTime >= producer.StartTime && timer.CurrentTime <= producer.EndTime
                )
                {
                    producer.Produce(tank, enemyRoot);
                }
                if (timer.CurrentTime >= producer.EndTime)
                {
                    toRemove.Add(producer);
                }
            }
            if (toRemove.Count > 0)
            {
                foreach (IEnemyProducer item in toRemove)
                {
                    _ = enemyProducers.Remove(item);
                }
                toRemove.Clear();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using DataStructs;
using Enemies.EnemyProducers;
using Enemies.Producers;
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

        private List<IEnemyProducer> enemyProducers = new();
        private List<IEnemyProducer> toRemove = new();
        private List<BossProducer> bossProducers;

        private void Awake()
        {
            enemyProducers = gameContext.GameConfig.EnemiesConfig.EnemyProducers.ToList();
            bossProducers = gameContext.GameConfig.EnemiesConfig.BossProducers.ToList();
        }

        private void Update()
        {
            if (!timer.IsPaused)
            {
                GenerateEnemies();
                GenerateBoss();
            }
        }

        private void GenerateEnemies()
        {
            foreach (IEnemyProducer producer in enemyProducers)
            {
                if (
                    timer.CurrentTime >= producer.StartTime
                    && timer.CurrentTime <= producer.EndTime
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

        private void GenerateBoss()
        {
            BossProducer producerToRemove = null;
            foreach (var producer in bossProducers)
            {
                if (timer.CurrentTime >= producer.StartTime)
                {
                    producer.Produce(tank, enemyRoot);
                    timer.StopTimer();
                    producer.OnBossDead(timer.StartTimer);
                    producerToRemove = producer;
                    break;
                }
            }
            if (producerToRemove != null)
            {
                _ = bossProducers.Remove(producerToRemove);
            }
        }
    }
}

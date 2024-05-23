using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Enemies.EnemyProducers;
using Enemies.Producers;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.EnemyGenerator")]
    public class EnemyGenerator : SerializedMonoBehaviour
    {
        [Required]
        [SerializeField]
        private Transform enemyRoot;

        [Required]
        [SerializeField]
        private GameContext gameContext;

        [Required]
        [SerializeField]
        private Timer timer;

        [Required]
        [SerializeField]
        private TankImpl tank;

        [ReadOnly]
        [SerializeField]
        private List<Transform> enemies = new();
        public IEnumerable<Transform> Enemies => enemies;

        [ReadOnly]
        [SerializeField]
        private List<Transform> bosses = new();
        public IEnumerable<Transform> Bosses => bosses;

        private List<IEnemyProducer> enemyProducers = new();
        private List<IEnemyProducer> toRemove = new();
        private List<BossProducer> bossProducers = new();

        public event Action OnBossDead;

        private void Awake()
        {
            enemyProducers = gameContext.GameConfig.EnemiesConfig.EnemyProducers.ToList();
            bossProducers = gameContext.GameConfig.EnemiesConfig.BossProducers.ToList();
            enemyProducers.ForEach(producer => producer.Initialize());
            bossProducers.ForEach(producer => producer.Initialize());
        }

        private void Update()
        {
            enemies = enemies.Where(x => x != null).ToList();
            bosses = bosses.Where(x => x != null).ToList();
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
                    IEnemy newEnemy = producer.Produce(tank, enemyRoot);
                    if (newEnemy != null)
                    {
                        enemies.Add(newEnemy.Transform);
                    }
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
            foreach (BossProducer producer in bossProducers)
            {
                if (timer.CurrentTime >= producer.StartTime)
                {
                    IEnemy boss = producer.Produce(tank, enemyRoot);
                    if (boss != null)
                    {
                        bosses.Add(boss.Transform);
                    }
                    timer.StopTimer();
                    producer.OnBossDead(() =>
                    {
                        timer.StartTimer();
                        OnBossDead?.Invoke();
                    });
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

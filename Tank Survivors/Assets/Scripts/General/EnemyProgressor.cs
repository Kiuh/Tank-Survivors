using System.Collections;
using System.Collections.Generic;
using DataStructs;
using Enemies;
using UnityEngine;

namespace General
{
    public class EnemyProgressor : MonoBehaviour
    {
        [SerializeField]
        private Configs.Enemies enemies;

        [SerializeField]
        private Timer timer;

        private List<EnemyProducer> producers = new();

        public void Awake()
        {
            producers = enemies.EnemyProducers;
            foreach (EnemyProducer producer in producers)
            {
                producer.Progressor.LastUpdateTime = 0.0f;
            }
        }

        public void Start()
        {
            _ = StartCoroutine(UpgradeEnemy());
        }

        private IEnumerator UpgradeEnemy()
        {
            while (true)
            {
                if (!timer.IsPaused)
                {
                    ImproveEnemies();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        public void ImproveEnemies()
        {
            foreach (EnemyProducer producer in producers)
            {
                if (
                    timer.CurrentTime - producer.Progressor.LastUpdateTime
                    >= producer.Progressor.Interval
                )
                {
                    foreach (IModuleUpgrade upgrade in producer.Progressor.UpgradebleModules)
                    {
                        upgrade.ApplyUpgrade(producer);
                    }
                    producer.Progressor.LastUpdateTime = timer.CurrentTime;
                }
            }
        }
    }
}

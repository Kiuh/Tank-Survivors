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
        }

        public void Update()
        {
            if (!timer.IsPaused)
            {
                ImproveEnemies();
            }
        }

        public void ImproveEnemies()
        {
            foreach (EnemyProducer producer in producers)
            {
                if (
                    timer.CurrentTime - producer.ProgressorProperties.LastUpdateTime
                    >= producer.ProgressorProperties.Interval
                )
                {
                    foreach (
                        IModuleUpgrade upgrade in producer.ProgressorProperties.UpgradebleModules
                    )
                    {
                        upgrade.ApplyUpgrade(producer);
                    }
                    producer.ProgressorProperties.LastUpdateTime = timer.CurrentTime;
                }
            }
        }
    }
}

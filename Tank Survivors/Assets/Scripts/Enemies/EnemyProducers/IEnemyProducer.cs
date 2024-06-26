using System.Collections.Generic;
using Configs;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    public interface IEnemyProducer
    {
        public void Initialize();
        public float StartTime { get; }
        public float EndTime { get; }
        public IEnemy Enemy { get; }
        public List<IModule> Modules { get; set; }
        public Progressor Progressor { get; set; }
        public IEnemy Produce(TankImpl tank, Transform enemyRoot);
    }
}

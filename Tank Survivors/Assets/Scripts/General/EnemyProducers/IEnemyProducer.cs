using Common;
using Tank;
using UnityEngine;

namespace General.EnemyProducers
{
    [InterfaceEditor]
    public interface IEnemyProducer
    {
        public float StartTime { get; }
        public float EndTime { get; }
        public void Produce(TankImpl tank, Transform enemyRoot);
    }
}

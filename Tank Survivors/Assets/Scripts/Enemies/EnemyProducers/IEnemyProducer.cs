using Common;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [InterfaceEditor]
    public interface IEnemyProducer
    {
        public float StartTime { get; }
        public float EndTime { get; }
        public void Produce(TankImpl tank, Transform enemyRoot);
    }
}

using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [HideReferenceObjectPicker]
    public interface IEnemyProducer
    {
        public float StartTime { get; }
        public float EndTime { get; }
        public void Produce(TankImpl tank, Transform enemyRoot);
    }
}

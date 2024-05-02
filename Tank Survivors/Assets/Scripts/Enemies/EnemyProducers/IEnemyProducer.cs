using System.Collections.Generic;
using Configs;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [HideReferenceObjectPicker]
    public interface IEnemyProducer
    {
        public void Initialize();
        public float StartTime { get; }
        public float EndTime { get; }
        public IEnemy Enemy { get; }
        public List<IModule> Modules { get; set; }
        public Progressor Progressor { get; set; }
        public void Produce(TankImpl tank, Transform enemyRoot);
    }
}

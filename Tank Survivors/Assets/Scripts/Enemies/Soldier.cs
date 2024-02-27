using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Soldier")]
    public class Soldier : SerializedMonoBehaviour, IEnemy
    {
        public string EnemyName { get; private set; }

        [OdinSerialize]
        public List<IModule> Modules { get; private set; }
        public event Action OnDeath;
        private TankImpl tank;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            Modules = new() { new MovementModule() };
        }

        public void TakeDamage(float damageAmount) { }
    }
}

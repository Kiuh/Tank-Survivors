using System;
using System.Collections.Generic;
using Tank;
using Tank.Weapons;
using UnityEngine;

namespace Enemies
{
    public interface IEnemy
    {
        public Transform Transform { get; }
        public float Health { get; }
        public string EnemyName { get; }
        public string ViewEnemyName { get; }
        public void Initialize(TankImpl tank);
        public void TakeDamage(Damage damage);
        public event Action OnDeath;
        public List<IModule> Modules { get; set; }
    }
}

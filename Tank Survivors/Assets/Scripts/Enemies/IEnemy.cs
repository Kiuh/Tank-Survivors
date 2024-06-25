using System;
using System.Collections.Generic;
using Tank;
using UnityEngine;

namespace Enemies
{
    public interface IEnemy
    {
        public Transform Transform { get; }
        public string EnemyName { get; }
        public void Initialize(TankImpl tank);
        public void TakeDamage(float damageAmount);
        public event Action OnDeath;
        public List<IModule> Modules { get; set; }
    }
}

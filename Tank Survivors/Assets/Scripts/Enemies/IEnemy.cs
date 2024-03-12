using System;
using System.Collections.Generic;
using Tank;

namespace Enemies
{
    public interface IEnemy
    {
        public string EnemyName { get; }
        public void Initialize(TankImpl tank);
        public void TakeDamage(float damageAmount);
        public event Action OnDeath;
        public List<IModule> Modules { get; set; }
    }
}

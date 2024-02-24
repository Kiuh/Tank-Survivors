using System;
using System.Collections.Generic;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;

namespace Enemies.Bosses
{
    public class Boss : SerializedMonoBehaviour, IEnemy
    {
        [OdinSerialize]
        private BossStats stats;

        [ReadOnly]
        [OdinSerialize]
        public float Health { get; private set; }

        [OdinSerialize]
        [LabelText("Abilities")]
        private List<IAbility> abilities = new();

        private TankImpl tank;
        public event Action OnDeath;

        public string EnemyName { get; private set; }

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            Health = stats.Health;

            foreach (IAbility ability in abilities)
            {
                ability.Initialize(this, tank);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            if (Health <= 0.0f)
            {
                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}

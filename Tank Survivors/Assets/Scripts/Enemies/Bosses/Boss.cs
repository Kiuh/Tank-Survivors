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

        [OdinSerialize]
        [LabelText("Abilities")]
        private List<IAbility> abilities = new();

        [OdinSerialize]
        private TankImpl tank;

        public event Action OnDeath;

        public string EnemyName => throw new System.NotImplementedException();

        public void Awake()
        {
            foreach (var ability in abilities)
            {
                ability.Initialize(this, tank);
            }
        }

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
        }

        public void TakeDamage(float damageAmount)
        {
            OnDeath?.Invoke();
        }
    }
}

using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies.Bosses
{
    public class Enemy : SerializedMonoBehaviour, IEnemy
    {
        [OdinSerialize]
        public string EnemyName { get; private set; }

        [OdinSerialize]
        public Rigidbody2D Rigidbody { get; private set; }

        [OdinSerialize]
        public Collider2D Collider { get; private set; }
        public float Health { get; private set; }

        [OdinSerialize]
        [LabelText("Abilities")]
        private List<IAbility> abilities = new();

        [OdinSerialize]
        [ListDrawerSettings(DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
        [FoldoutGroup("Modules")]
        public List<IModule> Modules { get; set; } = new();

        private TankImpl tank;
        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            Health = Modules.GetConcrete<HealthModule, IModule>().Health.GetModifiedValue();
            abilities.ForEach(ability => ability.Initialize(this, this.tank, Modules));
            OnDeath += () => tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            OnDeath += () => Destroy(gameObject);
        }

        public void Update()
        {
            abilities.ForEach(
                (ability) =>
                {
                    if (ability.IsActive)
                    {
                        ability.Use();
                    }
                }
            );
        }

        public void TakeDamage(float damageAmount)
        {
            if (Health < 0)
            {
                return;
            }
            Health -= damageAmount;
            if (Health <= 0.0f)
            {
                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }

        public T GetAbility<T>()
            where T : class, IAbility
        {
            return abilities.GetConcrete<T, IAbility>();
        }

        [Button("GetModules")]
        private void SetModules()
        {
            Modules = new();
            abilities.ForEach((ability) => Modules.AddRange(ability.GetModules()));
            Modules.Add(new HealthModule());
            Modules.Add(new DamageModule());
        }
    }
}

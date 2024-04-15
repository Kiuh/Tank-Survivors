using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("CloseRangeAttack")]
    public class CloseRangeAttack : IAbility
    {
        private float damage;
        private float attackCooldown;
        private Collider2D collider;
        private TankImpl tank;
        private Collider2D tankCollider;
        public bool IsActive { get; set; }
        private float timer = 0f;

        public List<IModule> GetModules()
        {
            return new() { new AttackCooldownModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.tank = tank;
            collider = enemy.Collider;
            tankCollider = tank.GetComponent<BoxCollider2D>();
            damage = enemy.Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue();
            attackCooldown = enemy
                .Modules.GetConcrete<AttackCooldownModule, IModule>()
                .Cooldown.GetModifiedValue();
            enemy.FixedUpdatableAbilities.Add(this);
            IsActive = true;
        }

        public void Use()
        {
            timer -= Time.deltaTime;
            if (collider.IsTouching(tankCollider))
            {
                if (timer <= 0)
                {
                    tank.TakeDamage(damage);
                    timer = attackCooldown;
                    return;
                }
            }
        }
    }
}

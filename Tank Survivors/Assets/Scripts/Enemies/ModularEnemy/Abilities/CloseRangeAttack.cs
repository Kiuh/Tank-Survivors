using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Tank;
using Unity.Loading;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("CloseRangeAttack")]
    public class CloseRangeAttack : IAbility
    {
        private DamageModule damage;
        private AttackCooldownModule attackCooldown;
        private Collider2D collider;
        private TankImpl tank;
        private Collider2D tankCollider;
        public bool IsActive { get; set; }
        private float timer = 0f;

        public List<IModule> GetModules()
        {
            return new() { new AttackCooldownModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank, List<IModule> modules)
        {
            this.tank = tank;
            collider = enemy.Collider;
            tankCollider = tank.GetComponent<BoxCollider2D>();
            damage = modules.GetConcrete<DamageModule, IModule>();
            attackCooldown = modules.GetConcrete<AttackCooldownModule, IModule>();
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
                    tank.TakeDamage(damage.Damage.GetModifiedValue());
                    timer = attackCooldown.Cooldown.GetModifiedValue();
                    return;
                }
            }
        }
    }
}

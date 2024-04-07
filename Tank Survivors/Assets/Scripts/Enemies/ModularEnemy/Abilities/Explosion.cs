using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Explosion")]
    public class Explosion : IAbility
    {
        [OdinSerialize]
        private ParticleSystem particle;

        [OdinSerialize]
        private SpriteRenderer dangerZone;

        private DamageModule damage;
        private ExplosionModule explosion;
        private TankImpl tank;
        private Enemy enemy;
        private bool isExploded = false;
        public bool IsActive { get; set; }

        public List<IModule> GetModules()
        {
            return new() { new ExplosionModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank, List<IModule> modules)
        {
            this.tank = tank;
            this.enemy = enemy;
            damage = modules.GetConcrete<DamageModule, IModule>();
            explosion = modules.GetConcrete<ExplosionModule, IModule>();
            enemy.FixedUpdatableAbilities.Add(this);
            dangerZone.transform.localScale *= explosion.Radius.GetModifiedValue();
            particle.transform.localScale *= explosion.Radius.GetModifiedValue();
            IsActive = true;
        }

        public void Use()
        {
            if (isExploded)
            {
                return;
            }
            if (
                (enemy.transform.position - tank.transform.position).magnitude
                < explosion.Radius.GetModifiedValue()
            )
            {
                Explode();
            }
        }

        private void Explode()
        {
            isExploded = true;
            Movement movement = enemy.GetConcreteAbility<Movement>();
            if (movement != null)
            {
                movement.IsActive = false;
            }
            enemy.Collider.enabled = false;
            tank.TakeDamage(damage.Damage.GetModifiedValue());
            particle.Play();
            GameObject.Destroy(
                enemy.gameObject,
                particle.main.duration * particle.main.startLifetimeMultiplier
            );
        }
    }
}

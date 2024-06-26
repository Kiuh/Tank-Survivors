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
    [LabelText("Explosion")]
    public class Explosion : IAbility
    {
        [SerializeField]
        private ParticleSystem particle;

        [SerializeField]
        private SpriteRenderer dangerZone;

        private float damage;
        private float radius;
        private TankImpl tank;
        private Enemy enemy;
        private bool isExploded = false;
        public bool IsActive { get; set; }

        public List<IModule> GetModules()
        {
            return new() { new ExplosionModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.tank = tank;
            this.enemy = enemy;
            damage = enemy.Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue();
            radius = enemy
                .Modules.GetConcrete<ExplosionModule, IModule>()
                .Radius.GetModifiedValue();
            enemy.FixedUpdatableAbilities.Add(this);
            dangerZone.transform.localScale *= radius;
            particle.transform.localScale *= radius;
            IsActive = true;
        }

        public void Use()
        {
            if (isExploded)
            {
                return;
            }
            if ((enemy.transform.position - tank.transform.position).magnitude < radius)
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
            enemy.OwnCollider.enabled = false;
            tank.TakeDamage(damage);
            particle.Play();
            GameObject.Destroy(
                enemy.gameObject,
                particle.main.duration * particle.main.startLifetimeMultiplier
            );
        }
    }
}

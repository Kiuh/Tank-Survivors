using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Enemies.Projectiles;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Shooting")]
    public class Shooting : IAbility
    {
        [OdinSerialize]
        private Transform shootingPoint;

        [OdinSerialize]
        private Projectile projectile;

        private DamageModule damage;
        private ShootingRangeModule range;
        private ShootingRateModule rate;
        private ProjectileSpeedModule speed;
        private TankImpl tank;
        private Enemy enemy;
        private float timer;
        public bool IsActive { get; set; }

        public List<IModule> GetModules()
        {
            return new()
            {
                new ShootingRangeModule(),
                new ShootingRateModule(),
                new ProjectileSpeedModule()
            };
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.tank = tank;
            this.enemy = enemy;
            damage = enemy.Modules.GetConcrete<DamageModule, IModule>();
            range = enemy.Modules.GetConcrete<ShootingRangeModule, IModule>();
            rate = enemy.Modules.GetConcrete<ShootingRateModule, IModule>();
            speed = enemy.Modules.GetConcrete<ProjectileSpeedModule, IModule>();
            enemy.FixedUpdatableAbilities.Add(this);
            IsActive = true;
        }

        public void Use()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Shoot();
                timer = rate.ShootCooldown.GetModifiedValue();
            }
        }

        private void Shoot()
        {
            Vector3 direction = (tank.transform.position - enemy.transform.position).normalized;
            GameObject
                .Instantiate(projectile, shootingPoint.position, Quaternion.identity)
                .GetComponent<Projectile>()
                .Initialize(
                    damage.Damage.GetModifiedValue(),
                    range.ShootingRange.GetModifiedValue(),
                    speed.ProjectileSpeed.GetModifiedValue(),
                    direction
                );
            ;
        }
    }
}

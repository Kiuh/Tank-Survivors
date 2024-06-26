using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Enemies.Projectiles;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Shooting")]
    public class Shooting : IAbility
    {
        [Required]
        [SerializeField]
        private Transform shootingPoint;

        [Required]
        [SerializeField]
        private Projectile projectile;

        public float Damage { set; get; }
        public float Range { set; get; }
        public float ShootingCooldown { set; get; }
        public float ProjectileSpeed { set; get; }
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
            Damage = enemy.Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue();
            Range = enemy
                .Modules.GetConcrete<ShootingRangeModule, IModule>()
                .ShootingRange.GetModifiedValue();
            ShootingCooldown = enemy
                .Modules.GetConcrete<ShootingRateModule, IModule>()
                .ShootCooldown.GetModifiedValue();
            ProjectileSpeed = enemy
                .Modules.GetConcrete<ProjectileSpeedModule, IModule>()
                .ProjectileSpeed.GetModifiedValue();
            enemy.UpdatableAbilities.Add(this);
            IsActive = true;
        }

        public void Use()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Shoot();
                timer = ShootingCooldown;
            }
        }

        private void Shoot()
        {
            Vector3 direction = (tank.transform.position - enemy.transform.position).normalized;
            GameObject
                .Instantiate(projectile, shootingPoint.position, Quaternion.identity)
                .GetComponent<Projectile>()
                .Initialize(Damage, Range, ProjectileSpeed, direction);
        }
    }
}

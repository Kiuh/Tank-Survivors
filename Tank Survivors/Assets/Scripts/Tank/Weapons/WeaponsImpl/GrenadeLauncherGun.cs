using System;
using System.Collections.Generic;
using Tank.Towers;
using Tank.Weapons.Modules;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class GrenadeLauncherGun : GunBase
    {
        private SingleShotTower tower;

        private float selfExplosionRemainingTime = 0f;

        public override void ProceedAttack()
        {
            tower.ProceedAttack();

            selfExplosionRemainingTime -= Time.deltaTime;
            if (selfExplosionRemainingTime < 0f)
            {
                selfExplosionRemainingTime += GetModule<Modules.SelfExplosion.FireRateModule>()
                    .FireRate.GetModifiedValue();

                SelfExplosion();
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            Tank = tank;
            EnemyFinder = enemyFinder;
        }

        public override void CreateGun()
        {
            tower = CreateTower<SingleShotTower>(Tank.transform, SpawnVariation.Disconnected);
        }

        public override void DestroyGun()
        {
            DestroyTower(tower);
        }

        public override void SwapWeapon(IWeapon newWeapon)
        {
            DestroyGun();
            Tank.SwapWeapon(newWeapon);
            newWeapon.CreateGun();
        }

        protected override List<IWeaponModule> GetBaseModules()
        {
            return new()
            {
                new DamageModule(),
                new FireRateModule(),
                new CriticalChanceModule(),
                new CriticalMultiplierModule(),
                new ProjectileModule(),
                new FireRangeModule(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<SingleShotTower>(),
                new ProjectileDamageRadiusModule(),
                new ProjectileSpreadAngleModule(),
                new TowerRotationModule(),
                new Modules.SelfExplosion.ProjectileModule(),
                new Modules.SelfExplosion.DamageModule(),
                new Modules.SelfExplosion.FireRateModule(),
                new Modules.SelfExplosion.SelfExplosionCountModule(),
                new Modules.SelfExplosion.RadiusModule(),
                new Modules.SelfExplosion.HitMarkTimerModule(),
                new Modules.SelfExplosion.FireTimerModule(),
                new FireDamageModule(),
                new FireFireRateModule(),
                new ProjectileFireTimerModule(),
            };
        }

        private void SelfExplosion()
        {
            int explosionCount = GetModule<Modules.SelfExplosion.SelfExplosionCountModule>()
                .Count.GetModifiedValue();

            for (int i = 0; i < explosionCount; i++)
            {
                IProjectile projectile = GetModule<Modules.SelfExplosion.ProjectileModule>()
                    .ProjectilePrefab.SpawnConnected(Tank.transform);

                projectile.Initialize(this, Tank, tower);
                projectile.Shoot();
            }
        }
    }
}

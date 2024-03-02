using System;
using System.Collections.Generic;
using Common;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class GrenadeLauncherGun : GunBase
    {
        private SingleShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private AimController aimController;
        private ProjectileSpawner projectileSpawner;

        private float projectileShotRemainingTime = 0f;
        private float selfExplosionRemainingTime = 0f;

        public override void ProceedAttack()
        {
            selfExplosionRemainingTime -= Time.deltaTime;
            if (selfExplosionRemainingTime < 0f)
            {
                selfExplosionRemainingTime += GetModule<SelfExplosionFireRate>()
                    .FireRate.GetModifiedValue();

                SelfExplosion();
            }

            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (tower == null || nearestEnemy == null)
            {
                return;
            }

            aimController.Aim(nearestEnemy);

            projectileShotRemainingTime -= Time.deltaTime;
            if (projectileShotRemainingTime < 0f)
            {
                projectileShotRemainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);

                FireAllProjectiles();
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tank = tank;
            this.enemyFinder = enemyFinder;
        }

        public override void CreateGun()
        {
            tower = UnityEngine.Object.Instantiate(
                GetModule<TowerModule<SingleShotTower>>().TowerPrefab,
                tank.transform
            );
            aimController = new(tank, this, tower);
            projectileSpawner = new(this, tower);
        }

        public override void DestroyGun()
        {
            GameObject.Destroy(tower.gameObject);
        }

        public override void SwapWeapon(IWeapon newWeapon)
        {
            DestroyGun();
            tank.SwapWeapon(newWeapon);
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
                new ProjectileModule<FlyingProjectile>(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<SingleShotTower>(),
                new ProjectileDamageRadiusModule(),
                new ProjectileSpreadAngleModule(),
                new TowerRotationModule(),
                new SelfExplosionDamageModule(),
                new SelfExplosionFireRate(),
                new SelfExplosionCountModule(),
                new SelfExplosionRadiusModule(),
                new SelfExplosionHitMarkTimerModule(),
            };
        }

        private void SelfExplosion()
        {
            int explosionCount = GetModule<SelfExplosionCountModule>().Count.GetModifiedValue();

            for (int i = 0; i < explosionCount; i++)
            {
                float damage = GetModule<SelfExplosionDamageModule>()
                    .Damage.GetPercentagesModifiableValue(tank.DamageModifier)
                    .GetModifiedValue();
                float speed = 0f;

                FlyingProjectile projectile = projectileSpawner.SpawnConnected<FlyingProjectile>(
                    tank.transform
                );
                projectile.Initialize(
                    damage,
                    speed,
                    GetModule<ProjectileSizeModule>()
                        .ProjectileSize.GetPercentagesValue(tank.ProjectileSize),
                    GetModule<SelfExplosionRadiusModule>().Radius.GetModifiedValue(),
                    Vector3.zero
                );

                projectile.StartExplosion(
                    GetModule<SelfExplosionHitMarkTimerModule>().Time.GetModifiedValue()
                );
            }
        }

        private void FireAllProjectiles()
        {
            int projectileCount = GetModule<ProjectilesPerShootModule>()
                .ProjectilesPerShoot.GetModifiedValue();

            for (int i = 0; i < projectileCount; i++)
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            float damage = GetModifiedDamage(
                GetModule<DamageModule>().Damage,
                GetModule<CriticalChanceModule>().CriticalChance,
                GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            var towerDirection = tower.GetDirection();
            Vector3 spreadDirection = GetSpreadDirection(
                towerDirection,
                GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
            );

            FlyingProjectile projectile = projectileSpawner.Spawn<FlyingProjectile>();
            projectile.Initialize(
                damage,
                GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                GetModule<ProjectileSizeModule>()
                    .ProjectileSize.GetPercentagesValue(tank.ProjectileSize),
                GetModule<ProjectileDamageRadiusModule>().DamageRadius.GetModifiedValue(),
                spreadDirection
            );

            projectile.StartFly();
        }
    }
}

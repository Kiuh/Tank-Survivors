using System;
using System.Collections.Generic;
using Common;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class DoubleShotGun : GunBase
    {
        private DoubleShotTower doubleShotTower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private AimController aimController;

        private float doubleShotTowerRemainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (doubleShotTower == null || nearestEnemy == null)
            {
                return;
            }

            aimController.Aim(nearestEnemy);

            doubleShotTowerRemainingTime -= Time.deltaTime;
            if (doubleShotTowerRemainingTime < 0f)
            {
                doubleShotTowerRemainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);
                FireDoubleShotTowerProjectiles();
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
            doubleShotTower = CreateTower<DoubleShotTower>(
                tank.transform,
                SpawnVariation.Disconnected
            );

            aimController = new(tank, this, doubleShotTower);
        }

        public override void DestroyGun()
        {
            DestroyTower(doubleShotTower);
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
                new FireRangeModule(),
                new PenetrationModule(),
                new ProjectileModule<SimpleProjectile>(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<DoubleShotTower>(),
                new ProjectileSpreadAngleModule(),
                new TowerRotationModule(),
                new MultiShotTowerFireRateModule(),
            };
        }

        private void FireDoubleShotTowerProjectiles()
        {
            int projectileCount = GetModule<ProjectilesPerShootModule>()
                .ProjectilesPerShoot.GetModifiedValue();

            for (int i = 0; i < projectileCount; i++)
            {
                SimpleProjectile projectile = doubleShotTower.GetProjectile<SimpleProjectile>();

                var towerDirection = doubleShotTower.GetDirection();
                Vector3 spreadDirection = GetSpreadDirection(
                    towerDirection,
                    GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
                );
                FireProjectile(projectile, spreadDirection);
            }
        }

        private void FireProjectile(SimpleProjectile projectile, Vector3 spreadDirection)
        {
            float damage = GetModifiedDamage(
                GetModule<DamageModule>().Damage,
                GetModule<CriticalChanceModule>().CriticalChance,
                GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            projectile.Initialize(
                damage,
                GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                GetModule<ProjectileSizeModule>()
                    .ProjectileSize.GetPercentagesValue(tank.ProjectileSize),
                GetModule<FireRangeModule>().FireRange.GetPercentagesValue(tank.RangeModifier),
                GetModule<PenetrationModule>().Penetration.GetModifiedValue(),
                spreadDirection
            );
        }
    }
}

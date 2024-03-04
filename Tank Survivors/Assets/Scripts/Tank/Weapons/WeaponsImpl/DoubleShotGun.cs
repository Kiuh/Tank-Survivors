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
        private DoubleShotTower tower;
        private MultiShotTower multiShotTower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private AimController aimController;
        private ProjectileSpawner towerProjectileSpawner;
        private ProjectileSpawner multiTowerProjectileSpawner;

        private float mainTowerRemainingTime = 0f;
        private float multiTowerRemainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (tower == null || nearestEnemy == null)
            {
                return;
            }

            aimController.Aim(nearestEnemy);

            mainTowerRemainingTime -= Time.deltaTime;
            if (mainTowerRemainingTime < 0f)
            {
                mainTowerRemainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);
                FireAllProjectiles();
            }

            multiTowerRemainingTime -= Time.deltaTime;
            if (multiTowerRemainingTime < 0f)
            {
                multiTowerRemainingTime +=
                    GetModule<FireRateModule>().FireRate.GetPercentagesValue(tank.FireRateModifier)
                    / GetModule<MultiShotTowerFireRateModule>()
                        .Percent.GetModifiedValue()
                        .GetNormalized();
                FireAllAdditionalProjectiles();
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
            var towerModule = GetModule<TowerModule<DoubleShotTower>>();
            tower = UnityEngine.Object.Instantiate(towerModule.TowerPrefab, tank.transform);
            towerModule.Tower = tower;

            var multiShotTowerModule = GetModule<TowerModule<MultiShotTower>>();
            multiShotTower = UnityEngine.Object.Instantiate(
                multiShotTowerModule.TowerPrefab,
                tower.transform
            );
            multiShotTowerModule.Tower = multiShotTower;

            aimController = new(tank, this, tower);
            towerProjectileSpawner = new(this, tower);
            multiTowerProjectileSpawner = new(this, multiShotTower);
        }

        public override void DestroyGun()
        {
            GameObject.Destroy(tower.gameObject);
            GetModule<TowerModule<DoubleShotTower>>().Tower = null;

            GameObject.Destroy(multiShotTower.gameObject);
            GetModule<TowerModule<MultiShotTower>>().Tower = null;
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
                new TowerModule<MultiShotTower>(),
                new MultiShotTowerFireRateModule(),
                new CannonModule(),
            };
        }

        private void FireAllAdditionalProjectiles()
        {
            int projectileCount = multiShotTower.CannonsCount;

            for (int i = 0; i < projectileCount; i++)
            {
                SimpleProjectile projectile = multiTowerProjectileSpawner.Spawn<SimpleProjectile>();
                var towerDirection = multiShotTower.GetDirection();
                Vector3 spreadDirection = GetSpreadDirection(
                    towerDirection,
                    GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
                );
                FireProjectile(projectile, spreadDirection);
            }
        }

        private void FireAllProjectiles()
        {
            int projectileCount = GetModule<ProjectilesPerShootModule>()
                .ProjectilesPerShoot.GetModifiedValue();

            for (int i = 0; i < projectileCount; i++)
            {
                SimpleProjectile projectile = towerProjectileSpawner.Spawn<SimpleProjectile>();
                var towerDirection = tower.GetDirection();
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

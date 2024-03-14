using System;
using System.Collections.Generic;
using Common;
using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class BasicGun : GunBase
    {
        private SingleShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private AimController aimController;
        private ProjectileSpawner projectileSpawner;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (tower == null || nearestEnemy == null)
            {
                return;
            }

            aimController.Aim(nearestEnemy);

            remainingTime -= Time.deltaTime;
            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);

                FireAllProjectiles();
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tank = tank;
            this.enemyFinder = enemyFinder;
            CreateGun();
        }

        public override void CreateGun()
        {
            tower = CreateTower<SingleShotTower>(tank.transform, SpawnVariation.Disconnected);

            GetModule<TowerModule<SingleShotTower>>().Tower = tower;

            aimController = new(tank, this, tower);
            projectileSpawner = new(this, tower);
        }

        public override void DestroyGun()
        {
            GetModule<TowerModule<SingleShotTower>>().Tower = null;
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
                new FireRangeModule(),
                new PenetrationModule(),
                new ProjectileModule(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<SingleShotTower>(),
                new ProjectileSpreadAngleModule(),
                new TowerRotationModule(),
            };
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
            var projectile = tower.GetProjectile();

            projectile.Initialize(this, tank, tower);
            projectile.Shoot();
        }
    }
}

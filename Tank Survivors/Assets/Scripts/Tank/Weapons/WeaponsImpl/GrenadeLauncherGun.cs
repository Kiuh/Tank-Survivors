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

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (tower == null || nearestEnemy == null)
            {
                return;
            }

            RotateTower(nearestEnemy);

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
        }

        public override void CreateGun()
        {
            tower = UnityEngine.Object.Instantiate(
                GetModule<TowerModule<SingleShotTower>>().TowerPrefab,
                tank.transform
            );
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
            };
        }

        private void RotateTower(Transform nearestEnemy)
        {
            Vector3 shotDirection = nearestEnemy.position - tank.transform.position;
            tower.RotateTo(
                new RotationParameters()
                {
                    Direction = shotDirection,
                    Speed = GetModule<TowerRotationModule>().RotationSpeed.GetModifiedValue()
                }
            );
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
            FlyingProjectile projectile = UnityEngine.Object.Instantiate(
                GetModule<ProjectileModule<FlyingProjectile>>().ProjectilePrefab,
                tower.GetShotPoint(),
                Quaternion.identity
            );

            float damage = GetModifiedDamage(
                GetModule<DamageModule>().Damage,
                GetModule<CriticalChanceModule>().CriticalChance,
                GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            var towerDirection = tower.transform.up;
            Vector3 spreadDirection = GetSpreadDirection(
                towerDirection,
                GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
            );

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

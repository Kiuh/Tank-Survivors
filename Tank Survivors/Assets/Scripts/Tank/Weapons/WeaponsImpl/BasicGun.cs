using System;
using System.Collections.Generic;
using Common;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class BasicGun : GunBase
    {
        private SingleShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            remainingTime -= Time.deltaTime;

            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);
                Vector3 shotDirection = nearestEnemy.position - tank.transform.position;

                int projectilesCount = GetModule<ProjectilesPerShootModule>()
                    .ProjectilesPerShoot.GetModifiedValue();

                for (int i = 0; i < projectilesCount; i++)
                {
                    Vector3 spreadDirection = GetSpreadDirection(
                        shotDirection,
                        GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
                    );

                    tower.RotateTo(spreadDirection);

                    SimpleProjectile projectile = UnityEngine.Object.Instantiate(
                        GetModule<ProjectileModule<SimpleProjectile>>().ProjectilePrefab,
                        tower.GetShotPoint(),
                        Quaternion.identity
                    );

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
                        GetModule<FireRangeModule>()
                            .FireRange.GetPercentagesValue(tank.RangeModifier),
                        GetModule<PenetrationModule>().Penetration.GetModifiedValue(),
                        spreadDirection
                    );
                }
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tank = tank;
            this.enemyFinder = enemyFinder;
            tower = UnityEngine.Object.Instantiate(
                GetModule<TowerModule<SingleShotTower>>().TowerPrefab,
                tank.transform
            );
        }

        public override void CreateGun() { }

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
                new FireRangeModule(),
                new PenetrationModule(),
                new ProjectileModule<SimpleProjectile>(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<SingleShotTower>(),
                new ProjectileSpreadAngleModule()
            };
        }
    }
}

using System;
using System.Collections.Generic;
using Common;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class MultiShotGun : GunBase
    {
        private MultiShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private ProjectileSpawner projectileSpawner;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (tower == null || nearestEnemy == null)
            {
                return;
            }

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
            var towerModule = GetModule<TowerModule<MultiShotTower>>();
            tower = UnityEngine.Object.Instantiate(towerModule.TowerPrefab, tank.transform);
            towerModule.Tower = tower;

            foreach (var startCannon in towerModule.Tower.CannonPositions)
            {
                tower.AddCannon(GetModule<CannonModule>().Prefab, startCannon);
            }

            projectileSpawner = new(this, tower);
        }

        public override void DestroyGun()
        {
            GetModule<TowerModule<MultiShotTower>>().Tower = null;
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
                new TowerModule<MultiShotTower>(),
                new ProjectileSpreadAngleModule(),
                new CannonModule(),
            };
        }

        private void FireAllProjectiles()
        {
            int projectileCount = tower.CannonsCount;

            for (int i = 0; i < projectileCount; i++)
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            var towerDirection = tower.GetDirection();
            Vector3 spreadDirection = GetSpreadDirection(
                towerDirection,
                GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
            );

            SimpleProjectile projectile = projectileSpawner.Spawn<SimpleProjectile>();

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

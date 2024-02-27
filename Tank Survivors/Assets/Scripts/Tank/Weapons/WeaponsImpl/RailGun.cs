using System;
using System.Collections.Generic;
using Common;
using Tank;
using Tank.Towers;
using Tank.Weapons;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Tank.Weapons
{
    [Serializable]
    public class RailGun : GunBase
    {
        private SingleShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private AimController aimController;

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

                Fire();
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tank = tank;
            this.enemyFinder = enemyFinder;
            aimController = new(tank, this, tower);
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
                new FireRangeModule(),
                new ProjectileModule<RayRenderer>(),
                new RayDurationModule(),
                new TowerModule<SingleShotTower>(),
                new TowerRotationModule(),
            };
        }

        private void Fire()
        {
            float fireRange = GetModule<FireRangeModule>()
                .FireRange.GetPercentagesValue(tank.RangeModifier);

            RayRenderer ray = UnityEngine.Object.Instantiate(
                GetModule<ProjectileModule<RayRenderer>>().ProjectilePrefab
            );

            float damage = GetModifiedDamage(
                GetModule<DamageModule>().Damage,
                GetModule<CriticalChanceModule>().CriticalChance,
                GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            var towerDirection = tower.transform.up;
            ray.Initialize(
                damage,
                GetModule<RayDurationModule>().RayDuration.GetModifiedValue(),
                tower.GetShotPoint(),
                tank.transform.position + (towerDirection.normalized * fireRange)
            );
            ray.Show();
        }
    }
}

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

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            if (tower == null)
            {
                return;
            }

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

                tower.RotateTo(shotDirection);

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

                ray.Initialize(
                    damage,
                    GetModule<RayDurationModule>().RayDuration.GetModifiedValue(),
                    tower.GetShotPoint(),
                    tank.transform.position + (shotDirection.normalized * fireRange)
                );
                ray.Show();
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
            newWeapon.CreateGun();
            tank.SwapWeapon(newWeapon);
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
            };
        }
    }
}

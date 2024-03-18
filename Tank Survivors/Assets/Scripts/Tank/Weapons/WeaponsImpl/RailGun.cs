using System;
using System.Collections.Generic;
using Common;
using Tank;
using Tank.Towers;
using Tank.Weapons;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Assets.Scripts.Tank.Weapons
{
    [Serializable]
    public class RailGun : GunBase
    {
        private SingleShotTower mainTower;

        private TankImpl tank;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (mainTower == null || nearestEnemy == null)
            {
                return;
            }

            remainingTime -= Time.deltaTime;
            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);

                var ray = GetModule<ProjectileModule>().ProjectilePrefab.Spawn();

                ray.Initialize(this, tank, mainTower);
                ray.Shoot();
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
            mainTower = CreateTower<SingleShotTower>(tank.transform);
        }

        public override void DestroyGun()
        {
            GameObject.Destroy(mainTower.gameObject);
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
                new ProjectileModule(),
                new RayDurationModule(),
                new TowerModule<SingleShotTower>(),
                new TowerRotationModule(),
            };
        }
    }
}

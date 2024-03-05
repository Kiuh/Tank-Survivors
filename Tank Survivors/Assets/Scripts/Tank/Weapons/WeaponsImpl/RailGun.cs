﻿using System;
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
        private SingleShotTower mainTower;
        private MultiShotTower multiShotTower;

        private TankImpl tank;
        private EnemyFinder enemyFinder;
        private AimController aimController;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (mainTower == null || nearestEnemy == null)
            {
                return;
            }

            aimController.Aim(nearestEnemy);

            remainingTime -= Time.deltaTime;
            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);

                RayRenderer ray = mainTower.GetProjectile<RayRenderer>();
                var towerDirection = mainTower.GetDirection();

                RailGunFireProjectile(ray, towerDirection);
                FireMultiShotTowerProjectiles();
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

            multiShotTower = CreateTower<MultiShotTower>(
                mainTower.transform,
                SpawnVariation.Disconnected
            );

            aimController = new(tank, this, mainTower);
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
                new ProjectileModule<RayRenderer>(),
                new RayDurationModule(),
                new TowerModule<SingleShotTower>(),
                new TowerRotationModule(),
                new TowerModule<MultiShotTower>(),
                new CannonModule(),
                new ProjectileSpawnVariationModule(),
            };
        }

        private void FireMultiShotTowerProjectiles()
        {
            int projectileCount = multiShotTower.CannonsCount;

            for (int i = 0; i < projectileCount; i++)
            {
                RayRenderer projectile = multiShotTower.GetProjectile<RayRenderer>();
                var towerDirection = multiShotTower.GetDirection();

                RailGunFireProjectile(projectile, towerDirection);
            }
        }

        private void RailGunFireProjectile(RayRenderer ray, Vector3 direction)
        {
            float fireRange = GetModule<FireRangeModule>()
                .FireRange.GetPercentagesValue(tank.RangeModifier);

            float damage = GetModifiedDamage(
                GetModule<DamageModule>().Damage,
                GetModule<CriticalChanceModule>().CriticalChance,
                GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            ray.Initialize(
                damage,
                GetModule<RayDurationModule>().RayDuration.GetModifiedValue(),
                mainTower.GetShotPoint(),
                tank.transform.position + (direction.normalized * fireRange)
            );
            ray.Show();
        }
    }
}

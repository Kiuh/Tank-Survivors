using System;
using System.Collections.Generic;
using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class GrenadeLauncherGun : GunBase
    {
        private SingleShotTower tower;

        private float selfExplosionRemainingTime = 0f;

        public override void ProceedAttack()
        {
            tower.ProceedAttack();

            selfExplosionRemainingTime -= Time.deltaTime;
            if (selfExplosionRemainingTime < 0f)
            {
                selfExplosionRemainingTime += GetModule<SelfExplosionFireRateModule>()
                    .FireRate.GetModifiedValue();

                SelfExplosion();
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            Tank = tank;
            EnemyFinder = enemyFinder;
        }

        public override void CreateGun()
        {
            tower = CreateTower<SingleShotTower>(Tank.transform, SpawnVariation.Disconnected);
        }

        public override void DestroyGun()
        {
            GameObject.Destroy(tower.gameObject);
        }

        public override void SwapWeapon(IWeapon newWeapon)
        {
            DestroyGun();
            Tank.SwapWeapon(newWeapon);
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
                new ProjectileModule(),
                new FireRangeModule(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<SingleShotTower>(),
                new ProjectileDamageRadiusModule(),
                new ProjectileSpreadAngleModule(),
                new TowerRotationModule(),
                new SelfExplosionPrefabModule(),
                new SelfExplosionDamageModule(),
                new SelfExplosionFireRateModule(),
                new SelfExplosionCountModule(),
                new SelfExplosionRadiusModule(),
                new SelfExplosionHitMarkTimerModule(),
                new SelfExplosionFireTimerModule(),
                new FireDamageModule(),
                new FireFireRateModule(),
                new ProjectileFireTimerModule(),
            };
        }

        private void SelfExplosion()
        {
            int explosionCount = GetModule<SelfExplosionCountModule>().Count.GetModifiedValue();

            for (int i = 0; i < explosionCount; i++)
            {
                var projectile = GetModule<SelfExplosionPrefabModule>()
                    .Prefab.SpawnConnected(Tank.transform);

                projectile.Initialize(this, Tank, tower);
                projectile.Shoot();
            }
        }
    }
}

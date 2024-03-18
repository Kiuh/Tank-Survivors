using System;
using System.Collections.Generic;
using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class BasicGun : GunBase
    {
        private SingleShotTower tower;
        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            tower.ProceedAttack();
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            base.Tank = tank;
            EnemyFinder = enemyFinder;
            CreateGun();
        }

        public override void CreateGun()
        {
            tower = CreateTower<SingleShotTower>(Tank.transform, SpawnVariation.Disconnected);
            GetModule<TowerModule<SingleShotTower>>().Tower = tower;
        }

        public override void DestroyGun()
        {
            GetModule<TowerModule<SingleShotTower>>().Tower = null;
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
    }
}

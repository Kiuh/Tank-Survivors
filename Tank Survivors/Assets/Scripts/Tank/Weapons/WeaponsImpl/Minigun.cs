using System;
using System.Collections.Generic;
using Panels.Pause;
using Tank.Towers;
using Tank.Weapons.Modules;

namespace Tank.Weapons
{
    [Serializable]
    public class Minigun : GunBase
    {
        private ITower tower;

        public override void ProceedAttack()
        {
            tower.ProceedAttack();
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            Tank = tank;
            EnemyFinder = enemyFinder;
        }

        public override void CreateGun()
        {
            tower = CreateTower(Tank.transform, SpawnVariation.Disconnected);
        }

        public override void DestroyGun()
        {
            DestroyTower(tower);
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
                new FireRangeModule(),
                new CriticalChanceModule(),
                new CriticalMultiplierModule(),
                new FireRateModule(),
                new PenetrationModule(),
                new ProjectileModule(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule(),
                new ProjectileSpreadAngleModule(),
                new TowerRotationModule(),
            };
        }

        public override StatBlockData GetStatBlockData()
        {
            StatBlockData statBlockData =
                new()
                {
                    StatName = "Миниган",
                    StatsData = new() { }
                };
            return statBlockData;
        }
    }
}

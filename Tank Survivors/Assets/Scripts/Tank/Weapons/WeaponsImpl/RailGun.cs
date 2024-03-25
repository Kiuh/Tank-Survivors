using System;
using System.Collections.Generic;
using Tank.Towers;
using Tank.Weapons.Modules;
using Tank.Weapons.Modules.Cannon;

namespace Tank.Weapons
{
    [Serializable]
    public class RailGun : GunBase
    {
        private SingleShotTower tower;

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
            tower = CreateTower<SingleShotTower>(Tank.transform);
            tower.GetComponent<Towers.Cannon.RailGunController>().Initialize(this, Tank);
        }

        public override void DestroyGun()
        {
            DestroyGun();
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
                new ProjectilesPerShootModule(),
                new ProjectileModule(),
                new RayDurationModule(),
                new TowerModule<SingleShotTower>(),
                new TowerRotationModule(),
                new CannonModule(),
            };
        }
    }
}

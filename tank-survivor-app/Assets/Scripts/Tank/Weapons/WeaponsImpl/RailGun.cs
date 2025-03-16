using System;
using System.Collections.Generic;
using Tank.Towers;
using Tank.Weapons.Modules;
using Tank.Weapons.Modules.Cannon;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class RailGun : GunBase
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
            tower = CreateTower(Tank.transform);
            (tower as MonoBehaviour)
                .GetComponent<Towers.Cannon.Controller>()
                .Initialize(this, Tank);
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
                new RayFireRateModule(),
                new TowerModule(),
                new TowerRotationModule(),
                new CannonModule(),
                new MultiCannonFireRateModule(),
            };
        }
    }
}

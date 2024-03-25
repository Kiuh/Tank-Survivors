using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Tank.Weapons;
using Tank.Weapons.Modules;
using Tank.Weapons.Modules.Cannon;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Towers.Cannon
{
    public class DoubleGunController : MonoBehaviour
    {
        [SerializeField]
        private DoubleShotTower tower;

        [SerializeField]
        private Positioner positioner;

        private List<Cannon> cannons;
        private GunBase weapon;
        private TankImpl tank;

        private float shotCooldown;

        public void Initialize(GunBase weapon, TankImpl tank)
        {
            cannons = new List<Cannon>();
            this.weapon = weapon;
            this.tank = tank;

            tower.OnProceedAttack += ProceedAttack;
        }

        private void OnDestroy()
        {
            tower.OnProceedAttack -= ProceedAttack;
        }

        public void ProceedAttack()
        {
            shotCooldown -= Time.deltaTime;
            if (shotCooldown <= 0)
            {
                shotCooldown +=
                    weapon
                        .GetModule<FireRateModule>()
                        .FireRate.GetPercentagesValue(tank.FireRateModifier)
                    / weapon
                        .GetModule<MultiCannonFireRateModule>()
                        .Percent.GetModifiedValue()
                        .NormalizedValue;

                FireAllCannons();
            }
        }

        private void FireAllCannons()
        {
            foreach (var cannon in cannons)
            {
                IProjectile projectile = weapon
                    .GetModule<ProjectileModule>()
                    .ProjectilePrefab.Spawn();
                projectile.Initialize(
                    weapon,
                    tank,
                    tower,
                    cannon.GetShotPoint(),
                    cannon.GetDirection()
                );
                projectile.Shoot();
            }
        }

        public void AddCannon(string positionName)
        {
            Property cannonProperty = positioner.Properties.FirstOrDefault(p =>
                p.Name.Equals(positionName)
            );

            if (cannonProperty == null)
            {
                throw new Exception(
                    $"Can't find property with name {positionName} in Cannon Positioner on gameObject {gameObject.name}"
                );
            }

            Cannon newCannon = Instantiate(
                weapon.GetModule<CannonModule>().CannonPrefab,
                cannonProperty.transform.position,
                cannonProperty.transform.rotation,
                transform
            );

            cannons.Add(newCannon);
        }
    }
}

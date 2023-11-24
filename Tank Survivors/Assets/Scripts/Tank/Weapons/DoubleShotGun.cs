using Assets.Scripts.Tank.Weapons;
using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class DoubleShotGun : GunBase
    {
        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("$UpgradeName")]
        public override List<IWeaponModule> Modules { get; protected set; } =
            new()
            {
                new DamageModule(),
                new FireRateModule(),
                new CriticalChanceModule(),
                new CriticalMultiplierModule(),
                new FireRangeModule(),
                new PenetrationModule(),
                new ProjectileModule<SimpleProjectile>(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<DoubleShotTower>(),
                new ProjectileSpreadAngleModule()
            };

        private DoubleShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            remainingTime -= Time.deltaTime;

            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>().FireRate.GetPrecentageValue(
                    tank.FireRateModifier
                );
                Vector3 shotDirection = nearestEnemy.position - tank.transform.position;

                int projectilesCount =
                    GetModule<ProjectilesPerShootModule>().ProjectilesPerShoot.GetModifiedValue();
                for (int i = 0; i < projectilesCount; i++)
                {
                    Vector3 spreadDirection = this.GetSpreadDirection(
                        shotDirection,
                        GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
                    );

                    tower.RotateTo(spreadDirection);

                    SimpleProjectile projectile = UnityEngine.Object.Instantiate(
                        GetModule<ProjectileModule<SimpleProjectile>>().ProjectilePrefab,
                        tower.GetShotPoint(),
                        Quaternion.identity
                    );

                    float damage = this.GetModifiedDamage(
                        GetModule<DamageModule>().Damage,
                        GetModule<CriticalChanceModule>().CriticalChance,
                        GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                        tank
                    );

                    projectile.Initialize(
                        damage,
                        GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                        GetModule<ProjectileSizeModule>().ProjectileSize.GetPrecentageValue(
                            tank.ProjectileSize
                        ),
                        GetModule<FireRangeModule>().FireRange.GetPrecentageValue(
                            tank.RangeModifier
                        ),
                        GetModule<PenetrationModule>().Penetration.GetModifiedValue(),
                        spreadDirection
                    );
                }
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tank = tank;
            this.enemyFinder = enemyFinder;
            tower = UnityEngine.Object.Instantiate(
                GetModule<TowerModule<DoubleShotTower>>().TowerPrefab,
                tank.transform
            );
        }
    }
}

using Assets.Scripts.Tank.Weapons;
using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    public abstract class GunBase : IWeapon
    {
        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        [LabelText("Weapon Name")]
        public string UpgradeName { get; private set; }

        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        public uint CurrentLevel { get; set; }

        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        public uint MaxLevel { get; private set; }

        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        [ShowInInspector]
        private List<LeveledWeaponUpgrade> leveledUpgrades;

        public abstract List<IWeaponModule> Modules { get; protected set; }

        public T GetModule<T>()
            where T : class, IWeaponModule
        {
            IWeaponModule module = Modules.FirstOrDefault(x => x is T);
            return module == null ? null : module as T;
        }

        public IEnumerable<ILeveledUpgrade> Upgrades => leveledUpgrades;

        public abstract void Initialize(TankImpl tank, EnemyFinder enemyFinder);

        public abstract void ProceedAttack();
    }

    [Serializable]
    public class BasicGun : GunBase
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
                new TowerModule<SingleShotTower>(),
            };

        private SingleShotTower tower;
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
                    tower.RotateTo(shotDirection);

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
                        shotDirection
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
                GetModule<TowerModule<SingleShotTower>>().TowerPrefab,
                tank.transform
            );
        }
    }
}

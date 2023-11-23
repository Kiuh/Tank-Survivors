using Sirenix.OdinInspector;
using Sirenix.Serialization;
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

        public abstract void Initialize(Transform tankRoot, EnemyFinder enemyFinder);

        public abstract void ProceedAttack(float deltaTime);
    }

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
        private Transform tankRoot;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public override void ProceedAttack(float deltaTime)
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            remainingTime -= deltaTime;

            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>().FireRate.GetModifiedValue();
                Vector3 shotDirection = nearestEnemy.position - tankRoot.position;

                for (
                    int i = 0;
                    i
                        < GetModule<ProjectilesPerShootModule>().ProjectilesPerShoot.GetModifiedValue();
                    i++
                )
                {
                    tower.RotateTo(shotDirection);

                    SimpleProjectile projectile = UnityEngine.Object.Instantiate(
                        GetModule<ProjectileModule<SimpleProjectile>>().ProjectilePrefab,
                        tower.GetShotPoint(),
                        Quaternion.identity
                    );

                    float damage = GetModule<DamageModule>().Damage.GetModifiedValue();
                    bool isCritical = GetModule<CriticalChanceModule>().CriticalChance
                        .GetModifiedValue()
                        .TryChance();
                    if (isCritical)
                    {
                        float criticalMultiplier =
                            GetModule<CriticalMultiplierModule>().CriticalMultiplier
                                .GetModifiedValue()
                                .Value;
                        damage *= 1f + criticalMultiplier;
                    }
                    projectile.Initialize(
                        damage,
                        GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                        GetModule<ProjectileSizeModule>().ProjectileSize.GetModifiedValue(),
                        GetModule<FireRangeModule>().FireRange.GetModifiedValue(),
                        GetModule<PenetrationModule>().Penetration.GetModifiedValue(),
                        shotDirection
                    );
                }
            }
        }

        public override void Initialize(Transform tankRoot, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tankRoot = tankRoot;
            this.enemyFinder = enemyFinder;
            tower = UnityEngine.Object.Instantiate(
                GetModule<TowerModule<SingleShotTower>>().TowerPrefab,
                tankRoot
            );
        }
    }
}

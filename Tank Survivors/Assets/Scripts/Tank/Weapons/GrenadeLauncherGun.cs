using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    public class GrenadeLauncherGun : GunBase
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
                new ProjectileModule<FlyingProjectile>(),
                new ProjectileSizeModule(),
                new ProjectileSpeedModule(),
                new ProjectilesPerShootModule(),
                new TowerModule<SingleShotTower>(),
                new ProjectileDamageRadiusModule()
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

                int projectileCount =
                    GetModule<ProjectilesPerShootModule>().ProjectilesPerShoot.GetModifiedValue();

                for (int i = 0; i < projectileCount; i++)
                {
                    tower.RotateTo(shotDirection);

                    FlyingProjectile projectile = UnityEngine.Object.Instantiate(
                        GetModule<ProjectileModule<FlyingProjectile>>().ProjectilePrefab,
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
                        GetModule<ProjectileDamageRadiusModule>().DamageRadius.GetModifiedValue(),
                        nearestEnemy.position
                    );
                    projectile.StartFly();
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

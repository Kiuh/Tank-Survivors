using Common;
using DataStructs;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class GrenadeLauncherGun : IWeapon
    {
        [SerializeField]
        [ReadOnly]
        private uint currentLevel;
        public uint CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        [SerializeField]
        private uint maxLevel;
        public uint MaxLevel => maxLevel;

        [SerializeField]
        private ModifiableValue<Percentage> criticalMultiplier;
        public ModifiableValue<Percentage> CriticalMultiplier => criticalMultiplier;

        [SerializeField]
        private ModifiableValue<float> damage;
        public ModifiableValue<float> Damage => damage;

        [SerializeField]
        private ModifiableValue<Percentage> criticalChance;
        public ModifiableValue<Percentage> CriticalChance => criticalChance;

        [SerializeField]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;

        [SerializeField]
        private FlyingProjectile projectilePrefab;
        public FlyingProjectile ProjectilePrefab => projectilePrefab;

        [SerializeField]
        private ModifiableValue<float> projectileSize;
        public ModifiableValue<float> ProjectileSize => projectileSize;

        [SerializeField]
        private ModifiableValue<float> projectileSpeed;
        public ModifiableValue<float> ProjectileSpeed => projectileSpeed;

        [SerializeField]
        private ModifiableValue<int> projectilePerShot;
        public ModifiableValue<int> ProjectilesPerShoot => projectilePerShot;

        [SerializeField]
        private ModifiableValue<float> damageRadius;
        public ModifiableValue<float> DamageRadius => damageRadius;

        [SerializeField]
        private List<SerializedLeveledBasicGunUpgrade> leveledBasicGunUpgrades;

        [SerializeField]
        private SingleShotTower towerPrefab;
        public SingleShotTower TowerPrefab => towerPrefab;

        [SerializeField]
        private string upgradeName;
        public string UpgradeName => upgradeName;

        public IEnumerable<ILeveledUpgrade> Upgrades =>
            leveledBasicGunUpgrades.Select(x => x.ToLeveledBasicGunUpgrade());

        private SingleShotTower tower;
        private Transform tankRoot;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public void ProceedAttack(float deltaTime)
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            remainingTime -= deltaTime;

            if (remainingTime < 0f)
            {
                remainingTime += fireRate.GetModifiedValue();
                Vector3 shotDirection = nearestEnemy.position - tankRoot.position;

                for (int i = 0; i < projectilePerShot.GetModifiedValue(); i++)
                {
                    tower.RotateTo(shotDirection);

                    FlyingProjectile projectile = UnityEngine.Object.Instantiate(
                        projectilePrefab,
                        tower.GetShotPoint(),
                        Quaternion.identity
                    );

                    projectile.Initialize(
                        damage.GetModifiedValue()
                            * (
                                1f
                                + (
                                    criticalChance.SourceValue.TryChance()
                                        ? 0f
                                        : criticalMultiplier.GetModifiedValue().Value
                                )
                            ),
                        projectileSpeed.GetModifiedValue(),
                        projectileSize.GetModifiedValue(),
                        damageRadius.GetModifiedValue(),
                        nearestEnemy.position
                    );
                    projectile.StartFly();
                }
            }
        }

        public void Initialize(Transform tankRoot, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tankRoot = tankRoot;
            this.enemyFinder = enemyFinder;
            tower = UnityEngine.Object.Instantiate(towerPrefab, tankRoot);
        }
    }
}

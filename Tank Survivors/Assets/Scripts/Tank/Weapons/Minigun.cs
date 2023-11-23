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
    public class Minigun : IWeapon
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
        private ModifiableValue<float> fireRange;
        public ModifiableValue<float> FireRange => fireRange;

        [SerializeField]
        private ModifiableValue<Percentage> criticalChance;
        public ModifiableValue<Percentage> CriticalChance => criticalChance;

        [SerializeField]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;

        [SerializeField]
        private ModifiableValue<int> penetration;
        public ModifiableValue<int> Penetration => penetration;

        [SerializeField]
        private SimpleProjectile projectilePrefab;
        public SimpleProjectile ProjectilePrefab => projectilePrefab;

        [SerializeField]
        private ModifiableValue<float> projectileSize;
        public ModifiableValue<float> ProjectileSize => projectileSize;

        [SerializeField]
        private ModifiableValue<float> projectileSpeed;
        public ModifiableValue<float> ProjectileSpeed => projectileSpeed;

        [SerializeField]
        private ModifiableValue<int> projectilesPerShoot;
        public ModifiableValue<int> ProjectilesPerShoot => projectilesPerShoot;

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

                for (int i = 0; i < projectilesPerShoot.GetModifiedValue(); i++)
                {
                    tower.RotateTo(shotDirection);

                    SimpleProjectile projectile = UnityEngine.Object.Instantiate(
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
                        fireRange.GetModifiedValue(),
                        penetration.GetModifiedValue(),
                        shotDirection
                    );
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

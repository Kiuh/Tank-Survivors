﻿using Common;
using DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Tank.Weapons
{
    [Serializable]
    public class DoubleShotGun
        : IWeapon,
            IHaveDamage,
            IHaveFireRange,
            IHaveCriticalChance,
            IHaveFireRate,
            IHavePenetration,
            IHaveProjectile,
            IHaveProjectileSize,
            IHaveProjectileSpeed,
            IHaveProjectilesPerShoot,
            IHaveTower<DoubleShotTower>
    {
        [SerializeField]
        [InspectorReadOnly]
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
        private Projectile projectilePrefab;
        public Projectile ProjectilePrefab => projectilePrefab;

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
        private DoubleShotTower towerPrefab;
        public DoubleShotTower TowerPrefab => towerPrefab;

        [SerializeField]
        private string upgradeName;
        public string UpgradeName => upgradeName;

        public IEnumerable<ILeveledUpgrade> Upgrades =>
            leveledBasicGunUpgrades.Select(x => x.ToLeveledBasicGunUpgrade());

        private DoubleShotTower tower;
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

                    Projectile projectile = UnityEngine.Object.Instantiate(
                        projectilePrefab,
                        tower.GetShotPoint(),
                        Quaternion.identity
                    );

                    projectile.Init(
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
            this.tankRoot = tankRoot;
            this.enemyFinder = enemyFinder;
            tower = UnityEngine.Object.Instantiate(towerPrefab, tankRoot);
        }
    }
}
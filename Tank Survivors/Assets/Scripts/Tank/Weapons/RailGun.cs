using Common;
using DataStructs;
using Enemies;
using System;
using System.Collections.Generic;
using Tank;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Tank.Weapons
{
    [Serializable]
    public class RailGun : IWeapon
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
        private RayRenderer rayRenderer;
        public RayRenderer ProjectilePrefab => rayRenderer;

        [SerializeField]
        private float rayDuration;
        public float RayDuration => rayDuration;

        [SerializeField]
        private RayRenderer rayPrefab;
        public RayRenderer RayPrefab => rayPrefab;

        [SerializeField]
        private SingleShotTower towerPrefab;
        public SingleShotTower TowerPrefab => towerPrefab;

        [SerializeField]
        private string upgradeName;
        public string UpgradeName => upgradeName;

        public List<IWeaponModule> Modules => throw new NotImplementedException();

        public IEnumerable<ILeveledUpgrade> Upgrades => throw new NotImplementedException();

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

                tower.RotateTo(shotDirection);

                RaycastHit2D[] collisions = Physics2D.RaycastAll(
                    tower.GetShotPoint(),
                    shotDirection,
                    fireRange.GetModifiedValue()
                );

                RayRenderer ray = UnityEngine.Object.Instantiate(rayPrefab);
                Vector3 endPoint =
                    tankRoot.position + (shotDirection.normalized * fireRange.GetModifiedValue());
                ray.Initialize(rayDuration, tower.GetShotPoint(), endPoint);
                ray.Show();

                foreach (RaycastHit2D collision in collisions)
                {
                    if (collision.transform.TryGetComponent<IEnemy>(out IEnemy enemy))
                    {
                        enemy.TakeDamage(
                            damage.GetModifiedValue()
                                * (
                                    1f
                                    + (
                                        criticalChance.SourceValue.TryChance()
                                            ? 0f
                                            : criticalMultiplier.GetModifiedValue().Value
                                    )
                                )
                        );
                    }
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

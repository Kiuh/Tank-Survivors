using Common;
using DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class BasicGun
        : IWeapon,
            IHaveDamage,
            IHaveFireRange,
            IHaveCriticalChance,
            IHaveFireRate,
            IHavePenetration,
            IHaveProjectilesPerShoot,
            IHaveProjectileSize,
            IHaveProjectile
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
        private ModifiableValue<float> damage;
        public ModifiableValue<float> Damage => damage;

        [SerializeField]
        private ModifiableValue<float> fireRange;
        public ModifiableValue<float> FireRange => fireRange;

        [SerializeField]
        private ModifiableValue<float> projectileSize;
        public ModifiableValue<float> ProjectileSize => projectileSize;

        [SerializeField]
        private ModifiableValue<int> penetration;
        public ModifiableValue<int> Penetration => penetration;

        [SerializeField]
        private ModifiableValue<int> projectilesPerShoot;
        public ModifiableValue<int> ProjectilesPerShoot => projectilesPerShoot;

        [SerializeField]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;

        [SerializeField]
        private ModifiableValue<Percentage> criticalChance;
        public ModifiableValue<Percentage> CriticalChance => criticalChance;

        [SerializeField]
        private ModifiableValue<Percentage> criticalMultiplier;
        public ModifiableValue<Percentage> CriticalMultiplier => criticalMultiplier;

        [SerializeField]
        private List<SerializedLeveledBasicGunUpgrade> leveledBasicGunUpgrades;

        [SerializeField]
        private string upgradeName;
        public string UpgradeName => upgradeName;

        public IEnumerable<ILeveledUpgrade> Upgrades =>
            leveledBasicGunUpgrades.Select(x => x.ToLeveledBasicGunUpgrade());

        [SerializeField]
        private Projectile projectilePrefab;
        public Projectile ProjectilePrefab => projectilePrefab;

        private float remainingTime;

        public void ProceedAttack(float deltaTime)
        {
            remainingTime -= deltaTime;

            if (remainingTime < 0f)
            {
                remainingTime += fireRate.GetModifiedValue();
                for (int i = 0; i < projectilesPerShoot.GetModifiedValue(); i++)
                {
                    float resultDamage = damage.GetModifiedValue() * (1f + 
                        (criticalChance.SourceValue.TryChance() ? 0f : criticalMultiplier.GetModifiedValue().Value));

                    Projectile projectile = UnityEngine.Object
                        .Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity); // TODO: projectile spawn positon
                    projectile.Init(
                        resultDamage,
                        projectileSize.GetModifiedValue(),
                        fireRange.GetModifiedValue(),
                        penetration.GetModifiedValue(),
                        Vector3.up); // TODO: direction towards the enemy
                }
            }
        }

        public BasicGun()
        {
            remainingTime = fireRate.GetModifiedValue();
        }
    }

    [InterfaceEditor]
    public interface ILeveledBasicGunUpgrade : ILeveledUpgrade { }

    [Serializable]
    public class BasicGunDamageUp : ILeveledBasicGunUpgrade
    {
        [SerializeField]
        private uint upgradingLevel;

        [SerializeField]
        private float damageUpValue;
        public uint UpgradingLevel => upgradingLevel;

        [SerializeField]
        private string description;
        public string Description => description;

        public void ApplyUpgrade(TankImpl tank)
        {
            BasicGun weapon = tank.Weapons.First(x => x is BasicGun) as BasicGun;
            weapon.Damage.Modifications.Add(
                new ValueModification<float>((x) => x + damageUpValue, ModificationPriority.Medium)
            );
        }
    }
}

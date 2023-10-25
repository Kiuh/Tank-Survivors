using Common;
using DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tank.Weapons
{
    [InterfaceEditor]
    public interface ILeveledBasicGunUpgrade
    {
        public uint UpgradingLevel { get; }
        public void ApplyUpgrade(TankImpl tank);
        public UpgradeVariantInformation GetUpgradeInformation();
        public bool IsMyUpgrade(UpgradeVariantInformation upgradeVariant);
    }

    public class BasicGun
        : IWeapon,
            IHaveDamage,
            IHaveFireRange,
            IHaveCriticalChance,
            IHaveFireRate,
            IHavePenetration,
            IHaveProjectilesPerShoot,
            IHaveProjectileSize
    {
        private uint currentLevel;
        public uint CurrentLevel => currentLevel;

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
        public IEnumerable<ILeveledBasicGunUpgrade> LeveledBasicGunUpgrades =>
            leveledBasicGunUpgrades.Select(x => x.ToLeveledBasicGunUpgrade());

        public NextUpgradeInformation GetNextUpgradeInformation()
        {
            return NextUpgradeInformation.Construct(
                LeveledBasicGunUpgrades
                    .Where(x => x.UpgradingLevel == currentLevel)
                    .Select(x => x.GetUpgradeInformation())
            );
        }

        public void ProceedAttack()
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public void ApplyUpgrade(TankImpl tank, UpgradeVariantInformation upgradeVariant)
        {
            LeveledBasicGunUpgrades.First(x => x.IsMyUpgrade(upgradeVariant)).ApplyUpgrade(tank);
            currentLevel++;
        }
    }

    [Serializable]
    public class BasicGunDamageUp : ILeveledBasicGunUpgrade
    {
        [SerializeField]
        private uint upgradingLevel;

        [SerializeField]
        private float damageUpValue;
        public uint UpgradingLevel => upgradingLevel;

        public void ApplyUpgrade(TankImpl tank)
        {
            BasicGun weapon = tank.Weapons.First(x => x is BasicGun) as BasicGun;
            weapon.Damage.Modifications.Add(
                new ValueModification<float>((x) => x + damageUpValue, ModificationPriority.Medium)
            );
        }

        public UpgradeVariantInformation GetUpgradeInformation()
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public bool IsMyUpgrade(UpgradeVariantInformation upgradeVariant)
        {
            // TODO: implement
            throw new NotImplementedException();
        }
    }
}

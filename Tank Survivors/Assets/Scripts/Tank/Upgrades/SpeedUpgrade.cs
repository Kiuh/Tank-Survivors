using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tank.Upgrades
{
    [InterfaceEditor]
    public interface ILeveledSpeedUpgrade
    {
        public uint UpgradingLevel { get; }
        public void ApplyUpgrade(TankImpl tank);
        public UpgradeVariantInformation GetUpgradeInformation();
        public bool IsMyUpgrade(UpgradeVariantInformation upgradeVariant);
    }

    [Serializable]
    public class SpeedUpgrade : ITankUpgrade
    {
        private uint currentLevel = 0;

        [SerializeField]
        private uint maxLevel;

        [SerializeField]
        private List<SerializedLeveledSpeedUpgrade> upgradeList;
        public IEnumerable<ILeveledSpeedUpgrade> Upgrades =>
            upgradeList.Select(x => x.ToLeveledSpeedUpgrade());

        public uint CurrentLevel => currentLevel;

        public uint MaxLevel => maxLevel;

        public void ApplyUpgrade(TankImpl tank, UpgradeVariantInformation upgradeVariant)
        {
            Upgrades.First(x => x.IsMyUpgrade(upgradeVariant)).ApplyUpgrade(tank);
            currentLevel++;
        }

        public NextUpgradeInformation GetNextUpgradeInformation()
        {
            return NextUpgradeInformation.Construct(
                Upgrades
                    .Where(x => x.UpgradingLevel == currentLevel)
                    .Select(x => x.GetUpgradeInformation())
            );
        }
    }

    [Serializable]
    public class SimpleAddingLeveledSpeedUpgrade : ILeveledSpeedUpgrade
    {
        [SerializeField]
        private uint level = 0;

        [SerializeField]
        private float addingValue;
        public uint UpgradingLevel => level;

        public void ApplyUpgrade(TankImpl tank)
        {
            tank.Speed.Modifications.Add(
                new ValueModification<float>((x) => x + addingValue, ModificationPriority.Medium)
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

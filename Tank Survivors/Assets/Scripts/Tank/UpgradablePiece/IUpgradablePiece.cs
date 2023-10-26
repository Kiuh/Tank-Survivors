using System.Collections.Generic;
using System.Linq;

namespace Tank.UpgradablePiece
{
    public interface IUpgradablePiece
    {
        public string UpgradeName { get; }
        public uint CurrentLevel { get; set; }
        public uint MaxLevel { get; }
        public bool IsReachedMaxLevel => CurrentLevel >= MaxLevel;
        public IEnumerable<ILeveledUpgrade> Upgrades { get; }
        public void ApplyUpgrade(TankImpl tank, UpgradeVariantInformation upgradeVariant)
        {
            Upgrades.First(x => x.IsMyUpgrade(upgradeVariant)).ApplyUpgrade(tank);
            CurrentLevel++;
        }
        public IEnumerable<UpgradeVariantInformation> GetNextUpgradeInformation()
        {
            return Upgrades
                .Where(x => x.UpgradingLevel == CurrentLevel)
                .Select(x => x.GetUpgradeInformation());
        }
    }

    public interface ILeveledUpgrade
    {
        public string Guid { get; }
        public string Description { get; }
        public uint UpgradingLevel { get; }

        public void ApplyUpgrade(TankImpl tank);
        public UpgradeVariantInformation GetUpgradeInformation()
        {
            return new UpgradeVariantInformation(Description, Guid);
        }
        public bool IsMyUpgrade(UpgradeVariantInformation upgradeVariant)
        {
            return upgradeVariant.Guid == Guid;
        }
    }
}

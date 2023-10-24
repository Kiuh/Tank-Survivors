using System;
using System.Collections.Generic;

namespace Tank
{
    public struct NextUpgradeInformation
    {
        // TODO: implement

        public UpgradeVariantInformation GetSelectedUpgradeVariant()
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public static NextUpgradeInformation Construct(
            IEnumerable<UpgradeVariantInformation> upgradeVariants
        )
        {
            // TODO: implement
            throw new NotImplementedException();
        }
    }

    public struct UpgradeVariantInformation
    {
        // TODO: implement
    }

    public interface IUpgradablePiece
    {
        public uint CurrentLevel { get; }
        public uint MaxLevel { get; }
        public bool IsReachedMaxLevel => CurrentLevel >= MaxLevel;
        public void ApplyUpgrade(TankImpl tank, UpgradeVariantInformation upgradeVariant);
        public NextUpgradeInformation GetNextUpgradeInformation();
    }
}

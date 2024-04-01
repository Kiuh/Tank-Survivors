using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.UpgradablePiece;
using Tank.Weapons.ModulesUpgrades;

namespace Tank.Weapons
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class LeveledWeaponUpgrade : ILeveledUpgrade
    {
        [FoldoutGroup("$UpgradingLevel")]
        [OdinSerialize]
        public uint UpgradingLevel { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [MultiLineProperty]
        [OdinSerialize]
        public string Description { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [NonSerialized, OdinSerialize]
        [PropertyOrder(1)]
        private List<IModuleUpgrade> moduleUpgrades = new();

        public void ApplyUpgrade(TankImpl tank)
        {
            if (moduleUpgrades == null)
            {
                return;
            }
            foreach (IModuleUpgrade upgrade in moduleUpgrades)
            {
                upgrade.ApplyUpgrade(tank.Weapons.First(x => x.Upgrades.Contains(this)));
            }
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    public class LevelUpWeaponUpgrade : ILevelUpUpgrade
    {
        [FoldoutGroup("$LevelForUpgrade")]
        [OdinSerialize]
        public uint LevelForUpgrade { get; private set; }

        [FoldoutGroup("$LevelForUpgrade")]
        [MultiLineProperty]
        [OdinSerialize]
        public string Description { get; private set; }

        [FoldoutGroup("$LevelForUpgrade")]
        [NonSerialized, OdinSerialize]
        [PropertyOrder(1)]
        private List<IModuleUpgrade> moduleUpgrades = new();

        public void ApplyUpgrade(TankImpl tank)
        {
            if (moduleUpgrades == null)
            {
                return;
            }
            foreach (IModuleUpgrade upgrade in moduleUpgrades)
            {
                upgrade.ApplyUpgrade(tank.Weapons.First(x => x.LevelUpUpgrades.Contains(this)));
            }
        }
    }
}

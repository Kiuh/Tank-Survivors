using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;

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
}

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Tank.UpgradablePiece;

namespace Tank.Upgrades
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class LeveledTankUpgrade : ILeveledUpgrade
    {
        [FoldoutGroup("$UpgradingLevel")]
        [MinValue(0)]
        [OdinSerialize]
        public uint UpgradingLevel { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [MultiLineProperty]
        [OdinSerialize]
        public string Description { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [NonSerialized, OdinSerialize]
        [PropertyOrder(1)]
        private List<IPropertyUpgrade> propertyUpgrades;

        public void ApplyUpgrade(TankImpl tank)
        {
            if (propertyUpgrades == null)
            {
                return;
            }
            foreach (IPropertyUpgrade upgrade in propertyUpgrades)
            {
                upgrade.ApplyUpgrade(tank);
            }
        }
    }
}

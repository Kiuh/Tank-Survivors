﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.UpgradablePiece;

namespace Tank.Upgrades
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class LeveledTankUpgrade : ILeveledUpgrade
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
        private List<IPropertyUpgrade> propertyUpgrades = new();

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

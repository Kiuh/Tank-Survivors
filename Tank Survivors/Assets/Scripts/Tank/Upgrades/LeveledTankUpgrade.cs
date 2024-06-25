using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tank.UpgradablePiece;
using UnityEngine;

namespace Tank.Upgrades
{
    [Serializable]
    public class LeveledTankUpgrade : ILeveledUpgrade
    {
        [FoldoutGroup("$UpgradingLevel")]
        [SerializeField]
        private uint upgradingLevel;
        public uint UpgradingLevel
        {
            get => upgradingLevel;
            private set => upgradingLevel = value;
        }

        [FoldoutGroup("$UpgradingLevel")]
        [MultiLineProperty]
        [SerializeField]
        private string description;
        public string Description
        {
            get => description;
            private set => description = value;
        }

        [FoldoutGroup("$UpgradingLevel")]
        [SerializeReference]
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

using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Tank.UpgradablePiece;
using Tank.Weapons.ModulesUpgrades;
using UnityEngine;

namespace Tank.Weapons
{
    [Serializable]
    public class LeveledWeaponUpgrade : ILeveledUpgrade
    {
        [FoldoutGroup("$UpgradingLevel")]
        [SerializeField]
        private uint upgradingLevel;
        public uint UpgradingLevel => upgradingLevel;

        [FoldoutGroup("$UpgradingLevel")]
        [MultiLineProperty]
        [SerializeField]
        private string description;
        public string Description => description;

        [FoldoutGroup("$UpgradingLevel")]
        [SerializeReference]
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
    public class LevelUpWeaponUpgrade : ILevelUpUpgrade
    {
        [FoldoutGroup("$LevelForUpgrade")]
        [SerializeField]
        private uint levelForUpgrade;
        public uint LevelForUpgrade => levelForUpgrade;

        [FoldoutGroup("$LevelForUpgrade")]
        [MultiLineProperty]
        [SerializeField]
        private string description;
        public string Description => description;

        [FoldoutGroup("$LevelForUpgrade")]
        [SerializeReference]
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

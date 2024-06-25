using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tank.UpgradablePiece;
using UnityEngine;

namespace Tank.Upgrades
{
    [Serializable]
    public class TankUpgrade : IUpgradablePiece
    {
        [FoldoutGroup("$UpgradeName")]
        [SerializeField]
        private string upgradeName;
        public string UpgradeName
        {
            get => upgradeName;
            private set => upgradeName = value;
        }

        [FoldoutGroup("$UpgradeName")]
        [HorizontalGroup("$UpgradeName/Top")]
        [MinValue(0)]
        [SerializeField]
        private uint currentLevel;
        public uint CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        [FoldoutGroup("$UpgradeName")]
        [HorizontalGroup("$UpgradeName/Top")]
        [MinValue(0, Expression = "@this.CurrentLevel")]
        [SerializeField]
        private uint maxLevel;
        public uint MaxLevel
        {
            get => maxLevel;
            private set => maxLevel = value;
        }

        [FoldoutGroup("$UpgradeName")]
        [SerializeField]
        [LabelText("List of Upgrades in levels")]
        [PropertyOrder(1)]
        private List<LeveledTankUpgrade> upgradeList = new();

        public IEnumerable<ILeveledUpgrade> Upgrades => upgradeList;

        public void Initialize()
        {
            CurrentLevel = 0;
        }
    }
}

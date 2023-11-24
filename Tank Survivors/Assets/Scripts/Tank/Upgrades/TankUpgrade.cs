using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Tank.UpgradablePiece;

namespace Tank.Upgrades
{
    [HideReferenceObjectPicker]
    public class TankUpgrade : IUpgradablePiece
    {
        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        public string UpgradeName { get; private set; }

        [FoldoutGroup("$UpgradeName")]
        [HorizontalGroup("$UpgradeName/Top")]
        [MinValue(0)]
        [OdinSerialize]
        public uint CurrentLevel { get; set; }

        [FoldoutGroup("$UpgradeName")]
        [HorizontalGroup("$UpgradeName/Top")]
        [MinValue(0, Expression = "@this.CurrentLevel")]
        [OdinSerialize]
        public uint MaxLevel { get; private set; }

        [FoldoutGroup("$UpgradeName")]
        [NonSerialized, OdinSerialize]
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

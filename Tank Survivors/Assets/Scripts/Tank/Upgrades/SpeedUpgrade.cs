using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;
using UnityEngine;

namespace Tank.Upgrades
{
    [InterfaceEditor]
    public interface ILeveledSpeedUpgrade : ILeveledUpgrade { }

    [Serializable]
    public class SpeedUpgrade : ITankUpgrade
    {
        [SerializeField]
        [InspectorReadOnly]
        private uint currentLevel = 0;
        public uint CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        [SerializeField]
        private uint maxLevel;
        public uint MaxLevel => maxLevel;

        [SerializeField]
        private string upgradeName;
        public string UpgradeName => upgradeName;

        [SerializeField]
        private List<SerializedLeveledSpeedUpgrade> upgradeList;
        public IEnumerable<ILeveledUpgrade> Upgrades =>
            upgradeList.Select(x => x.ToLeveledSpeedUpgrade());
    }

    [Serializable]
    public class SimpleAddingLeveledSpeedUpgrade : ILeveledSpeedUpgrade
    {
        [SerializeField]
        private uint level = 0;

        [SerializeField]
        private float addingValue;
        public uint UpgradingLevel => level;

        [SerializeField]
        private string description;
        public string Description => description;

        private string guid = System.Guid.NewGuid().ToString();
        public string Guid => guid;

        public void ApplyUpgrade(TankImpl tank)
        {
            tank.Speed.Modifications.Add(
                new ValueModification<float>((x) => x + addingValue, ModificationPriority.Medium)
            );
        }
    }
}

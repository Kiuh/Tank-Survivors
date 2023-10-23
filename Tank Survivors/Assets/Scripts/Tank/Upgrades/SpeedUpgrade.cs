using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tank.Upgrades
{
    [InterfaceEditor]
    public interface ILeveledSpeedUpgrade
    {
        public uint UpgradingLevel { get; }
        public void ApplyUpgrade(TankImpl tank);
        public PresentationInformation GetPresentationInformation();
    }

    [Serializable]
    public class SpeedUpgrade : ITankUpgrade
    {
        private uint currentLevel = 0;

        [SerializeField]
        private uint maxLevel;

        [SerializeField]
        private List<SerializedLeveledSpeedUpgrade> upgradeList;
        public IEnumerable<ILeveledSpeedUpgrade> Upgrades =>
            upgradeList.Select(x => x.ToLeveledSpeedUpgrade());

        public uint CurrentLevel => currentLevel;
        public bool IsReachedMaxLevel => currentLevel >= maxLevel;

        public void ApplyUpgrade(TankImpl tank)
        {
            Upgrades.First(x => x.UpgradingLevel == currentLevel).ApplyUpgrade(tank);
            currentLevel++;
        }

        public PresentationInformation GetPresentationInformation()
        {
            return Upgrades
                .First(x => x.UpgradingLevel == currentLevel)
                .GetPresentationInformation();
        }
    }

    [Serializable]
    public class SimpleAddingLeveledSpeedUpgrade : ILeveledSpeedUpgrade
    {
        [SerializeField]
        private uint level = 0;

        [SerializeField]
        private float addingValue;
        public uint UpgradingLevel => level;

        public void ApplyUpgrade(TankImpl tank)
        {
            tank.Speed.Modifications.Add(
                new ValueModification<float>((x) => x + addingValue, ModificationPriority.Medium)
            );
        }

        public PresentationInformation GetPresentationInformation()
        {
            throw new NotImplementedException();
        }
    }
}

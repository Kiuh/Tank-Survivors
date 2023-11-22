using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;
using UnityEngine;

namespace Tank.Upgrades
{
    [Serializable]
    public class TankUpgrade : ITankUpgrade
    {
        [SerializeField]
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
        private List<SerializedLeveledTankUpgrade> upgradeList;
        public IEnumerable<ILeveledUpgrade> Upgrades =>
            upgradeList.Select(x => x.ToLeveledTankUpgrade());

        public void Initialize()
        {
            currentLevel = 0;
        }
    }

    [InterfaceEditor]
    public interface ILeveledTankUpgrade : ILeveledUpgrade { }

    [Serializable]
    public class LeveledTankUpgrade : ILeveledTankUpgrade
    {
        [SerializeField]
        private uint level = 0;

        [SerializeField]
        private float addingValue;
        public uint UpgradingLevel => level;

        [SerializeField]
        private string description;
        public string Description => description;

        [SerializeField]
        private List<SerializedPropertyUpgrade> upgradeList;

        public void ApplyUpgrade(TankImpl tank)
        {
            foreach (IPropertyUpgrade upgrade in upgradeList.Select(x => x.ToPropertyUpgrade()))
            {
                upgrade.ApplyUpgrade(tank);
            }
        }
    }

    [InterfaceEditor]
    public interface IPropertyUpgrade
    {
        public void ApplyUpgrade(TankImpl tank);
    }

    [Serializable]
    public class SpeedUpgrade : IPropertyUpgrade
    {
        [SerializeField]
        private MathOperation mathOperation = MathOperation.Plus;

        [SerializeField]
        private float operationValue;

        [SerializeField]
        private ModificationPriority modificationPriority = ModificationPriority.Medium;

        public void ApplyUpgrade(TankImpl tank)
        {
            tank.Speed.Modifications.Add(
                new ValueModification<float>(
                    mathOperation.ToFunction(operationValue),
                    modificationPriority
                )
            );
        }
    }
}

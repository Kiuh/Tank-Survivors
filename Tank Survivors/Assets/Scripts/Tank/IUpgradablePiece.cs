using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tank.UpgradablePiece
{
    public interface IUpgradablePiece
    {
        public string UpgradeName { get; }
        public uint CurrentLevel { get; set; }
        public uint MaxLevel { get; }
        public bool IsReachedMaxLevel => CurrentLevel >= MaxLevel;
        public IEnumerable<ILeveledUpgrade> Upgrades { get; }
        public void ApplyUpgrade(TankImpl tank, ILeveledUpgrade leveledUpgrade)
        {
            if (!Upgrades.Contains(leveledUpgrade))
            {
                Debug.LogError("Given ILeveledUpgrade not contains in IUpgradablePiece");
            }
            leveledUpgrade.ApplyUpgrade(tank);
            CurrentLevel++;
        }
        public IEnumerable<ILeveledUpgrade> GetNextUpgrades()
        {
            return Upgrades.Where(x => x.UpgradingLevel == CurrentLevel);
        }

        public void Initialize();
    }

    public interface ILeveledUpgrade
    {
        public string Description { get; }
        public uint UpgradingLevel { get; }
        public void ApplyUpgrade(TankImpl tank);
    }
}

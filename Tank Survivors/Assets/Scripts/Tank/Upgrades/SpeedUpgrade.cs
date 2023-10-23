using Common;
using System;
using UnityEngine;

namespace Tank.Upgrades
{
    public class SpeedUpgrade : ITankUpgrade
    {
        [SerializeField]
        [InspectorReadOnly]
        private uint currentLevel = 0;

        [SerializeField]
        private uint maxLevel;
        public uint CurrentLevel => currentLevel;

        public bool IsReachedMaxLevel => currentLevel >= maxLevel;

        public void ApplyUpgrade(TankImpl tank)
        {
            throw new NotImplementedException();
        }

        public PresentationInformation GetPresentationInformation()
        {
            throw new NotImplementedException();
        }
    }
}

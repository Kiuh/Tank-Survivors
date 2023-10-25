using Common;
using System;
using UnityEngine;

namespace DataStructs
{
    [Serializable]
    public class PlayerLevel
    {
        [SerializeField]
        [InspectorReadOnly]
        private uint currentLevel = 0;
        public uint CurrentLevel => currentLevel;

        [SerializeField]
        [InspectorReadOnly]
        private uint experienceCount;
        public uint ExperienceCount => experienceCount;

        [SerializeField]
        private uint maxExperienceCount;
        public uint MaxExperienceCount
        {
            get => maxExperienceCount;
            set => maxExperienceCount = Math.Max(value, maxExperienceCount);
        }

        public event Action<float> OnExperienceChange;
        public event Action OnLevelUp;

        public void AddExperience(uint value)
        {
            experienceCount += value;
            if (experienceCount >= maxExperienceCount)
            {
                experienceCount %= maxExperienceCount;
                currentLevel++;
                OnLevelUp?.Invoke();
            }

            OnExperienceChange?.Invoke((float)experienceCount / maxExperienceCount);
        }
    }
}

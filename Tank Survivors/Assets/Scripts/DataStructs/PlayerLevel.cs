using Common;
using General.Configs;
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
        private float experienceCount;
        public float ExperienceCount => experienceCount;

        [SerializeField]
        [InspectorReadOnly]
        private ModifiableValue<float> maxExperienceCount;
        public float MaxExperienceCount => maxExperienceCount.SourceValue;

        public event Action OnExperienceChange;
        public event Action OnLevelUp;

        public PlayerLevel(LevelProgressionConfig progressionConfig)
        {
            maxExperienceCount = new ModifiableValue<float>(progressionConfig.FirstLevelExperience);
            maxExperienceCount.Modifications.Add(
                new ValueModification<float>(
                    (x) => x * progressionConfig.ExperienceUpValue,
                    ModificationPriority.Medium));
            AddExperience(progressionConfig.DefaultExperience);
        }

        public void AddExperience(float value)
        {
            experienceCount += value;
            while (experienceCount >= maxExperienceCount.SourceValue)
            {
                experienceCount -= maxExperienceCount.SourceValue;
                maxExperienceCount.SourceValue = maxExperienceCount.GetModifiedValue();
                currentLevel++;
                OnLevelUp?.Invoke();
            }

            OnExperienceChange?.Invoke();
        }
    }
}

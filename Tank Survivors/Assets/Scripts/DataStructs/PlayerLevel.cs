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

        private LevelProgressionConfig progressionConfig;

        public event Action OnLevelUp;

        public PlayerLevel(LevelProgressionConfig progressionConfig)
        {
            this.progressionConfig = progressionConfig;
            maxExperienceCount = new ModifiableValue<float>(progressionConfig.FirstLevelExperience);
            AddModification();
            AddExperience(progressionConfig.DefaultExperience);
        }

        public void AddExperience(float value)
        {
            experienceCount += value;

            float maxExperience = maxExperienceCount.GetModifiedValue();
            while (experienceCount >= maxExperience)
            {
                experienceCount -= maxExperience;
                AddModification();
                currentLevel++;
                OnLevelUp?.Invoke();
            }
        }

        private void AddModification()
        {
            maxExperienceCount.Modifications.Add(
                new ValueModification<float>(
                    (x) => x * progressionConfig.ExperienceUpValue,
                    ModificationPriority.Medium));
        }
    }
}

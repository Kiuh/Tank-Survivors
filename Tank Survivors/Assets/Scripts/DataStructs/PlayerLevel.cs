using Common;
using Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;

namespace DataStructs
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class PlayerLevel
    {
        public PlayerLevel() { }

        [SerializeField]
        [ReadOnly]
        private uint currentLevel = 0;
        public uint CurrentLevel => currentLevel;

        [SerializeField]
        [ReadOnly]
        private float experienceCount;
        public float ExperienceCount => experienceCount;

        [ReadOnly]
        [OdinSerialize]
        private ModifiableValue<float> maxExperienceCount = new();
        public float MaxExperienceCount => maxExperienceCount.GetModifiedValue();

        private LevelProgression progressionConfig;

        public event Action OnLevelUp;

        public void Initialize(LevelProgression progressionConfig)
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
                    ModificationPriority.Medium
                )
            );
        }
    }
}

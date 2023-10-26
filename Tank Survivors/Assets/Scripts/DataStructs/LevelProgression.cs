﻿using UnityEngine;

namespace DataStructs
{
    [System.Serializable]
    public struct LevelProgression
    {
        [SerializeField]
        private float firstLevelExperience;
        public float FirstLevelExperience => firstLevelExperience;

        [SerializeField]
        [Range(1f, 10f)]
        private float experienceUpValue;
        public float ExperienceUpValue => experienceUpValue;

        [SerializeField]
        private float defaultExperience;
        public float DefaultExperience => defaultExperience;
    }
}
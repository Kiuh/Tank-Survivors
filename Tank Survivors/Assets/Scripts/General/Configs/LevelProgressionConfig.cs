using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        fileName = "LevelProgressionConfig",
        menuName = "Configs/LevelProgressionConfig",
        order = 5
    )]
    public class LevelProgressionConfig : ScriptableObject
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
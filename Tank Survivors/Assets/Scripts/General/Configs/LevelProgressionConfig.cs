using DataStructs;
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
        private LevelProgression levelProgression;
        public LevelProgression LevelProgression => levelProgression;
    }
}
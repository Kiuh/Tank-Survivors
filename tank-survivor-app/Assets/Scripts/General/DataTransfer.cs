using Configs;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "DataTransfer", menuName = "DataTransfer")]
    public class DataTransfer : ScriptableObject
    {
        [SerializeField]
        private LevelInfo levelInfo;

        public LevelInfo LevelInfo
        {
            get => levelInfo;
            set => levelInfo = value;
        }
    }
}

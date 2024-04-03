using Configs;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "DataTransfer", menuName = "DataTransfer")]
    public class DataTransfer : ScriptableObject
    {
        public LevelInfo LevelInfo { get; set; }
    }
}

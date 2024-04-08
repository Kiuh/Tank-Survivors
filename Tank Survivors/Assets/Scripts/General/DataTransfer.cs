using Configs;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "DataTransfer", menuName = "DataTransfer")]
    public class DataTransfer : ScriptableObject
    {
        [field: SerializeField]
        public LevelInfo LevelInfo { get; set; }
    }
}

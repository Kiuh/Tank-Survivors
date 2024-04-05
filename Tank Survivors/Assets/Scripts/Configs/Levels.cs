using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class Levels : SerializedScriptableObject
    {
        public List<LevelInfo> LevelInfos = new List<LevelInfo>();
    }

    [Serializable]
    public class LevelInfo
    {
        public string Name;

        [Range(0f, 3f)]
        [ReadOnly]
        public int Progress;

        [OdinSerialize]
        public Enemies Enemies;
    }
}

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class Levels : ScriptableObject
    {
        public List<LevelInfo> LevelInfos = new();

        [Button]
        private void ResetConfig()
        {
            foreach (LevelInfo level in LevelInfos)
            {
                level.Progress = 0;
            }
        }
    }

    [Serializable]
    public class LevelInfo
    {
        public string Name;

        [Range(0f, 3f)]
        [ReadOnly]
        public int Progress;

        public Enemies Enemies;
    }
}

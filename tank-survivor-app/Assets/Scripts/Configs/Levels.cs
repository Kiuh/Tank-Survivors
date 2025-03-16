using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class Levels : ScriptableObject
    {
        private static Levels cashedInstance = null;
        public static Levels Data
        {
            get
            {
                if (cashedInstance == null)
                {
                    cashedInstance = Resources.Load<Levels>("LevelsConfig");
                }
                return cashedInstance;
            }
        }

        public List<LevelInfo> LevelInfos = new();

        [Button]
        public void ResetConfig()
        {
            foreach (LevelInfo level in LevelInfos)
            {
                level.Progress = 0;
                PlayerPrefs.SetInt(level.Name, level.Progress);
            }
            PlayerPrefs.Save();
        }
    }

    [Serializable]
    public class LevelInfo
    {
        public string Name;

        [Range(0f, 3f)]
        public int Progress;

        public Enemies Enemies;
    }
}

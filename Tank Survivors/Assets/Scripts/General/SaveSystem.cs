using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
    public static void SaveData(Levels levels)
    {
        List<LevelData> oldData = YandexGame.savesData.LevelsData;
        List<LevelData> newData = new();
        foreach (LevelInfo levelInfo in levels.LevelInfos)
        {
            LevelData oldLevelInfo = oldData.FirstOrDefault(x => x.LevelName == levelInfo.Name);
            string levelName = levelInfo.Name;
            int stars;
            if (oldLevelInfo != null)
            {
                stars = Math.Max(oldLevelInfo.Stars, levelInfo.Progress);
            }
            else
            {
                stars = levelInfo.Progress;
            }
            newData.Add(new LevelData() { LevelName = levelName, Stars = stars });
        }
        YandexGame.savesData.LevelsData = newData;
        YandexGame.SaveProgress();
    }

    public static void LoadData(Levels levels)
    {
        YandexGame.LoadProgress();
        List<LevelData> levelsData = YandexGame.savesData.LevelsData;

        foreach (LevelInfo level in levels.LevelInfos)
        {
            LevelData levelData = levelsData.FirstOrDefault(x => x.LevelName == level.Name);
            if (levelData != null)
            {
                level.Progress = levelData.Stars;
            }
        }
    }
}

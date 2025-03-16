using System;
using System.Collections.Generic;
using Configs;
using PlatformSpecific;
using UnityEngine;
#if YG_PLUGIN_YANDEX_GAME
using YG;
using Newtonsoft.Json;
using System.Linq;
#endif

[Serializable]
public class UpdateLevelData
{
    public string Level;
    public int NewScore;
}

[Serializable]
public class LevelPack
{
    public string LevelName;
    public int Progress;
}

public static class SaveSystem
{
    public static bool IsSaveSystemAvailable =>
#if YG_PLUGIN_YANDEX_GAME
        BuildSettings.Data.IsYandexGameBuild ? YandexGame.SDKEnabled : true;
#else
        true;
#endif

    public static void UpdateLevelsData(UpdateLevelData levelUpdate)
    {
        LevelInfo oldLevel = Levels.Data.LevelInfos.Find(x => x.Name == levelUpdate.Level);
        oldLevel.Progress = Math.Max(oldLevel.Progress, levelUpdate.NewScore);
        if (BuildSettings.Data.IsYandexGameBuild)
        {
#if YG_PLUGIN_YANDEX_GAME
            List<LevelPack> toSerialize = Levels
                .Data.LevelInfos.Select(x => new LevelPack()
                {
                    LevelName = x.Name,
                    Progress = x.Progress
                })
                .ToList();
            YandexGame.savesData.LevelsDataString = JsonConvert.SerializeObject(toSerialize);
            YandexGame.SaveProgress();
#endif
        }
        else
        {
            PlayerPrefs.SetInt(oldLevel.Name, oldLevel.Progress);
        }
    }

    public static List<LevelInfo> GetLevelsData()
    {
        if (BuildSettings.Data.IsYandexGameBuild)
        {
#if YG_PLUGIN_YANDEX_GAME
            YandexGame.LoadProgress();
            List<LevelPack> levelsData = JsonConvert.DeserializeObject<List<LevelPack>>(
                YandexGame.savesData.LevelsDataString
            );

            if (levelsData == null)
            {
                return Levels.Data.LevelInfos;
            }

            foreach (LevelInfo level in Levels.Data.LevelInfos)
            {
                LevelPack levelData = levelsData.FirstOrDefault(x => x.LevelName == level.Name);
                if (levelData != null)
                {
                    level.Progress = levelData.Progress;
                }
            }
#endif
        }
        else
        {
            foreach (LevelInfo item in Levels.Data.LevelInfos)
            {
                item.Progress = PlayerPrefs.GetInt(item.Name, 0);
            }
        }

        return Levels.Data.LevelInfos;
    }

    private const string music_label = "MusicValue";
    private const string sounds_label = "SoundsValue";

    public static bool GetMusicValue()
    {
        if (BuildSettings.Data.IsYandexGameBuild)
        {
#if YG_PLUGIN_YANDEX_GAME
            return YandexGame.savesData.IsMusicOn;
#else
            return false;
#endif
        }
        else
        {
            return PlayerPrefs.GetInt(music_label, 1) != 0;
        }
    }

    public static void SetMusicValue(bool value)
    {
        if (BuildSettings.Data.IsYandexGameBuild)
        {
#if YG_PLUGIN_YANDEX_GAME
            YandexGame.savesData.IsMusicOn = value;
            YandexGame.SaveProgress();
#endif
        }
        else
        {
            PlayerPrefs.SetInt(music_label, value ? 1 : 0);
        }
    }

    public static bool GetSoundsValue()
    {
        if (BuildSettings.Data.IsYandexGameBuild)
        {
#if YG_PLUGIN_YANDEX_GAME
            return YandexGame.savesData.IsSoundsOn;
#else
            return false;
#endif
        }
        else
        {
            return PlayerPrefs.GetInt(sounds_label, 1) != 0;
        }
    }

    public static void SetSoundsValue(bool value)
    {
        if (BuildSettings.Data.IsYandexGameBuild)
        {
#if YG_PLUGIN_YANDEX_GAME
            YandexGame.savesData.IsSoundsOn = value;
            YandexGame.SaveProgress();
#endif
        }
        else
        {
            PlayerPrefs.SetInt(sounds_label, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
    public static void SaveData(Levels levels)
    {
        List<int> progresses = GetProgresses(levels.LevelInfos);

        YandexGame.savesData.LevelProgresses = progresses;
        YandexGame.SaveProgress();
    }

    public static void LoadData(Levels levels)
    {
        YandexGame.LoadProgress();
        List<int> progresses = YandexGame.savesData.LevelProgresses;
        //if (progresses.Count != levels) { }

        for (int i = 0; i < progresses.Count; i++)
        {
            levels.LevelInfos[i].Progress = progresses[i];
        }
    }

    private static List<int> GetProgresses(List<LevelInfo> levelInfos)
    {
        return levelInfos.Select(x => x.Progress).ToList();
    }
}

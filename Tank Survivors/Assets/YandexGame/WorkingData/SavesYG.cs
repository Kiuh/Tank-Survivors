using System;
using System.Collections.Generic;

namespace YG
{
    [Serializable]
    public class LevelData
    {
        public string LevelName;
        public int Stars;
    }

    [Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public List<LevelData> LevelsData = new();
        public bool IsSoundsOn = true;
        public bool IsMusicOn = true;

        public SavesYG() { }
    }
}

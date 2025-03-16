using System;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public string LevelsDataString = "";
        public bool IsSoundsOn = true;
        public bool IsMusicOn = true;

        public SavesYG() { }
    }
}

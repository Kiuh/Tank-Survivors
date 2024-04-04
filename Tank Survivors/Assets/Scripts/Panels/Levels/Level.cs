using System;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.Level")]
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelName;

        [SerializeField]
        private StarsContainer starsContainer;

        [SerializeField]
        private Button button;

        public void SetupLevelButton(LevelInfo levelInfo, Action action)
        {
            levelName.text = levelInfo.Name;
            starsContainer.SetupProgress(levelInfo.Progress);
            button.onClick.AddListener(() => action());
        }
    }
}

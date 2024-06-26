using System;
using Configs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.Level")]
    public class Level : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI levelName;

        [Required]
        [SerializeField]
        private StarsContainer starsContainer;

        [Required]
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

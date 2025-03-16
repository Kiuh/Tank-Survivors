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

        [Required]
        [SerializeField]
        private GameObject lockImage;

        public void SetupLevelButton(LevelInfo levelInfo, bool isLocked, Action action)
        {
            levelName.text = levelInfo.Name;
            lockImage.SetActive(isLocked);
            button.interactable = !isLocked;
            starsContainer.SetupProgress(levelInfo.Progress);
            button.onClick.AddListener(() => action());
        }
    }
}

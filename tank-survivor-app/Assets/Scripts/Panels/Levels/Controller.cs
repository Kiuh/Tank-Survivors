using System.Collections.Generic;
using Configs;
using General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.Controller")]
    public class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private View view;

        [Required]
        [SerializeField]
        private Level levelTemplate;

        [Required]
        [SerializeField]
        private Configs.Levels levels;

        [Required]
        [SerializeField]
        private DataTransfer dataTransfer;

        public void ShowPanel()
        {
            view.Show();
        }

        public void SetData(LevelInfo levelInfo)
        {
            dataTransfer.LevelInfo = levelInfo;
        }

        public void CreateLevels(RectTransform levelContainer)
        {
            List<LevelInfo> levels = SaveSystem.GetLevelsData();
            LevelInfo previousLevel = null;
            bool isLocked = false;
            foreach (LevelInfo level in levels)
            {
                Level levelButton = Instantiate(levelTemplate, levelContainer);
                if (previousLevel == null)
                {
                    isLocked = false;
                }
                else
                {
                    isLocked = previousLevel.Progress == 0;
                }

                levelButton.SetupLevelButton(
                    level,
                    isLocked,
                    () =>
                    {
                        SetData(level);
                        ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
                    }
                );
                previousLevel = level;
            }
        }
    }
}

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
            SaveSystem.LoadData(levels);
            foreach (LevelInfo level in levels.LevelInfos)
            {
                Level levelButton = Instantiate(levelTemplate, levelContainer);

                levelButton.SetupLevelButton(
                    level,
                    () =>
                    {
                        SetData(level);
                        ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
                    }
                );
            }
        }
    }
}

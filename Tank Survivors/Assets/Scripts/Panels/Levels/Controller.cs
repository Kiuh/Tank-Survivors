using Configs;
using General;
using UnityEngine;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.Controller")]
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private View view;

        [SerializeField]
        private Level levelTemplate;

        [SerializeField]
        private Configs.Levels levels;

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
            foreach (LevelInfo level in levels.LevelsInfo)
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

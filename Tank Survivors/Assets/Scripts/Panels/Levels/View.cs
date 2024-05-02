using General;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.View")]
    public class View : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;

        [SerializeField]
        private GameObject levelsPanel;

        [SerializeField]
        private RectTransform levelsContainer;

        [SerializeField]
        private Button exitButton;

        private void Awake()
        {
            exitButton.onClick.AddListener(Exit);
            controller.CreateLevels(levelsContainer);
        }

        private void Exit()
        {
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }

        public void Show()
        {
            levelsPanel.SetActive(true);
        }

        public void Hide()
        {
            levelsPanel.SetActive(false);
        }
    }
}

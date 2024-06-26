using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.View")]
    public class View : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Controller controller;

        [Required]
        [SerializeField]
        private GameObject levelsPanel;

        [Required]
        [SerializeField]
        private RectTransform levelsContainer;

        [Required]
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

using General;
using UnityEngine;
using UnityEngine.UI;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.Pause")]
    internal class Pause : MonoBehaviour
    {
        [SerializeField]
        private GlobalInput globalInput;

        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button repeatButton;

        [SerializeField]
        private Button leaveButton;

        [SerializeField]
        private GameObject pausePanel;

        private void Awake()
        {
            resumeButton.onClick.AddListener(globalInput.UnSetPause);
            repeatButton.onClick.AddListener(
                () => ScenesController.Instance.LoadScene(InGameScene.GameplayScene)
            );
            leaveButton.onClick.AddListener(
                () => ScenesController.Instance.LoadScene(InGameScene.MainScene)
            );
        }

        public void ShowPausePanel()
        {
            pausePanel.SetActive(true);
        }

        public void HidePausePanel()
        {
            pausePanel.SetActive(false);
        }
    }
}

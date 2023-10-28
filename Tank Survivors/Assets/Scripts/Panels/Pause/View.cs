using UnityEngine;
using UnityEngine.UI;

namespace Panels.Pause
{
    [AddComponentMenu("Panels.Pause.View")]
    public class View : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;

        [SerializeField]
        private GameObject pausePanel;

        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button repeatButton;

        [SerializeField]
        private Button leaveButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(ResumeButtonClick);
            repeatButton.onClick.AddListener(RepeatButtonClick);
            leaveButton.onClick.AddListener(LeaveButtonClick);
        }

        private void ResumeButtonClick()
        {
            controller.Resume();
        }

        private void RepeatButtonClick()
        {
            controller.Repeat();
        }

        private void LeaveButtonClick()
        {
            controller.Leave();
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

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Pause
{
    [AddComponentMenu("Panels.Pause.View")]
    public class View : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Controller controller;

        [Required]
        [SerializeField]
        private GameObject pausePanel;

        [Required]
        [SerializeField]
        private Button resumeButton;

        [Required]
        [SerializeField]
        private Button repeatButton;

        [Required]
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

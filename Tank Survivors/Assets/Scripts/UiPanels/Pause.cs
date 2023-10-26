using General;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        [SerializeField]
        private string repeatSceneName;

        [SerializeField]
        private string leaveSceneName;

        private void Awake()
        {
            resumeButton.onClick.AddListener(globalInput.UnSetPause);
            repeatButton.onClick.AddListener(() => SceneManager.LoadScene(repeatSceneName));
            leaveButton.onClick.AddListener(() => SceneManager.LoadScene(leaveSceneName));
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Win
{
    [AddComponentMenu("Panels.Win.View")]
    public class View : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;

        [SerializeField]
        private StarsContainer starsContainer;

        [SerializeField]
        private GameObject winPanel;

        [SerializeField]
        private TMP_Text infoLabel;

        [SerializeField]
        private Button leaveButton;

        private void Awake()
        {
            leaveButton.onClick.AddListener(LeaveButtonClick);
        }

        private void LeaveButtonClick()
        {
            controller.LeaveGame();
        }

        public void ShowWinPanel(string infoString, int starsCount)
        {
            infoLabel.text = infoString;
            starsContainer.SetupProgress(starsCount);
            winPanel.SetActive(true);
        }

        public void HideLosePanel()
        {
            winPanel.SetActive(false);
        }
    }
}

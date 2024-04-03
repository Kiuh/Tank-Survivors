using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Death
{
    [AddComponentMenu("Panels.Death.View")]
    public class View : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;

        [SerializeField]
        private GameObject losePanel;

        [SerializeField]
        private TMP_Text infoLabel;

        [SerializeField]
        private Button secondLifeButton;

        [SerializeField]
        private Button repeatButton;

        [SerializeField]
        private Button leaveButton;

        private void Awake()
        {
            secondLifeButton.onClick.AddListener(UseSecondLifeBonusClick);
            repeatButton.onClick.AddListener(RepeatButtonClick);
            leaveButton.onClick.AddListener(LeaveButtonClick);
        }

        private void UseSecondLifeBonusClick()
        {
            controller.UseSecondLifeBonus();
        }

        private void RepeatButtonClick()
        {
            controller.RepeatGame();
        }

        private void LeaveButtonClick()
        {
            controller.LeaveGame();
        }

        public void ShowLosePanel(string infoString)
        {
            infoLabel.text = infoString;
            losePanel.SetActive(true);
        }

        public void HideLosePanel()
        {
            losePanel.SetActive(false);
        }
    }
}

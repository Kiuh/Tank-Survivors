using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Death
{
    [AddComponentMenu("Panels.Death.View")]
    public class View : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Controller controller;

        [Required]
        [SerializeField]
        private GameObject losePanel;

        [Required]
        [SerializeField]
        private StarsContainer starsContainer;

        [Required]
        [SerializeField]
        private TMP_Text infoLabel;

        [Required]
        [SerializeField]
        private Button secondLifeButton;

        [Required]
        [SerializeField]
        private Button repeatButton;

        [Required]
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

        public void HideSecondLifeButton()
        {
            secondLifeButton.gameObject.SetActive(false);
        }

        public void ShowLosePanel(string infoString, int starsCount)
        {
            infoLabel.text = infoString;
            starsContainer.SetupProgress(starsCount);
            losePanel.SetActive(true);
        }

        public void HideLosePanel()
        {
            losePanel.SetActive(false);
        }
    }
}

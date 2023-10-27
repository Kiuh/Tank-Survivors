using General;
using System.Text;
using Tank;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.Death")]
    public class Death : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private Timer timer;

        [SerializeField]
        private GameObject losePanel;

        [SerializeField]
        private TMP_Text infoLabel;

        [SerializeField]
        private Button repeatButton;

        [SerializeField]
        private Button leaveButton;

        private void Awake()
        {
            tank.OnDeath += ShowLosePanel;
            repeatButton.onClick.AddListener(
                () => ScenesController.Instance.LoadScene(InGameScene.GameplayScene)
            );
            leaveButton.onClick.AddListener(
                () => ScenesController.Instance.LoadScene(InGameScene.MainScene)
            );
        }

        private void ShowLosePanel()
        {
            infoLabel.text = GetInfoString();
            losePanel.SetActive(true);
        }

        private string GetInfoString()
        {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine($"Time: {timer.CurrentTime:0.0}");
            return stringBuilder.ToString();
        }
    }
}

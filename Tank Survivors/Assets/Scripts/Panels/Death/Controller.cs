using General;
using System.Text;
using Tank;
using UnityEngine;

namespace Panels.Death
{
    [AddComponentMenu("Panels.Death.Controller")]
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private Timer timer;

        [SerializeField]
        private View view;

        private void Awake()
        {
            tank.OnDeath += ShowLosePanel;
        }

        private void ShowLosePanel()
        {
            view.ShowLosePanel(GetInfoString());
        }

        public void RepeatGame()
        {
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }

        public void LeaveGame()
        {
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }

        private string GetInfoString()
        {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine($"Time: {timer.CurrentTime:0.0}");
            return stringBuilder.ToString();
        }
    }
}

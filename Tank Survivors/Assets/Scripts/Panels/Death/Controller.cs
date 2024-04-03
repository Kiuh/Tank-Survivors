using System.Text;
using General;
using Tank;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using YG;

namespace Panels.Death
{
    [AddComponentMenu("Panels.Death.Controller")]
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private General.Timer timer;

        [SerializeField]
        private View view;

        private void Awake()
        {
            tank.OnDeath += ShowLosePanel;
        }

        private void ShowLosePanel()
        {
            Time.timeScale = 0.0f;
            view.ShowLosePanel(GetInfoString());
        }

        public void UseSecondLifeBonus()
        {
            int idOfSecondLife = 0;
            YandexGame.RewVideoShow(idOfSecondLife);

            Time.timeScale = 1.0f;
            view.HideLosePanel();
        }

        public void RepeatGame()
        {
            Time.timeScale = 1.0f;
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }

        public void LeaveGame()
        {
            Time.timeScale = 1.0f;
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

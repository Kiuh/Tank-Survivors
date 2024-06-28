using System.Text;
using General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panels.Win
{
    [AddComponentMenu("Panels.Win.Controller")]
    public class Controller : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private ProgressController progressController;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private General.Timer timer;

        [Required]
        [SerializeField]
        private View view;

        private void Awake()
        {
            progressController.OnWin += Show;
        }

        private void Show()
        {
            Time.timeScale = 0.0f;
            view.ShowWinPanel(GetInfoString(), progressController.Progress);
        }

        public void Hide()
        {
            Time.timeScale = 1.0f;
            view.HideLosePanel();
        }

        public void LeaveGame()
        {
            Time.timeScale = 1.0f;
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }

        private string GetInfoString()
        {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine($"Время: {timer.CurrentTime:0.0}");
            return stringBuilder.ToString();
        }
    }
}

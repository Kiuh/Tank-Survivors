using System.Text;
using Audio;
using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent OnWinPanelShown;

        private void Awake()
        {
            progressController.OnWin += Show;
        }

        private void Show()
        {
            Time.timeScale = 0.0f;
            OnWinPanelShown?.Invoke();
            SoundsManager.Instance.PauseSounds();
            view.ShowWinPanel(GetInfoString(), progressController.Progress);
        }

        public void LeaveGame()
        {
            Time.timeScale = 1.0f;
            ScenesController.Instance.LoadScene(InGameScene.LevelsScene);
        }

        private string GetInfoString()
        {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine($"Время: {timer.FormattedCurrentTime}");
            return stringBuilder.ToString();
        }
    }
}

using System.Text;
using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Panels.Death
{
    [AddComponentMenu("Panels.Death.Controller")]
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

        public UnityEvent<Reward> OnUseSecondLife;

        private void Awake()
        {
            progressController.OnLoose += ShowLosePanel;
        }

        public void ShowLosePanel()
        {
            Time.timeScale = 0.0f;
            view.ShowLosePanel(GetInfoString(), progressController.Progress);
        }

        public void HideLosePanel()
        {
            Time.timeScale = 1.0f;
            view.HideLosePanel();
        }

        public void HideSecondLifeButton()
        {
            view.HideSecondLifeButton();
        }

        public void UseSecondLifeBonus()
        {
            OnUseSecondLife?.Invoke(Reward.SecondLife);
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
            _ = stringBuilder.AppendLine($"Время: {timer.CurrentTime:0.0}");
            return stringBuilder.ToString();
        }
    }
}

using General;
using UnityEngine;

namespace Panels.Pause
{
    [AddComponentMenu("Panels.Pause.Controller")]
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private View view;

        [SerializeField]
        private GlobalInput globalInput;

        public void HidePause()
        {
            view.HidePausePanel();
        }

        public void ShowPause()
        {
            view.ShowPausePanel();
        }

        public void Resume()
        {
            globalInput.UnSetPause();
            view.HidePausePanel();
        }

        public void Repeat()
        {
            globalInput.UnSetPause();
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }

        public void Leave()
        {
            globalInput.UnSetPause();
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }
    }
}

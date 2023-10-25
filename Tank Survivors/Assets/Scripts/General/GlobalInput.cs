using UiPanels;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.GlobalInput")]
    public class GlobalInput : MonoBehaviour
    {
        [SerializeField]
        private Pause pause;

        // TODO: get input

        public void SetPause()
        {
            Time.timeScale = 0.0f;
            pause.ShowPausePanel();
        }

        public void UnSetPause()
        {
            Time.timeScale = 1.0f;
            pause.HidePausePanel();
        }
    }
}

using UiPanels;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    [AddComponentMenu("General.GlobalInput")]
    public class GlobalInput : MonoBehaviour
    {
        [SerializeField]
        private Pause pause;

        [SerializeField]
        private Button pauseButton;

        private void Awake()
        {
            pauseButton.onClick.AddListener(SetPause);
        }

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

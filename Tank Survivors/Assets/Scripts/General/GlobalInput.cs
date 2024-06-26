using Panels.Pause;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    [AddComponentMenu("General.GlobalInput")]
    public class GlobalInput : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Controller pauseController;

        [Required]
        [SerializeField]
        private Button pauseButton;

        private void Awake()
        {
            pauseButton.onClick.AddListener(SetPause);
        }

        public void SetPause()
        {
            Time.timeScale = 0.0f;
            pauseController.ShowPause();
        }

        public void UnSetPause()
        {
            Time.timeScale = 1.0f;
            pauseController.HidePause();
        }
    }
}

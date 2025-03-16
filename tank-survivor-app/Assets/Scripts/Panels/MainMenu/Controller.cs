using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.Controller")]
    internal class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Button tutorialButton;

        private void Awake()
        {
            tutorialButton.onClick.AddListener(
                () => ScenesController.Instance.LoadScene(InGameScene.Tutorial)
            );
        }

        public void Play()
        {
            ScenesController.Instance.LoadScene(InGameScene.LevelsScene);
        }
    }
}

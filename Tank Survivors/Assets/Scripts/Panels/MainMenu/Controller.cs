using General;
using UnityEngine;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.Controller")]
    internal class Controller : MonoBehaviour
    {
        private void Start()
        {
            // YandexGame.FullscreenShow();
        }

        public void Play()
        {
            ScenesController.Instance.LoadScene(InGameScene.LevelsScene);
        }
    }
}

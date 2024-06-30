using General;
using UnityEngine;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.Controller")]
    internal class Controller : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        public void Play()
        {
            ScenesController.Instance.LoadScene(InGameScene.LevelsScene);
        }
    }
}

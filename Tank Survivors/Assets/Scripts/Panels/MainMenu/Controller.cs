using General;
using UnityEngine;
using YG;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.Controller")]
    internal class Controller : MonoBehaviour
    {
        private void Start()
        {
            YandexGame.FullscreenShow();
        }

        public void Play()
        {
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }
    }
}

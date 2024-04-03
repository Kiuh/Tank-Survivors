using UnityEngine;
using YG;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Levels.Controller levelsController;

        private void Start()
        {
            YandexGame.FullscreenShow();
        }

        public void Play()
        {
            levelsController.ShowPanel();
        }
    }
}

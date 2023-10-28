using General;
using UnityEngine;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.Controller")]
    internal class Controller : MonoBehaviour
    {
        public void Play()
        {
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }
    }
}

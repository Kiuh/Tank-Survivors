using UnityEngine;
using UnityEngine.UI;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.View")]
    public class View : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;

        [SerializeField]
        private Button playButton;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayButtonClick);
        }

        private void PlayButtonClick()
        {
            controller.Play();
        }
    }
}

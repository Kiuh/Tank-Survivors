using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.MainMenu
{
    [AddComponentMenu("Panels.MainMenu.View")]
    public class View : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Controller controller;

        [Required]
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

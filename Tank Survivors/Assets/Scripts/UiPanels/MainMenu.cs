using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.MainMenu")]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;

        [SerializeField]
        private string nextScene;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayButtonClick);
        }

        private void PlayButtonClick()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

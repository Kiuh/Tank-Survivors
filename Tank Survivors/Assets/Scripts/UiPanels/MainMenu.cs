using General;
using UnityEngine;
using UnityEngine.UI;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.MainMenu")]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayButtonClick);
        }

        private void PlayButtonClick()
        {
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }
    }
}

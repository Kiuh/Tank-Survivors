using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Levels
{
    [AddComponentMenu("Scripts/Panels/Levels/Panels.Levels.Tutorial")]
    internal class Tutorial : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Button button;

        private void Awake()
        {
            button.onClick.AddListener(
                () => ScenesController.Instance.LoadScene(InGameScene.Tutorial)
            );
        }
    }
}

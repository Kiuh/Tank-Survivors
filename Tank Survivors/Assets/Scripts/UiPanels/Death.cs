using Tank;
using UnityEngine;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.Death")]
    public class Death : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        private void Awake()
        {
            tank.OnDeath += ShowLosePanel;
        }

        private void ShowLosePanel()
        {
            // TODO: implement
        }
    }
}

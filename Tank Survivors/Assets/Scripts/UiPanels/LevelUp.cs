using Tank;
using UnityEngine;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.LevelUp")]
    public class LevelUp : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        private void Awake()
        {
            tank.Experience.OnLevelUp += LevelUpRelease;
        }

        public void LevelUpRelease()
        {
            _ = tank.GetAvailableUpgrades();
            // TODO: implement
        }
    }
}

using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.LevelUpPanel")]
    public class LevelUpPanel : MonoBehaviour
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

using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public TankStartProperties TankStartProperties;
        public EnemiesConfig EnemiesConfig;
        public TankUpgradesConfig TankUpgradesConfig;
        public WeaponsConfig WeaponsConfig;
    }
}

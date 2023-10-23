using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public EnemiesConfig EnemiesConfig;
        public PlayerUpgradesConfig PlayerUpgradesConfig;
        public WeaponsConfig WeaponsConfig;
    }
}

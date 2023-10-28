using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class Game : ScriptableObject
    {
        public TankStartProperties TankStartProperties;
        public Enemies EnemiesConfig;
        public TankUpgrades TankUpgradesConfig;
        public Weapons WeaponsConfig;
        public LevelProgression LevelProgressionConfig;
    }
}

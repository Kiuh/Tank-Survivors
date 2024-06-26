using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class Game : ScriptableObject
    {
        [Required]
        public TankStartProperties TankStartProperties;

        [Required]
        public Enemies EnemiesConfig;

        [Required]
        public TankUpgrades TankUpgradesConfig;

        [Required]
        public TankWeapons WeaponsConfig;

        [Required]
        public LevelProgression LevelProgressionConfig;

        [Required]
        public EnemiesPickupsDrops EnemiesPickupsDropsConfig;
    }
}

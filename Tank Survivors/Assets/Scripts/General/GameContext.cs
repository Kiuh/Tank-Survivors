using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.GameContext")]
    public class GameContext : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private DataTransfer dataTransfer;
        public DataTransfer DataTransfer
        {
            get => dataTransfer;
            private set => dataTransfer = value;
        }

        [Required]
        [SerializeField]
        private Game gameConfig;
        public Game GameConfig => gameConfig;

        private void Awake()
        {
            gameConfig.EnemiesConfig = DataTransfer.LevelInfo.Enemies;
        }
    }
}

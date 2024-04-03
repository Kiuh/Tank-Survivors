using Configs;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.GameContext")]
    public class GameContext : MonoBehaviour
    {
        [SerializeField]
        private DataTransfer dataTransfer;

        [SerializeField]
        private Game gameConfig;
        public Game GameConfig => gameConfig;

        private void Awake()
        {
            gameConfig.EnemiesConfig = dataTransfer.LevelInfo.Enemies;
        }
    }
}

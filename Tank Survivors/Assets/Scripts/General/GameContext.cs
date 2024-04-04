using Configs;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.GameContext")]
    public class GameContext : MonoBehaviour
    {
        [field: SerializeField]
        public DataTransfer DataTransfer { get; private set; }

        [SerializeField]
        private Game gameConfig;
        public Game GameConfig => gameConfig;

        private void Awake()
        {
            gameConfig.EnemiesConfig = DataTransfer.LevelInfo.Enemies;
        }
    }
}

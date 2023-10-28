using Configs;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.GameContext")]
    public class GameContext : MonoBehaviour
    {
        [SerializeField]
        private Game gameConfig;
        public Game GameConfig => gameConfig;
    }
}

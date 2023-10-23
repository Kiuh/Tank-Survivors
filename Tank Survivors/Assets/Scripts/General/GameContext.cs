using General.Configs;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.GameContext")]
    public class GameContext : MonoBehaviour
    {
        [SerializeField]
        private GameConfig gameConfig;
        public GameConfig GameConfig => gameConfig;
    }
}

using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "MovingEnemyConfig",
        menuName = "Configs/EnemiesConfigs/MovingEnemyConfig",
        order = 0
    )]
    public class MovingEnemy : ScriptableObject
    {
        [SerializeField]
        private float health;
        public float Health => health;

        [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;
    }
}

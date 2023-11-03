using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "DroneConfig",
        menuName = "Configs/EnemiesConfigs/DroneConfig",
        order = 2
    )]
    public class Drone : ScriptableObject
    {
        [SerializeField]
        private MovingEnemy movingEnemyConfig;
        public MovingEnemy MovingEnemyConfig => movingEnemyConfig;

        [SerializeField]
        private float damage;
        public float Damage => damage;

        [SerializeField]
        private float explosionRadius;
        public float ExplosionRadius => explosionRadius;

        [SerializeField]
        private float timeToExplode;
        public float TimeToExplode => timeToExplode;
    }
}

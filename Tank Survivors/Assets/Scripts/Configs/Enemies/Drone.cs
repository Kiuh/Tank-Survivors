using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "Configs/Enemies/Drone", order = 1)]
    public class Drone : ScriptableObject
    {
        [SerializeField]
        private float health;
        public float Health => health;

        [SerializeField]
        private float damage;
        public float Damage => damage;

        [SerializeField]
        private float explosionRadius;
        public float ExplosionRadius => explosionRadius;

        [SerializeField]
        private float timeToExplode;
        public float TimeToExplode => timeToExplode;

        [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;
    }
}

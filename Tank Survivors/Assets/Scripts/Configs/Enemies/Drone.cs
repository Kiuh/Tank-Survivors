using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "Configs/Enemies/DroneConfig", order = 4)]
    public class Drone : ScriptableObject
    {
        [SerializeField]
        private float health;
        public float Health
        {
            get => health;
            set => health = value;
        }

        [SerializeField]
        private float damage;
        public float Damage => damage;

        [SerializeField]
        private float explosionRadius;
        public float ExplosionRadius => explosionRadius;

        [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;

        [SerializeField]
        private float experienceDropAmount;
        public float ExperienceDropAmount => experienceDropAmount;
    }
}

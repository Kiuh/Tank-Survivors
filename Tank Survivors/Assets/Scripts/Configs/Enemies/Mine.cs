using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MineConfig", menuName = "Configs/Enemies/MineConfig", order = 4)]
    public class Mine : ScriptableObject
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
    }
}

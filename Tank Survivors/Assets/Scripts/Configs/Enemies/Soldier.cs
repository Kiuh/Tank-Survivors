using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SoldierConfig", menuName = "Configs/Enemies/Soldier", order = 0)]
    public class Soldier : ScriptableObject
    {
        [SerializeField]
        private float health;
        public float Health => health;

        [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;

        [SerializeField]
        private float damage;
        public float Damage => damage;

        [SerializeField]
        private float timeForNextHit;
        public float TimeForNextHit => timeForNextHit;
    }
}

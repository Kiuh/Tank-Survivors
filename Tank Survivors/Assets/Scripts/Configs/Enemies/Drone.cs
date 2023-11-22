using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class Drone
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
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;
    }
}

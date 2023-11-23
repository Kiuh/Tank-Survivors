using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class Soldier
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

        [SerializeField]
        private float experienceDropAmount;
        public float ExperienceDropAmount => experienceDropAmount;
    }
}

using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "Configs/Enemies/DroneConfig", order = 4)]
    public class Drone : ScriptableObject
    {
        [SerializeField]
        private DroneConfig config;
        public DroneConfig Config => config;
    }

    [Serializable]
    public struct DroneConfig
    {
        public float Health;
        public float Damage;
        public float ExplosionRadius;
        public float MovementSpeed;
        public float ExperienceDropAmount;
    }
}

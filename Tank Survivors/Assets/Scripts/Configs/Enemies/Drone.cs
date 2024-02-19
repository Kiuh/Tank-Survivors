using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "Configs/Enemies/DroneConfig")]
    [Serializable]
    public class Drone : SerializedScriptableObject
    {
        [OdinSerialize]
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

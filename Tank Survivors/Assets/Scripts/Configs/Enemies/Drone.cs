using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "Configs/Enemies/DroneConfig", order = 4)]
    [Serializable]
    public class Drone : SerializedScriptableObject, IEnemyConfig
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

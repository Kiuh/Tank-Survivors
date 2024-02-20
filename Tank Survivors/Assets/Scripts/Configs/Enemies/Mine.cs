using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MineConfig", menuName = "Configs/Enemies/MineConfig")]
    public class Mine : SerializedScriptableObject
    {
        [SerializeField]
        private MineConfig config;
        public MineConfig Config => config;
    }

    [Serializable]
    public struct MineConfig
    {
        public float Health;
        public float Damage;
        public float ExplosionRadius;
    }
}

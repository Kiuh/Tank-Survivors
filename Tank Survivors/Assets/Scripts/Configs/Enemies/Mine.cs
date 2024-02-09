using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MineConfig", menuName = "Configs/Enemies/MineConfig", order = 4)]
    public class Mine : ScriptableObject
    {
        [field: SerializeField]
        public MineConfig Config { get; private set; }
    }

    [Serializable]
    public struct MineConfig
    {
        public float Health;
        public float Damage;
        public float ExplosionRadius;
    }
}

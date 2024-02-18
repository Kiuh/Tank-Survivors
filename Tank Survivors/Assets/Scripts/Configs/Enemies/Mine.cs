using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MineConfig", menuName = "Configs/Enemies/MineConfig", order = 4)]
    public class Mine : SerializedScriptableObject, IEnemyConfig
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

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "SoldierConfig",
        menuName = "Configs/Enemies/SoldierConfig",
        order = 4
    )]
    public class Soldier : SerializedScriptableObject, IEnemyConfig
    {
        [SerializeField]
        private SoliderConfig config;
        public SoliderConfig Config => config;
    }

    [Serializable]
    public struct SoliderConfig
    {
        public float Health;

        public float MovementSpeed;

        public float Damage;

        public float TimeForNextHit;

        public float ExperienceDropAmount;
    }
}

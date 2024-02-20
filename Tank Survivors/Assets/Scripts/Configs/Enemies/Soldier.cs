using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SoldierConfig", menuName = "Configs/Enemies/SoldierConfig")]
    public class Soldier : SerializedScriptableObject
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

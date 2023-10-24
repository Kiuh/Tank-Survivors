using System;
using UnityEngine;

namespace DataStructs
{
    [Serializable]
    public struct Percentage
    {
        [Range(0, 3)]
        [SerializeField]
        private float value;

        public float Value => value;

        public bool TryChance()
        {
            return UnityEngine.Random.Range(0f, 1f) < value;
        }
    }
}

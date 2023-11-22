using Sirenix.OdinInspector;
using System;

namespace DataStructs
{
    [Serializable]
    public struct Percentage
    {
        [MinValue(0)]
        [ShowInInspector]
        [LabelText("Percentages")]
        [Unit(Units.Percent)]
        public float Value { get; private set; }

        public bool TryChance()
        {
            return UnityEngine.Random.Range(0f, 1f) < Value;
        }
    }
}

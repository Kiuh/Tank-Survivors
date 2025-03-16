using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataStructs
{
    [Serializable]
    public struct Percentage
    {
        [MinValue(0)]
        [ShowInInspector]
        [SerializeField]
        [LabelText("Percentages")]
        [Unit(Units.Percent)]
        private float value;
        public float Value
        {
            get => value;
            private set => this.value = value;
        }
        public float NormalizedValue => Value / 100;

        public Percentage(float value)
        {
            this.value = value;
        }

        public bool TryChance()
        {
            return UnityEngine.Random.Range(0f, 1f) < NormalizedValue;
        }

        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a.Value + b.Value);
        }

        public static Percentage operator -(Percentage a, Percentage b)
        {
            return new Percentage(a.Value - b.Value);
        }

        public static Percentage operator *(Percentage a, Percentage b)
        {
            return new Percentage(a.Value * b.Value / 100);
        }

        public static Percentage operator /(Percentage a, Percentage b)
        {
            return new Percentage(a.Value / 100 / (b.Value / 100) * 100);
        }
    }
}

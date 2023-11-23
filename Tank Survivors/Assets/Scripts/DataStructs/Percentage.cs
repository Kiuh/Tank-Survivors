﻿using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using Unity.Android.Gradle.Manifest;

namespace DataStructs
{
    [Serializable]
    public struct Percentage
    {
        [MinValue(0)]
        [ShowInInspector]
        [OdinSerialize]
        [LabelText("Percentages")]
        [Unit(Units.Percent)]
        public float Value { get; private set; }

        public Percentage(float value)
        {
            Value = value;
        }

        public bool TryChance()
        {
            return UnityEngine.Random.Range(0f, 1f) < Value;
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

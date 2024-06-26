using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SpawnRadiusConfig", menuName = "Configs/SpawnRadiusConfig")]
    public class CircleRadius : ScriptableObject
    {
        [Serializable]
        public enum Preset
        {
            Small,
            Medium,
            Big
        }

        [Serializable]
        private struct PresetZone
        {
            public Preset Preset;
            public CircleZone CircleZone;
        }

        [SerializeField]
        private List<PresetZone> zones;

        private Dictionary<Preset, CircleZone> sizes;

        public Dictionary<Preset, CircleZone> Sizes
        {
            get
            {
                sizes ??= zones.ToDictionary(x => x.Preset, x => x.CircleZone);
                return sizes;
            }
        }

        public CircleZone GetCircleZone(Preset preset)
        {
            return sizes[preset];
        }
    }

    [Serializable]
    public class CircleZone
    {
        [SerializeField]
        [MinValue(0.0f)]
        [MaxValue(nameof(Max))]
        [FoldoutGroup("CircleZone")]
        private float min = 0.0f;

        public float Min
        {
            get => min;
            private set => min = value;
        }

        [SerializeField]
        [FoldoutGroup("CircleZone")]
        [MinValue(nameof(Min))]
        private float max = 0.0f;
        public float Max
        {
            get => max;
            private set => max = value;
        }

        public Vector3 GetRandomPoint()
        {
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (Max - Min)) + (point.normalized * Min);
        }
    }
}

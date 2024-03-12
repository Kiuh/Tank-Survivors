using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SpawnRadiusConfig", menuName = "Configs/SpawnRadiusConfig")]
    public class CircleRadius : SerializedScriptableObject
    {
        public enum Preset
        {
            Small,
            Medium,
            Big
        }

        [OdinSerialize]
        private Dictionary<Preset, CircleZone> sizes =
            new()
            {
                { Preset.Small, new CircleZone() },
                { Preset.Medium, new CircleZone() },
                { Preset.Big, new CircleZone() },
            };

        public CircleZone GetCircleZone(Preset preset)
        {
            return sizes[preset];
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    public class CircleZone
    {
        [OdinSerialize]
        [MinValue(0.0f)]
        [MaxValue(nameof(Max))]
        [FoldoutGroup("CircleZone")]
        public float Min { get; private set; } = 0.0f;

        [OdinSerialize]
        [FoldoutGroup("CircleZone")]
        [MinValue(nameof(Min))]
        public float Max { get; private set; } = 0.0f;

        public Vector3 GetRandomPoint()
        {
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (Max - Min)) + (point.normalized * Min);
        }
    }
}

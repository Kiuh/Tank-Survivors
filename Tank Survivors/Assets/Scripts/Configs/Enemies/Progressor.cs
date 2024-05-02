using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProgressorConfig", menuName = "Configs/ProgressorConfig")]
    public class Progressor : SerializedScriptableObject
    {
        public enum Mode
        {
            Source,
            Current
        }

        [OdinSerialize]
        [Unit(Units.Second)]
        private float interval;

        public float Interval
        {
            get => interval;
            private set => interval = value;
        }

        [OdinSerialize]
        [Unit(Units.Minute)]
        [ReadOnly]
        public float IntervalInMinutes
        {
            get => interval / 60.0f;
            private set => interval = value * 60.0f;
        }

        [OdinSerialize]
        [EnumToggleButtons]
        public Mode CurrentMode { get; private set; } = Mode.Source;

        [OdinSerialize]
        public List<IModuleUpgrade> UpgradebleModules { get; set; } = new();
    }
}

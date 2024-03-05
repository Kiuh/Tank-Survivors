using System;
using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Configs
{
    [Serializable]
    [HideLabel]
    [HideReferenceObjectPicker]
    public class Properties
    {
        public enum ProgressorMode
        {
            Source,
            Current
        }

        [OdinSerialize]
        [Unit(Units.Percent)]
        private float value;
        public float Value => value / 100.0f;

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
        public float IntervalInMinuts
        {
            get => interval / 60.0f;
            private set => interval = value * 60.0f;
        }

        [OdinSerialize]
        [EnumToggleButtons]
        public ProgressorMode Mode { get; private set; } = ProgressorMode.Source;

        [OdinSerialize]
        public List<IModuleUpgrade> UpgradebleModules { get; set; } = new();

        public float LastUpdateTime { get; set; } = 0;
    }
}

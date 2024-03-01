using System;
using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Configs
{
    public enum ProgressorMode
    {
        fromSource,
        fromCurrent
    }

    [Serializable]
    [HideLabel]
    [HideReferenceObjectPicker]
    public class ProgressorProperties
    {
        [OdinSerialize]
        [FoldoutGroup("Progressor")]
        [Unit(Units.Percent)]
        private float value;
        public float Value => value / 100.0f;

        [OdinSerialize]
        [FoldoutGroup("Progressor")]
        [Unit(Units.Minute)]
        private float interval;
        public float Interval
        {
            get => interval * 60.0f;
            private set => interval = value;
        }

        [OdinSerialize]
        [EnumToggleButtons]
        [FoldoutGroup("Progressor")]
        public ProgressorMode Mode { get; private set; } = ProgressorMode.fromSource;

        [OdinSerialize]
        [FoldoutGroup("Progressor")]
        public List<IModuleUpgrade> UpgradebleModules { get; set; } = new();

        public float LastUpdateTime { get; set; } = 0;
    }
}

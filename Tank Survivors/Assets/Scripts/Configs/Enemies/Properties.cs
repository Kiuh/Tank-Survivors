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
        [FoldoutGroup("$" + nameof(progressor))]
        [Unit(Units.Percent)]
        private float value;
        public float Value => value / 100.0f;

        [OdinSerialize]
        [FoldoutGroup("$" + nameof(progressor))]
        [Unit(Units.Minute)]
        private float interval;
        public float Interval
        {
            get => interval * 60.0f;
            private set => interval = value;
        }

        [OdinSerialize]
        [EnumToggleButtons]
        [FoldoutGroup("$" + nameof(progressor), AnimateVisibility = true)]
        public ProgressorMode Mode { get; private set; } = ProgressorMode.Source;

        [OdinSerialize]
        [FoldoutGroup("$" + nameof(progressor), AnimateVisibility = true)]
        public List<IModuleUpgrade> UpgradebleModules { get; set; } = new();

        public float LastUpdateTime { get; set; } = 0;

        private string progressor = "Progressor";
    }
}

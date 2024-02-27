using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Assets.Scripts.Configs.Enemies
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
        public float Value { get; private set; }

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
        private ProgressorMode mode = ProgressorMode.fromSource;
    }
}

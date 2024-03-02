using System;
using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Enemies
{
    public interface IModule : ICloneable { }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class MovementModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Movement speed")]
        public ModifiableValue<float> Speed { get; set; } = new();

        public object Clone()
        {
            MovementModule module = new MovementModule();
            module.Speed.SourceValue = Speed.SourceValue;
            module.Speed.Modifications.AddRange(Speed.Modifications);
            return module;
        }
    }
}

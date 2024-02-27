using System;
using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Enemies
{
    public interface IModule { }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class MovementModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Movement speed")]
        public ModifiableValue<float> Speed { get; set; } = new();
    }
}

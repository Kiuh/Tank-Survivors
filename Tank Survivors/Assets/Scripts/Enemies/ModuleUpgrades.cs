using System;
using System.Collections.Generic;
using Common;
using Configs;
using Sirenix.OdinInspector;

namespace Enemies
{
    [HideReferenceObjectPicker]
    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(List<IModule> modules, float percentage, ProgressorMode mode);
    }

    [Serializable]
    [LabelText("Movement")]
    public class MovementUpgrade : IModuleUpgrade
    {
        public void ApplyUpgrade(List<IModule> modules, float percentage, ProgressorMode mode)
        {
            MovementModule module = modules.GetConcrete<MovementModule, IModule>();
            float value;
            if (mode == ProgressorMode.fromCurrent)
            {
                value = module.Speed.GetModifiedValue() * percentage;
            }
            else
            {
                value = module.Speed.SourceValue * percentage;
            }
            module.Speed.Modifications.Add(
                new(MathOperation.Plus.ToFunction(value), ModificationPriority.Medium)
            );
        }
    }
}

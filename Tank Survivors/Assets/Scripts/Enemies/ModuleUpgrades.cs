using System;
using Common;
using Configs;
using DataStructs;
using Sirenix.OdinInspector;

namespace Enemies
{
    [HideReferenceObjectPicker]
    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(EnemyProducer producer);
    }

    [Serializable]
    [LabelText("Movement")]
    public class MovementUpgrade : IModuleUpgrade
    {
        public void ApplyUpgrade(EnemyProducer producer)
        {
            MovementModule module = producer.Producer.Modules.GetConcrete<
                MovementModule,
                IModule
            >();
            float value = producer.ProgressorProperties.Value;
            if (producer.ProgressorProperties.Mode == ProgressorMode.fromCurrent)
            {
                value *= module.Speed.GetModifiedValue();
            }
            else
            {
                value *= module.Speed.SourceValue;
            }
            module.Speed.Modifications.Add(
                new(MathOperation.Plus.ToFunction(value), ModificationPriority.Medium)
            );
        }
    }
}

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

    [Serializable]
    [LabelText("Health")]
    public class HealthUpgrade : IModuleUpgrade
    {
        public void ApplyUpgrade(EnemyProducer producer)
        {
            HealthModule module = producer.Producer.Modules.GetConcrete<HealthModule, IModule>();
            float value = producer.ProgressorProperties.Value;
            if (producer.ProgressorProperties.Mode == ProgressorMode.fromCurrent)
            {
                value *= module.Health.GetModifiedValue();
            }
            else
            {
                value *= module.Health.SourceValue;
            }
            module.Health.Modifications.Add(
                new(MathOperation.Plus.ToFunction(value), ModificationPriority.Medium)
            );
        }
    }

    [Serializable]
    [LabelText("Cooldown")]
    public class CooldownUpgrade : IModuleUpgrade
    {
        public void ApplyUpgrade(EnemyProducer producer)
        {
            AttackCooldownModule module = producer.Producer.Modules.GetConcrete<
                AttackCooldownModule,
                IModule
            >();
            float value = producer.ProgressorProperties.Value;
            if (producer.ProgressorProperties.Mode == ProgressorMode.fromCurrent)
            {
                value *= module.Cooldown.GetModifiedValue();
            }
            else
            {
                value *= module.Cooldown.SourceValue;
            }
            module.Cooldown.Modifications.Add(
                new(MathOperation.Minus.ToFunction(value), ModificationPriority.Medium)
            );
        }
    }

    [Serializable]
    [LabelText("XP")]
    public class ExpeirienceUpgrade : IModuleUpgrade
    {
        public void ApplyUpgrade(EnemyProducer producer)
        {
            ExperienceModule module = producer.Producer.Modules.GetConcrete<
                ExperienceModule,
                IModule
            >();
            float value = producer.ProgressorProperties.Value;
            if (producer.ProgressorProperties.Mode == ProgressorMode.fromCurrent)
            {
                value *= module.DropAmount.GetModifiedValue();
            }
            else
            {
                value *= module.DropAmount.SourceValue;
            }
            module.DropAmount.Modifications.Add(
                new(MathOperation.Plus.ToFunction(value), ModificationPriority.Medium)
            );
        }
    }
}

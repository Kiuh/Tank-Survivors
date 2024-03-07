using System;
using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Configs.Progressor;

namespace Enemies
{
    [HideReferenceObjectPicker]
    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(EnemyProducer producer);
    }

    [Serializable]
    public abstract class BaseModuleUpgrade : IModuleUpgrade
    {
        [OdinSerialize]
        [Unit(Units.Percent)]
        [MinValue(0.0f)]
        private float persent = 0;
        public float Persent => persent / 100.0f;

        [OdinSerialize]
        [EnumToggleButtons]
        public MathOperation Operation { get; private set; } = MathOperation.Plus;
        public abstract void ApplyUpgrade(EnemyProducer producer);
    }

    [Serializable]
    [LabelText("Movement")]
    public class MovementUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(EnemyProducer producer)
        {
            MovementModule module = producer.Producer.Modules.GetConcrete<
                MovementModule,
                IModule
            >();
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.Mode == ProgressorMode.Current)
                {
                    value *= module.Speed.GetModifiedValue();
                }
                else
                {
                    value *= module.Speed.SourceValue;
                }
                module.Speed.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }

    [Serializable]
    [LabelText("Health")]
    public class HealthUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(EnemyProducer producer)
        {
            HealthModule module = producer.Producer.Modules.GetConcrete<HealthModule, IModule>();
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.Mode == ProgressorMode.Current)
                {
                    value *= module.Health.GetModifiedValue();
                }
                else
                {
                    value *= module.Health.SourceValue;
                }
                module.Health.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }

    [Serializable]
    [LabelText("Cooldown")]
    public class CooldownUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(EnemyProducer producer)
        {
            AttackCooldownModule module = producer.Producer.Modules.GetConcrete<
                AttackCooldownModule,
                IModule
            >();
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.Mode == ProgressorMode.Current)
                {
                    value *= module.Cooldown.GetModifiedValue();
                }
                else
                {
                    value *= module.Cooldown.SourceValue;
                }
                module.Cooldown.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }

    [Serializable]
    [LabelText("XP")]
    public class ExpeirienceUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(EnemyProducer producer)
        {
            ExperienceModule module = producer.Producer.Modules.GetConcrete<
                ExperienceModule,
                IModule
            >();
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.Mode == ProgressorMode.Current)
                {
                    value *= module.DropAmount.GetModifiedValue();
                }
                else
                {
                    value *= module.DropAmount.SourceValue;
                }
                module.DropAmount.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }
}

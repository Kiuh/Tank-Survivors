using System;
using Common;
using Enemies.Producers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Configs.Progressor;

namespace Enemies
{
    [HideReferenceObjectPicker]
    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(IEnemyProducer producer);
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
        public abstract void ApplyUpgrade(IEnemyProducer producer);

        protected T GetModule<T>(IEnemyProducer producer)
            where T : class, IModule
        {
            return producer.Modules.GetConcrete<T, IModule>();
        }
    }

    [Serializable]
    [LabelText("Movement")]
    public class MovementUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            MovementModule module = GetModule<MovementModule>(producer);
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.CurrentMode == Mode.Current)
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
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            HealthModule module = GetModule<HealthModule>(producer);
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.CurrentMode == Mode.Current)
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
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            AttackCooldownModule module = GetModule<AttackCooldownModule>(producer);
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.CurrentMode == Mode.Current)
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
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            ExperienceModule module = GetModule<ExperienceModule>(producer);
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.CurrentMode == Mode.Current)
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

    [Serializable]
    [LabelText("Explosion")]
    public class ExplosionUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            ExplosionModule module = GetModule<ExplosionModule>(producer);
            if (module != null)
            {
                float value = Persent;
                if (producer.Progressor.CurrentMode == Mode.Current)
                {
                    value *= module.Radius.GetModifiedValue();
                }
                else
                {
                    value *= module.Radius.SourceValue;
                }
                module.Radius.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }
}

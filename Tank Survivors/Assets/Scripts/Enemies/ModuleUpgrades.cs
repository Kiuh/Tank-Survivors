using System;
using Common;
using Configs;
using Enemies.Producers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(IEnemyProducer producer);
    }

    [Serializable]
    public abstract class BaseModuleUpgrade : IModuleUpgrade
    {
        [SerializeField]
        [Unit(Units.Percent)]
        [MinValue(0.0f)]
        private float present = 0;
        public float Present => present / 100.0f;

        [SerializeField]
        [EnumToggleButtons]
        private MathOperation operation = MathOperation.Plus;
        public MathOperation Operation => operation;
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
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
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
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
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
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
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
    public class ExperienceUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            ExperienceModule module = GetModule<ExperienceModule>(producer);
            if (module != null)
            {
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
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
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
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

    [Serializable]
    [LabelText("FireRange")]
    public class FireRangeUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            ShootingRangeModule module = GetModule<ShootingRangeModule>(producer);
            if (module != null)
            {
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
                {
                    value *= module.ShootingRange.GetModifiedValue();
                }
                else
                {
                    value *= module.ShootingRange.SourceValue;
                }
                module.ShootingRange.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }

    [Serializable]
    [LabelText("FireRate")]
    public class FireRateUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            ShootingRateModule module = GetModule<ShootingRateModule>(producer);
            if (module != null)
            {
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
                {
                    value *= module.ShootCooldown.GetModifiedValue();
                }
                else
                {
                    value *= module.ShootCooldown.SourceValue;
                }
                module.ShootCooldown.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }

    [Serializable]
    [LabelText("Projectile Speed")]
    public class ProjectileSpeedUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            ProjectileSpeedModule module = GetModule<ProjectileSpeedModule>(producer);
            if (module != null)
            {
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
                {
                    value *= module.ProjectileSpeed.GetModifiedValue();
                }
                else
                {
                    value *= module.ProjectileSpeed.SourceValue;
                }
                module.ProjectileSpeed.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }

    [Serializable]
    [LabelText("Damage")]
    public class DamageUpgrade : BaseModuleUpgrade
    {
        public override void ApplyUpgrade(IEnemyProducer producer)
        {
            DamageModule module = GetModule<DamageModule>(producer);
            if (module != null)
            {
                float value = Present;
                if (producer.Progressor.CurrentMode == Progressor.Mode.Current)
                {
                    value *= module.Damage.GetModifiedValue();
                }
                else
                {
                    value *= module.Damage.SourceValue;
                }
                module.Damage.Modifications.Add(
                    new(Operation.ToFunction(value), ModificationPriority.Medium)
                );
            }
        }
    }
}

using System;
using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Enemies
{
    public interface IModule
    {
        public IModule Clone();
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class MovementModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Movement speed")]
        public ModifiableValue<float> Speed { get; set; } = new();

        public IModule Clone()
        {
            MovementModule module = new MovementModule();
            module.Speed.SourceValue = Speed.SourceValue;
            module.Speed.Modifications.AddRange(Speed.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class HealthModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Health")]
        public ModifiableValue<float> Health { get; set; } = new();

        public IModule Clone()
        {
            HealthModule module = new HealthModule();
            module.Health.SourceValue = Health.SourceValue;
            module.Health.Modifications.AddRange(Health.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class DamageModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Damage")]
        public ModifiableValue<float> Damage { get; set; } = new();

        public IModule Clone()
        {
            DamageModule module = new DamageModule();
            module.Damage.SourceValue = Damage.SourceValue;
            module.Damage.Modifications.AddRange(Damage.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class AttackCooldownModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Cooldown")]
        public ModifiableValue<float> Cooldown { get; set; } = new(0.001f);

        public IModule Clone()
        {
            AttackCooldownModule module = new AttackCooldownModule();
            module.Cooldown.SourceValue = Cooldown.SourceValue;
            module.Cooldown.Modifications.AddRange(Cooldown.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class ExperienceModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("XP drop amount")]
        public ModifiableValue<float> DropAmount { get; set; } = new();

        public IModule Clone()
        {
            ExperienceModule module = new ExperienceModule();
            module.DropAmount.SourceValue = DropAmount.SourceValue;
            module.DropAmount.Modifications.AddRange(DropAmount.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class ExplosionModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Explosive radius")]
        public ModifiableValue<float> Radius { get; set; } = new(1.0f);

        public IModule Clone()
        {
            ExplosionModule module = new ExplosionModule();
            module.Radius.SourceValue = Radius.SourceValue;
            module.Radius.Modifications.AddRange(Radius.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class ShootingRangeModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Shootin Range")]
        public ModifiableValue<float> ShootingRange { get; set; } = new(1.0f);

        public IModule Clone()
        {
            ShootingRangeModule module = new();
            module.ShootingRange.SourceValue = ShootingRange.SourceValue;
            module.ShootingRange.Modifications.AddRange(ShootingRange.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class ShootingRateModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Explosive radius")]
        [Unit(Units.Second)]
        public ModifiableValue<float> ShootCooldown { get; set; } = new(1.0f);

        public IModule Clone()
        {
            ShootingRateModule module = new();
            module.ShootCooldown.SourceValue = ShootCooldown.SourceValue;
            module.ShootCooldown.Modifications.AddRange(ShootCooldown.Modifications);
            return module;
        }
    }
}

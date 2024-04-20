using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Configs;
using Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

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
            MovementModule module = new();
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
            HealthModule module = new();
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
            DamageModule module = new();
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
            AttackCooldownModule module = new();
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
            ExperienceModule module = new();
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
            ExplosionModule module = new();
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
        [FoldoutGroup("Shooting Range")]
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
        [FoldoutGroup("Shoot Cooldown")]
        public ModifiableValue<float> ShootCooldown { get; set; } = new(0.001f);

        public IModule Clone()
        {
            ShootingRateModule module = new();
            module.ShootCooldown.SourceValue = ShootCooldown.SourceValue;
            module.ShootCooldown.Modifications.AddRange(ShootCooldown.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class ProjectileSpeedModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("BulletSpeed")]
        public ModifiableValue<float> ProjectileSpeed { get; set; } = new(1.0f);

        public IModule Clone()
        {
            ProjectileSpeedModule module = new();
            module.ProjectileSpeed.SourceValue = ProjectileSpeed.SourceValue;
            module.ProjectileSpeed.Modifications.AddRange(ProjectileSpeed.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class DashModule : IModule
    {
        [OdinSerialize]
        [FoldoutGroup("Dash")]
        [MinValue(0)]
        public float AimTime { private set; get; } = 0.0f;

        [OdinSerialize]
        [FoldoutGroup("Dash")]
        [PropertyRange(0.0f, 1.0f)]
        public float SlowPercent { private set; get; } = 0.0f;

        [OdinSerialize]
        [FoldoutGroup("Dash")]
        public float DashSpeedMultiplier { private set; get; } = 0.0f;

        [OdinSerialize]
        [FoldoutGroup("Dash")]
        public CircleZone CircleZone { private set; get; } = new();

        [OdinSerialize]
        [FoldoutGroup("Dash")]
        [MinValue(0.01f)]
        public float CoolDown { private set; get; } = 1.0f;

        public IModule Clone()
        {
            return this;
        }
    }
}

[Serializable]
[HideReferenceObjectPicker]
[HideLabel]
public class RageModule : IModule
{
    [OdinSerialize]
    [FoldoutGroup("RageModule")]
    public List<RageProperties> ScaleList = new();

    [OdinSerialize]
    [FoldoutGroup("RageModule")]
    public float MinimumCooldown = 0.0f;

    public IModule Clone()
    {
        RageModule rage = new();
        rage.MinimumCooldown = MinimumCooldown;
        rage.ScaleList = ScaleList.OrderBy((x) => x.EnemyHealthPercentage).ToList();
        return rage;
    }
}

[Serializable]
[HideReferenceObjectPicker]
[HideLabel]
public class RageProperties
{
    [OdinSerialize]
    [PropertyRange(0, 1.0f)]
    public float EnemyHealthPercentage { private set; get; } = 0.0f;

    [OdinSerialize]
    [PropertyRange(0, 1.0f)]
    public float CooldownPercentage { private set; get; } = 0.0f;
}

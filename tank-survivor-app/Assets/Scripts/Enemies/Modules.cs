using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public interface IModule
    {
        public IModule Clone();
    }

    [Serializable]
    [HideLabel]
    public class MovementModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Movement speed")]
        private ModifiableValue<float> speed = new();
        public ModifiableValue<float> Speed => speed;

        public IModule Clone()
        {
            MovementModule module = new();
            module.Speed.SourceValue = Speed.SourceValue;
            module.Speed.Modifications.AddRange(Speed.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class HealthModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Health")]
        private ModifiableValue<float> health = new();
        public ModifiableValue<float> Health => health;

        public IModule Clone()
        {
            HealthModule module = new();
            module.Health.SourceValue = Health.SourceValue;
            module.Health.Modifications.AddRange(Health.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class DamageModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Damage")]
        private ModifiableValue<float> damage = new();
        public ModifiableValue<float> Damage => damage;

        public IModule Clone()
        {
            DamageModule module = new();
            module.Damage.SourceValue = Damage.SourceValue;
            module.Damage.Modifications.AddRange(Damage.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class AttackCooldownModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Cooldown")]
        private ModifiableValue<float> cooldown = new(0.001f);
        public ModifiableValue<float> Cooldown => cooldown;

        public IModule Clone()
        {
            AttackCooldownModule module = new();
            module.Cooldown.SourceValue = Cooldown.SourceValue;
            module.Cooldown.Modifications.AddRange(Cooldown.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class ExperienceModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("XP drop amount")]
        private ModifiableValue<float> dropAmount = new();
        public ModifiableValue<float> DropAmount => dropAmount;

        public IModule Clone()
        {
            ExperienceModule module = new();
            module.DropAmount.SourceValue = DropAmount.SourceValue;
            module.DropAmount.Modifications.AddRange(DropAmount.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class ExplosionModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Explosive radius")]
        private ModifiableValue<float> radius = new(1.0f);
        public ModifiableValue<float> Radius => radius;

        public IModule Clone()
        {
            ExplosionModule module = new();
            module.Radius.SourceValue = Radius.SourceValue;
            module.Radius.Modifications.AddRange(Radius.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class ShootingRangeModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Shooting Range")]
        private ModifiableValue<float> shootingRange = new(1.0f);
        public ModifiableValue<float> ShootingRange => shootingRange;

        public IModule Clone()
        {
            ShootingRangeModule module = new();
            module.ShootingRange.SourceValue = ShootingRange.SourceValue;
            module.ShootingRange.Modifications.AddRange(ShootingRange.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class ShootingRateModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Shoot Cooldown")]
        private ModifiableValue<float> shootCooldown = new(0.001f);
        public ModifiableValue<float> ShootCooldown => shootCooldown;

        public IModule Clone()
        {
            ShootingRateModule module = new();
            module.ShootCooldown.SourceValue = ShootCooldown.SourceValue;
            module.ShootCooldown.Modifications.AddRange(ShootCooldown.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class ProjectileSpeedModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("BulletSpeed")]
        private ModifiableValue<float> projectileSpeed = new(1.0f);
        public ModifiableValue<float> ProjectileSpeed => projectileSpeed;

        public IModule Clone()
        {
            ProjectileSpeedModule module = new();
            module.ProjectileSpeed.SourceValue = ProjectileSpeed.SourceValue;
            module.ProjectileSpeed.Modifications.AddRange(ProjectileSpeed.Modifications);
            return module;
        }
    }

    [Serializable]
    [HideLabel]
    public class DashModule : IModule
    {
        [SerializeField]
        [FoldoutGroup("Dash")]
        [MinValue(0)]
        private float aimTime = 0.0f;
        public float AimTime => aimTime;

        [SerializeField]
        [FoldoutGroup("Dash")]
        [PropertyRange(0.0f, 1.0f)]
        private float slowPercent = 0.0f;
        public float SlowPercent => slowPercent;

        [SerializeField]
        [FoldoutGroup("Dash")]
        private float dashSpeedMultiplier = 0.0f;
        public float DashSpeedMultiplier => dashSpeedMultiplier;

        [SerializeField]
        [FoldoutGroup("Dash")]
        private CircleZone circleZone = new();
        public CircleZone CircleZone => circleZone;

        [SerializeField]
        [FoldoutGroup("Dash")]
        [MinValue(0.01f)]
        private float coolDown = 1.0f;
        public float CoolDown => coolDown;

        [SerializeField]
        [FoldoutGroup("Dash")]
        private float damageMultiplier = 2.0f;
        public float DamageMultiplier => damageMultiplier;

        public IModule Clone()
        {
            return this;
        }
    }

    [Serializable]
    [HideLabel]
    public class RageModule : IModule
    {
        [FoldoutGroup("RageModule")]
        public List<RageProperties> ScaleList = new();

        [FoldoutGroup("RageModule")]
        public float MinimumCooldown = 0.0f;

        public IModule Clone()
        {
            RageModule rage =
                new()
                {
                    MinimumCooldown = MinimumCooldown,
                    ScaleList = ScaleList.OrderBy((x) => x.EnemyHealthPercentage).ToList()
                };
            return rage;
        }
    }

    [Serializable]
    [HideLabel]
    public class RageProperties
    {
        [SerializeField]
        [PropertyRange(0, 1.0f)]
        private float enemyHealthPercentage = 0.0f;
        public float EnemyHealthPercentage => enemyHealthPercentage;

        [SerializeField]
        [PropertyRange(0, 1.0f)]
        private float cooldownPercentage = 0.0f;
        public float CooldownPercentage => cooldownPercentage;
    }
}

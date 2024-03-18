using System;
using Common;
using Configs;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Tank.Weapons.ModulesUpgrades
{
    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(IWeapon weapon);
    }

    [HideLabel]
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public abstract class BaseModuleMathUpgrade<T> : IModuleUpgrade
    {
        [OdinSerialize]
        [FoldoutGroup("@GetType()")]
        [HorizontalGroup("@GetType()/Horizontal")]
        [EnumToggleButtons]
        protected MathOperation MathOperation = MathOperation.Plus;

        [FoldoutGroup("@GetType()")]
        [InlineProperty]
        [OdinSerialize]
        protected T OperationValue;

        [FoldoutGroup("@GetType()")]
        [LabelText("Priority")]
        [EnumToggleButtons]
        [OdinSerialize]
        protected ModificationPriority ModificationPriority = ModificationPriority.Medium;

        public abstract void ApplyUpgrade(IWeapon weapon);
    }

    [HideLabel]
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public class WeaponGunCreation : IModuleUpgrade
    {
        [InfoBox("This Module Upgrade Creates Gun")]
        [Space]
        public bool UselessCheckBox;

        public void ApplyUpgrade(IWeapon weapon)
        {
            weapon.CreateGun();
        }
    }

    [Serializable]
    public class SwapWeapon : IModuleUpgrade
    {
        public WeaponConfig NewWeapon;

        public void ApplyUpgrade(IWeapon weapon)
        {
            weapon.SwapWeapon(NewWeapon.Weapon);
        }
    }

    [Serializable]
    public class Damage : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<DamageModule, IWeaponModule>()
                .Damage.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class FireRange : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<FireRangeModule, IWeaponModule>()
                .FireRange.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class ProjectileSize : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<ProjectileSizeModule, IWeaponModule>()
                .ProjectileSize.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class Penetration : BaseModuleMathUpgrade<int>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<PenetrationModule, IWeaponModule>()
                .Penetration.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class ProjectilesPerShoot : BaseModuleMathUpgrade<int>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<ProjectilesPerShootModule, IWeaponModule>()
                .ProjectilesPerShoot.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class FireRate : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<FireRateModule, IWeaponModule>()
                .FireRate.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class CriticalChance : BaseModuleMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<CriticalChanceModule, IWeaponModule>()
                .CriticalChance.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class CriticalMultiplier : BaseModuleMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<CriticalMultiplierModule, IWeaponModule>()
                .CriticalMultiplier.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class ProjectileDamageRadius : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<ProjectileDamageRadiusModule, IWeaponModule>()
                .DamageRadius.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class RayDuration : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<RayDurationModule, IWeaponModule>()
                .RayDuration.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class ProjectileSpreadAngle : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<ProjectileSpreadAngleModule, IWeaponModule>()
                .SpreadAngle.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    [Serializable]
    public class TowerRotation : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<TowerRotationModule, IWeaponModule>()
                .RotationSpeed.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class FireDamage : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<FireDamageModule, Modules.IWeaponModule>()
                .Damage.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class FireFireRate : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<FireFireRateModule, IWeaponModule>()
                .FireRate.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class ProjectileFireTimer : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<ProjectileFireTimerModule, Modules.IWeaponModule>()
                .Time.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }
}

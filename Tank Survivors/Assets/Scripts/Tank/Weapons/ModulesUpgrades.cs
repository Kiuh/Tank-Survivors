using System;
using System.Collections;
using System.Linq;
using Common;
using Configs;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons
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
    public class AddCannonUpgrade : IModuleUpgrade
    {
        public CannonPositioner CannonPositioner;

        [ValueDropdown("GetAllPositions")]
        public string CannonPosition;

        public void ApplyUpgrade(IWeapon weapon)
        {
            var cannonPrefab = weapon
                .Modules.GetConcrete<CannonModule, IWeaponModule>()
                .CannonPrefab;
            weapon
                .Modules.GetConcrete<TowerModule<MultiShotTower>, IWeaponModule>()
                .Tower.AddCannon(cannonPrefab, CannonPosition);
        }

        private IEnumerable GetAllPositions()
        {
            return CannonPositioner?.CannonProperties.Select(x => x.Name);
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
}

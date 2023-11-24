using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;

namespace Tank.Weapons
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class LeveledWeaponUpgrade : ILeveledUpgrade
    {
        [FoldoutGroup("$UpgradingLevel")]
        [OdinSerialize]
        public uint UpgradingLevel { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [MultiLineProperty]
        [OdinSerialize]
        public string Description { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [NonSerialized, OdinSerialize]
        [PropertyOrder(1)]
        private List<IModuleUpgrade> moduleUpgrades;

        public void ApplyUpgrade(TankImpl tank)
        {
            if (moduleUpgrades == null)
            {
                return;
            }
            foreach (IModuleUpgrade upgrade in moduleUpgrades)
            {
                upgrade.ApplyUpgrade(tank.Weapons.First(x => x.Upgrades.Contains(this)));
            }
        }
    }

    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(IWeapon tank);
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

        public abstract void ApplyUpgrade(IWeapon tank);
    }

    [Serializable]
    public class Damage : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon.Modules
                .GetConcrete<DamageModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<FireRangeModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<ProjectileSizeModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<PenetrationModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<ProjectilesPerShootModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<FireRateModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<CriticalChanceModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<CriticalMultiplierModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<ProjectileDamageRadiusModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<RayDurationModule, IWeaponModule>()
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
            weapon.Modules
                .GetConcrete<ProjectileSpreadAngleModule, IWeaponModule>()
                .SpreadAngle.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }
}

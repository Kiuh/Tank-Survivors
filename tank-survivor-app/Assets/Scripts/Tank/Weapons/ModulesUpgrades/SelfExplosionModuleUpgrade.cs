using Common;
using Tank.Weapons.Modules.SelfExplosion;

namespace Tank.Weapons.ModulesUpgrades.SelfExplosion
{
    public class SelfExplosionCount : BaseModuleMathUpgrade<int>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<SelfExplosionCountModule, Modules.IWeaponModule>()
                .Count.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class SelfExplosionFireRate : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<FireRateModule, Modules.IWeaponModule>()
                .FireRate.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class SelfExplosionRadius : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<RadiusModule, Modules.IWeaponModule>()
                .Radius.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class SelfExplosionDamage : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<DamageModule, Modules.IWeaponModule>()
                .Damage.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class SelfExplosionHitMarkTimer : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<HitMarkTimerModule, Modules.IWeaponModule>()
                .Time.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }

    public class SelfExplosionFireTimer : BaseModuleMathUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<FireTimerModule, Modules.IWeaponModule>()
                .Time.Modifications.Add(
                    new(MathOperation.ToFunction(OperationValue), ModificationPriority)
                );
        }
    }
}

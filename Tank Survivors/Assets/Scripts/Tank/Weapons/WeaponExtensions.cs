using Common;
using DataStructs;
using Tank;
using Tank.Weapons;

namespace Assets.Scripts.Tank.Weapons
{
    public static class WeaponExtensions
    {
        public static float GetModifiedDamage(
            this GunBase gun,
            ModifiableValue<float> damage,
            ModifiableValue<Percentage> criticalChance,
            ModifiableValue<Percentage> criticalMultiplier,
            TankImpl tank
        )
        {
            var damageModifiableValue = damage.GetPrecentageModifiableValue(tank.DamageModifier);

            bool isCritical = (
                criticalChance.GetModifiedValue() + tank.CriticalChance.GetModifiedValue()
            ).TryChance();

            return isCritical
                ? damageModifiableValue.GetPrecentageValue(criticalMultiplier)
                : damageModifiableValue.GetModifiedValue();
        }
    }
}

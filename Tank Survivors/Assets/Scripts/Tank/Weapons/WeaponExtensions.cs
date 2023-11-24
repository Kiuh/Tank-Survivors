using Common;
using DataStructs;
using Tank;
using Tank.Weapons;
using UnityEngine;

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

        public static Vector3 GetSpreadDirection(this GunBase gun, Vector3 direction, float angle)
        {
            Quaternion rotation = Quaternion.AngleAxis(
                Random.Range(-angle, angle),
                Vector3.forward
            );
            return rotation * direction;
        }
    }
}

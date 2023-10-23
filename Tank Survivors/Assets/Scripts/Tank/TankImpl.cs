using Common;
using DataStructs;
using UnityEngine;

namespace Tank
{
    [SelectionBase]
    [AddComponentMenu("Tank.TankImpl")]
    public class TankImpl : MonoBehaviour
    {
        [SerializeField]
        private ModifiableValueContainer health;
        public ModifiableValueContainer Health => health;

        [SerializeField]
        private Experience experience;
        public Experience Experience => experience;

        [SerializeField]
        private ModifiableValue<float> speed;
        public ModifiableValue<float> Speed => speed;

        [SerializeField]
        private ModifiableValue<float> pickupRadius;
        public ModifiableValue<float> PickupRadius => pickupRadius;

        [SerializeField]
        private ModifiableValue<float> armor;
        public ModifiableValue<float> Armor => armor;

        [SerializeField]
        private ModifiableValue<Percentage> criticalChance;
        public ModifiableValue<Percentage> CriticalChance => criticalChance;

        [SerializeField]
        private ModifiableValue<Percentage> evadeChance;
        public ModifiableValue<Percentage> EvadeChance => evadeChance;

        [SerializeField]
        private ModifiableValue<Percentage> damageModifier;
        public ModifiableValue<Percentage> DamageModifier => damageModifier;

        [SerializeField]
        private ModifiableValue<Percentage> projectileSize;
        public ModifiableValue<Percentage> ProjectileSize => projectileSize;

        [SerializeField]
        private ModifiableValue<Percentage> rangeModifier;
        public ModifiableValue<Percentage> RangeModifier => rangeModifier;

        [SerializeField]
        private ModifiableValue<Percentage> fireRateModifier;
        public ModifiableValue<Percentage> FireRateModifier => fireRateModifier;

        private void Update()
        {
            // TODO: pickup pickups
        }
    }
}

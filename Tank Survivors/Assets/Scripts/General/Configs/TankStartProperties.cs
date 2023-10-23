using Tank;
using DataStructs;
using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        fileName = "TankStartProperties",
        menuName = "Configs/TankStartProperties",
        order = 4
    )]
    public class TankStartProperties : ScriptableObject
    {
        [SerializeField]
        private float health;
        public float Health => health;

        [SerializeField]
        private uint experience;
        public uint Experience => experience;

        [SerializeField]
        private float speed;
        public float Speed => speed;

        [SerializeField]
        private float pickupRadius;
        public float PickupRadius => pickupRadius;

        [SerializeField]
        private float armor;
        public float Armor => armor;

        [SerializeField]
        private Percentage criticalChance;
        public Percentage CriticalChance => criticalChance;

        [SerializeField]
        private Percentage evadeChance;
        public Percentage EvadeChance => evadeChance;

        [SerializeField]
        private Percentage damageModifier;
        public Percentage DamageModifier => damageModifier;

        [SerializeField]
        private Percentage projectileSize;
        public Percentage ProjectileSize => projectileSize;

        [SerializeField]
        private Percentage rangeModifier;
        public Percentage RangeModifier => rangeModifier;

        [SerializeField]
        private Percentage fireRateModifier;
        public Percentage FireRateModifier => fireRateModifier;

        public void AssignStartProperties(TankImpl tank)
        {
            tank.Health.BaseValue = health;
            tank.Experience.ValueContainer.BaseValue = experience;
            tank.Speed.SourceValue = speed;
            tank.PickupRadius.SourceValue = pickupRadius;
            tank.Armor.SourceValue = armor;
            tank.CriticalChance.SourceValue = criticalChance;
            tank.EvadeChance.SourceValue = evadeChance;
            tank.DamageModifier.SourceValue = damageModifier;
            tank.ProjectileSize.SourceValue = projectileSize;
            tank.RangeModifier.SourceValue = rangeModifier;
            tank.FireRateModifier.SourceValue = fireRateModifier;
        }
    }
}

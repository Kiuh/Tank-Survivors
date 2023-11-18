using DataStructs;
using Tank;
using UnityEngine;

namespace Configs
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

        [SerializeField]
        private float speed;

        [SerializeField]
        private float pickupRadius;

        [SerializeField]
        private float armor;

        [SerializeField]
        private Percentage criticalChance;

        [SerializeField]
        private Percentage evadeChance;

        [SerializeField]
        private Percentage damageModifier;

        [SerializeField]
        private Percentage projectileSize;

        [SerializeField]
        private Percentage rangeModifier;

        [SerializeField]
        private Percentage fireRateModifier;

        [SerializeField]
        private uint levelUpChoicesCount;

        public void AssignStartProperties(TankImpl tank)
        {
            tank.Health.BaseValue = health;
            tank.Speed.SourceValue = speed;
            tank.PickupRadius.SourceValue = pickupRadius;
            tank.Armor.BaseValue = armor;
            tank.CriticalChance.SourceValue = criticalChance;
            tank.EvadeChance.SourceValue = evadeChance;
            tank.DamageModifier.SourceValue = damageModifier;
            tank.ProjectileSize.SourceValue = projectileSize;
            tank.RangeModifier.SourceValue = rangeModifier;
            tank.FireRateModifier.SourceValue = fireRateModifier;
            tank.LevelUpChoicesCount.SourceValue = levelUpChoicesCount;
        }
    }
}

using Common;
using DataStructs;
using General;
using System.Collections.Generic;
using System.Linq;
using Tank.Upgrades;
using Tank.Weapons;
using UnityEngine;

namespace Tank
{
    [SelectionBase]
    [AddComponentMenu("Tank.TankImpl")]
    public class TankImpl : MonoBehaviour
    {
        [SerializeField]
        private GameContext gameContext;

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

        private List<ITankUpgrade> tankUpgrades = new();
        private List<IWeapon> weapons = new();

        private void Awake()
        {
            tankUpgrades = gameContext.GameConfig.PlayerUpgradesConfig.TankUpgrades.ToList();
            weapons = gameContext.GameConfig.WeaponsConfig.Weapons.ToList();
        }

        private void Update()
        {
            // TODO: pickup pickups
        }

        public List<IUpgradablePiece> GetAvailableUpgrades()
        {
            List<IUpgradablePiece> availableUpgrades = new();
            availableUpgrades.AddRange(tankUpgrades.Cast<IUpgradablePiece>());
            availableUpgrades.AddRange(weapons.Cast<IUpgradablePiece>());
            return availableUpgrades.Where(x => !x.IsReachedMaxLevel).ToList();
        }
    }
}

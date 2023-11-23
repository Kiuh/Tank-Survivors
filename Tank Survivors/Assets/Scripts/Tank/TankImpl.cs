using Common;
using DataStructs;
using General;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;
using Tank.Upgrades;
using Tank.Weapons;
using UnityEngine;

namespace Tank
{
    [SelectionBase]
    [AddComponentMenu("Tank.TankImpl")]
    public class TankImpl : SerializedMonoBehaviour
    {
        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private GameContext gameContext;

        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private EnemyFinder enemyFinder;

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Health"), HideLabel]
        public ModifiableValueContainer Health { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("PlayerLevel"), HideLabel]
        public PlayerLevel PlayerLevel { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<uint> LevelUpChoicesCount { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<float> Speed { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<float> PickupRadius { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Armor"), HideLabel]
        public ModifiableValueContainer Armor { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<Percentage> CriticalChance { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<Percentage> EvadeChance { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<Percentage> DamageModifier { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats", order: 1)]
        public ModifiableValue<Percentage> ProjectileSize { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<Percentage> RangeModifier { get; private set; } = new();

        [ReadOnly]
        [OdinSerialize]
        [FoldoutGroup("Tank Stats")]
        public ModifiableValue<Percentage> FireRateModifier { get; private set; } = new();

        [ReadOnly]
        [PropertyOrder(2)]
        [OdinSerialize]
        [ShowInInspector]
        private List<TankUpgrade> tankUpgrades = new();

        [ReadOnly]
        [PropertyOrder(2)]
        [OdinSerialize]
        [ShowInInspector]
        private List<IWeapon> weapons = new();
        public IEnumerable<IWeapon> Weapons => weapons;

        public event Action OnDeath;

        private void Awake()
        {
            tankUpgrades = gameContext.GameConfig.TankUpgradesConfig.Upgrades.ToList();
            tankUpgrades.ForEach(x => x.Initialize());
            weapons = gameContext.GameConfig.WeaponsConfig.Weapons.ToList();
            weapons.ForEach(x => x.Initialize(gameObject.transform, enemyFinder));
            PlayerLevel.Initialize(gameContext.GameConfig.LevelProgressionConfig);
            gameContext.GameConfig.TankStartProperties.AssignStartProperties(this);
        }

        private void Update()
        {
            foreach (IWeapon weapon in weapons)
            {
                weapon.ProceedAttack(Time.deltaTime);
            }
        }

        public IEnumerable<IUpgradablePiece> GetAvailableUpgrades()
        {
            List<IUpgradablePiece> availableUpgrades = new();
            availableUpgrades.AddRange(tankUpgrades.Cast<IUpgradablePiece>());
            availableUpgrades.AddRange(weapons.Cast<IUpgradablePiece>());
            return availableUpgrades.Where(x => !x.IsReachedMaxLevel);
        }

        public void Heal(float healAmount)
        {
            Health.Value = Mathf.Min(Health.Value + healAmount, Health.MaxValue);
        }

        public void FixArmor(float armorAmount)
        {
            Armor.Value = Mathf.Min(Armor.Value + armorAmount, Armor.MaxValue);
        }

        public void TakeDamage(float damageAmount)
        {
            Health.Value -= damageAmount;
            if (Health.Value <= 0)
            {
                Health.Value = 0;
                OnDeath?.Invoke();
            }
        }
    }
}

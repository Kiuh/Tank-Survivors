using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using DataStructs;
using General;
using Sirenix.OdinInspector;
using Tank.UpgradablePiece;
using Tank.Upgrades;
using Tank.Weapons;
using UnityEngine;

namespace Tank
{
    [SelectionBase]
    [AddComponentMenu("Tank.TankImpl")]
    public class TankImpl : MonoBehaviour
    {
        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private GameContext gameContext;

        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private EnemyFinder enemyFinder;
        public EnemyFinder EnemyFinder
        {
            get => enemyFinder;
            private set => enemyFinder = value;
        }

        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private EnemyPickupsGenerator enemyPickupsGenerator;
        public EnemyPickupsGenerator EnemyPickupsGenerator
        {
            get => enemyPickupsGenerator;
            private set => enemyPickupsGenerator = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Health"), HideLabel]
        private ModifiableValueContainer health = new();
        public ModifiableValueContainer Health
        {
            get => health;
            private set => health = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("PlayerLevel"), HideLabel]
        private PlayerLevel playerLevel = new();
        public PlayerLevel PlayerLevel
        {
            get => playerLevel;
            private set => playerLevel = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<uint> levelUpChoicesCount = new();
        public ModifiableValue<uint> LevelUpChoicesCount
        {
            get => levelUpChoicesCount;
            private set => levelUpChoicesCount = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<float> speed = new();
        public ModifiableValue<float> Speed
        {
            get => speed;
            private set => speed = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<float> pickupRadius = new();
        public ModifiableValue<float> PickupRadius
        {
            get => pickupRadius;
            private set => pickupRadius = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> criticalChance = new();
        public ModifiableValue<Percentage> CriticalChance
        {
            get => criticalChance;
            private set => criticalChance = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> evadeChance = new();
        public ModifiableValue<Percentage> EvadeChance
        {
            get => evadeChance;
            private set => evadeChance = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> damageModifier = new();
        public ModifiableValue<Percentage> DamageModifier
        {
            get => damageModifier;
            private set => damageModifier = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats", order: 1)]
        private ModifiableValue<Percentage> projectileSize = new();
        public ModifiableValue<Percentage> ProjectileSize
        {
            get => projectileSize;
            private set => projectileSize = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> rangeModifier = new();
        public ModifiableValue<Percentage> RangeModifier
        {
            get => rangeModifier;
            private set => rangeModifier = value;
        }

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> fireRateModifier = new();
        public ModifiableValue<Percentage> FireRateModifier
        {
            get => fireRateModifier;
            private set => fireRateModifier = value;
        }

        [ReadOnly]
        [PropertyOrder(2)]
        [SerializeReference]
        private List<TankUpgrade> tankUpgrades = new();

        [ReadOnly]
        [PropertyOrder(2)]
        [SerializeReference]
        private List<IWeapon> weapons = new();

        public IEnumerable<IWeapon> Weapons => weapons;

        public event Action OnDeath;

        private void Awake()
        {
            tankUpgrades = gameContext.GameConfig.TankUpgradesConfig.Upgrades.ToList();
            tankUpgrades.ForEach(x => x.Initialize());
            weapons = gameContext.GameConfig.WeaponsConfig.Weapons.ToList();
            weapons.ForEach(x => x.Initialize(this, EnemyFinder));
            PlayerLevel.Initialize(gameContext.GameConfig.LevelProgressionConfig);
            gameContext.GameConfig.TankStartProperties.AssignStartProperties(this);
        }

        private void Update()
        {
            foreach (IWeapon weapon in weapons)
            {
                weapon.ProceedAttack();
            }
        }

        public void SwapWeapon(IWeapon weapon)
        {
            weapons.Clear();
            weapon.Initialize(this, EnemyFinder);
            weapons.Add(weapon);
        }

        public IEnumerable<IUpgradablePiece> GetLevelUpUpgrades()
        {
            return weapons.Where(x =>
                x.LevelUpUpgrades.Any(u => u.LevelForUpgrade.Equals(PlayerLevel.CurrentLevel))
            );
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

        public void TakeDamage(float damageAmount)
        {
            if (Health.Value <= 0)
            {
                return;
            }

            if (EvadeChance.GetModifiedValue().TryChance())
            {
                return;
            }
            Health.Value -= damageAmount;
            if (Health.Value <= 0)
            {
                Health.Value = 0;
                OnDeath?.Invoke();
            }
        }
    }
}

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
        [Required]
        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private GameContext gameContext;

        [Required]
        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private EnemyFinder enemyFinder;
        public EnemyFinder EnemyFinder => enemyFinder;

        [Required]
        [FoldoutGroup("General Bindings")]
        [PropertyOrder(0)]
        [SerializeField]
        private EnemyPickupsGenerator enemyPickupsGenerator;
        public EnemyPickupsGenerator EnemyPickupsGenerator => enemyPickupsGenerator;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Health"), HideLabel]
        private ModifiableValueContainer health = new();
        public ModifiableValueContainer Health => health;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("PlayerLevel"), HideLabel]
        private PlayerLevel playerLevel = new();
        public PlayerLevel PlayerLevel => playerLevel;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<uint> levelUpChoicesCount = new();
        public ModifiableValue<uint> LevelUpChoicesCount => levelUpChoicesCount;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<float> speed = new();
        public ModifiableValue<float> Speed => speed;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<float> pickupRadius = new();
        public ModifiableValue<float> PickupRadius => pickupRadius;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> criticalChance = new();
        public ModifiableValue<Percentage> CriticalChance => criticalChance;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> evadeChance = new();
        public ModifiableValue<Percentage> EvadeChance => evadeChance;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> damageModifier = new();
        public ModifiableValue<Percentage> DamageModifier => damageModifier;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats", order: 1)]
        private ModifiableValue<Percentage> projectileSize = new();
        public ModifiableValue<Percentage> ProjectileSize => projectileSize;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> rangeModifier = new();
        public ModifiableValue<Percentage> RangeModifier => rangeModifier;

        [ReadOnly]
        [SerializeField]
        [FoldoutGroup("Tank Stats")]
        private ModifiableValue<Percentage> fireRateModifier = new();
        public ModifiableValue<Percentage> FireRateModifier => fireRateModifier;

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

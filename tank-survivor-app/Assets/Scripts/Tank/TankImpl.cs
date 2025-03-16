using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Configs;
using DataStructs;
using DG.Tweening;
using General;
using Panels.Pause;
using Sirenix.OdinInspector;
using Tank.PickUps;
using Tank.UpgradablePiece;
using Tank.Upgrades;
using Tank.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
        [SerializeReference]
        private List<TankUpgrade> tankUpgrades = new();

        [ReadOnly]
        [SerializeReference]
        private List<IWeapon> weapons = new();

        public IEnumerable<IWeapon> Weapons => weapons;

        public event Action OnDeath;
        public UnityEvent OnDeathEvent;
        public UnityEvent OnDamageTaken;

        private bool isDead = false;
        public bool IsDead => isDead;

        private void Awake()
        {
            _ = profile.TryGet(out vignette);
            tankUpgrades = gameContext.GameConfig.TankUpgradesConfig.Upgrades.ToList();
            tankUpgrades.ForEach(x => x.Initialize());
            TankWeapons clonedConfig = Instantiate(gameContext.GameConfig.WeaponsConfig);
            weapons = clonedConfig.Weapons.ToList();
            weapons.ForEach(x => x.Initialize(this, EnemyFinder));
            gameContext.GameConfig.TankStartProperties.AssignStartProperties(this);
            PlayerLevel.Initialize(gameContext.GameConfig.LevelProgressionConfig);
        }

        private void Update()
        {
            if (isDead)
            {
                return;
            }

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

        public IEnumerable<IUpgradablePiece> GetLevelUpUpgrades(uint level)
        {
            return weapons.Where(x => x.LevelUpUpgrades.Any(u => u.LevelForUpgrade.Equals(level)));
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
            if (isDead)
            {
                return;
            }

            Health.Value = Mathf.Min(Health.Value + healAmount, Health.MaxValue);
            vignetteTween?.Kill();
            vignette.color.value = vignetteHealColor;
            vignetteTween = DOVirtual.Float(
                maxIntensity,
                0,
                vignetteDuration,
                (x) => vignette.intensity.value = x
            );
        }

        public void TakeDamage(float damageAmount)
        {
            if (isDead)
            {
                return;
            }

            if (EvadeChance.GetModifiedValue().TryChance())
            {
                ShowEvasion();
                return;
            }

            Health.Value -= damageAmount;
            ShowDamageTaken(damageAmount);
            OnDamageTaken?.Invoke();
            if (Health.Value <= 0)
            {
                Health.Value = 0;
                isDead = true;
                DeathExplode();
            }
        }

        [AssetSelector]
        [SerializeField]
        private ParticleSystem tankExplosion;

        [SerializeField]
        private Color darkColor;

        private Tween deathDelayTween;
        private List<SpriteRenderer> spriteRenderers = new();

        public void RecoverDeathExplode()
        {
            isDead = false;
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.material.color = Color.white;
            }
        }

        private void DeathExplode()
        {
            ParticleSystem instance = Instantiate(
                tankExplosion,
                transform.position,
                Quaternion.identity
            );
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.material.color = darkColor;
            }
            instance.Play();
            OnDeathEvent?.Invoke();
            float delay = instance.main.duration * instance.main.startLifetimeMultiplier;
            deathDelayTween = DOVirtual.DelayedCall(
                delay,
                () =>
                {
                    Destroy(instance.gameObject);
                    OnDeath?.Invoke();
                },
                false
            );
        }

        [AssetList]
        [SerializeField]
        private FloatingEffect floatingEffect;

        [SerializeField]
        private string evasionText;

        [SerializeField]
        private Color evasionColor;

        [SerializeField]
        private string damageText;

        [SerializeField]
        private Color damageColor;

        [SerializeField]
        private VolumeProfile profile;
        private Vignette vignette;

        [SerializeField]
        private float maxIntensity;

        [SerializeField]
        private float vignetteDuration;

        [SerializeField]
        private Color vignetteDamageColor;

        [SerializeField]
        private Color vignetteHealColor;

        private Tween vignetteTween;

        private void ShowEvasion()
        {
            floatingEffect.CreateAndLaunch(transform.position, evasionText, evasionColor);
        }

        private void ShowDamageTaken(float damage)
        {
            vignette.intensity.value = maxIntensity;
            vignetteTween?.Kill();
            vignette.color.value = vignetteDamageColor;
            vignetteTween = DOVirtual.Float(
                maxIntensity,
                0,
                vignetteDuration,
                (x) => vignette.intensity.value = x
            );
            floatingEffect.CreateAndLaunch(
                transform.position,
                damageText + damage.ToString("0.0"),
                damageColor
            );
        }

        public void StopScreenEffects()
        {
            vignette.intensity.value = 0;
            vignetteTween?.Kill();
        }

        private void OnDestroy()
        {
            vignette.intensity.value = 0;
            vignetteTween?.Kill();
            deathDelayTween?.Kill();
            _ = DOTween.KillAll();
        }

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string attributesTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string healthTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string speedTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string evadeTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string pickupRadiusTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string playerLevelTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string damageModifierTitle;

        [FoldoutGroup("Tank Attributes")]
        [SerializeField]
        private string fireRateModifierTitle;

        public StatBlockData GetStatBlockData()
        {
            StatBlockData statBlockData =
                new()
                {
                    StatName = attributesTitle,
                    StatsData = new()
                    {
                        new StatData()
                        {
                            Name = healthTitle,
                            Value = $"{Health.Value:0.0}/{Health.MaxValue:0}"
                        },
                        new StatData() { Name = speedTitle, Value = $"{Speed.Value:0.0}" },
                        new StatData()
                        {
                            Name = evadeTitle,
                            Value = $"{EvadeChance.Value.Value:0.0}%"
                        },
                        new StatData()
                        {
                            Name = pickupRadiusTitle,
                            Value = $"{PickupRadius.Value:0.0}"
                        },
                        new StatData()
                        {
                            Name = playerLevelTitle,
                            Value = $"{PlayerLevel.CurrentLevel}"
                        },
                        new StatData()
                        {
                            Name = damageModifierTitle,
                            Value =
                                damageModifier.Value.Value.GetSign()
                                + $"{damageModifier.Value.Value}%"
                        },
                        new StatData()
                        {
                            Name = fireRateModifierTitle,
                            Value =
                                fireRateModifier.Value.Value.GetSign()
                                + $"{fireRateModifier.Value.Value}%"
                        }
                    }
                };
            return statBlockData;
        }

        [Button]
        private void AddExperience(int exp)
        {
            playerLevel.AddExperience(exp);
        }
    }
}

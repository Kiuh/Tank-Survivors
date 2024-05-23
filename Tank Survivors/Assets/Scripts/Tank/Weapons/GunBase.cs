using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Tank.Weapons
{
    public abstract class GunBase : IWeapon
    {
        [Title("@GetType()")]
        [FoldoutGroup("$UpgradeName", expanded: true)]
        [OdinSerialize]
        [LabelText("Weapon Name")]
        public string UpgradeName { get; private set; }

        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        public uint CurrentLevel { get; set; }

        [FoldoutGroup("$UpgradeName")]
        [OdinSerialize]
        public uint MaxLevel { get; private set; }

        [FoldoutGroup("$UpgradeName")]
        [PropertyOrder(1)]
        [OdinSerialize]
        [ShowInInspector]
        private List<LeveledWeaponUpgrade> leveledUpgrades = new();

        [FoldoutGroup("$UpgradeName")]
        [PropertyOrder(1)]
        [OdinSerialize]
        [ShowInInspector]
        private List<LevelUpWeaponUpgrade> levelUpUpgrades = new();

        protected TankImpl Tank;
        protected EnemyFinder EnemyFinder;

        private Dictionary<Type, IWeaponModule> cachedModules = new();

        public T GetModule<T>()
            where T : class, IWeaponModule
        {
            cachedModules ??= new();
            if (cachedModules.TryGetValue(typeof(T), out IWeaponModule value))
            {
                return value as T;
            }
            IWeaponModule module = Modules.FirstOrDefault(x => x is T);
            if (module != null)
            {
                cachedModules.Add(typeof(T), module);
                return module as T;
            }
            return null;
        }

        public IEnumerable<ILeveledUpgrade> Upgrades => leveledUpgrades;
        public IEnumerable<ILevelUpUpgrade> LevelUpUpgrades => levelUpUpgrades;

        public abstract void Initialize(TankImpl tank, EnemyFinder enemyFinder);

        public abstract void ProceedAttack();

        public abstract void CreateGun();
        public abstract void DestroyGun();
        public abstract void SwapWeapon(IWeapon newWeapon);

        protected abstract List<IWeaponModule> GetBaseModules();

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("$UpgradeName")]
        [PropertyOrder(2)]
        public List<IWeaponModule> Modules { get; protected set; } = new();

        [Button]
        [PropertyOrder(2)]
        [FoldoutGroup("$UpgradeName")]
        private void RefreshModules()
        {
            List<IWeaponModule> bufferModules = GetBaseModules();

            foreach (IWeaponModule module in bufferModules)
            {
                if (
                    Modules.Select(x => x.GetType()).FirstOrDefault(x => x == module.GetType())
                    == null
                )
                {
                    Modules.Add(module);
                }
            }
        }

        public Vector3 GetSpreadDirection(Vector3 direction, float angle)
        {
            Quaternion rotation = Quaternion.AngleAxis(
                UnityEngine.Random.Range(-angle, angle),
                Vector3.forward
            );
            return rotation * direction;
        }

        public float GetModifiedDamage(
            ModifiableValue<float> damage,
            ModifiableValue<Percentage> criticalChance,
            ModifiableValue<Percentage> criticalMultiplier,
            TankImpl tank
        )
        {
            ModifiableValue<float> damageModifiableValue = damage.GetPercentagesModifiableValue(
                tank.DamageModifier
            );

            Percentage wholeChance =
                criticalChance.GetModifiedValue() + tank.CriticalChance.GetModifiedValue();

            if (wholeChance.TryChance())
            {
                return damageModifiableValue.GetPercentagesValue(criticalMultiplier);
            }
            else
            {
                return damageModifiableValue.GetModifiedValue();
            }
        }

        protected ITower CreateTower(Transform transform)
        {
            return CreateTower(transform, SpawnVariation.Disconnected);
        }

        protected ITower CreateTower(Transform transform, SpawnVariation spawnVariation)
        {
            TowerModule towerModule = GetModule<TowerModule>();
            ITower tower = towerModule.TowerPrefab.Spawn(transform);
            towerModule.Tower = tower;

            tower.Initialize(Tank, EnemyFinder, this, spawnVariation);

            return tower;
        }

        protected void DestroyTower(ITower tower)
        {
            tower.DestroyYourself();
        }
    }
}

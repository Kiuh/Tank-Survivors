﻿using System.Collections.Generic;
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

        public T GetModule<T>()
            where T : class, IWeaponModule
        {
            IWeaponModule module = Modules.FirstOrDefault(x => x is T);
            return module == null ? null : module as T;
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
                Random.Range(-angle, angle),
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

            bool isCritical = (
                criticalChance.GetModifiedValue() + tank.CriticalChance.GetModifiedValue()
            ).TryChance();

            return isCritical
                ? damageModifiableValue.GetPercentagesValue(criticalMultiplier)
                : damageModifiableValue.GetModifiedValue();
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

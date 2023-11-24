using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;
using UnityEngine;

namespace Tank.Weapons
{
    public abstract class GunBase : IWeapon
    {
        [Title("@GetType()")]
        [FoldoutGroup("$UpgradeName")]
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

        public T GetModule<T>()
            where T : class, IWeaponModule
        {
            IWeaponModule module = Modules.FirstOrDefault(x => x is T);
            return module == null ? null : module as T;
        }

        public IEnumerable<ILeveledUpgrade> Upgrades => leveledUpgrades;

        public abstract void Initialize(TankImpl tank, EnemyFinder enemyFinder);

        public abstract void ProceedAttack();

        public abstract void CreateGun();

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

        protected Vector3 GetSpreadDirection(Vector3 direction, float angle)
        {
            Quaternion rotation = Quaternion.AngleAxis(
                UnityEngine.Random.Range(-angle, angle),
                Vector3.forward
            );
            return rotation * direction;
        }

        protected float GetModifiedDamage(
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
    }
}

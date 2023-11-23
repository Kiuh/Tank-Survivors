using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Tank.UpgradablePiece;

namespace Tank.Weapons
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class LeveledWeaponUpgrade : ILeveledUpgrade
    {
        [FoldoutGroup("$UpgradingLevel")]
        [OdinSerialize]
        public uint UpgradingLevel { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [MultiLineProperty]
        [OdinSerialize]
        public string Description { get; private set; }

        [FoldoutGroup("$UpgradingLevel")]
        [NonSerialized, OdinSerialize]
        [PropertyOrder(1)]
        private List<IModuleUpgrade> moduleUpgrades;

        public void ApplyUpgrade(TankImpl tank)
        {
            if (moduleUpgrades == null)
            {
                return;
            }
            foreach (IModuleUpgrade upgrade in moduleUpgrades)
            {
                upgrade.ApplyUpgrade(tank.Weapons.First(x => x.Upgrades.Contains(this)));
            }
        }
    }

    public interface IModuleUpgrade
    {
        public void ApplyUpgrade(IWeapon tank);
    }

    [HideLabel]
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public abstract class BaseModuleUpgrade<T> : IModuleUpgrade
    {
        [OdinSerialize]
        [FoldoutGroup("@GetType()")]
        [HorizontalGroup("@GetType()/Horizontal")]
        [EnumToggleButtons]
        protected MathOperation MathOperation = MathOperation.Plus;

        [FoldoutGroup("@GetType()")]
        [InlineProperty]
        [OdinSerialize]
        protected T OperationValue;

        [FoldoutGroup("@GetType()")]
        [LabelText("Priority")]
        [EnumToggleButtons]
        [OdinSerialize]
        protected ModificationPriority ModificationPriority = ModificationPriority.Medium;

        public abstract void ApplyUpgrade(IWeapon tank);
    }

    [Serializable]
    public class Damage : BaseModuleUpgrade<float>
    {
        public override void ApplyUpgrade(IWeapon weapon)
        {
            (weapon.Modules.First(x => x is DamageModule) as DamageModule).Damage.Modifications.Add(
                new(MathOperation.ToFloatFunction(OperationValue), ModificationPriority)
            );
        }
    }
}

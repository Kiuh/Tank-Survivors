using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;

namespace Tank.Upgrades
{
    public interface IPropertyUpgrade
    {
        public void ApplyUpgrade(TankImpl tank);
    }

    [HideLabel]
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public abstract class BasePropertyUpgrade<T> : IPropertyUpgrade
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

        public abstract void ApplyUpgrade(TankImpl tank);
    }

    [Serializable]
    public class Speed : BasePropertyUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.Speed.Modifications.Add(
                new(MathOperation.ToFloatFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class CriticalChance : BasePropertyUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.CriticalChance.Modifications.Add(
                new(MathOperation.ToPercentageFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class MaxHealth : BasePropertyUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.Health.AddModification(
                new(MathOperation.ToFloatFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class LevelUpChoicesCount : BasePropertyUpgrade<uint>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.LevelUpChoicesCount.Modifications.Add(
                new(MathOperation.ToUIntFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class PickupRadius : BasePropertyUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.PickupRadius.Modifications.Add(
                new(MathOperation.ToFloatFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class Armor : BasePropertyUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.Armor.AddModification(
                new(MathOperation.ToFloatFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class EvadeChance : BasePropertyUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.EvadeChance.Modifications.Add(
                new(MathOperation.ToPercentageFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class DamageModifier : BasePropertyUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.DamageModifier.Modifications.Add(
                new(MathOperation.ToPercentageFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class ProjectileSize : BasePropertyUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.ProjectileSize.Modifications.Add(
                new(MathOperation.ToPercentageFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class RangeModifier : BasePropertyUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.RangeModifier.Modifications.Add(
                new(MathOperation.ToPercentageFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class FireRateModifier : BasePropertyUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.FireRateModifier.Modifications.Add(
                new(MathOperation.ToPercentageFunction(OperationValue), ModificationPriority)
            );
        }
    }
}

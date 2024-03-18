using System;
using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

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
    public abstract class BasePropertyMathUpgrade<T> : IPropertyUpgrade
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
    public class Speed : BasePropertyMathUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.Speed.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class CriticalChance : BasePropertyMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.CriticalChance.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class MaxHealth : BasePropertyMathUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.Health.AddModification(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class LevelUpChoicesCount : BasePropertyMathUpgrade<uint>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.LevelUpChoicesCount.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class PickupRadius : BasePropertyMathUpgrade<float>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.PickupRadius.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class EvadeChance : BasePropertyMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.EvadeChance.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class DamageModifier : BasePropertyMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.DamageModifier.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class ProjectileSize : BasePropertyMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.ProjectileSize.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class RangeModifier : BasePropertyMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.RangeModifier.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }

    [Serializable]
    public class FireRateModifier : BasePropertyMathUpgrade<Percentage>
    {
        public override void ApplyUpgrade(TankImpl tank)
        {
            tank.FireRateModifier.Modifications.Add(
                new(MathOperation.ToFunction(OperationValue), ModificationPriority)
            );
        }
    }
}

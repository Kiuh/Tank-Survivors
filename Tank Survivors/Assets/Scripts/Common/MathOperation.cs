using DataStructs;
using System;

namespace Common
{
    [Serializable]
    public enum MathOperation
    {
        Plus,
        Minus,
        Mul,
        Divide
    }

    public static class MathOperationTools
    {
        public static Func<float, float> ToFloatFunction(
            this MathOperation mathOperation,
            float value
        )
        {
            return mathOperation switch
            {
                MathOperation.Plus => (source) => source + value,
                MathOperation.Minus => (source) => source - value,
                MathOperation.Mul => (source) => source * value,
                MathOperation.Divide
                    => value == 0 ? throw new DivideByZeroException() : (source) => source / value,
                _ => (source) => source
            };
        }

        public static Func<Percentage, Percentage> ToPercentageFunction(
            this MathOperation mathOperation,
            Percentage value
        )
        {
            return mathOperation switch
            {
                MathOperation.Plus => (source) => source + value,
                MathOperation.Minus => (source) => source - value,
                MathOperation.Mul => (source) => source * value,
                MathOperation.Divide
                    => value.Value == 0
                        ? throw new DivideByZeroException()
                        : (source) => source / value,
                _ => (source) => source
            };
        }

        public static Func<uint, uint> ToUIntFunction(this MathOperation mathOperation, uint value)
        {
            return mathOperation switch
            {
                MathOperation.Plus => (source) => source + value,
                MathOperation.Minus => (source) => source - value,
                MathOperation.Mul => (source) => source * value,
                MathOperation.Divide
                    => value == 0 ? throw new DivideByZeroException() : (source) => source / value,
                _ => (source) => source
            };
        }
    }
}

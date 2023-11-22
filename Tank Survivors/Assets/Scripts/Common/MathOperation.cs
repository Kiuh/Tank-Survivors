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
        public static Func<float, float> ToFunction(this MathOperation mathOperation, float value)
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

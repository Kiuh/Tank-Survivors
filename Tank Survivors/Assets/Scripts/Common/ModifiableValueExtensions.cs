using DataStructs;

namespace Common
{
    public static class ModifiableValueExtensions
    {
        public static float GetPercentagesValue(
            this ModifiableValue<float> value,
            ModifiableValue<Percentage> percentage
        )
        {
            return value.GetModifiedValue() * (1f + percentage.GetModifiedValue().Value);
        }

        public static ModifiableValue<float> GetPercentagesModifiableValue(
            this ModifiableValue<float> value,
            ModifiableValue<Percentage> percentage
        )
        {
            return new ModifiableValue<float>(
                value.GetModifiedValue() * (1f + percentage.GetModifiedValue().Value)
            );
        }
    }
}

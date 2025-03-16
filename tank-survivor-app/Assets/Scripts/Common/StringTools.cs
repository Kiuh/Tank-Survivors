namespace Common
{
    internal static class StringTools
    {
        public static string GetSign(this float number)
        {
            return number > 0
                ? "+"
                : number < 0
                    ? "-"
                    : "";
        }
    }
}
